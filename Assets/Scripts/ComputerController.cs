using UnityEngine;
using System.Collections;

public class ComputerController : MonoBehaviour
{
	private float evasionCooldown;
	private float detectionCooldown;

	void Start()
	{
		evasionCooldown = 0;
		detectionCooldown = 0;
	}

	void Update ()
	{
		HeadLogic head = GetComponent<HeadLogic>();
		Vector3 pos = transform.position;
		if (!head.isDead ()) {

			Vector3 nextDir = head.getMoveDirection();
			detectionCooldown -= Time.deltaTime;
			evasionCooldown -= Time.deltaTime;

			// seek out food
			if (evasionCooldown <= 0 && detectionCooldown <= 0) {
				Collider2D[] detected = Physics2D.OverlapCircleAll(transform.position, 50);
				GameObject closest = null;
				float closestDist = 0;
				foreach (Collider2D obj in detected) {
					GameObject other = obj.gameObject;
					if (other.tag == "food") {
						float dist = Vector3.Distance (pos, other.transform.position);
						if (!closest || dist < closestDist) {
							closest = other;
							closestDist = dist;
						}
					}
				}
				if (closest) {
					nextDir = closest.transform.position - pos;
					detectionCooldown = 0.5f;
				}
				else {
					detectionCooldown = 0.3f;
				}
			}

			// set direction
			head.setMoveDirection (nextDir);
		}
	}
}
