using UnityEngine;
using System.Collections;

public class ChamberScript : MonoBehaviour {

	public static bool Loaded;

	[RPC]
	public void LoadRound () {
		Loaded = !Loaded;
	}
	
	void Update () {
		if (Loaded == true) {
			renderer.enabled = true;
		}else{
			renderer.enabled = false;
		}
	}
}
