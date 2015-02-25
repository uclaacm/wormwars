using UnityEngine;
using System.Collections;

public class HeadLogic : MonoBehaviour
{
	public float moveSpeed;
	public GameObject follower;

	private int playerNum;
	private int length;
	private Vector3 moveDirection;
	private bool dead;
	private float deathWaitTimer;
	
	void Start ()
	{
		dead = false;
		length = 1;
		moveDirection.x = 1.0f;
	}

	public void SetPlayerNum(int num)
	{
		playerNum = num;
		if (follower)
			follower.GetComponent<FollowerLogic>().SetPlayerNum (num);
	}

	public void ChangeColor()
	{
		renderer.material.color = PlayerColors.GetHeadColor (playerNum);
		if (follower)
			follower.GetComponent<FollowerLogic>().ChangeColor ();
	}

	public bool IsDead()
	{
		return dead;
	}

	public void SetMoveDirection(Vector3 nextMoveDirection)
	{
		nextMoveDirection.z = 0;
		nextMoveDirection.Normalize ();
		moveDirection = nextMoveDirection;
	}

	public Vector3 GetMoveDirection()
	{
		return moveDirection;
	}
	
	void Grow(Vector3 direction)
	{
		length += 1;
		if (!follower) {
			follower = (GameObject)Instantiate(Resources.Load ("follower"));
			FollowerLogic flogic = follower.GetComponent<FollowerLogic>();
			FollowerController fctrl = follower.GetComponent<FollowerController>();
			flogic.target = gameObject;
			flogic.SetPlayerNum(playerNum);
			flogic.ChangeColor();
			follower.transform.position = transform.position - moveDirection * fctrl.maxDistance;
			follower.transform.parent = transform.parent;
		}
		else {
			follower.GetComponent<FollowerLogic>().Grow (direction);
		}
	}
	
	void Respawn(Vector3 pos)
	{
		dead = false;
		length = 1;
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
		float popTime = 0.5f + (length / 20f);
		float delay = popTime / length;
		if (follower)
			follower.GetComponent<FollowerLogic> ().Die (0, delay);
		deathWaitTimer = 2.0f + popTime;
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
