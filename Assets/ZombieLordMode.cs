using UnityEngine;
using System.Collections;

public class ZombieLordMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		guiText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Combatant.zombieCount > 1) {
						guiText.enabled = true;
				} else {
			guiText.enabled = false;
				}
	}
}
