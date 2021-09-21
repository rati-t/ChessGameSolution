class ChessBoard {
    constructor(horizontalMax, verticalMax) {
        this.horizontalMax = horizontalMax;
        this.verticalMax = verticalMax;
        this.game = this.createBoard();
        this.killedPieces = [];
        this.moveCount = 0;
    }

    createBoard() {
        var boardArray = [];
        for (var index = 0; index < this.horizontalMax; index++) {

            var supporterArray = [];
            for (var verticalIndex = 0; verticalIndex < this.verticalMax; verticalIndex++) {
                supporterArray.push(0);
            }
            boardArray.push(supporterArray);
        }
        return boardArray;
    }
}

class Piece {
    constructor(horizontalPosition, verticalPosition, color, name, chessBoard) {
        this.horizontalPosition = horizontalPosition;
        this.verticalPosition = verticalPosition;
        this.color = color;
        this.name = name;
        this.chessBoard = chessBoard;
    }
}

class NormalPiece extends Piece {

    constructor(horizontalPosition, verticalPosition, color, name, chessBoard) {
        super(horizontalPosition, verticalPosition, color, name, chessBoard)
    }

    validateMovesForKing() {

        var squaresArray = [];

        for (var index in this.moves) {

            if (this.horizontalPosition + this.moves[index][0] > 0 && this.horizontalPosition + this.moves[index][0] <= this.chessBoard.horizontalMax
                && this.verticalPosition + this.moves[index][1] > 0 && this.verticalPosition + this.moves[index][1] <= this.chessBoard.verticalMax) {

                var horizontal = this.horizontalPosition + this.moves[index][0];
                var vertical = this.verticalPosition + this.moves[index][1];

                if (isAlly(horizontal, vertical, this.horizontalPosition, this.verticalPosition, this.chessBoard) || isEmpty(horizontal, vertical, this.chessBoard)) {
                    squaresArray.push([horizontal, vertical]);
                }
            }
        }

        return squaresArray;
    }

    validateMoves() {

        var squaresArray = [];

        for (var index in this.moves) {

            if (this.horizontalPosition + this.moves[index][0] > 0 && this.horizontalPosition + this.moves[index][0] <= this.chessBoard.horizontalMax
                && this.verticalPosition + this.moves[index][1] > 0 && this.verticalPosition + this.moves[index][1] <= this.chessBoard.verticalMax) {

                var horizontal = this.horizontalPosition + this.moves[index][0];
                var vertical = this.verticalPosition + this.moves[index][1];

                if (isEnemy(horizontal, vertical, this.horizontalPosition, this.verticalPosition, this.chessBoard) || isEmpty(horizontal, vertical, this.chessBoard)) {
                    squaresArray.push([horizontal, vertical]);
                }
            }
        }

        return squaresArray;
    }
}

class LoopingPiece extends Piece {

    constructor(horizontalPosition, verticalPosition, color, name, chessBoard) {
        super(horizontalPosition, verticalPosition, color, name, chessBoard)
    }

    validateMoves() {

        var squaresArray = [];

        for (var index in this.moves) {

            var multiplier = 1;

            while (this.horizontalPosition + this.moves[index][0] * multiplier > 0 && this.horizontalPosition + this.moves[index][0] * multiplier <= this.chessBoard.horizontalMax
                && this.verticalPosition + this.moves[index][1] * multiplier > 0 && this.verticalPosition + this.moves[index][1] * multiplier <= this.chessBoard.verticalMax) {

                var horizontal = this.horizontalPosition + this.moves[index][0] * multiplier;
                var vertical = this.verticalPosition + this.moves[index][1] * multiplier;

                if (isAlly(horizontal, vertical, this.horizontalPosition, this.verticalPosition, this.chessBoard)) {

                    break;
                }

                if (isEmpty(horizontal, vertical, this.chessBoard)) {
                    squaresArray.push([horizontal, vertical]);
                    multiplier += 1;
                    continue;
                }

                if (isEnemy(horizontal, vertical, this.horizontalPosition, this.verticalPosition, this.chessBoard)) {
                    squaresArray.push([horizontal, vertical]);
                    break;
                }
            }
        }
        return squaresArray;
    }

    validateMovesForKing() {

        var squaresArray = [];

        for (var index in this.moves) {

            var multiplier = 1;
            console.log("s")
            while (this.horizontalPosition + this.moves[index][0] * multiplier > 0 && this.horizontalPosition + this.moves[index][0] * multiplier <= this.chessBoard.horizontalMax
                && this.verticalPosition + this.moves[index][1] * multiplier > 0 && this.verticalPosition + this.moves[index][1] * multiplier <= this.chessBoard.verticalMax) {

                var horizontal = this.horizontalPosition + this.moves[index][0] * multiplier;
                var vertical = this.verticalPosition + this.moves[index][1] * multiplier;

                if (isAlly(horizontal, vertical, this.horizontalPosition, this.verticalPosition, this.chessBoard)) {
                    squaresArray.push([horizontal, vertical]);
                    break;
                }

                if (isEmpty(horizontal, vertical, this.chessBoard)) {
                    squaresArray.push([horizontal, vertical]);
                    multiplier += 1;
                    continue;
                }

                if (isEnemy(horizontal, vertical, this.horizontalPosition, this.verticalPosition, this.chessBoard)) {
                    if (this.chessBoard.game[vertical - 1][horizontal - 1].name != KingPiece) {
                        break;
                    }
                    multiplier += 1;
                }
            }
        }

        return squaresArray;
    }

}

class Knight extends NormalPiece {

    moves = [[1, 2], [2, 1], [2, -1], [1, -2], [-1, -2], [-2, -1], [-2, 1], [-1, 2]];

    constructor(horizontalPosition, verticalPosition, color, name, chessBoard) {
        super(horizontalPosition, verticalPosition, color, name, chessBoard);
    }
}

class King extends NormalPiece {

    moves = [[0, 1], [1, 1], [1, 0], [1, -1], [0, -1], [-1, -1], [-1, 0], [-1, 1]];
    RookingMoves = [[2, 0], [-2, 0]]

    constructor(horizontalPosition, verticalPosition, color, name, chessBoard) {
        super(horizontalPosition, verticalPosition, color, name, chessBoard);
    }

    validateMovesForKing() {

        var squaresArray = [];

        for (var index in this.moves) {

            if (this.horizontalPosition + this.moves[index][0] > 0 && this.horizontalPosition + this.moves[index][0] <= this.chessBoard.horizontalMax
                && this.verticalPosition + this.moves[index][1] > 0 && this.verticalPosition + this.moves[index][1] <= this.chessBoard.verticalMax) {

                var horizontal = this.horizontalPosition + this.moves[index][0];
                var vertical = this.verticalPosition + this.moves[index][1];

                if (isAlly(this.horizontalPosition, this.verticalPosition, horizontal, vertical, this.chessBoard) || isEmpty(horizontal, vertical, this.chessBoard)) {
                    squaresArray.push([horizontal, vertical]);
                }
            }
        }

        return squaresArray;
    }

    validateMoves() {

        var squaresArray = [];

        for (var index in this.moves) {

            if (this.horizontalPosition + this.moves[index][0] > 0 && this.horizontalPosition + this.moves[index][0] <= this.chessBoard.horizontalMax
                && this.verticalPosition + this.moves[index][1] > 0 && this.verticalPosition + this.moves[index][1] <= this.chessBoard.verticalMax) {

                var horizontal = this.horizontalPosition + this.moves[index][0];
                var vertical = this.verticalPosition + this.moves[index][1];

                if (isEnemy(this.horizontalPosition, this.verticalPosition, horizontal, vertical, this.chessBoard) || isEmpty(horizontal, vertical, this.chessBoard)) {
                    if (checkAvailable(horizontal, vertical, this.color, this.chessBoard)) {
                        squaresArray.push([horizontal, vertical]);
                    }
                }
            }
        }

        for (var index in this.RookingMoves) {

            if ((this.verticalPosition == 1 || this.verticalPosition == this.chessBoard.verticalMax)
                && this.horizontalPosition == 5) {

                var horizontal = this.horizontalPosition + this.RookingMoves[index][0];
                var vertical = this.verticalPosition + this.RookingMoves[index][1];

                if (index == 0) {
                    if (isEmpty(horizontal - 1, vertical, this.chessBoard) && isEmpty(horizontal, vertical, this.chessBoard)
                        && this.chessBoard.game[this.verticalPosition - 1][this.chessBoard.horizontalMax - 1].name == RookPiece
                        && this.chessBoard.game[this.verticalPosition - 1][this.chessBoard.horizontalMax - 1].color == this.color) {
                        if (checkAvailable(horizontal, vertical, this.color, this.chessBoard)) {
                            squaresArray.push([horizontal, vertical]);
                        }
                    }
                }
                if (index == 1) {
                    if (isEmpty(horizontal - 1, vertical, this.chessBoard)
                        && isEmpty(horizontal + 1, vertical, this.chessBoard)
                        && isEmpty(horizontal, vertical, this.chessBoard)
                        && this.chessBoard.game[this.verticalPosition - 1][0].name == RookPiece
                        && this.chessBoard.game[this.verticalPosition - 1][0].color == this.color) {
                        if (checkAvailable(horizontal, vertical, this.color, this.chessBoard)) {
                            squaresArray.push([horizontal, vertical]);
                        }
                    }
                }
            }

        }
        return squaresArray;
    }

}

class Bioshop extends LoopingPiece {

    moves = [[1, 1], [1, -1], [-1, 1], [-1, -1]];

    constructor(horizontalPosition, verticalPosition, color, name, chessBoard) {
        super(horizontalPosition, verticalPosition, color, name, chessBoard);
    }
}

class Rook extends LoopingPiece {

    moves = [[0, 1], [0, -1], [-1, 0], [1, 0],]

    constructor(horizontalPosition, verticalPosition, color, name, chessBoard) {
        super(horizontalPosition, verticalPosition, color, name, chessBoard);
    }
}

class Queen extends LoopingPiece {

    moves = [[0, 1], [1, 1], [1, 0], [1, -1], [0, -1], [-1, -1], [-1, 1], [-1, 0]]

    constructor(horizontalPosition, verticalPosition, color, name, chessBoard) {
        super(horizontalPosition, verticalPosition, color, name, chessBoard);
    }
}

class Pawn extends NormalPiece {

    moves = [[0, 1], [0, 2], [-1, 1], [1, 1]]

    constructor(horizontalPosition, verticalPosition, color, name, chessBoard) {
        super(horizontalPosition, verticalPosition, color, name, chessBoard)
    }

    validateMovesForKing() {

        var multiplier = 1;

        if (this.color == Black) {
            multiplier = -1;
        }

        var squaresArray = [];

        for (var index in this.moves) {

            if (this.horizontalPosition + this.moves[index][0] * multiplier > 0 && this.horizontalPosition + this.moves[index][0] * multiplier <= this.chessBoard.horizontalMax
                && this.verticalPosition + this.moves[index][1] * multiplier > 0 && this.verticalPosition + this.moves[index][1] * multiplier <= this.chessBoard.verticalMax) {

                var horizontal = this.horizontalPosition + this.moves[index][0] * multiplier;
                var vertical = this.verticalPosition + this.moves[index][1] * multiplier;

                if (index >= 2) {
                    squaresArray.push([horizontal, vertical]);
                }

            }
        }

        return squaresArray;

    }

    validateMoves() {

        var multiplier = 1;

        if (this.color == Black) {
            multiplier = -1;
        }

        var squaresArray = [];

        for (var index in this.moves) {

            if (this.horizontalPosition + this.moves[index][0] * multiplier > 0 && this.horizontalPosition + this.moves[index][0] * multiplier <= this.chessBoard.horizontalMax
                && this.verticalPosition + this.moves[index][1] * multiplier > 0 && this.verticalPosition + this.moves[index][1] * multiplier <= this.chessBoard.verticalMax) {

                var horizontal = this.horizontalPosition + this.moves[index][0] * multiplier;
                var vertical = this.verticalPosition + this.moves[index][1] * multiplier;

                if (index == 0 && isEmpty(horizontal, vertical, this.chessBoard)) {
                    squaresArray.push([horizontal, vertical]);
                }

                if (index == 1 && isEmpty(horizontal, vertical, this.chessBoard) && isEmpty(horizontal, vertical - multiplier, this.chessBoard) && (this.verticalPosition == 2 || this.verticalPosition == this.chessBoard.verticalMax - 1)) {
                    squaresArray.push([horizontal, vertical]);
                }

                if (index >= 2 && isEnemy(this.horizontalPosition, this.verticalPosition, horizontal, vertical, this.chessBoard)) {
                    squaresArray.push([horizontal, vertical]);
                }

            }
        }

        return squaresArray;
    }
}

function isEmpty(horizontal, vertical, chessBoard) {
    return chessBoard.game[vertical - 1][horizontal - 1] == 0;
}

function isAlly(fromtHorizontal, fromVertical, toHorizontal, toVertical, chessBoard) {
    return chessBoard.game[fromVertical - 1][fromtHorizontal - 1].color == chessBoard.game[toVertical - 1][toHorizontal - 1].color
}

function isEnemy(fromtHorizontal, fromVertical, toHorizontal, toVertical, chessBoard) {

    if (chessBoard.game[fromVertical - 1][fromtHorizontal - 1] != 0 && chessBoard.game[toVertical - 1][toHorizontal - 1] != 0) {
        return chessBoard.game[fromVertical - 1][fromtHorizontal - 1].color != chessBoard.game[toVertical - 1][toHorizontal - 1].color
    }
}

function checkAvailable(horizontal, vertical, color, chessBoard) {
    for (elem in chessBoard.game) {

        for (object in chessBoard.game[elem]) {

            if (chessBoard.game[elem][object] != 0 && chessBoard.game[elem][object].color != color) {
                if (chessBoard.game[elem][object].name == "pawn") {
                    for (var index in chessBoard.game[elem][object].validateMovesForKing()) {
                        if (chessBoard.game[elem][object].validateMovesForKing()[index][0] == horizontal && chessBoard.game[elem][object].validateMovesForKing()[index][1] == vertical) {
                            return false;
                        }
                    }
                } else {
                    for (var index in chessBoard.game[elem][object].validateMovesForKing()) {
                        if (chessBoard.game[elem][object].validateMovesForKing()[index][0] == horizontal && chessBoard.game[elem][object].validateMovesForKing()[index][1] == vertical) {
                            return false;
                        }
                    }
                }
            }

        }
    }
    return true;
}



