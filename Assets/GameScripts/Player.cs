using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool possessing;
	public int deaths = 1;
	public int kills = 0;
	public bool gameOvel;
	public string key = "ghost_sprite";
	public static Player player;
	public static GameObject possessedEnemy;
	public Vector3 possessedPos;
	public static bool handGrab;
	///The below variables bar the user from repossessing immediately after being ejected
	//keep below corpseTimer (5.0f)
	public static float possessBuffer = 2.0f;
	public float possessTimer;

	public Rect grabBounds;
	
	//allows for demon hand to grab.
	public Rect transformedGrabBounds() {
		Rect result = grabBounds;
		result.center += (Vector2) transform.position;
		result.center -= new Vector2 (result.width / 2, result.height / 2);
		return result;
	}


	// Use this for initialization
	void Start () {
		possessing = false;
		player = this;
		//initialize at 0 so that the initial corpse can be acquired
		possessTimer = 0f;
	}
	/// <summary>
	/// Toggles the active status of  the player,
	/// upon decactivation the possess buffer gets reuped
	/// upon deactivation the deaths ++
	/// </summary>
	public void toggleStatus() {
		if (gameObject.activeSelf) {
			gameObject.SetActive(false);
			possessTimer = possessBuffer;
		} else {
			transform.position = possessedPos;
			gameObject.SetActive(true);
			deaths += 1;
		}
	}

 

	// Update is called once per frame
	void Update () {
		if (possessing) {
			toggleStatus();
		} else if (possessTimer > -1){
			possessTimer -= Time.deltaTime;
		}
		if (gameOvel) {
			Application.LoadLevel("gameOvel");
		}
	}

	//allows visualization of the bounds
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transformedGrabBounds().center, new Vector3(grabBounds.width, grabBounds.height, 0));
	}
}
