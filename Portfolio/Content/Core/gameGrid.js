"use strict";
class gameGrid {

    initializeGridFromServer(gameData) {



        this.grid = Array();


        //This assumes the array is rectangular
        for (var y = 0; y < gameData[0].length; y++) {
            this.grid[y] = Array();
            for (var x = 0; x < gameData.length; x++) {
                var cell = new gameCell();
                cell.color = gameData[x][y].Color;
                cell.owner = gameData[x][y].Owner;
                cell.surrounded = false;
                this.grid[y][x] = cell;
                cell.x = x;
                cell.y = y;
            }
        }


        this.horizontalCount = x;
        this.verticalCount = y;

        this.playerCells = Array();
        this.playerCountElements = Array();
        this.players = Array();
        this.currentPlayer = 1;

    }


    initializeGrid(width, height) {

        //Saves grid dimensions
        this.horizontalCount=width;
        this.verticalCount=height;

        this.grid = Array();

        this.playerCells = Array();
        this.playerCountElements = Array();
        this.players = Array();
        this.currentPlayer = 1;

        //Initializes grid with cells
        for(var i=0;i<height;i++)
        {
            this.grid[i] = Array();
            for (var j = 0; j < width; j++) {
                var cell = new gameCell();
                cell.initRandom();
                this.grid[i][j] = cell;
                cell.x = j;
                cell.y = i;
            }
        }
    }

    gridToText() {
        var result='';
        for(var h=0;h<this.grid.length;h++)
        {
            for (var w = 0; w < this.grid[h].length; w++) {
                result = result + this.grid[h][w];
            }
            result = result + '<br/>';
        }
        return result;
    }

    getCellCenter(x, y) {
        var tmp = Array();
        tmp[0] = x * (this.horizontalStep)*2 + this.horizontalStep ;
        tmp[1] = y * this.verticalStep + this.verticalStep / 2;


        //Offsets in y every second column
        if ((x % 2) == 0)
        {
            tmp[1] += this.verticalStep / 2;
        }


        return tmp;
    }

    initCanvasSteps() {

        //Need to make room for offset rows
        this.horizontalStep = this.canvasWidth / (3 + (this.horizontalCount - 1) * 2);

        //This will change for hexagonal cells
        this.verticalStep = this.canvasHeight / (this.verticalCount + 0.5);


    }

    initDivsSteps() {





        var r=Math.sqrt(3)/2;

        //Vertical step is calculated first because it is taken into account on overlap calculation
        this.divHeight = this.containerHeight / (this.verticalCount + 0.5 + (1 - r) / 2);

        //Then vertical steps are corrected to account for the symetry adjustment
        this.divHeight = this.divHeight / r;

        var nx = this.horizontalCount;
        //Then horizontal step with overlap taken into account
        this.divWidth = 4 * this.containerWidth / (3 * nx + 1);






        this.divHeight =  Math.min(this.divHeight, this.divWidth);
        this.divWidth = this.divHeight;


    }

    setPlayerCell(x, y, player) {

        this.playerCells[player]=Array();

        this.captureCell(x, y, player);
        this.players.push(player);
        if (player < this.currentPlayer) {
            this.currentPlayer = player;
        }

        this.grid[y][x].initialCell=true;


        this.updateScores();
    }

    updateScores() {
        for (var i = 0; i < this.players.length;i++)
            //Display score if element was defined
            if (this.playerCountElements[this.players[i]] != 'undefined') {
                this.playerCountElements[this.players[i]].text(this.playerCells[this.players[i]].length);
            }
    }

    updateDivDisplay(cell) {
        var cellJ = $(cell.associatedDiv);
        var active = cellJ.hasClass('hexagon-active');
        var captured = cellJ.hasClass('hexagon-just-captured');
        cell.associatedDiv.className = 'cell content hexagon hexagon-' + cell.color;

        var cellJ = $(cell.associatedDiv);

        if (active) {
            cellJ.addClass('hexagon-active');
        }
        if (captured) {
            cellJ.addClass('hexagon-just-captured');
        }
        if (cell.initialCell) {
            cellJ.addClass('initial-player-cell');
        }
        var ownerText;
        if (cell.owner < 1) {
            ownerText = "";
        }
        else {
            ownerText = cell.owner;
        }
        cell.associatedDiv.setAttribute('data-content', ownerText);
    }


    ClearJustCaptured(player) {

        for (var i = 0; i < this.playerCells[player].length; i++)
        {
            this.playerCells[player][i].justCaptured = false;
            $(this.playerCells[player][i].associatedDiv).removeClass('hexagon-just-captured');
        }

    }

    play(player,newColor)
    {



        if (this.currentPlayer != player)
        {
            console.warn("Not this player's turn!");
            return;
        }

        this.ClearJustCaptured(player);

        for (var i = 0; i < this.playerCells[player].length;i++)
        {
            var cell= this.playerCells[player][i];
            cell.color = newColor;
            if(!cell.surrounded)
            {
                this.checkNeighbors(cell);
            }

            if(this.usingDivs)
            {
                this.updateDivDisplay(cell);
            }
        }

        this.draw();

        this.updateScores();

        this.nextPlayerTurn(player);

    }

    setActive(player,active) {
        for (var i = 0; i < this.playerCells[player].length; i++)
        {
            if (active) {
                $(this.playerCells[player][i].associatedDiv).addClass('hexagon-active');
            }
            else {
                $(this.playerCells[player][i].associatedDiv).removeClass('hexagon-active');
            }


        }
    }

    nextPlayerTurn(currentPlayer) {

        this.setActive(currentPlayer,false);
        for (var i = 0; i < this.players.length; i++) {
            if (this.players[i] == currentPlayer) {
                var nextPlayerIndex = (i + 1) % this.players.length;
                this.currentPlayer = this.players[nextPlayerIndex];
                break;
            }
        }
        this.setActive(this.currentPlayer, true);

    }

    captureCell(x,y,player)
    {
        this.grid[y][x].owner = player;
        this.playerCells[player].push(this.grid[y][x]);
        this.grid[y][x].justCaptured = true;



       if (this.usingDivs) {
           this.updateDivDisplay(this.grid[y][x]);
           $(this.grid[y][x].associatedDiv).addClass('hexagon-just-captured');
        }
        if (!this.grid[y][x].surrounded) {
            this.checkNeighbors(this.grid[y][x]);
        }
    }

    checkNeighbors(cell) {
        var surrounded = true;

        //Setup deltas for neighbors
        var deltasX = [-1,-1,0,0,1,1]
        if (cell.x % 2 == 0) {
            var deltasY = [0, 1, -1, 1, 0, 1]
        }
        else {
            var deltasY = [-1, 0, -1, 1, -1, 0]
        }

        //Iterate through every neighbor
        for (var i = 0; i <= 5; i++) {
            var dx = deltasX[i];
            var dy = deltasY[i];

            var neighborX = cell.x + dx;
            var neighborY = cell.y + dy;

            //Check if neighbor is not out of the grid, or the same cell
            if (((dx != 0) || (dy != 0)) &&
                (neighborX >= 0) && (neighborX < this.horizontalCount) &&
                (neighborY>=0) && (neighborY<this.verticalCount))
            {
                var neighbor = this.grid[neighborY][neighborX];

                if(cell.owner!=neighbor.owner)
                {
                    //Oponent's cell: do nothing
                    if (neighbor.owner != 0) {}
                    //Capture
                    else if(cell.color==neighbor.color){
                        this.captureCell(neighborX, neighborY, cell.owner);
                    }
                    //Neighbor is not owned, not same color: mark cell as not surrounded
                    else {
                        surrounded = false;
                    }

                }
            }

        }
        cell.surrounded=surrounded
    }

    draw() {
        if(this.usingCanvas)
        {
            this.drawCanvas();
        }
    }

    drawDivs(e) {
        if (arguments.length >= 1) {
            this.divsContainer = e;
            this.usingDivs = true;
            this.containerWidth = e.width();
            this.containerHeight = e.height();
        }
        var parent = this.divsContainer;

        if(parent.children().length==0)
        {
            this.createDivs();
        }
    }

    createDivs() {
        this.initDivsSteps();


        for (var y = 0; y < this.grid.length; y++) {

            for (var x = 0; x < this.grid[y].length; x++) {

                var div = document.createElement('div');
                var cell = document.createElement('div');
                div.className = 'cell-container';

                div.setAttribute('data-pos', '(' + x + ',' + y + ')');

                var r=Math.sqrt(3)/2
                var top = (this.divHeight * r  * y)-(this.divHeight*(1-r)/2);

                //Position taking into account overlap
                var left = ((this.divWidth / 2 + this.divHeight / 4) * x);
                if ((x % 2) == 0) {
                    top += (this.divHeight * r / 2);
                }



                div.style.left = left + 'px';
                div.style.top = top + 'px';
                div.style.width = this.divWidth  + 'px';
                div.style.height = this.divHeight + 'px';
                //cell.className = 'cell content hexagon hexagon-' + this.grid[y][x].colorString();

                if (this.grid[y][x].owner != 0) {
                    $(cell).addClass('hexagon-just-captured');

                }
                if(this.grid[y][x].initialCell){
                  $(cell).addClass('initial-player-cell');
                }
                if (this.grid[y][x].owner == 1) {
                    $(cell).addClass('hexagon-active');
                }


                this.grid[y][x].associatedDiv = cell;
                this.updateDivDisplay(this.grid[y][x]);
                this.divsContainer.append(div);
                $(div).append(cell);

                var maskLeft = document.createElement('div');
                maskLeft.className = "cell-mask-left";
                var maskRight = document.createElement('div');
                maskRight.className = "cell-mask-right";

                $(cell).append(maskLeft);
                $(cell).append(maskRight);

            }
        }


    }

    drawCanvas(e) {

        //If a canvas was passed, save it
        if (arguments.length >= 1)
        {
            this.canvas = e;
            this.canvasContext = this.canvas.getContext("2d");
            this.usingCanvas = true;
        }

        var ctx = this.canvasContext;


        //Saves canvas dimensions
        this.canvasHeight = this.canvas.height;
        this.canvasWidth = this.canvas.width;

        ctx.clearRect(0, 0, this.canvasWidth, this.canvasHeight);

        //Initializes drawing steps
        this.initCanvasSteps();

        var dx = this.horizontalStep / 2*3;
        var dy = this.verticalStep / 2;
        var dxSmall = this.horizontalStep / 2;


        for (var y = 0; y < this.grid.length; y++) {
            for (var x = 0; x < this.grid[y].length; x++) {

                var center = this.getCellCenter(x, y);



                ctx.beginPath();
                ctx.moveTo(center[0]-dx, center[1]);
                ctx.lineTo(center[0] - dxSmall, center[1] - dy);
                ctx.lineTo(center[0] + dxSmall, center[1] - dy);
                ctx.lineTo(center[0] + dx, center[1]);
                ctx.lineTo(center[0] + dxSmall, center[1] + dy);
                ctx.lineTo(center[0] - dxSmall, center[1] + dy);
                ctx.fillStyle = this.grid[y][x].colorString();
                ctx.fill();

                if (this.grid[y][x].owner != 0) {
                    ctx.font = dy + "px Arial";
                    ctx.fillStyle = "black";
                    ctx.fillText(this.grid[y][x].owner, center[0] - dy / 4, center[1] + dy / 3);
                }


            }
        }


    }

}
