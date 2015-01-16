using UnityEngine;
using System.Collections;

public class MasterController : MonoBehaviour {

	private int state = 0;

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
		if (Input.GetKeyUp ("space")) {
			SpawnAI ();
		}
	}

	private void SinglePlayer()
	{
		state = 1;
		SpawnPlayer();
	}

	private void ObserveMode()
	{
		state = 2;
	}
	
	private void SpawnPlayer()
	{
		GameObject player = (GameObject)GameObject.Instantiate(Resources.Load ("Player"), Vector3.zero, Quaternion.identity);
		GameObject head = player.transform.FindChild ("head").gameObject;
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		camera.GetComponent<CameraController> ().player = head;
	}

	private void SpawnAI()
	{
		GameObject.Instantiate(Resources.Load ("Computer"), Vector3.zero, Quaternion.identity);
	}
}
