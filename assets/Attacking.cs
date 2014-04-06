using UnityEngine;
using System.Collections;

public class Attacking : MonoBehaviour
{
	
	public bool possesed = false;
	private LayerMask targetLayers;
	public float attackRange = 0.2f;
	public float attackStr = 0.5f;
	public float attackDmg = 20;
	
	private float attackTimer = 0;
	
	public float attackFreq = 0.5f;
	
	Vector2 dir;
	
	// Use this for initialization
	void Start ()
	{
		targetLayers = 1 << LayerMask.NameToLayer ("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (attackTimer > 0)
			attackTimer -= Time.deltaTime;
		
		
		if (rigidbody2D.velocity.x < 0)
			dir = Vector2.right * -1;
		if (rigidbody2D.velocity.x > 0)
			dir = Vector2.right;
		
		if (possesed) {
			if(Input.GetButtonDown("Fire1")) {
				Attack();
			}
		} else {
			EnemyMove em = GetComponent<EnemyMove>();
			if(Mathf.Abs(em.mobTarget.transform.position.x - transform.position.x) <= attackRange) Attack();
		}
	}
	
	void GetPossessed ()
	{
		possesed = true;
		targetLayers = 1 << LayerMask.NameToLayer ("Enemies");
		gameObject.layer = LayerMask.NameToLayer ("Player");
	}
	
	void Attack ()
	{
		if (attackTimer > 0)
			return;
		
		attackTimer = attackFreq;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, dir, attackRange, targetLayers);
		if (hit != null) {
			if(hit.rigidbody!= null) hit.rigidbody.velocity += dir * attackStr;
			Combatant comb = hit.collider.GetComponent<Combatant>();
			if(comb != null) comb.health -= attackDmg;
		}
	}
}