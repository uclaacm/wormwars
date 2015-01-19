using UnityEngine;
using System.Collections;

public class ComputerController : MonoBehaviour
{
	private float evasionCooldown;
	private float detectionCooldown;
	private float dangerThreshold;

	void Start()
	{
		evasionCooldown = 0;
		detectionCooldown = 0;
		dangerThreshold = 3f;
	}

	// Evaluates the danger value (inverse distance to collision)
	float Danger(RaycastHit2D[] collisions)
	{
		Vector2 pos = transform.position;
		foreach (RaycastHit2D hit in collisions) {
			if (hit.collider.tag == "head")
				continue;
			float dist = Vector2.Distance (pos, hit.point);
			if (hit.collider.gameObject.tag != "food") {
				return dangerThreshold / dist;
			}
		}
		return 0f;
	}

	void Update ()
	{
		HeadLogic head = GetComponent<HeadLogic>();
		Vector3 pos = transform.position;
		if (!head.IsDead ()) {

			Vector3 curDir = head.GetMoveDirection();
			Vector3 nextDir = curDir;
			detectionCooldown -= Time.deltaTime;
			evasionCooldown -= Time.deltaTime;

			// seek out food
			if (evasionCooldown <= 0 && detectionCooldown <= 0) {
				Collider2D[] detected = Physics2D.OverlapCircleAll(transform.position, 8);
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
					detectionCooldown = 0.2f;
				}
				else {
					detectionCooldown = 0.1f;
				}
			}

			// ----- evasive maneuvers -----
			// make sure the turn is not too sharp
			float turnRate = 20 * Mathf.PI / (head.moveSpeed / 5f + 1);
			float turn = turnRate * Time.deltaTime;
			if (Vector3.Angle (curDir, nextDir) > 2.5 * turn) {
				nextDir = Vector3.RotateTowards (curDir, nextDir, 2.5f * turn, 0.0f);
			}
			// avoid incoming collisions
			RaycastHit2D[] forward = Physics2D.BoxCastAll (pos + 0.3f * nextDir, renderer.bounds.size, Vector2.Angle (Vector2.zero, nextDir), nextDir);
			if (Danger (forward) > 1) {
				int dir = Random.Range(0, 1) == 0 ? 90 : -90;
				Vector3 turn1 = Util.Rotated (curDir, dir),
						turn2 = Util.Rotated (curDir, -dir);
				RaycastHit2D[] d1 = Physics2D.RaycastAll(pos, turn1),
 							   d2 = Physics2D.RaycastAll(pos, turn2);
				if (Danger (d2) < Danger (d1))
					turn1 = turn2;
				nextDir = Vector3.RotateTowards(curDir, turn1, turn, 0.0f);
				evasionCooldown = 0.1f;
			}

			// set direction
			head.SetMoveDirection (nextDir);
		}
	}
}
