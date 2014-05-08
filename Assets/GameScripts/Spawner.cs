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

	//declared as gameobjects because they will be destroyed in this script aswell
	public GameObject mound;
	public GameObject demon;
	private GameObject demon1;
	private GameObject mound1;
	private GameObject demon2;
	private GameObject mound2;

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
	public static int enemiesOnScreen = 0;

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
		playerDeaths = Player.player.deaths;
		if (enemiesOnScreen < 5) {
						if (mournerTimer > 0) {
								mournerTimer -= now;
						} else {
								mournerTimer = mournerRate;
								gender = Random.Range (1, 10);
								if (gender % 2 == 0) {
										Instantiate (femaleMourner, getNewEnemySpawnPosition (), Quaternion.identity);
					enemiesOnScreen +=1;
								} else {
										Instantiate (maleMourner, getNewEnemySpawnPosition (), Quaternion.identity);
					enemiesOnScreen +=1;
								}
						}

						//for each spawnRate,
						foreach (SpawnSetting ss in mournerRates) {
								//if player kills equals the killCount treshhold 
								if (Player.player.kills == ss.killCount) {
										//assign the rate the according spawnRate for the kllcount
										mournerRate = ss.spawnRate;
										///this line clips the mournerTimer down to at most the new mournerRate.
										/// This makes the new mournerRate take effect immediately, 
										/// so that if the timer was previously higher it now reflects the new value.
										mournerTimer = Mathf.Min (mournerTimer, mournerRate);
								}
						}

						foreach (SpawnSetting ss in copRates) {
								if (Player.player.kills == ss.killCount) {
										copRate = ss.spawnRate;
										//here it effectively disables spawning until killCount is reached 
										copTimer = Mathf.Min (copTimer, copRate);
								}
						}


						if (copTimer > 0) {
								copTimer -= now;
						} else {
								copTimer = copRate;
								Instantiate (cop, getNewEnemySpawnPosition (), Quaternion.identity);
				enemiesOnScreen +=1;
						}

						//the player is a ghost again, ie not possessing, having died (as to avoid spawning upon load)
						//and spawn is false to prevent too many of these spawning
						//think of way of progressive difficulty.
						if (!Player.player.possessing && playerDeaths > 1 && spawn == false) {
								float playerX = Player.player.transform.position.x;
								spawn = true;
								//offsets the spawn of the demon hands/mounds
								offset = Random.Range (2.5f, 4.5f);
								if (!mound1 && !demon1 && !mound2 && !demon2) {
										mound1 = (GameObject)Instantiate (mound, new Vector3 (playerX + offset, -3.140015f, 0), Quaternion.identity);
										demon1 = (GameObject)Instantiate (demon, new Vector3 (playerX + offset, -4.505769f, 0), Quaternion.Euler (0, 0, -90));
										mound2 = (GameObject)Instantiate (mound, new Vector3 (playerX - offset, -3.140015f, 0), Quaternion.identity);
										demon2 = (GameObject)Instantiate (demon, new Vector3 (playerX - offset, -4.505769f, 0), Quaternion.Euler (0, 0, -90));
								} else {
										mound1.transform.position = new Vector3 (playerX + offset, -3.140015f, 0);
										demon1.transform.position = new Vector3 (playerX + offset, -4.505769f, 0);
										mound2.transform.position = new Vector3 (playerX - offset, -3.140015f, 0);
										demon2.transform.position = new Vector3 (playerX - offset, -4.505769f, 0);
										mound1.SetActive (true);
										demon1.SetActive (true);
										mound2.SetActive (true);
										demon2.SetActive (true);

								}
						}
			//the relational operator effectively clips the amount of demon spawn to 5
			else if (Player.player.possessing && spawn == true) {
								spawn = false;
								mound1.SetActive (false);
								demon1.SetActive (false);
								mound2.SetActive (false);
								demon2.SetActive (false);
						}
				}
	}
}
