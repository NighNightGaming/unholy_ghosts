#pragma strict
var pane : GameObject;
function Start () {
	pane = GameObject.FindGameObjectWithTag("AboutInfo");
}

function OnMouseDown () {
	if (pane.activeSelf) {
		pane.SetActive(false);
	} else {
		pane.SetActive(true);
	}
}