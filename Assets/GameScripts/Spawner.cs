using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	[System.Serializable]
	public class SpawnSetting
		{
			public int killCount;
			public float spawnRate;
		}

	public Transform femaleMourner;
	public Transform maleMourner;
	public Transform cop;
	public Transform mound;
	public Transform demon;

	public SpawnSetting[] mournerRates;
	public SpawnSetting[] copRates;

	public static float mournerRate = 15.0f;
	private float mournerTimer = mournerRate;
	public static float copRate = 15.0f;
	private float copTimer = copRate;
	private int side = 0;
	private float now = 0f;
	private bool spawn = false;
	private float offset = 0f;
	private int playerDeaths = 0;

	public Vector3 getNewEnemySpawnPosition()
	{
		int side = 1;

		int rand = Random.Range (0, 100);

		if (rand > 50) side = -1;

		return new Vector3 (13 * side, 0, 0f);


	}

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
				Instantiate (femaleMourner, getNewEnemySpawnPosition(), Quaternion.identity);
			} else {
				Instantiate( maleMourner, getNewEnemySpawnPosition(), Quaternion.identity);
			}
		}


		foreach (SpawnSetting ss in mournerRates) {
			if (Player.player.GetComponent<Player> ().kills == ss.killCount) {
				mournerRate = ss.spawnRate;
				mournerTimer = Mathf.Min(mournerTimer, mournerRate);
			}
		}

		foreach (SpawnSetting ss in copRates) {
			if (Player.player.GetComponent<Player> ().kills == ss.killCount) {
				copRate = ss.spawnRate;
				copTimer = Mathf.Min(copTimer, copRate);
			}
		}


			if (copTimer > 0) {
				copTimer -= now;
			} else {
				copTimer = copRate;
				side = Random.Range(1,10);
				if (side % 2 == 0) {
					Instantiate( cop, getNewEnemySpawnPosition(), Quaternion.identity);
				} else {
					Instantiate( cop, getNewEnemySpawnPosition(), Quaternion.identity);
				}
			}

		if (Player.player.GetComponent<Player> ().possessing == false && playerDeaths > 1 && spawn == false) {
			float playerX = Player.player.GetComponent<Transform>().position.x;
			spawn = true;
			offset = Random.Range(1f, 2.5f);
			for (int x = 0; x < playerDeaths; x+= 1) {
				if (x % 2 == 0) {
					Instantiate(mound, new Vector3(playerX + offset, -3.140015f, 0), Quaternion.identity);
					Instantiate(demon, new Vector3(playerX + offset, -3.121371f, 0), Quaternion.identity);
				} else {
					Instantiate(mound, new Vector3(playerX - offset, -3.140015f, 0), Quaternion.identity);
					Instantiate(demon, new Vector3(playerX - offset, -3.121371f, 0), Quaternion.identity);
				}
			}
		}
	}
}
