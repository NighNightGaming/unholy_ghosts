using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
	
	//Target for all enemies to move towards
	public Transform mobTarget;
	public float moveForce = 1;
	public int avoidanceClass = 0;
	public float closeToDist = 0.1f;
	public float repulsionForce = 0.1f;
	public static System.Collections.Generic.HashSet<EnemyMove> allEnemies;
	public bool possessed = false;
	public string facing = "right";
	private Combatant combatant;
	public float jumpTimer = 1.0f;

	/// <summary>
	/// Raises the level was loaded event.
	/// </summary>
	/// <param name="n">N.</param>
	void OnLevelWasLoaded (int n)
	{
		if (allEnemies != null)
			allEnemies = null;
	}
	
	// Use this for initialization
	void Start ()
	{
		if (allEnemies == null)
			allEnemies = new System.Collections.Generic.HashSet<EnemyMove> ();
		allEnemies.Add (this);
		combatant = GetComponent<Combatant> ();
		mobTarget = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	/// <summary>
	/// Gets the possessed.
	/// </summary>
	void GetPossessed ()
	{
		possessed = true;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!possessed && !combatant.corpse) {
			mobTarget = Player.player.transform;
			float xDiff = mobTarget.transform.position.x - transform.position.x;
			if (Mathf.Sign (xDiff) ==  -1) {
				transform.localScale = new Vector2(-8, 8);
				facing = "left";
			} else {
				transform.localScale = new Vector2(8,8);
				facing = "right";
			}
			if (Mathf.Abs (xDiff) > closeToDist) {
				rigidbody2D.AddForce (new Vector2 (Mathf.Sign (xDiff) * moveForce, 0));
			}
			foreach (EnemyMove other in allEnemies) {
				if (other != this && other.avoidanceClass == avoidanceClass && other.combatant.corpse == false) {
					rigidbody2D.AddForce ((other.transform.position - transform.position).normalized * -1 * repulsionForce / (other.transform.position - transform.position).sqrMagnitude);
				}
			}
		}
	 else if (!combatant.corpse && possessed) {
			gameObject.tag = "Player";
			allEnemies.Remove(this);
			if(Input.GetKey("left")) {
				rigidbody2D.AddForce(new Vector2(-25f, 0.0f));
				//rigidbody2D.velocity = new Vector2(-2.5f, 0.0f);
				if (facing != "left") {
					transform.localScale = new Vector2(-8, 8);
					facing = "left";
				}
			}
			else if(Input.GetKey("right")) {
				rigidbody2D.AddForce(new Vector2(25f, 0.0f));
				//rigidbody2D.velocity = new Vector2(2.5f, 0.0f);
				if (facing != "right") {
					transform.localScale = new Vector2(8,8);
					facing = "right";
				}
			} 
			if (jumpTimer > 0) {
				jumpTimer -= Time.deltaTime;
			} else {
				if(Input.GetKey("up")){
					rigidbody2D.velocity += new Vector2(0.0f,5.0f);
					//rigidbody2D.velocity = (new Vector2(0.0f,5.0f));
					jumpTimer = 1.0f;
				}
			}	
		}
	}
}