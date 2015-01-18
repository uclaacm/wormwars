using UnityEngine;
using System.Collections;

public static class PlayerColors
{
	private static Color[] headColors = new Color[]{
		new Color(0.898f, 0.198f, 0.198f),
		new Color(0.175f, 0.502f, 0.909f),
		new Color(0.180f, 0.820f, 0.733f),
		new Color(0.410f, 0.199f, 0.830f),
		new Color(0.881f, 0.854f, 0.257f),
		new Color(0.929f, 0.372f, 0.000f),
		new Color(0.227f, 0.749f, 0.157f),
		new Color(0.490f, 0.286f, 0.117f)
	};
	private static Color[] followerColors = new Color[]{
		new Color(0.839f, 0.453f, 0.453f),
		new Color(0.474f, 0.647f, 0.819f),
		new Color(0.529f, 0.961f, 0.902f),
		new Color(0.600f, 0.475f, 0.851f),
		new Color(0.961f, 0.929f, 0.478f),
		new Color(0.929f, 0.573f, 0.333f),
		new Color(0.412f, 0.859f, 0.353f),
		new Color(0.678f, 0.470f, 0.294f)
	};


	public static Color GetHeadColor(int p)
	{
		return headColors [p];
	}

	public static Color GetFollowerColor(int p)
	{
		return followerColors [p];
	}
}
