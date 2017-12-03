var pixels = [];
var areasize = 500;
var squaresize = 20;
var squarescount = areasize / squaresize;
var currentcolor = [255, 0, 0];
var backgroundcolor = [255, 255, 255];

function setup() {
	var canvas = createCanvas(areasize, areasize);
	canvas.parent("sketch-holder");
	initialisepixelsarray();
}

function initialisepixelsarray() {
	for (var i = 0; i < squarescount; i++) {
		pixels.push([]);
	}
}

function draw() {
	background(backgroundcolor);
	drawlines();
	drawpixels();
	if (mouseIsPressed) {
		fillpixel(mouseButton);
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

function fillpixel(mouseButton) {
	var row = Math.floor(mouseX / squaresize);
	var col = Math.floor(mouseY / squaresize);

	if (row < squarescount && row >= 0 && col < squarescount && col >= 0) {
		if (mouseButton == LEFT) {
			pixels[row][col] = currentcolor;
		}
		else {
			pixels[row][col] = backgroundcolor;
		}
	}
}