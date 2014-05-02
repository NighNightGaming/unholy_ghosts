#pragma strict

//custom cursor
var cursorTexture : Texture2D;

//use auto for better compatability
var cursorMode : CursorMode = CursorMode.ForceSoftware;

var hotSpot : Vector2 = Vector2.zero;

function OnMouseEnter() {
	Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
}

function OnMouseExit() {
	Cursor.SetCursor(null, Vector2.zero, cursorMode);
}