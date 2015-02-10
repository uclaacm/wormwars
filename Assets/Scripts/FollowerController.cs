using UnityEngine;
using System.Collections;

public class FollowerController : MonoBehaviour
{
	public GameObject target;
	public GameObject follower;
	public float maxDistance;

	private int playerNum;
	private bool dead;
	private float deathWaitTimer;

	// Use this for initialization
	void Start () {
		dead = false;
	}

	public void Grow(Vector3 direction)
	{
		if (!follower) {
			follower = (GameObject)Instantiate(Resources.Load ("follower"));
			FollowerController fcnt = follower.GetComponent<FollowerController>();
			fcnt.target = gameObject;
			fcnt.SetPlayerNum(playerNum);
			fcnt.ChangeColor();
			follower.transform.position = transform.position - direction * maxDistance;
			follower.transform.parent = transform.parent;
		}
		else {
			follower.GetComponent<FollowerController>().Grow (direction);
		}
	}

	public void SetPlayerNum(int num)
	{
		playerNum = num;
		if (follower)
			follower.GetComponent<FollowerController>().SetPlayerNum (num);
	}
	
	public void ChangeColor()
	{
		renderer.material.color = PlayerColors.GetFollowerColor (playerNum);
		if (follower)
			follower.GetComponent<FollowerController>().ChangeColor ();
	}

	public void Die(float wait, float delay)
	{
		dead = true;
		collider2D.enabled = false;
		deathWaitTimer = wait + delay;
		if (follower)
			follower.GetComponent<FollowerController> ().Die (deathWaitTimer, delay);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!dead) {
			Vector3 myPos = transform.position;
			Vector3 targetPos = target.transform.position;
			Vector3 v = targetPos - myPos;
			if (v.magnitude > maxDistance) {
				v = v - v.normalized * maxDistance;
				transform.position += v;
			}
			else {
				transform.position += v.normalized * maxDistance * 1.0f * Time.deltaTime;
			}
		}
		else {
			deathWaitTimer -= Time.deltaTime;
			if (deathWaitTimer <= 0) {
				Destroy (gameObject);
			}
		}
	}
}
