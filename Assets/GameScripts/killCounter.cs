using UnityEngine;
using System.Collections;

public class killCounter : MonoBehaviour {
	public static GameObject player;
	public Player thePlayer;
	private string kills;
	// Use this for initialization
	void Start () {
		thePlayer = player.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		kills = thePlayer.kills.ToString ();
		guiText.text = kills;
	}
}
