using UnityEngine;
using System.Collections;

public class Mouselock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}
}
