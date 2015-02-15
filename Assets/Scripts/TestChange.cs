using UnityEngine;
using System.Collections;

public class TestChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Submit")){
			DontDestroyOnLoad (gameObject);
			Application.LoadLevel (1);
		}
	}
}
