using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	Vector3 GetDirection()
	{
		Vector3 currentPosition = transform.position;
		Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 nextMoveDirection = moveToward - currentPosition;
		return nextMoveDirection;
	}

	void Update ()
	{
		HeadLogic head = GetComponent<HeadLogic>();
		if (!head.IsDead()) {
			head.SetMoveDirection (GetDirection ());
		}
	}
}
