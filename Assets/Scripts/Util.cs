﻿using UnityEngine;
using System.Collections;

public static class Util
{
	public static Vector2 Rotated(Vector2 v, float angle)
	{
		return Quaternion.AngleAxis(angle, Vector3.forward) * v;
	}

}
