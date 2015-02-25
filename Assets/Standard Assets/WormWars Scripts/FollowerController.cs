using UnityEngine;
using System.Collections;

public class FollowerController : MonoBehaviour
{
	public float maxDistance;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		FollowerLogic self = GetComponent<FollowerLogic>();
		if (!self.isDead()) {
			Vector3 myPos = transform.position;
			Vector3 targetPos = self.target.transform.position;
			Vector3 v = targetPos - myPos;
			if (v.magnitude > maxDistance) {
				v = v - v.normalized * maxDistance;
				transform.position += v;
			}
			else {
				transform.position += v.normalized * maxDistance * 1.0f * Time.deltaTime;
			}
		}
	}
}
