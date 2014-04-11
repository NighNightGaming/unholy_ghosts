#pragma strict

var timer : float;
function Update () {
	if (timer > 0) {
		timer -= Time.deltaTime;
	} else {
		Destroy(gameObject);
	}
}