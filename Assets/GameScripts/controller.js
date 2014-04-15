#pragma strict


var facing = "right";

function FixedUpdate () {
	
	if(Input.GetKey(KeyCode.LeftArrow)) {
		rigidbody2D.velocity = Vector2(-3.0, 0);
		if (facing != "left") {
			transform.localScale = Vector2(-2, 2);
			facing = "left";
		}
	}
	else if(Input.GetKey(KeyCode.RightArrow)) {
		rigidbody2D.velocity = Vector2(3.0, 0);
		if (facing != "right") {
			transform.localScale = Vector2(2,2);
			facing = "right";
		}
	}
	else if(Input.GetKey(KeyCode.DownArrow)) {
		if (transform.position.y > -2.5) {
		rigidbody2D.velocity = (Vector2(0, -3.0));
		}
	}
	else if(Input.GetKey(KeyCode.UpArrow)) {
		if (transform.position.y < -1) {
		rigidbody2D.velocity = (Vector2(0, 3.0));
		}
	}
}

