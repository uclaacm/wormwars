using UnityEngine;
using System.Collections;

public class FoodController : MonoBehaviour {
	
	void Update ()
	{
		if (Input.GetKeyUp ("space")) {
			gameObject.GetComponent<Collider2D> ().enabled = true;
			gameObject.GetComponent<Renderer> ().enabled = true;
		}
	}
}
