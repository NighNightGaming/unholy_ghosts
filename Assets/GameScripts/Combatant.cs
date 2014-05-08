	using UnityEngine;
using System.Collections;

public class Combatant : MonoBehaviour {

	///to keep track of the corpses in-game
	public static int corpseCount; 
	public GameObject Timer;
	public float maxHealth = 100;
	public float health = 100;    
	public bool corpse = false;
	public bool possessed = false;
	public Color zombieColor = Color.cyan;
	public Color deathColor = Color.green;
	public int zombieLordStart = 10;
	public float destroyTimeout = 5.0f;
	private bool timerActivate;
	private GameObject destroyTimer; 
	public static int zombieCount = 0;
	private Animator anim;

	// Use this for initialization
	void Start () {
		//this is included to take into consideration the start_mourner
		if (corpse) corpseCount += 1;
		anim = GetComponent<Animator> ();
	}
	/// <summary>
	/// This function changes the necesary components to have a possessed enemy:
	/// Enemy is no longer a corpse, but a zombie
	/// It is now a zombie, thus it is possessed.
	/// Zombies are a certain color
	/// health should be back up as they are sort of revived, right?
	/// the timer is reset.
	/// Destroy the  destroyTimer.
	/// </summary>
	void GetPossessed(bool possession){	
		possessed = possession;
		if (possession) {
			zombieCount += 1;
			corpse = false;
			corpseCount -= 1;
			Destroy (destroyTimer);
			///makes em somewhat tougher
			health = maxHealth + 50;
			#if UNITY_EDITOR
			Debug.Log ("Possesed: Zombie count is " + zombieCount);
			#endif
		} else {
			zombieCount -= 1;
			corpse = true;
			#if UNITY_EDITOR
			Debug.Log ("Ejected: Zombie count is " + zombieCount);
			#endif
			if(Player.possessedEnemy == gameObject) Player.possessedEnemy = null;
			Player.player.possessing = false;
			Player.player.toggleStatus();
		}
		GetComponent<SpriteRenderer> ().color = zombieColor;
		GetComponent<SpriteRenderer> ().sortingOrder = 2;	
		destroyTimeout = 5.0f;
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
		//commentout the final condition to remove possessbuffer
		//comment second position to not allow multiple possessions
		if (Player.player.kills < zombieLordStart) {
			if (diffY < 1 && diffX < 1 && !(Player.player.possessing) /* && Player.player.possessTimer <= 0f*/) {
#if UNITY_EDITOR
				Debug.Log ("possession time!");
#endif
				SendMessage ("GetPossessed", true);
				Player.player.possessing = true;
			}
		} else {
			if (diffY < 1 && diffX < 1  /*&& !(Player.player.possessing) && Player.player.possessTimer <= 0f*/) {
				#if UNITY_EDITOR
				Debug.Log ("possession time!");
				#endif
				SendMessage ("GetPossessed", true);
				Player.player.possessing = true;
				Player.ZOMBIELORD = true;
			}
		}
	}

	/// <summary>
	/// This function instatiates the timer which appears above the corpses indicating their eventual demise
	/// </summary>
	/// <param name="timerActivate">If set to <c>true</c> then function instantiates a timer above the corpse.</param>
	bool startTimer (bool timerActivate) {
		if (timerActivate) {
			destroyTimer = (GameObject) Instantiate(Timer, new Vector3(transform.position.x,transform.position.y + 0.5f, 0), Quaternion.identity);
			destroyTimer.GetComponent<corpseTimer>().corpse = this;
		}
		return timerActivate = false;
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
			GetComponent<SpriteRenderer> ().color = deathColor;
			target = Quaternion.Euler(0,0,90);
		}
		transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);
	}

	// Update is called once per frame
	void Update () {

		//if possessed, assign the player's possession to the current game object
		if (possessed) Player.possessedEnemy = gameObject;

		//check for corpse status, if true, then animate the death, otherwise maintain erection, hehe...
		deathAnim (!corpse);

		if (health <= 0) {
			//before corpse, raise the corpse count, else it gets mixed up with the start_mourner
			if(!corpse) {
				corpse = true;
				corpseCount +=1; 
				#if UNITY_EDITOR
				Debug.Log("Died: New corpse count is " + corpseCount);
				#endif
				timerActivate = true;
			}
			//if possessed then corpse, remove possession and related flags
			if (possessed) {
				SendMessage("GetPossessed", false);
			}
			checkGhost();
			timerActivate = startTimer(timerActivate);
		}

		if (corpse) {
			anim.SetBool("walking", false);	
			health = 0;
			if (destroyTimeout > 0) {
				destroyTimeout -= Time.deltaTime;
			} else {
				Player.player.kills += 1;
				corpseCount -= 1;
				#if UNITY_EDITOR
					Debug.Log("Despawned: New corpse count is " + corpseCount);
				#endif
				//if there are no corpses on the screen, nothing can happen
				if(corpseCount <= 0 && Player.player.possessing == false) {
					Player.handGrab = false;
					Application.LoadLevel("gameOvel");
				}
				Spawner.enemiesOnScreen -= 1;
				Destroy (destroyTimer);
				Destroy(gameObject);
			}
		}
	}
}
