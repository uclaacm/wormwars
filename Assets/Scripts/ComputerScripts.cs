using UnityEngine;
using System.Collections;

public static class ComputerScripts
{
	private static string[] scripts = new string[] {
		"ComputerController", // p1 is unused if in single player mode
		"ComputerController",
		"ComputerController",
		"ComputerController",
		"ComputerController",
		"ComputerController",
		"ComputerController",
		"ComputerController"
	};
	
	public static Component AddController(GameObject obj, int p)
	{
		return obj.AddComponent (scripts [p]);
	}

	public static Component GetController(GameObject obj, int p)
	{
		return obj.GetComponent (scripts[p]);
	}

}
