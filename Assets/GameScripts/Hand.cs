using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
	public float range = 100f;
	private float diffX;
	private LayerMask targetLayers; 

	void Start () {
				targetLayers = 1 << LayerMask.NameToLayer ("Player");
		}
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit2D reach = Physics2D.Raycast (transform.position, Vector2.up, range, targetLayers);
		diffX = Mathf.Abs(Player.player.GetComponent<Transform> ().position.x - gameObject.transform.position.x);
		if (diffX < 1 && gameObject.transform.position.y < -3.5) {
			rigidbody2D.velocity = (new Vector2(0.0f, 7f));
			if (reach.rigidbody != null) {
				print("You touched");
				Application.LoadLevel("gameOvel");
			}
		}

	}
}
