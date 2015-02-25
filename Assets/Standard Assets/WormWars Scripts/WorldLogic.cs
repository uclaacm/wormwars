using UnityEngine;
using System.Collections;

public class WorldLogic : MonoBehaviour {

	public int foodLimitBase;
	public int foodLimitChange;
	public float foodSpawnRateBase;
	public float foodSpawnRateChange;
	public float foodSpawnRange;

	private Transform food;
	private float foodSpawnRate;
	private float foodLimit;
	private float foodSpawnTimer;

	// Use this for initialization
	void Start () {
		food = transform.FindChild ("Food");
		foodSpawnTimer = 0;
		AdjustRate (0);
	}

	public void AdjustRate(int numPlayers)
	{
		foodSpawnRate = foodSpawnRateChange * numPlayers + foodSpawnRateBase;
		foodLimit = foodLimitChange * numPlayers + foodLimitBase;
	}
	
	// Update is called once per frame
	void Update () {
		foodSpawnTimer -= foodSpawnRate * Time.deltaTime;
		if (food.childCount < foodLimit) {
			if (foodSpawnTimer <= 0) {
				Vector3 pos = new Vector3(Random.Range(-foodSpawnRange, foodSpawnRange), Random.Range(-foodSpawnRange, foodSpawnRange), 0);
				GameObject newFood = (GameObject)GameObject.Instantiate(Resources.Load ("cheese"), pos, Quaternion.identity);
				newFood.transform.parent = food;
				foodSpawnTimer = 1;
			}
		}
	}
}
