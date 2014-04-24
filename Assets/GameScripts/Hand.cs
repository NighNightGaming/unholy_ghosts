using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
	public float range = 100f;
	private float diffX;

	public Rect grabBounds;


	private Rect transformedGrabBounds() {
		Rect result = grabBounds;
		result.center += (Vector2) transform.position;
		result.center -= new Vector2 (result.width / 2, result.height / 2);
		return result;
	}

	void Start () {
		}
	// Update is called once per frame
	void FixedUpdate () {
		diffX = Mathf.Abs(Player.player.transform.position.x - gameObject.transform.position.x);
		if (diffX < 1 && gameObject.transform.position.y < -3.5) {
			rigidbody2D.velocity = (new Vector2 (0.0f, 8f));
		}

		if (transformedGrabBounds().Overlaps(Player.player.transformedGrabBounds())) {
				print("You touched");
				Application.LoadLevel("gameOvel");

		}


	}


	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transformedGrabBounds().center, new Vector3(grabBounds.width, grabBounds.height, 0));
	}


}
