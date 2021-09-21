function createContextForGameInfo(matches) {

    for (var index in matches) {
        gameInfo = matches[index];
        createGameInfo(gameInfo.opponent, gameInfo.date.year, gameInfo.date.month, gameInfo.date.day, gameInfo.date.hour, gameInfo.date.minute, gameInfo.hero.name, gameInfo.venue, gameInfo.tournament, gameInfo.photoId, gameInfo.boardId, gameInfo.id)
    }
}

function createGameInfo(opponent, year, month, day, hour, minute, hero, venue, tournament, photo, board, id) {

    divElement = document.createElement("div");
    imgElement = document.createElement("img");
    heroElement = document.createElement("img");
    dateElement = document.createElement("h1");
    opponentElement = document.createElement("h1");
    arenaElement = document.createElement("h2");
    linkElement = document.createElement("a");
    tournamentElement = document.createElement("h2");

    divElement.id = id;
    heroElement.src = "../images/" + hero + ".jpg";
    imgElement.src = "../images/" + photo + ".jpg";
    dateElement.innerText = "Date: " + hour + ":" + minute + ", " + month + "/" + day + "/" + year
    opponentElement.innerText = "Opponent: " + opponent;
    arenaElement.innerText = "Venue: " + venue;
    linkElement.append(opponentElement);
    linkElement.href = "../Game/GamePage?gameid=" + id;
    linkElement.id = "opponent";
    tournamentElement.innerText = "Tournament: " + tournament;

    if (gameInfo.hero.color == "white") {

        divElement.append(heroElement, imgElement, linkElement, dateElement, arenaElement, tournamentElement)
    } else {
        divElement.append(imgElement, heroElement, linkElement, dateElement, arenaElement, tournamentElement)
    }
    document.body.appendChild(divElement);
}

function fillGames(data) {

    var wrapperElement = document.getElementById("games_list");
    wrapperElement.innerHTML = "";

    for (var index in data) {

        var status = "Waiting";

        var divElement = document.createElement("div");
        var whiteTextElement = document.createElement("h1");
        var blackTextElement = document.createElement("h1");
        var statusElement = document.createElement("h1");
        var spectateButton = document.createElement("button");
        var joinButton = document.createElement("button");

        divElement.className = "game";

        whiteTextElement.innerHTML = "White: " + data[index].whitePlayerName;
        blackTextElement.innerHTML = "Black: " + data[index].blackPlayerName;

        spectateButton.innerHTML = "SPECTATE";
        spectateButton.className = "join";
        spectateButton.value = data[index].gameId;
        spectateButton.setAttribute("onclick", "redirectToGame(this)")
        spectateButton.style.backgroundColor = Red;

        joinButton.innerHTML = "JOIN";
        joinButton.style.backgroundColor = Green;
        joinButton.className = "join";
        joinButton.setAttribute("onclick", "joinPlayer(this)")
        joinButton.setAttribute("value", data[index].gameId)


        if (data[index].whitePlayerName == url.searchParams.get("playerid")
            || data[index].blackPlayerName == url.searchParams.get("playerid")) {
            joinButton.innerHTML = "CONTINUE";
            joinButton.style.backgroundColor = Blue;
            joinButton.setAttribute("onclick", "returnPlayerToGame(this)");
            joinButton.setAttribute("value", data[index].gameId);
            statusElement.innerHTML = "Status: " + status;
            divElement.append(whiteTextElement, blackTextElement, statusElement, joinButton);
            wrapperElement.append(divElement);
            continue;
        }


        if (data[index].isStarted) {

            status = "Ongoing"
            statusElement.innerHTML = "Status: " + status;

            divElement.append(whiteTextElement, blackTextElement, statusElement, spectateButton);
            wrapperElement.append(divElement);
            continue;
        }

        statusElement.innerHTML = "Status: " + status;
        divElement.append(whiteTextElement, blackTextElement, statusElement, spectateButton, joinButton)

        wrapperElement.append(divElement);
    }
}



function joinPlayer(item) {
    $.ajaxSetup({ async: false });
    $.post('/Game/JoinPlayer',
        { gameId: item.value, playerWhoJoined: url.searchParams.get("playerid") });
    window.location.href =  defaultURl + "GamePage" + "?gameId=" + item.value + "&playerId=" + url.searchParams.get("playerid");
}


function returnPlayerToGame(item) {
    window.location.href = window.location.href = defaultURl + "GamePage" + "?gameId=" + item.value + "&playerId=" + url.searchParams.get("playerid");
}

function redirectToGame(item) {
    window.location.href = window.location.href = defaultURl + "GamePage" + "?gameId=" + item.value + "&playerId=" + url.searchParams.get("playerid");;
}
