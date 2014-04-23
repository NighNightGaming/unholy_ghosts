using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
	public float range = 100f;
	private float diffX;
	private LayerMask targetLayers;

	public Rect grabBounds;


	private Rect transformedGrabBounds() {
		Rect result = grabBounds;
		result.center += (Vector2) transform.position;
		result.center -= new Vector2 (result.width / 2, result.height / 2);
		return result;
	}

	void Start () {
				targetLayers = 1 << LayerMask.NameToLayer ("Player");
		}
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit2D reach = Physics2D.Raycast (transform.position, Vector2.up, range, targetLayers);
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
