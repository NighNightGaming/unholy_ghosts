#pragma strict


var facingRight = true;

function flip() {
  facingRight = !facingRight;
  
  var theScale = transform.localScale;
  theScale.x *= -1;
  transform.localScale = theScale;
}

function FixedUpdate () {
  var h : float = Input.GetAxis("Horizontal");
   var v : float = Input.GetAxis("Vertical");
  
  if(h < 0) {
    rigidbody2D.velocity = Vector2(-3.0, 0);
    if (facingRight) {
      flip();
    }
  } else if(h > 0) {
    rigidbody2D.velocity = Vector2(3.0, 0);
    if (!facingRight) {
      flip();
    }
  }
  if(v < 0) {
    if (transform.position.y > -2.5) {
    rigidbody2D.velocity = (Vector2(0, -3.0));
    }
  } else if(v > 0) {
    if (transform.position.y < -1) {
    rigidbody2D.velocity = (Vector2(0, 3.0));
    }
  }
}

