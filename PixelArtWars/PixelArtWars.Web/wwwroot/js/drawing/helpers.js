$("#myForm").submit(function (e) {
    e.preventDefault();
    hidebutton();
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

function hidebutton() {
	$("#savebutton").attr("disabled", "disabled").removeClass("btn-success").addClass("btn-disabled").val("Saving...");
    $('#img').show();
}