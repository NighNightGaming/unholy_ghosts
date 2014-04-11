#pragma strict

var pane = gameObject;

function OnMouseDown () {
	if (pane.activeSelf) {
		pane.SetActive(false);
	} else {
		pane.SetActive(true);
	}
}