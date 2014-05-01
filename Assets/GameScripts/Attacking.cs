using UnityEngine;
using System.Collections;

public class Attacking : MonoBehaviour
{
	
	public bool possessed = false;
	private LayerMask targetLayers;
	public float attackRange = 0.2f;
	public float attackStr = 0.5f;
	public float attackDmg = 20;
	public float zombieDmgBoost = 1.5f;
	public float zombieStrBoost = 2f;
	private float attackTimer = 0;
	public float attackFreq = 0.5f;
	public bool hasGun = false;
	Vector2 dir;
	
	// Use this for initialization
	void Start () {
		//setting the target Layer
		targetLayers = 1 << LayerMask.NameToLayer ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer > 0) {
						attackTimer -= Time.deltaTime;
		}
		//this governs the ability for the attacks to go bidirectionally
		if (rigidbody2D.velocity.x < -1)
			//left
			dir = Vector2.right * -1;
		else if (rigidbody2D.velocity.x > 1)
			//right
			dir = Vector2.right;
		
		if (possessed) {
			if(Input.GetButtonDown("Fire2") || Input.GetKey("space")) {
				#if UNITY_EDITOR
				Debug.Log("Player attacking");
				#endif
				Attack();
			}
		} else if (GetComponent<Combatant>().corpse == false) {
			EnemyMove em = GetComponent<EnemyMove>();
			if (em.mobTarget != null && this != null) {
				if(Mathf.Abs(em.mobTarget.transform.position.x - transform.position.x) 
				   <= attackRange && em.mobTarget != Player.player.GetComponent<Transform>()) {
					Attack();
				}
			}
		}
	}
	
	void GetPossessed (bool possession)
	{
		possessed = possession;
		targetLayers = 1 << LayerMask.NameToLayer ("Enemy");
		gameObject.layer = LayerMask.NameToLayer ("Player");
		//adding zombie bonus
		if (possession) {
			attackDmg *= zombieDmgBoost;
			attackStr *= zombieStrBoost;
		} else { //taking away zombie bonus
			attackDmg /= zombieDmgBoost;
			attackStr /= zombieStrBoost;
		}
	}
	
	void Attack ()
	{
		if (attackTimer > 0) {
			return;
		}
		gameObject.GetComponent<Animator> ().SetTrigger("attackNow");
		attackTimer = attackFreq;	
		RaycastHit2D hit = Physics2D.Raycast (transform.position, dir, attackRange, targetLayers);
		audio.Play ();
		if (hit.rigidbody != null) {
			hit.rigidbody.velocity += dir * attackStr;
			Combatant comb = hit.collider.GetComponent<Combatant>();
			if(comb != null) comb.health -= attackDmg;
		}
	}
}