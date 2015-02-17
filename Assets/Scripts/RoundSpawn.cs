using UnityEngine;
using System.Collections;

public class RoundSpawn : MonoBehaviour {

	public GameObject SpawnPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	[RPC]
	public void SpawnRound () {
		PhotonNetwork.Instantiate ("Round", SpawnPos.transform.position, SpawnPos.transform.rotation, 0);
	}
}
