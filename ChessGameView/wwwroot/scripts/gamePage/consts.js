const Blue = "blue";
const Black = "Black";
const White = "White";
const Red = "red";
const Yellow = "yellow";
const blackHexadecimal = "#1b6b31";  // green in hexadecimal
const whiteHexadecimal = "#BDBDBD";  // white in hexadecimal 
const BlackPiece = "BlackPiece";
const WhitePiece = "WhitePiece";

const normal = "normal";
const looping = "looping";
const Transfer = "transfer";
const Kill = "kill";
const Rooking = "rooking";

const KingPiece = "king";
const RookPiece = "rook";
const Spectator = "Spectator";

var board;
var activeElement = undefined;
var nextMoves;

const Speed = 200;

var playerWhoCreatedGame;
var playerWhoJoined;
var counter;
var gameVersion = 0;
var IsStarted = false;
var interval; 
var playerHavetoChoosePieceToRevive = false;
var horizontalOfPieceToBeRevived;
var verticalOfPieceToBeRevived;

var url = new URL(window.location.href);
var defaultURl = window.location.href.split("Game")[0] + "Game/";

$.get('/Game/GetMoveNumber',
    { gameId: url.searchParams.get("gameId") }, function (data) {
        counter = data;
    });
