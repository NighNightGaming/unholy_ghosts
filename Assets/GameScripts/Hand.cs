using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
	public float range = 100f;
	private float diffX;

	public Rect grabBounds;

	/// <summary>
	/// Estbalishes the grab bounds
	/// </summary>
	/// returns the reulint rect.
	private Rect transformedGrabBounds() {
		Rect result = grabBounds;
		result.center += (Vector2) transform.position;
		result.center -= new Vector2 (result.width / 2, result.height / 2);
		return result;
	}
	// Update is called once per frame
	void FixedUpdate () {

		diffX = Mathf.Abs(Player.player.transform.position.x - gameObject.transform.position.x);
		//if the player is active
		if (Player.player.gameObject.activeSelf) {
			//if the player is close (ie difference between player.x and hand.x is less than threshold) jump!
			if (diffX < 1 && gameObject.transform.position.y < -2.36) {
								rigidbody2D.velocity = (new Vector2 (0.0f, 8f));
						}
						//if the bounds overlaps with the players bounds, GAMEOVER!
						if (transformedGrabBounds ().Overlaps (Player.player.transformedGrabBounds ())) {
								#if UNITY_EDITOR
								Debug.Log ("You touched");
								#endif
								Player.handGrab = true;
								Application.LoadLevel ("gameOvel");

						}
				}
	}

	//allows for the visual representation of the grab box
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transformedGrabBounds().center, new Vector3(grabBounds.width, grabBounds.height, 0));
	}


}
