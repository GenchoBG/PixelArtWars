var pixels = [];
var areasize = 500;
var squaresize = 20;
var squarescount = areasize / squaresize;
var currentcolor = [0, 0, 0];
var backgroundcolor = [255, 255, 255];
var radius = 1;

function setup() {
	var canvas = createCanvas(areasize, areasize);
	canvas.parent("sketch-holder");
	initialisepixelsarray();
}

function draw() {
	background(backgroundcolor);
	drawlines();
	drawpixels();
	getcolor();
    getradius();
	if (mouseIsPressed) {
		fillpixels(mouseButton);
	}
}

function getcolor() {
    currentcolor = $("#myColor").val();
}

function getradius() {
    radius = $("#radius").val();
}

function initialisepixelsarray() {
    for (var i = 0; i < squarescount; i++) {
        pixels.push([]);
    }
}


function drawlines() {
	for (var i = 0; i < squarescount; i++) {
		line(i * squaresize, 0, i * squaresize, areasize);
	}
	for (var i = 0; i < squarescount; i++) {
		line(0, i * squaresize, areasize, i * squaresize);
	}
	line(0, areasize - 1, areasize - 1, areasize - 1);
	line(areasize - 1, 0, areasize - 1, areasize - 1);
}

function drawpixels() {
	for (var i = 0; i < pixels.length; i++) {
		for (var j = 0; j < pixels.length; j++) {
			if (pixels[i][j]) {
				fill(pixels[i][j]);
				rect(i * squaresize, j * squaresize, squaresize, squaresize);
			}
		}
	}
}

function fillpixels(mouseButton) {
	var mouseRow = Math.floor(mouseX / squaresize);
    var mouseCol = Math.floor(mouseY / squaresize);

	var r = radius - 1;

	for (var row = mouseRow - r; row <= mouseRow + r; row++) {
	    for (var col = mouseCol - r; col <= mouseCol + r; col++) {
	        fillpixel(mouseButton, row, col);
	    }
    }
}

function fillpixel(mouseButton, row, col) {
    if (row < squarescount && row >= 0 && col < squarescount && col >= 0) {
        if (mouseButton == LEFT) {
            pixels[row][col] = currentcolor;
        }
        else {
            pixels[row][col] = backgroundcolor;
        }
    }
}

function clearboard() {
    for (var i = 0; i < squarescount; i++) {
        for (var j = 0; j < squarescount; j++) {
            pixels[i][j] = null;
        }
    }
}
