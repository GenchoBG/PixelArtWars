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

$("#myForm").submit(function (e) {
    e.preventDefault();

    var id = document.getElementById("gameId").value;
    var imageData = canvas.toDataURL("image/png").replace("data:image/png;base64,", "");

    $.ajax({
        url: "/games/play/submitdrawing/" + id,
        method: "post",
        dataType: "json",
        data: {
            'id': id,
            'drawing': imageData
        },
        success: function(response) {
            window.location = response["url"];
        },
        error: function(req, status, err) {
            console.log("something went wrong");
            console.log(status);
            console.log(err);
            console.log(req);
        }
    });
});

bindButtons();