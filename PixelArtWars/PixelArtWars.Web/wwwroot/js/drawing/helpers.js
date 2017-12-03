function bindButtons() {
	var children = document.getElementById("buttons").children;
	for (var i = 0; i < children.length; i++) {
		var child = children[i];
		if (child.tagName.toLowerCase() !== "input") {
		    child.onclick = function () {
		        var value = child.value;
		        currentcolor = value;
		    }
		}
    }
}

bindButtons();