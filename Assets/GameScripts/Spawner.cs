using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	//serializing allows for cascading spawn times
	[System.Serializable]
	public class SpawnSetting
		{
			public int killCount;
			public float spawnRate;
		}

	//all the different prefabs
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
	private int gender = 0;
	private float now = 0f;
	private bool spawn = false;
	private float offset = 0f;
	private int playerDeaths = 0;

	/// <summary>
	/// Randomly decides on a side where the enemy will spawn 
	/// Note: spawning offscreen.
	/// </summary>
	/// <returns>The new enemy spawn position.</returns>
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
			gender = Random.Range(1,10);
			if (gender % 2 == 0) {
				Instantiate (femaleMourner, getNewEnemySpawnPosition(), Quaternion.identity);
			} else {
				Instantiate( maleMourner, getNewEnemySpawnPosition(), Quaternion.identity);
			}
		}

		//for each spawnRate,
		foreach (SpawnSetting ss in mournerRates) {
			//if player kills equals the killCount treshhold 
			if (Player.player.GetComponent<Player> ().kills == ss.killCount) {
				//assign the rate the according spawnRate for the kllcount
				mournerRate = ss.spawnRate;
				///this line clips the mournerTimer down to at most the new mournerRate.
				/// This makes the new mournerRate take effect immediately, 
				/// so that if the timer was previously higher it now reflects the new value.
				mournerTimer = Mathf.Min(mournerTimer, mournerRate);
			}
		}

		foreach (SpawnSetting ss in copRates) {
			if (Player.player.GetComponent<Player> ().kills == ss.killCount) {
				copRate = ss.spawnRate;
				//here it effectively disables spawning until killCount is reached 
				copTimer = Mathf.Min(copTimer, copRate);
			}
		}


			if (copTimer > 0) {
				copTimer -= now;
			} else {
				copTimer = copRate;
				Instantiate( cop, getNewEnemySpawnPosition(), Quaternion.identity);
			}

		//the player is a ghost again, ie not possessing, having died (as to avoid spawning upon load)
		//and spawn is false to prevent too many of these spawning
		//think of way of progressive difficulty.
		if (Player.player.GetComponent<Player> ().possessing == false && playerDeaths > 1 && spawn == false) {
			float playerX = Player.player.GetComponent<Transform>().position.x;
			spawn = true;
			//offsets the spawn of the demon hands/mounds
			offset = Random.Range(2.5f, 3.5f);
			for (int x = 0; x < playerDeaths; x+= 1) {
				if (x % 2 == 0) {
					Instantiate(mound, new Vector3(playerX + offset, -3.140015f, 0), Quaternion.identity);
					Instantiate(demon, new Vector3(playerX + offset, -4.505769f, 0), Quaternion.Euler(0,0,-90));
				} else {
					Instantiate(mound, new Vector3(playerX - offset, -3.140015f, 0), Quaternion.identity);
					Instantiate(demon, new Vector3(playerX - offset, -4.505769f, 0), Quaternion.Euler(0,0,-90));
				}
			}
		}
	}
}
