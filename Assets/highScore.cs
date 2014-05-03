using UnityEngine;
using System.Collections;

public class highScore : MonoBehaviour {
	public static bool retry = false;
	// Use this for initialization
	void Start () {
		if (!retry) {
			guiText.enabled = false;
		} else {
			guiText.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
