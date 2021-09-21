function MakeGame(information) {

    nextMoves = information;

    createBoard(8, 8, information);

    $("td").click(function () {

        var horizontalId = parseInt(this.id.split(":")[0]);
        var verticalId = parseInt(this.id.split(":")[1]);

        measureMovementType(horizontalId, verticalId);
    });

    $("button").click(function () {

        makeNextMove(this.id)

    });
}

function drawTable(horizontalValue, verticalValue, order) {

    var measureColor;
    var tdElement;
    var trElement;

    for (var verticalTd = 1; verticalTd < verticalValue + 2; verticalTd++) {

        if (verticalTd % 2 == 1) {

            measureColor = 1;
        } else {

            measureColor = 2;
        }

        trElement = document.createElement('tr');

        for (var horizontalTd = 0; horizontalTd < 9; horizontalTd++) {

            if (order == "reversed") {
                if (horizontalTd == 0 && verticalTd < 9) {

                    tdElement = document.createElement("td");
                    tdElement.className = "numbers";
                    tdElement.innerText = verticalTd;
                    trElement.append(tdElement);
                    continue;
                }

                if (verticalTd == 9) {

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
                tdElement.id = (9 - horizontalTd) + ":" + verticalTd;
                trElement.append(tdElement);
                measureColor++;
            }

            if (order == "normal") {

                if (horizontalTd == 0 && verticalTd < 9) {

                    tdElement = document.createElement("td");
                    tdElement.className = "numbers";
                    tdElement.innerText = 9 - verticalTd;
                    trElement.append(tdElement);
                    continue;
                }
                if (verticalTd == 9) {

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
                tdElement.id = horizontalTd + ":" + (9 - verticalTd);
                trElement.append(tdElement);
                measureColor++;
            }
        }
        $("table").append(trElement);
    }
}

function addPiecesToTable(boardArray) {

    for (verticalIndex in boardArray) {

        for (horizontalIndex in boardArray) {

            var element = boardArray[verticalIndex][horizontalIndex];

            if (element != 0) {

                var imgElement = document.createElement("img")
                imgElement.src = "../images/" + element.color + "_" + element.name + ".png"
                document.getElementById(element.horizontalPosition + ":" + element.verticalPosition).appendChild(imgElement)

            }
        }
    }
    resizeBoardForMobile();
}

function addPieceToTable(piece) {

    var imgElement = document.createElement("img")

    imgElement.src = "../images/" + piece.color + "_" + piece.name + ".png"
    document.getElementById(piece.horizontalPosition + ":" + piece.verticalPosition).appendChild(imgElement)

}

function paintSquares(Array, horiozontalBlue, verticalBlue) {

    paintSquaresDefault();

    if (horiozontalBlue != -1) {
        document.getElementById(horiozontalBlue + ":" + verticalBlue).style.backgroundColor = Blue;
    }

    for (element in Array) {
        var id = Array[element][0] + ":" + Array[element][1]
        document.getElementById(id).style.backgroundColor = Yellow;
    }
}

function paintSquaresDefault() {
    var blackSquares = document.getElementsByClassName(Black);
    var whiteSquares = document.getElementsByClassName(White);

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

function resizeBoardForMobile() {

    $("table").css({
        "margin-left": "-485px",
        "margin-top": "150px",
        "width": "350px",
        "height": "350px"
    })

    $("img").css({
        "width": "25px",
        "height": "25px"
    })

    $(".white").css({
        "width": "30px",
        "height": "30px"
    })

    $(".black").css({
        "width": "30px",
        "height": "30px"
    })

    $(".numbers").css({
        "width": "30px",
        "height": "30px",
        "font-size": "10px"
    })

    $("td").css({
        "text-align": "center",
        "width": "30px",
        "height": "30px"
    })

    $("img").css({
        "width": "26px",
        "height": "26px"
    })

    $("div img").css({
        "width": "100px",
        "height": "100px"
    })


    $("button").css({
        "top": "500px",
        "width": "60px",
        "height": "60px"
    })

    $("#less").css({
        "left": "550px",
    })

    $("#greater").css({
        "left": "550px",
    })

}