$(document).ready(function () {

    $("td").click(function () {

        if(!IsStarted) {
            return 0;
        }

        if(playerHavetoChoosePieceToRevive){
            return 0;
        }

        if (document.getElementById(this.id).childNodes.length > 0 && activeElement === undefined) {

            if (document.getElementById(this.id).firstChild.className == WhitePiece && counter % 2 != 1) {
                return 0;
            }
            if (document.getElementById(this.id).firstChild.className == BlackPiece && counter % 2 != 0) {
                return 0;
            }
        }

        if (document.getElementById(this.id).childNodes.length == 0 && activeElement === undefined) {
            return 0;
        }
        var horizontalId = parseInt(this.id.split(":")[0]);
        var verticalId = parseInt(this.id.split(":")[1]);

        measureMovementType(horizontalId, verticalId);
    });
});



function drawTable(horizontalValue, verticalValue, order) {

    var measureColor;
    var tdElement;
    var trElement;

    document.getElementById("table_wrapper").innerHTML = "";

    for (var verticalTd = 1; verticalTd < verticalValue + 1; verticalTd++) {

        if (verticalTd % 2 == 1) {

            measureColor = 1;
        } else {

            measureColor = 2;
        }

        trElement = document.createElement('tr');

        for (var horizontalTd = 0; horizontalTd <= horizontalValue; horizontalTd++) {

            if (order == "reversed") {
                if (horizontalTd == 0 && verticalTd < 9) {

                    tdElement = document.createElement("td");
                    tdElement.className = "numbers";
                    tdElement.innerText = verticalTd;
                    trElement.append(tdElement);
                    continue;
                }

                if (verticalTd == verticalValue + 1) {

                    tdElement = document.createElement("td");
                    tdElement.className = "numbers";
                    if (horizontalTd != 0) {

                        tdElement.innerText = String.fromCharCode(75 - horizontalTd - 2);
                    }
                    trElement.append(tdElement);
                    continue;
                }

                tdElement = document.createElement("td");
                if (measureColor % 2 == 1) {

                    tdElement.className = "white";
                } else {

                    tdElement.className = "black";
                }
                tdElement.id = (horizontalValue + 1 - horizontalTd) + ":" + verticalTd;
                trElement.append(tdElement);
                measureColor++;
            }

            if (order == "normal") {

                if (horizontalTd == 0 && verticalTd <= verticalValue) {

                    tdElement = document.createElement("td");
                    tdElement.className = "numbers";
                    tdElement.innerText = 9 - verticalTd;
                    trElement.append(tdElement);
                    continue;
                }
                if (verticalTd == verticalValue + 1) {

                    tdElement = document.createElement("td");
                    tdElement.className = "numbers";
                    if (horizontalTd != 0) {

                        tdElement.innerText = String.fromCharCode(65 + horizontalTd - 1);
                    }

                    trElement.append(tdElement);
                    continue;
                }
                tdElement = document.createElement("td");
                if (measureColor % 2 == 1) {

                    tdElement.className = "white";
                } else {

                    tdElement.className = "black";
                }
                tdElement.id = horizontalTd + ":" + (verticalValue + 1 - verticalTd);
                trElement.append(tdElement);
                measureColor++;
            }
        }
        $("table").append(trElement);
    }
}

function addPiecesToTable(boardArray) {
    
    
    for(var index in document.getElementsByTagName("td")){
        document.getElementsByTagName("td")[index].innerHTML = "";
    }

    for (element in boardArray) {

        element = boardArray[element];

        var imgElement = document.createElement("img")

        if (element.color == Black) {
            imgElement.className = BlackPiece;
        }
        if (element.color == White) {
            imgElement.className = WhitePiece;
        }

        imgElement.src = "../images/" + element.color + "_" + element.name + ".png"
        document.getElementById(element.horizontalPosition + ":" + element.verticalPosition).appendChild(imgElement)
    }
}

function addPieceToTable(piece) {

    var imgElement = document.createElement("img")

    imgElement.src = "../images/" + piece.color + "_" + piece.name + ".png"
    document.getElementById(piece.horizontalPosition + ":" + piece.verticalPosition).appendChild(imgElement)

}

function paintSquares(Data) {

    paintSquaresDefault();

    for (element in Data) {
        var id = Data[element].horizontal + ":" + Data[element].vertical
        document.getElementById(id).style.backgroundColor = Yellow;
    }
}

function paintSquaresDefault() {
    var blackSquares = document.getElementsByClassName("black");
    var whiteSquares = document.getElementsByClassName("white");

    for (horizontalIndex = 0; horizontalIndex < blackSquares.length; horizontalIndex++) {

        blackSquares[horizontalIndex].style.backgroundColor = blackHexadecimal;
        whiteSquares[horizontalIndex].style.backgroundColor = whiteHexadecimal;

    }

}

function movingAnimation(fromHorizontal, fromVertical, toHorizontal, toVertical, speed, type) {

    paintSquaresDefault();

    var toParent = document.getElementById(toHorizontal + ":" + toVertical);
    var fromChild = document.getElementById(fromHorizontal + ":" + fromVertical).firstChild;

    verticalForAnimation = toParent.offsetTop;
    horizontalForAnimation = toParent.offsetLeft;
    childHorizontalForAnimation = fromChild.offsetParent.offsetLeft;
    childVerticalForAnimation = fromChild.offsetParent.offsetTop;

    if (type == Kill) {
        toParent.firstChild.remove();
    }

    $(fromChild).css({ "position": "absolute", "top": childVerticalForAnimation, "left": childHorizontalForAnimation })
        .animate({
            top: verticalForAnimation,
            left: horizontalForAnimation
        }, speed, function () {
            toParent.appendChild(fromChild);
            $(fromChild).css("position", "static")
        })

}

function addPlayerToSquare(information) {
       
        playerWhoCreatedGame = information.playerWhoMadeGame;
        playerWhoJoined = information.playerWhoJoined;

    if(information.playerWhoMadeGame.id == url.searchParams.get("playerId")) {

        var bottomElement = document.getElementById("player2");
        var topElement = document.getElementById("player1");

        bottomElement.setAttribute("value", information.playerWhoMadeGame.color);
        bottomElement.innerHTML = information.playerWhoMadeGame.id;

        topElement.setAttribute("value", information.playerWhoJoined.color);
        topElement.innerHTML = information.playerWhoJoined.id;

    } else if(information.playerWhoJoined.id == url.searchParams.get("playerId")){

        var bottomElement =  document.getElementById("player2");
        var topElement = document.getElementById("player1");

        bottomElement.setAttribute("value", information.playerWhoJoined.color);
        bottomElement.innerHTML = information.playerWhoJoined.id;

        topElement.setAttribute("value", information.playerWhoMadeGame.color);
        topElement.innerHTML = information.playerWhoMadeGame.id;

    } else {
        var bottomElement =  document.getElementById("player2");
        var topElement = document.getElementById("player1");

        bottomElement.setAttribute("value", White);
        topElement.setAttribute("value", Black);

        if(information.playerWhoMadeGame.color == White){
            bottomElement.innerHTML = information.playerWhoMadeGame.id
            topElement.innerHTML = information.playerWhoJoined.id;
        } else {
            bottomElement.innerHTML = information.playerWhoJoined.id;
            topElement.innerHTML = information.playerWhoMadeGame.id;
        }    
    }
}

function MeveQueueValidator(counter) {
    if(counter % 2 == 0){
        document.querySelector('[value="White"]').style.backgroundColor = "red";
        document.querySelector('[value="Black"]').style.backgroundColor = "green";
        return 0;
    }
    if(counter % 2 == 1){
        document.querySelector('[value="Black"]').style.backgroundColor = "red";
        document.querySelector('[value="White"]').style.backgroundColor = "green";
        return 0;
    }
}

function askForRevive(){
    document.getElementById("revive_pieces").style.display = "inline";
}

function addImagesToReviveTab(pieceColor) {

    var RevivePieces = document.getElementsByClassName("revivePiece");
    
    for(var index in RevivePieces){ 
        if(RevivePieces[index].childElementCount > 0) {
            RevivePieces[index].firstChild.src = "../images/" + pieceColor + "_" + RevivePieces[index].id + ".png";
            RevivePieces[index].firstChild.id = pieceColor + "-" + RevivePieces[index].id
            RevivePieces[index].setAttribute("onclick", "playerChoosedPiece(this)")
        }
    }
}

function dissapearReviveItems(){
    document.getElementById("revive_pieces").style.display = "none";
}

function addRevivePiece(element){

    var imgElement = document.createElement("img")
    imgElement.src = "../images/" + element.firstChild.id.split("-")[0] + "_" + element.firstChild.id.split("-")[1] + ".png";
    imgElement.className = element.firstChild.id.split("-")[0] + "Piece";

    document.getElementById(horizontalOfPieceToBeRevived + ":" + verticalOfPieceToBeRevived).innerHTML = "";
    document.getElementById(horizontalOfPieceToBeRevived + ":" + verticalOfPieceToBeRevived).appendChild(imgElement)
}

function changeTime(information){

    var playerWhoMadeGameMinutes = parseInt(information.playerWhoMadeGame.timeForThink / 60);
    var playerWhoMadeGameSecond  = information.playerWhoMadeGame.timeForThink % 60;

    var playerWhoJoinedMinutes = parseInt(information.playerWhoJoined.timeForThink / 60);
    var playerWhoJoinedSeconds  = information.playerWhoJoined.timeForThink % 60;

    if(information.playerWhoMadeGame.id == url.searchParams.get("playerId")){

        document.getElementById("player1_timer").innerHTML = playerWhoJoinedMinutes + " : " + playerWhoJoinedSeconds;
        document.getElementById("player2_timer").innerHTML = playerWhoMadeGameMinutes + " : " + playerWhoMadeGameSecond;
    }

    if(information.playerWhoJoined.id == url.searchParams.get("playerId")){

        document.getElementById("player1_timer").innerHTML = playerWhoMadeGameMinutes + " : " + playerWhoMadeGameSecond;
        document.getElementById("player2_timer").innerHTML = playerWhoJoinedMinutes + " : " + playerWhoJoinedSeconds;
    }

}