using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool possessing;
	private int deaths = 0;
	public int kills = 0;
	public Transform demon;
	public Transform mound;
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
			gameObject.tag = "Possessing";
		} else {
			gameObject.SetActive(true);
			gameObject.tag = "Player";
			deaths += 1;
		}
	}
	/// <summary>
	/// Calls the demons.
	/// </summary>
	public void callDemons () {
		if (!possessing) {
				for (int x = 0; x < 2 * deaths; x += 1) {
						if (x % 2 == 0) {
								int randX = Random.Range (-7, -2);
								Instantiate (mound, new Vector3 (randX, -3, 0), Quaternion.identity);
								Instantiate (demon, new Vector3 (randX, -3, 0), Quaternion.identity);
						} else {
								int randX = Random.Range (2, 16);
								Instantiate (mound, new Vector3 (randX, 3, 0), Quaternion.identity);
								Instantiate (demon, new Vector3 (randX, 3, 0), Quaternion.identity);
						}
				}
		}
	}
	// Update is called once per frame
	void Update () {
		if (possessing) {
			toggleStatus();
		}
	}
}
