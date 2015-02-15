using UnityEngine;
using System.Collections;

public class PersistantDetails : MonoBehaviour {

	public static bool Multiplayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MPEnable () {
		Multiplayer = true;
		print ("MP Enabled");
	}

	public void SPEnable () {
		Multiplayer = false;
		print ("SP Enabled");
	}

	public void SceneChanger (int SceneToChangeTo) {
		Application.LoadLevel (SceneToChangeTo);
	}
}
