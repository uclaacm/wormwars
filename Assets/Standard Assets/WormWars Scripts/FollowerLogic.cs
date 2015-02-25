using UnityEngine;
using System.Collections;

public class FollowerLogic : MonoBehaviour
{
	public GameObject target;
	public GameObject follower;

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
			FollowerLogic flogic = follower.GetComponent<FollowerLogic>();
			FollowerController fctrl = follower.GetComponent<FollowerController>();
			flogic.target = gameObject;
			flogic.SetPlayerNum(playerNum);
			flogic.ChangeColor();
			follower.transform.position = transform.position - direction * fctrl.maxDistance;
			follower.transform.parent = transform.parent;
		}
		else {
			follower.GetComponent<FollowerLogic>().Grow (direction);
		}
	}

	public void SetPlayerNum(int num)
	{
		playerNum = num;
		if (follower)
			follower.GetComponent<FollowerLogic>().SetPlayerNum (num);
	}
	
	public void ChangeColor()
	{
		renderer.material.color = PlayerColors.GetFollowerColor (playerNum);
		if (follower)
			follower.GetComponent<FollowerLogic>().ChangeColor ();
	}

	public void Die(float wait, float delay)
	{
		dead = true;
		collider2D.enabled = false;
		deathWaitTimer = wait + delay;
		if (follower)
			follower.GetComponent<FollowerLogic> ().Die (deathWaitTimer, delay);
	}

	public bool isDead()
	{
		return dead;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (dead) {
			deathWaitTimer -= Time.deltaTime;
			if (deathWaitTimer <= 0) {
				Destroy (gameObject);
			}
		}
	}
}
