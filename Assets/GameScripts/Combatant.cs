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
	public float removeTimer = 5.0f;
	private bool activated;
	private GameObject newTimer;
	Player theGhost;
	// Use this for initialization
	void Start () {
		theGhost = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	/// <summary>
	/// Gets the possessed.
	/// </summary>
	void GetPossessed()
	{	
		corpse = false;
		possesed = true;
		GetComponent<SpriteRenderer> ().color = zombieColor;
		health = maxHealth;
		GetComponent<SpriteRenderer> ().sortingOrder = 2;	
		Destroy (newTimer);

	}
	/// <summary>
	/// Checks the ghost
	/// </summary>
	void checkGhost() {
		float diffX = Mathf.Abs(Player.player.transform.position.x - transform.position.x);
		float diffY = Mathf.Abs (Player.player.transform.position.y - transform.position.y);
		if (diffY < 1 && diffX < 1 && !(Player.player.possessing)) {
			SendMessage("GetPossessed");
			theGhost.possessing = true;
		}
	}

	void startTimer (bool activated) {
		if (activated) {
			newTimer = (GameObject) Instantiate(Timer, new Vector3(transform.position.x,transform.position.y + 0.5f, 0), Quaternion.identity);
			newTimer.GetComponent<corpseTimer>().corpse = this;
		}
	}
	/// <summary>
	/// Deaths the animation.
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
		//tween to death position
		else {
			target = Quaternion.Euler(0,0,90);
		}
		transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);
	}
	// Update is called once per frame
	void Update () {
		deathAnim (!corpse);
		if (health <= 0) {
			if(!corpse) {
				corpseCount++;
				Debug.Log("Died: New corpse count is " + corpseCount);
				activated = true;
			}
			corpse = true;
			checkGhost();
			startTimer(activated);
			activated = false;
		}
		if (corpse) {
			if (possesed) {
				possesed = false;
				theGhost.possessing = false;
				theGhost.toggleStatus();
			}
			if (removeTimer > 0) {
				removeTimer -= Time.deltaTime;
			} else {
				theGhost.kills += 1;
				corpseCount--;
				Debug.Log("Despawned: New corpse count is " + corpseCount);
				if(corpseCount <= 0) {
					Application.LoadLevel("nocorpse");
				}
				Destroy (newTimer);
				Destroy(gameObject);
			}
		}
	}
}
