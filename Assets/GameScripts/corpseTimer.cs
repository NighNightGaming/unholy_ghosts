using UnityEngine;
using System.Collections;

public class corpseTimer : MonoBehaviour {

	public Combatant corpse;
	private float timer;
	// Use this for initialization
	void Start () {
		timer = corpse.removeTimer;
		GetComponent<TextMesh> ().text = timer.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		GetComponent<TextMesh> ().text = (Mathf.CeilToInt(timer)).ToString();
	}
}
