using UnityEngine;
using System.Collections;

public class RoundSpawn : MonoBehaviour {

	public GameObject SpawnPos;
	public GameObject Round;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	[RPC]
	public void SpawnRound () {
		Round = PhotonNetwork.Instantiate ("Round", SpawnPos.transform.position, SpawnPos.transform.rotation, 0);
	}
}
