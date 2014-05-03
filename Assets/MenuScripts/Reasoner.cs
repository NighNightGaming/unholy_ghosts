using UnityEngine;
using System.Collections;

public class Reasoner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Player.handGrab == true) {
			guiText.text = "You were grabbed by\n\t DEMON HANDS";
				} else {
			guiText.text = "No more corpses onscreen";
				}
		highScore.retry = true;
	}
}
