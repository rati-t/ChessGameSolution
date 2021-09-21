function drawMainPage(informationJson) {

    var players = [];

    for (var index in informationJson.players) {

        var player = informationJson.players[index];
        var human = new Player(player.name, player.country, player.age, player.alive, player.born[0].year, player.peakRating, player.id)
        players.push(human);
    }
    drawMainPageVisual(players)
}