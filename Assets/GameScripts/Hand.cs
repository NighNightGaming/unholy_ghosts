using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	private float diffX;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per fram	e
	void Update () {	
		diffX = Mathf.Abs(Player.player.GetComponent<Transform> ().position.x - gameObject.transform.position.x);
		if (diffX < 1 && gameObject.transform.position.y <= -2.45) {
			rigidbody2D.velocity = (new Vector2(0.0f, 2f));
		} else {
			rigidbody2D.velocity = (new Vector2(0.0f, -1.5f));
		}
	}
}
