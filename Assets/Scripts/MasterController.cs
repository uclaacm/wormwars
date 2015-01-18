using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterController : MonoBehaviour
{
	public int maxPlayers = 8;

	private int state = 0;
	private int numPlayers = 0;
	private List<GameObject> players = new List<GameObject>();

	void OnGUI()
	{
		if (state == 0)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Single Player"))
				SinglePlayer();
			if (GUI.Button(new Rect(100, 250, 250, 100), "Observe"))
				ObserveMode();
		}
	}

	void Update()
	{
		if (state != 0) {
			if (Input.GetKeyUp ("space") && players.Count < maxPlayers) {
				SpawnAI ();
			}
		}
	}

	private void SinglePlayer()
	{
		SpawnPlayer();
		state = 1;
	}

	private void ObserveMode()
	{
		state = 2;
	}
	
	private void SpawnPlayer()
	{
		GameObject player = (GameObject)GameObject.Instantiate(Resources.Load ("Player"), Vector3.zero, Quaternion.identity);
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		GameObject head = player.transform.FindChild ("head").gameObject;
		camera.GetComponent<CameraController> ().player = head;
		AddPlayer (player);
	}

	private void SpawnAI()
	{
		GameObject comp = (GameObject)GameObject.Instantiate(Resources.Load ("Computer"), Vector3.zero, Quaternion.identity);
		AddPlayer (comp);
	}

	private void AddPlayer(GameObject p)
	{
		p = p.transform.FindChild ("head").gameObject;
		int idx = -1;
		for (int i = 0; i < players.Count; i++) {
			if (!players[i]) {
				idx = i;
				break;
			}
		}
		if (idx == -1) {
			idx = players.Count;
			players.Add (null);
		}
		players[idx] = p;
		HeadLogic hd = p.GetComponent<HeadLogic> ();
		hd.SetPlayerNum (idx);
		hd.ChangeColor ();
		numPlayers++;
	}
}
