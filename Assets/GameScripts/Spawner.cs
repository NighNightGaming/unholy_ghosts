using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public Transform femaleMourner;
	public Transform maleMourner;
	public Transform cop;
	public Transform mound;
	public Transform demon;
	private bool copOn = false;
	public static float mournerRate = 15.0f;
	private float mournerTimer = mournerRate;
	public static float copRate = 15.0f;
	private float copTimer = copRate;
	private int side = 0;
	private float now = 0f;
	private bool spawn = false;
	private float offset = 0f;
	private int playerDeaths = 0;
	
	// Update is called once per frame
	void Update () {
		now = Time.deltaTime;
		playerDeaths = Player.player.GetComponent<Player> ().deaths;
		if (mournerTimer > 0) {
			mournerTimer -= now;
		} else {
			mournerTimer = mournerRate;
			side = Random.Range(1,10);
			if (side % 2 == 0) {
				Instantiate (femaleMourner, new Vector3(Random.Range(2.5f,12.0f), -2.31614f, 0f), Quaternion.identity);
			} else {
				Instantiate( maleMourner, new Vector3(Random.Range(-2,-12), -2.39389f, 0), Quaternion.identity);
			}
		}
		if (Player.player.GetComponent<Player> ().kills == 5) {
			mournerRate /= 2;
			copOn = true;
				}
		if (copOn) {
			if (copTimer > 0) {
				copTimer -= now;
			} else {
				side = Random.Range(1,10);
				if (side % 2 == 0) {
					Instantiate( cop, new Vector3(Random.Range(2.5f,12.0f), -2.31614f, 0f), Quaternion.identity);
				} else {
					Instantiate( cop, new Vector3(Random.Range(-2,-12), -2.39389f, 0), Quaternion.identity);
				}
			}

		}
		if (Player.player.GetComponent<Player> ().possessing == false && playerDeaths > 1 && spawn == false) {
			float playerX = Player.player.GetComponent<Transform>().position.x;
			spawn = true;
			offset = Random.Range(1f, 2.5f);
			for (int x = 0; x < playerDeaths; x+= 1) {
				if (x % 2 == 0) {
					Instantiate(mound, new Vector3(playerX + offset, -2.983414f, 0), Quaternion.identity);
					Instantiate(demon, new Vector3(playerX + offset, -3.121371f, 0), Quaternion.identity);
				} else {
					Instantiate(mound, new Vector3(playerX - offset, -2.983414f, 0), Quaternion.identity);
					Instantiate(demon, new Vector3(playerX - offset, -3.121371f, 0), Quaternion.identity);
				}
			}
		}
	}
}
