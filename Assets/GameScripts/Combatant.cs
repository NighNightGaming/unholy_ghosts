using UnityEngine;
using System.Collections;

public class Combatant : MonoBehaviour {
	
	public float maxHealth = 100;
	public float health = 100;    
	public bool corpse = false;
	public bool possesed = false;
	public Color zombieColor = Color.green;
	public float corpseTimer = 2.0f;

	// Use this for initialization
	void Start () {
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

	}
	/// <summary>
	/// Checks the ghost
	/// </summary>
	void checkGhost() {
		Transform playerTran = Player.player.transform;
		float diffX = Mathf.Abs(playerTran.position.x - transform.position.x);
		float diffY = Mathf.Abs (playerTran.transform.position.y - transform.position.y);
		if (diffY < 1 && diffX < 1) {
			SendMessage("GetPossessed");
			Player.player.GetComponent<Player>().possessing = true;
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
			corpse = true;
			checkGhost();
		} 
		if (corpse) {
			if (possesed) {
				possesed = false;
				Player.player.GetComponent<Player>().possessing = false;
				Player.player.GetComponent<Player>().toggleStatus();
			}
			if (corpseTimer > 0) {
				corpseTimer -= Time.deltaTime;
			} else {
				Player.player.GetComponent<Player>().kills += 	1;
				Destroy(gameObject);
			}
		}
	}
}
