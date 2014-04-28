using UnityEngine;
using System.Collections;

public class Attacking : MonoBehaviour
{
	
	public bool possessed = false;
	private LayerMask targetLayers;
	public float attackRange = 0.2f;
	public float attackStr = 0.5f;
	public float attackDmg = 20;
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
				print("Player attacking");
				Attack();
			}
		} else if (GetComponent<Combatant>().corpse == false) {
			EnemyMove em = GetComponent<EnemyMove>();
			if (em.mobTarget != null && this != null) {
				if(Mathf.Abs(em.mobTarget.transform.position.x - transform.position.x) <= attackRange) Attack();
			}
		}
	}
	
	void GetPossessed ()
	{
		possessed = true;
		targetLayers = 1 << LayerMask.NameToLayer ("Enemy");
		gameObject.layer = LayerMask.NameToLayer ("Player");
	}
	
	void Attack ()
	{
		if (attackTimer > 0)
			return;
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