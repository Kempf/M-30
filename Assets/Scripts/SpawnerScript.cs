using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

	public GameObject Target1;
	public int DistMin = 100;
	public int DistMax = 9000;

	// Use this for initialization
	void Spawn () {

		Quaternion RandAng = Quaternion.Euler (0, Random.Range (-24,24) , 0);

		RandAng = transform.rotation * RandAng;

		Vector3 SpawnPos = transform.position + RandAng * Vector3.forward * Random.Range (DistMin, DistMax);

		Instantiate (Target1, SpawnPos, transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {
	
		if (GameObject.FindGameObjectWithTag ("Target")) {
				} else if (GameObject.FindGameObjectWithTag ("Player")) {
						Spawn ();
				}
		}
}
