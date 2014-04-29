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
		timer = corpse.destroyTimeout;
		thisText = GetComponent<TextMesh>();
		//assigning thisText, just the text of the mesh does not alow fo it to be updated.
		thisText.text = timer.ToString ();
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		//round the text to the cieling int.
		thisText.text = (Mathf.CeilToInt(timer)).ToString();
		//animate this floaty
		if (transform.position.y < corpse.GetComponent<Transform>().position.y + 1.5f && corpse != null) {
			transform.Translate(Vector2.up * (Time.deltaTime / 2f));
		} else {
			transform.position = initialPos;
		}
	}
}
