﻿using UnityEngine;
using System.Collections;

public class highKillCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = killCounter.highKills.ToString ();
	}
}
