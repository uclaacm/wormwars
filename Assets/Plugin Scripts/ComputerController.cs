using UnityEngine;
using System.Collections;

public class ComputerController : MonoBehaviour
{
	private float dangerThreshold = 3f;
	private float detectionRadius = 8f;

	void Start()
	{
	}

	// Evaluates the danger value (inverse distance to fatal collision)
	float Danger(RaycastHit2D[] collisions)
	{
		Vector2 pos = transform.position;
		foreach (RaycastHit2D hit in collisions) {
			if (hit.collider.tag == "head")
				continue;
			float dist = Vector2.Distance (pos, hit.point);
			if (hit.collider.tag != "food") {
				return dangerThreshold / dist;
			}
		}
		return 0f;
	}

	void Update ()
	{
		HeadLogic head = GetComponent<HeadLogic>();
		CircleCollider2D collider = GetComponent<CircleCollider2D>();
		Vector3 pos = transform.position;

		if (!head.IsDead ()) {

			Vector3 curDir = head.GetMoveDirection();
			Vector3 nextDir = curDir;

			// ----- objective maneuvers -----
			// seek out food
			Collider2D[] detected = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
			GameObject closest = null;
			float closestDist = float.PositiveInfinity;
			foreach (Collider2D obj in detected) {
				GameObject other = obj.gameObject;
				if (other.tag == "food") {
					float dist = Vector3.Distance (pos, other.transform.position);
					if (dist < closestDist) {
						closest = other;
						closestDist = dist;
					}
				}
			}
			if (closest) {
				nextDir = closest.transform.position - pos;
			}

			// ----- survival maneuvers -----
			// make sure the turn is not too sharp (or worm will hit its own body)
			float turnRate = 4 * Mathf.PI / (head.moveSpeed / 5f + 1);
			float turnAngle = turnRate * Time.deltaTime;
			if (Vector3.Angle (curDir, nextDir) > turnAngle) {
				nextDir = Vector3.RotateTowards (curDir, nextDir, turnAngle, 0.0f);
			}
			// avoid incoming collisions
			// cast a circle forward to detect incoming collisions
			RaycastHit2D[] forward = Physics2D.CircleCastAll (pos + 0.2f * nextDir, collider.radius, nextDir);
			if (Danger (forward) > 1) {
				// if danger detected, turn either left or right depending on which one is less dangerous
				float dir = 75f;
				Vector3 turn1 = Utils.Rotated (curDir, dir),
						turn2 = Utils.Rotated (curDir, -dir);
				RaycastHit2D[] d1 = Physics2D.RaycastAll(pos, turn1),
							   d2 = Physics2D.RaycastAll(pos, turn2);
				Vector3 decision = Danger (d1) < Danger (d2) ? turn1 : turn2;
				nextDir = Vector3.RotateTowards(curDir, decision, Danger(forward) * turnAngle, 0.0f);
			}

			// set direction
			head.SetMoveDirection (nextDir);
		}
	}
}
