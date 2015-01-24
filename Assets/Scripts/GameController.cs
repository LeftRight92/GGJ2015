using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameController {

	private static Dictionary<string, GameObject> objects;


	public static void Register(string name, GameObject obj)
	{
		objects.Add (name, obj);
	}

	public static GameObject Get(string name)
	{
		return objects.TryGetValue(name);	
	}

}
