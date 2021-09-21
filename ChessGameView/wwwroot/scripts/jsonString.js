var mainPageJson;
var playerPageJson;
var gamePageJson;

var namesToFind = ["Magnus",
    "Bobby",
    "Nona",
    "Viswanathan",
]


loadDoc();

function loadDoc() {

    $.ajaxSetup({ async: false });

    if (window.location.href == "https://localhost:5001/Game/mainpage"
        || window.location.href == "https://localhost:44312/Game/mainpage") {

        var MainPage = new XMLHttpRequest();
        MainPage.open("GET", "../jsons/mainPage.json", true);
        MainPage.send();

        MainPage.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {

                var information = JSON.parse(this.responseText);

                for (var index in information.players) {

                    var id = information.players[index].id;
                    $.post('/Game/AddPlayerToList',
                        { playerId: id });
                }

                mainPageJson = JSON.parse(this.responseText)
                drawMainPage(mainPageJson);
            }
        };
    }
}

function finder(txt) {
    return txt == page;
}

