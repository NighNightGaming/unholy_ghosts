	using UnityEngine;
using System.Collections;

public class Combatant : MonoBehaviour {

	public static int corpseCount = 0;
	public GameObject Timer;
	public float maxHealth = 100;
	public float health = 100;    
	public bool corpse = false;
	public bool possesed = false;
	public Color zombieColor = Color.green;
	public float destroyTimer = 5.0f;
	private bool timerActivate;
	private GameObject newTimer;

	// Use this for initialization
	void Start () {
		//this is included to take into consideration the start_mourner
		if (corpse) corpseCount++;
	}
	/// <summary>
	/// This function changes the necesary components to have a possesed enemy:
	/// Enemy is no longer a corpse, but a zombie
	/// It is now a zombie, thus it is possessed.
	/// Zombies are a certain color
	/// health should be back up as they are sort of revived, right?
	/// the timer is reset.
	/// Destroy the old timer.
	/// </summary>
	void GetPossessed()
	{	
		corpse = false;
		possesed = true;
		GetComponent<SpriteRenderer> ().color = zombieColor;
		health = maxHealth;
		GetComponent<SpriteRenderer> ().sortingOrder = 2;	
		destroyTimer = 5.0f;
		Destroy (newTimer);


	}
	/// <summary>
	/// This function aquires the difference between the enemy's position and the players
	/// if it gets below a threshold, 1 and the Player is not possessing
	/// sends a message to all the other scripts to adjust their flags.
	/// and it sets the player's possesion flag to true
	/// </summary>
	void checkGhost() {
		float diffX = Mathf.Abs(Player.player.transform.position.x - transform.position.x);
		float diffY = Mathf.Abs (Player.player.transform.position.y - transform.position.y);
		if (diffY < 1 && diffX < 1 && !(Player.player.possessing)) {
			SendMessage("GetPossessed");
			Player.player.possessing = true;
		}
	}

	/// <summary>
	/// This function instatiates the timer which appears above the corpses indicating their eventual demise
	/// </summary>
	/// <param name="timerActivate">If set to <c>true</c> then function instantiates a timer above the corpse.</param>
	void startTimer (bool timerActivate) {
		if (timerActivate) {
			newTimer = (GameObject) Instantiate(Timer, new Vector3(transform.position.x,transform.position.y + 0.5f, 0), Quaternion.identity);
			newTimer.GetComponent<corpseTimer>().corpse = this;
		}
	}
	/// <summary>
	/// This function animates the death of the enemy by rotating them over the z axis.
	/// </summary>
	/// <param name="revive">If set to <c>true</c> revive.</param>
	void deathAnim(bool revive) {
		//tween to live position
		Quaternion target;
		float speed = 120;
		float step = speed * Time.deltaTime;

		if (revive) {
			target = Quaternion.Euler(0,0,0);
		} 
		//tween to death position, ie laying down.
		else {
			target = Quaternion.Euler(0,0,90);
		}
		transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);
	}

	// Update is called once per frame
	void Update () {

		//if possessed, assign the player's possession to the current game object
		if (possesed) Player.possessedEnemy = gameObject;

		//check for corpse status, if true, then animate the death, otherwise maintain erection, hehe...
		deathAnim (!corpse);

		if (health <= 0) {
			//before corpse, raise the corpse count, else it gets mixed up with the start_mourner
			if(!corpse) {
				corpseCount++;
				Debug.Log("Died: New corpse count is " + corpseCount);
				timerActivate = true;
			}
			corpse = true;
			checkGhost();
			startTimer(timerActivate);
			timerActivate = false;
		}

		if (corpse) {
			//if possessed then corpse, remove possession and related flags
			if (possesed) {
				possesed = false;
				if(Player.possessedEnemy == gameObject) Player.possessedEnemy = null;
				Player.player.possessing = false;
				Player.player.toggleStatus();
			}
			if (destroyTimer > 0) {
				destroyTimer -= Time.deltaTime;
			} else {
				Player.player.kills += 1;
				corpseCount--;
				Debug.Log("Despawned: New corpse count is " + corpseCount);
				//if there are no corpses on the screen, nothing can happen
				if(corpseCount <= 0) {
					Application.LoadLevel("nocorpse");
				}
				Destroy (newTimer);
				Destroy(gameObject);
			}
		}
	}
}
