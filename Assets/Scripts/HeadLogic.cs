using UnityEngine;
using System.Collections;

public class HeadLogic : MonoBehaviour
{
	public GameObject follower;
	
	public Vector3 moveDirection;
	private float moveSpeed;
	private bool dead;
	private float deathWaitTimer;
	
	void Start ()
	{
		dead = false;
		moveSpeed = 4;
		moveDirection.x = 1.0f;
	}

	public bool isDead()
	{
		return dead;
	}

	public void setMoveDirection(Vector3 nextMoveDirection)
	{
		nextMoveDirection.z = 0;
		nextMoveDirection.Normalize ();
		moveDirection = nextMoveDirection;
	}

	public Vector3 getMoveDirection()
	{
		return moveDirection;
	}
	
	void Grow(Vector3 direction)
	{
		if (!follower) {
			follower = (GameObject)Instantiate(Resources.Load ("follower"));
			FollowerController fcnt = follower.GetComponent<FollowerController>();
			fcnt.target = gameObject;
			follower.transform.position = transform.position - moveDirection * fcnt.maxDistance;
			follower.transform.parent = transform.parent;
		}
		else {
			follower.GetComponent<FollowerController>().Grow (direction);
		}
	}
	
	void Respawn(Vector3 pos)
	{
		dead = false;
		collider2D.enabled = true;
		renderer.enabled = true;
		follower = null;
		transform.position = pos;
	}
	
	void Die()
	{
		dead = true;
		collider2D.enabled = false;
		renderer.enabled = false;
		deathWaitTimer = 2.0f;
		if (follower)
			deathWaitTimer += follower.GetComponent<FollowerController> ().Die (0);
	}
	
	void Move(float dur)
	{
		Vector3 currentPosition = transform.position;
		Vector3 target = moveDirection * moveSpeed + currentPosition;
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.position = Vector3.Lerp (currentPosition, target, dur);
		transform.rotation = Quaternion.Euler (0, 0, targetAngle);
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			Move (Time.deltaTime);
		}
		else {
			deathWaitTimer -= Time.deltaTime;
			if (deathWaitTimer <= 0) {
				Respawn(new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0));
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "food") {
			Destroy (other.gameObject);
			Grow (moveDirection);
		}
		else if (other.gameObject.tag == "wall") {
			Die ();
		}
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "follower") {
			Die();
		}
	}
}
