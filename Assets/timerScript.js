#pragma strict

private var gameTime = 0;
function Start () {
	gameTime = Time.time;
}

function Update () {
	var guiTime = gameTime + Time.time;
	var seconds : int = guiTime % 60; 
	var minutes : int = guiTime / 60;
	guiText.text = minutes + ":" + seconds;
}