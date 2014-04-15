using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	private float diffX;
	// Update is called once per frame
	void FixedUpdate () {	
		diffX = Mathf.Abs(Player.player.GetComponent<Transform> ().position.x - gameObject.transform.position.x);
		if (diffX < 1 && gameObject.transform.position.y < -3.5 ) {
			rigidbody2D.velocity = (new Vector2(0.0f, 7f));
		}
	}
	void OnCollisionEnter2D( Collision2D coll ) {
		if (coll.gameObject.tag == "Player") {
			Application.LoadLevel("gameOvel");
				}
		}
}
