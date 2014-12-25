using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	public int foodLimit;
	public float foodSpawnRate;
	public float foodSpawnRange;

	private Transform food;
	private float foodSpawnTimer;

	// Use this for initialization
	void Start () {
		food = transform.FindChild ("Food");
		foodSpawnTimer = foodSpawnRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (food.childCount < foodLimit) {
			foodSpawnTimer -= foodSpawnRate * Time.deltaTime;
			if (foodSpawnTimer <= 0) {
				Vector3 pos = new Vector3(Random.Range(-foodSpawnRange, foodSpawnRange), Random.Range(-foodSpawnRange, foodSpawnRange), 0);
				GameObject newFood = (GameObject)GameObject.Instantiate(Resources.Load ("cheese"), pos, Quaternion.identity);
				newFood.transform.parent = food;
				foodSpawnTimer = foodSpawnRate;
			}
		}
	}
}
