using UnityEngine;
using System.Collections;

public class killCounter : MonoBehaviour {
	private string kills;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		kills = Player.player.GetComponent<Player> ().kills.ToString ();
		guiText.text = kills;
	}
}
