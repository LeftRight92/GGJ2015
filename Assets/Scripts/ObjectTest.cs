using UnityEngine;
using System.Collections;

public class ObjectTest : MonoBehaviour, InteractableObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Execute on push command
	public void onPush() {
		Debug.Log (transform + "Being Pushed");
		transform.Translate (new Vector3 (0.5f, 0, 0));
	}

	// Execute on lift command
	public void onLift() {
		Debug.Log (transform + "Being Lift");
		transform.Translate (new Vector3 (0, 0.5f, 0));
	}
}
