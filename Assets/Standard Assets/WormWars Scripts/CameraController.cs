using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	public float arenaSize;

	private Vector3 offset;
	private Vector3 dragOrigin;

	void Start ()
	{
		offset = transform.position;
	}

	void LateUpdate ()
	{
		if (player) {
			transform.position = player.transform.position + offset;
		}
		else {
			if ( Input.GetMouseButtonDown(0)){
				dragOrigin = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
				dragOrigin = camera.ScreenToWorldPoint(dragOrigin);
			}
			if ( Input.GetMouseButton(0)){
				
				Vector3 currentPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
				currentPos = camera.ScreenToWorldPoint(currentPos);
				Vector3 movePos  = dragOrigin - currentPos;
				transform.position = transform.position + movePos;
			}
		}
	}
}
