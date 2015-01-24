using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onStartClick () {
		Application.LoadLevel ("gamestart"); 
	}

	void onQuitClick () {
		Application.Quit ();
	}
}
