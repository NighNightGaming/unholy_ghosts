using UnityEngine;
using System.Collections;

public class corpseTimer : MonoBehaviour {

	public Combatant corpse;
	private float timer;
	private TextMesh thisText;
	private float corpsepos;
	private Vector2 initialPos;
	// Use this for initialization
	void Start () {
		timer = corpse.removeTimer;
		thisText = GetComponent<TextMesh>();
		thisText.text = timer.ToString ();
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		thisText.text = (Mathf.CeilToInt(timer)).ToString();
		if (transform.position.y < corpse.GetComponent<Transform>().position.y + 1.5f) {
			transform.Translate(Vector2.up * (Time.deltaTime / 1.5f));
		} else {
			transform.position = initialPos;
		}
	}
}
