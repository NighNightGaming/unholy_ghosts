using UnityEngine;
using System.Collections;

public class killCounter : MonoBehaviour {
	private int kills = 0;
	public static int highKills = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		kills = Player.player.kills;
		guiText.text = kills.ToString ();
		if (kills > highKills) {
						highKills = kills;
				}

	}
}
