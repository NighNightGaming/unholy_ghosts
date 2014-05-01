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
	public bool facingRight = true;
	private Combatant combatant;
	public float jumpTimer = 1.0f;
	private Vector3 initFace;
	private Animator anim;	
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
		initFace = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
		anim = GetComponent<Animator> ();

	}

	/// <summary>
	/// sets the possessed flag
	/// </summary>
	void GetPossessed (bool possession)
	{
		possessed = possession;
	}
	/// <summary>
	/// Flip the sprite on the x, allowing for facing left/right.
	/// </summary>
	void flip() {
		facingRight = !facingRight;
		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	/// <summary>
	/// Gets the target of aggression.
	/// </summary>
	/// <returns>The target of aggression.</returns>
	public Transform getTargetOfAggression()
	{
		//if the player is possessing, 
		if (Player.possessedEnemy != null)
			//target is what ever is being possessed
			return Player.possessedEnemy.transform;
		else
			return Player.player.transform;
	}
	 
	// Update is called once per frame
	void FixedUpdate ()
	{

		if (!possessed && !combatant.corpse) {
			//if not possessed and not a corpse, move towards target
				mobTarget = getTargetOfAggression ();
				float xDiff = mobTarget.transform.position.x - transform.position.x;
				if (Mathf.Sign (xDiff) > -1) {
						transform.localScale = new Vector2 (0.2f, 0.2f);

				} else {
						transform.localScale = new Vector2 (-0.2f, 0.2f);
				}
				if (Mathf.Abs (xDiff) > closeToDist) {
				rigidbody2D.AddForce (new Vector2 (Mathf.Sign (xDiff) * moveForce, 0));
					anim.SetBool("walking", true);
				} else {
					anim.SetBool("walking", false);
				}
				// Causin' problems
				foreach (EnemyMove other in allEnemies) {
					if (other != null && other != this && other.avoidanceClass == avoidanceClass && other.combatant.corpse == false) {
						rigidbody2D.AddForce ((other.transform.position - transform.position).normalized * -1 * repulsionForce / (other.transform.position - transform.position).sqrMagnitude);
					}
			}

		} else if (!combatant.corpse && possessed) {
			//if not corpse, but possessed, give control to player.
				float h = Input.GetAxis ("Horizontal");
				gameObject.tag = "Player";
				allEnemies.Remove (this);
				if (h < 0) {
				rigidbody2D.AddForce (new Vector2 (-20f - 5 * Mathf.Sin (Time.time * 6), 0.0f));
						if (facingRight) {
								flip ();
						}
				} else if (h > 0) {
				rigidbody2D.AddForce (new Vector2 (20f + 5 * Mathf.Sin (Time.time * 6), 0.0f));
						if (!facingRight) {
								flip ();
						}
				} 
				if (jumpTimer > 0) {
						jumpTimer -= Time.deltaTime;
				} else {
						if (Input.GetKey ("up")) {
								//+= allows for momentum
								rigidbody2D.velocity += (new Vector2 (0.0f, 5f));
								jumpTimer = 1.0f;
						}
				}
			}
	}
}