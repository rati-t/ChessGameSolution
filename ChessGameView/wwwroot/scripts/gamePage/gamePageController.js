$.ajaxSetup({ async: false });

pingInterval = setInterval(function () {
    $.get('/Game/PingPlayer',
        { gameId: url.searchParams.get("gameId"), playerId: url.searchParams.get("playerId"), date: Math.round(Date.now() / 1000) }, function(data){
            
        }); 
}, 1000);


timeInterval = setInterval(function () {
    if(IsStarted){
        $.get('/Game/GetTimeForThink',
            { gameId: url.searchParams.get("gameId")}, function(data){
                console.log(data);
                changeTime(data);
            }); 
    }    
}, 1000);

$.get('/Game/GetGameInfo',
    { gameId: url.searchParams.get("gameId") }, function (data) {
        startingPosition = data;

        $.get('/Game/GetGameSize',
            { gameId: url.searchParams.get("gameId"), playerid: url.searchParams.get("playerId") }, function (data) {

                var order;

                boardHorizontalSize = data[0];
                boardverticalSize = data[1];

                if (data[2] > 0) {
                    order = "normal";
                    addImagesToReviveTab(White);
                }
                if (data[2] == 0) {
                    order = "reversed";
                    addImagesToReviveTab(Black);
                }

                drawTable(boardHorizontalSize, boardverticalSize, order);
                addPiecesToTable(startingPosition);
               
                Interval();
            });
    });

function Interval() {
    interval = setInterval(function () {
        $.get('/Game/GetGameVersion',
            { gameId: url.searchParams.get("gameId")}, function (data) {
                if (gameVersion == parseInt(data)) {
                    return 0;
                } else {

                    if(data == -4){
                        clearInterval(pingInterval);
                        clearInterval(interval);
                        alert(playerWhoCreatedGame.id + " left the game");
                        window.location.href =  defaultURl + "playerPage" + "?playerid=" + url.searchParams.get("playerId");           
                        return 0;
                    }
    
                    if(data == -5){
                        clearInterval(pingInterval)
                        clearInterval(interval)                   
                        alert(playerWhoJoined.id + " left the game")                        
                        window.location.href = defaultURl + "playerPage" + "?playerid=" + url.searchParams.get("playerId");
                        return 0;
                    }
                    if(data == -6){
                        clearInterval(pingInterval)
                        clearInterval(interval)                   
                        alert("both players left the game")                        
                        window.location.href = defaultURl + "playerPage" + "?playerid=" + url.searchParams.get("playerId");
                        return 0;
                    }
                    if(data == -7){
                        clearInterval(pingInterval)
                        clearInterval(interval)                   
                        alert("game did not start")                        
                        window.location.href = defaultURl + "playerPage" + "?playerid=" + url.searchParams.get("playerId");
                        return 0;
                    }
                    if(data == -8){
                        clearInterval(pingInterval)
                        clearInterval(interval)                   
                        alert("White player reached time limit")                        
                        window.location.href = defaultURl + "playerPage" + "?playerid=" + url.searchParams.get("playerId");
                        return 0;
                    }
                    if(data == -9){
                        clearInterval(pingInterval)
                        clearInterval(interval)                   
                        alert("Black player reached time limit")                        
                        window.location.href = defaultURl + "playerPage" + "?playerid=" + url.searchParams.get("playerId");
                        return 0;
                    }
                    
                    
                    IsStarted = true;
                    gameVersion = parseInt(data);

                    $.get('/Game/GetGameInfo',
                        { gameId: url.searchParams.get("gameId") }, function (data) {

                            startingPosition = data;
                            $.get('/Game/GetGameSize',
                                { gameId: url.searchParams.get("gameId"), playerid: url.searchParams.get("playerId") }, function (data) {    // Magnus there is temporary it should depend on whose browser is open

                                    var order;

                                    boardHorizontalSize = data[0];
                                    boardverticalSize = data[1];

                                    if (data[2] == 1) {
                                        order = "normal";
                                    }
                                    if (data[2] == 0) {
                                        order = "reversed";
                                    }

                                    addPiecesToTable(startingPosition);

                                    $.get('/Game/GetPlayerInfo',
                                        { gameId: url.searchParams.get("gameId") }, function (data) {
                                            addPlayerToSquare(data);
                                        });

                                    $.get('/Game/GetMoveCount',
                                        { gameId: url.searchParams.get("gameId") }, function (data) {
                                            counter = data;
                                            MeveQueueValidator(counter);
                                        });

                                    $.get('/Game/GameEndCheck',
                                        { gameId: url.searchParams.get("gameId") }, function (data1) {
                                            if (data1 == -1) {
                                                alert("White won");
                                            }
                                            if (data1 == -2) {
                                                alert("Black Won");
                                            }
                                            if (data1 == -3) {
                                                alert("tied");
                                            }
                                        
                                            if (data1 < 0) {
                                                $.post('/Game/DeleteGameFromList',
                                                    { gameId: url.searchParams.get("gameId"), finishStatus: data1 }, function (data) {
                                                        clearInterval(pingInterval)
                                                        clearInterval(interval);
                                                        window.location.href = defaultURl + "playerPage" + "?playerid=" + url.searchParams.get("playerId");
                                                    });
                                            }
                                        });
                                });
                        });
                }
            }
        )
    }, 500);
}

function measureMovementType(horizontal, vertical) {

    var validator = -1;
    var checker;

    if (activeElement === undefined || activeElement == null) {

        $.ajaxSetup({ async: false });

        $.get('/Game/pieceOwnerValidator',
            { gameId: url.searchParams.get("gameId"), playerId: url.searchParams.get("playerId"), horizontal: horizontal, vertical: vertical }, function (data) {
                validator = data;
            });

        if (validator == 0) {
            validator - 1;
            return 0;
        }

        var moves;
        $.post('/Game/GetMovements',
            { horizontal: horizontal, vertical: vertical, gameId: url.searchParams.get("gameId") }, function (data) {
                
                moves = data;
            });

        $.post('/Game/GetPiece',
            { horizontal: horizontal, vertical: vertical, gameId: url.searchParams.get("gameId") }, function (data) {
                activeElement = data;
            });

        if (activeElement === undefined || activeElement == null) {
            paintSquares(moves, -1, -1);
            return 0;
        }
        paintSquares(moves, horizontal, vertical);
        return 0;
    }

    if (activeElement !== undefined && activeElement != null) {

        var moves;

        $.get('/Game/GetMovements',
            { horizontal: activeElement.horizontalPosition, vertical: activeElement.verticalPosition, gameId: url.searchParams.get("gameId") }, function (data) {
                moves = data;
            });

        for (element in moves) {
            if (moves[element].horizontal == horizontal && moves[element].vertical == vertical) {
                checker = 1;
            }
        }

        if (checker == 1) {
            var piece;
            $.post('/Game/GetPiece',
                { horizontal: horizontal, vertical: vertical, gameId: url.searchParams.get("gameId") }, function (data) {
                    piece = data;
                });
            if (piece == null) {
                changeSituation(activeElement.horizontalPosition, activeElement.verticalPosition, horizontal, vertical);
                IncreaseCounter();
                movingAnimation(activeElement.horizontalPosition, activeElement.verticalPosition, horizontal, vertical, Speed, Transfer);
                activeElement = undefined;
            } else {
                changeSituation(activeElement.horizontalPosition, activeElement.verticalPosition, horizontal, vertical);
                IncreaseCounter();
                movingAnimation(activeElement.horizontalPosition, activeElement.verticalPosition, horizontal, vertical, Speed, Kill);
                activeElement = undefined;
            }
            checker = 0;

        } else {
            activeElement = undefined;
            paintSquaresDefault();
        }
    }
}



function changeSituation(fromHorizontal, fromVertical, toHorizontal, toVertical) {
    $.post('/Game/changeSituation',
        {
            fromHorizontal: fromHorizontal,
            fromVertical: fromVertical,
            toHorizontal: toHorizontal,
            toVertical: toVertical,
            gameId: url.searchParams.get("gameId")
        },
        function (data) {
            if (data.length == 4) {
                movingAnimation(data[0], data[1], data[2], data[3], Speed, Transfer);
            }

            if(data.length == 2){
                playerHavetoChoosePieceToRevive = true;
                askForRevive();
                horizontalOfPieceToBeRevived = toHorizontal;
                verticalOfPieceToBeRevived = toVertical;
            }
        });
}

function IncreaseCounter() {

    gameVersion++;

    if(playerHavetoChoosePieceToRevive){
        return 0; 
    }

    counter++;
  
    MeveQueueValidator(counter);

    $.post('/Game/IncreaseMoveCount',
        { gameId: url.searchParams.get("gameId") }, function (data) {

        });

}

function playerChoosedPiece(element) {
    dissapearReviveItems();
    addRevivePiece(element);
    $.get('/Game/RevivePiece',
        { horizontal: horizontalOfPieceToBeRevived, vertical: verticalOfPieceToBeRevived, pieceToBeRevived: element.id , Color: element.firstChild.id.split("-")[0] , gameId: url.searchParams.get("gameId") }, function (data) {
            
        });
    playerHavetoChoosePieceToRevive = false;
    IncreaseCounter();
    
}