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
		}
	}

	private void SinglePlayer()
	{
		state = 1;
		InitializeWorld();
		SpawnPlayer();
	}

	private void InitializeWorld()
	{
		GameObject.Instantiate (Resources.Load ("World"), Vector3.zero, Quaternion.identity);
	}
	
	private void SpawnPlayer()
	{
		GameObject player = (GameObject)GameObject.Instantiate(Resources.Load ("Player"), new Vector3(0f, 5f, 0f), Quaternion.identity);
		GameObject head = player.transform.FindChild ("head").gameObject;
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		camera.GetComponent<CameraController> ().player = head;
	}
}
