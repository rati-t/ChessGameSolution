setInterval(function(){ 
    $.ajaxSetup({ async: false });
        $.get('/Game/GetGames',
        { playerId: url.searchParams.get("playerid")}, function(data){            
            fillGames(data);
        });
}, 1000);

function uncheck(id) {
    if (id == White) {
        document.getElementById(Black).checked = false;
    }
    if (id == Black) {
        document.getElementById(White).checked = false;
    }
}

function createGame() {

    var color;

    if (document.getElementById(White).checked) {
        color = White;
    }  else if (document.getElementById(Black).checked) {
        color = Black;
    } else {
        alert("Choose Color");
        return 0;
    }
    
    $.post('/Game/CreateGame',
        { playerWhoCreatedGame:  url.searchParams.get("playerid"), choosenColor: color}, function(data){
            window.location.href = defaultURl + "GamePage" + "?gameId=" + data + "&playerId=" + url.searchParams.get("playerid");
        });
}