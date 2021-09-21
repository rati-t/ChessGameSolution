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
    linkElement.innerHTML = "&#9819;";
    linkElement.href = "../htmls/gamePageMobileView.html?gameid=" + board;
    tournamentElement.innerText = "Tournament: " + tournament;

    if (gameInfo.hero.color == "white") {

        divElement.append(heroElement, imgElement, opponentElement, dateElement, arenaElement, linkElement, tournamentElement)
    } else {
        divElement.append(imgElement, heroElement, opponentElement, dateElement, arenaElement, linkElement, tournamentElement)
    }
    document.body.appendChild(divElement);
    resizePlayerPage();
}

function resizePlayerPage() {

    $("div").css({
        "width": "180px",
        "height": "90px",
    })

    $("img").css({
        "width": "90px",
        "height": "90px"
    })

    $("h1").css({
        "font-size": "20px"
    })

    $("h2").css({
        "font-size": "20px"
    })

    $("a").css({
        "font-size": '50px'
    })
}