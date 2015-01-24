using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameController {

	private static Transform value;
	private static Dictionary<string, Transform> objects = new Dictionary<string, Transform>();

	public static void Register(string name, Transform obj)
	{
		objects.Add (name, obj);
	}

	public static Transform Get(string name)
	{
		if (objects.TryGetValue (name, out value))
						return value;

		return null;
	}

}
