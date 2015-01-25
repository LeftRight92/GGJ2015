using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameController {

	private static Dictionary<string, Transform> objects = new Dictionary<string, Transform>();
	private static int mode = 0;

	public static void clearState(){
		objects = new Dictionary<string, Transform>();
	}

	public static void Register(string name, Transform obj)
	{
		objects.Add (name, obj);
	}

	public static Transform Get(string name)
	{
		Transform value;
		if (objects.TryGetValue (name, out value))
						return value;

		return null;
	}
	
	public static void Restart(int mode){
		objects = new Dictionary<string, Transform>();
		Application.LoadLevel("gamestart");
		switch(mode){
			case 0:
				//Normal Restart
				break;
			case 1:
				//Cat Mode
				break;
		}
	}
	
	

}
