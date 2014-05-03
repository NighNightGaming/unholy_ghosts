using UnityEngine;
using System.Collections;

public class corpseTimer : MonoBehaviour {

	public Combatant corpse;
	private float timer;
	private TextMesh thisText;
	private float corpsepos;
	private Vector2 thisPos;
	// Use this for initialization
	void Start () {
		timer = corpse.destroyTimeout;
		thisText = GetComponent<TextMesh>();
		//assigning thisText, just the text of the mesh does not alow fo it to be updated.
		thisText.text = timer.ToString ();
		thisPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		//round the text to the cieling int.
		thisText.text = (Mathf.CeilToInt(timer)).ToString();
		//animate this floaty
		if (transform.position.y < corpse.transform.position.y + 1.5f && corpse != null) {
			transform.Translate(Vector2.up * (Time.deltaTime / 2f));
		} else {
			transform.position = thisPos;
		}
		float xDiff = corpse.transform.position.x - transform.position.x;
		if (Mathf.Abs (xDiff) > 0) {
			thisPos.x += xDiff;
			transform.position = thisPos;
		} 

	}
}
