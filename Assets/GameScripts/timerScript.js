#pragma strict

private var gameTime : float;
function Start () {
	gameTime = 0;
}

function Update () {
	var guiTime = gameTime + Time.timeSinceLevelLoad;
	var seconds : int = guiTime % 60; 
	var minutes : int = guiTime / 60;
	guiText.text = minutes + ":" + seconds;
}