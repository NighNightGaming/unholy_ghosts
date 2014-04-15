using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool possessing;
	public int deaths = 1;
	public int kills = 0;
	public Transform demon;
	public Transform mound;
	public bool gameOvel;
	public string key = "ghost_sprite";
	public static GameObject player;
	// Use this for initialization
	void Start () {
		possessing = false;
		player = gameObject;
	}
	/// <summary>
	/// Toggles the status.
	/// </summary>
	public void toggleStatus() {
		if (gameObject.activeSelf) {
			gameObject.SetActive(false);
		} else {
			gameObject.SetActive(true);
			gameObject.tag = "Player";
			deaths += 1;
		}
	}
	// Update is called once per frame
	void Update () {
		if (possessing) {
			toggleStatus();
		}
		if (gameOvel) {
			Application.LoadLevel("gameOvel");
		}
	}
}
