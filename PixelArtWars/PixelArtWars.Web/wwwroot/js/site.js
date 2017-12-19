// Write your JavaScript code.

//facebook share script
function fbs_click(id) {
	var theImg = document.getElementById(id);
	var u = theImg.src;
	var t = theImg.getAttribute("alt");
	window.open("http://www.facebook.com/sharer.php?u=" + encodeURIComponent(u) + "&t=" + encodeURIComponent(t), "sharer", "toolbar=0,status=0,width=626,height=626");
	return false;
}
