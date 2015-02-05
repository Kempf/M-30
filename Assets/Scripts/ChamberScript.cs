using UnityEngine;
using System.Collections;

public class ChamberScript : MonoBehaviour {

	public bool Loaded;

	// Use this for initialization
	void Awake () {
		Loaded = false;
	}
	
	// Update is called once per frame
	public void Load () {
		Loaded = true;
	}

	void Update () {
		if (Loaded == true)
						renderer.enabled = true;
				else
						renderer.enabled = false;
		}
}
