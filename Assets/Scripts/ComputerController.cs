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

	bool IsDangerous(RaycastHit2D[] collisions)
	{
		Vector2 pos = transform.position;
		foreach (RaycastHit2D hit in collisions) {
			if (hit.collider.tag == "head")
				continue;
			float dist = Vector2.Distance (pos, hit.point);
			if (dist > 3.0f)
				break;
			if (hit.collider.gameObject.tag != "food") {
				return true;
			}
		}
		return false;
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
				Collider2D[] detected = Physics2D.OverlapCircleAll(transform.position, 10);
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

			// evasive maneuvers
			if (Vector3.Angle (curDir, nextDir) > 90) {
				nextDir = Vector3.RotateTowards (curDir, nextDir, 2*Mathf.PI*Time.deltaTime, 0.0f);
			}
			Vector3 normalLine = 0.3f * Vector3.Cross (nextDir, Vector3.up).normalized;
			RaycastHit2D[] center = Physics2D.RaycastAll(pos, nextDir),
						   side1 = Physics2D.RaycastAll (pos + normalLine - 0.3f * nextDir, nextDir),
						   side2 = Physics2D.RaycastAll (pos - normalLine - 0.3f * nextDir, nextDir);
			if (IsDangerous (center) || IsDangerous (side1) || IsDangerous (side2)) {
				Vector3 curNormal = Quaternion.AngleAxis(90, Vector3.forward) * curDir;
				nextDir = Vector3.RotateTowards(curDir, curNormal, 4*Mathf.PI * Time.deltaTime, 0.0f);
				evasionCooldown = 0.1f;
			}

			// set direction
			head.SetMoveDirection (nextDir);
		}
	}
}
