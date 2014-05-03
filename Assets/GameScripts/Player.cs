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
	public float yOffset = 2f;
	public Vector3 possessedPos;
	public static bool handGrab;
	public static bool ZOMBIELORD = false;
	///The below variables bar the user from repossessing immediately after being ejected
	//keep below corpseTimer (5.0f)
	//to remove possessBuffer go to ln56 in Combantant.cs and (un)comment the last condition
	//STATUS: OFF
	public static float possessBuffer = 2.0f;
	public float possessTimer;

	/// this is the flag for the second and more play throughs to enable the high score
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
			//possessTimer = possessBuffer;
		} else {
			//this sets a y offeset for the respawn
			//possessedPos.y += yOffset;
			//commenting the following line makes the ghost spawn where it possessed,  
			//other wise it gets spawned at the first spawn point of that body
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
		if (!possessing) {
			gameObject.SetActive(true);
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
