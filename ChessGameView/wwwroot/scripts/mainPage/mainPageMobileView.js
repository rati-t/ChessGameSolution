function drawMainPageVisual(players) {

    for (var index in players) {
        playerInfo = players[index];
        createPlayerInfo(playerInfo.name, playerInfo.country, playerInfo.year, playerInfo.alive, playerInfo.age, playerInfo.PeakRating, playerInfo.id)
    }

    function createPlayerInfo(name, country, year, alive, age, rating, id) {

        playerElement = document.createElement("div");
        imgElement = document.createElement("img");
        nameElement = document.createElement("h1");
        countryElement = document.createElement("h2");
        yearElement = document.createElement("h3");
        ratingElement = document.createElement("h3");
        ageElement = document.createElement("h3");
        linkElement = document.createElement("a");

        playerElement.id = mainPageJson.players[index].playerId;
        imgElement = document.createElement("img");
        imgElement.src = "../images/" + id + ".jpg";
        nameElement.innerText = "Name: " + name;
        countryElement.innerText = "Country: " + country;
        yearElement.innerText = "Born: " + year;
        if (alive) {
            ageElement.innerText = "Age: " + age;
        } else {
            ageElement.innerText = "Died at the age of " + age;
        }


        ratingElement.innerText = "Peak rating: " + rating;
        linkElement.href = "../htmls/playerPageMobileView.html?playerid=" + id
        linkElement.innerHTML = "&#9817";

        playerElement.append(imgElement, nameElement, countryElement, yearElement, ageElement, ratingElement, linkElement)
        document.body.appendChild(playerElement)
    }

}