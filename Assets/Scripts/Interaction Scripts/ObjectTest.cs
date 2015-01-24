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
	public bool onPush(bool right) {
		Debug.Log (transform + "Being Pushed" + right);
		transform.Translate (new Vector3 (0.5f, 0, 0));
		return true;
	}

	// Execute on lift command
	public bool onLift() {
		Debug.Log (transform + "Being Lift");
		transform.Translate (new Vector3 (0, 0.5f, 0));
		return true;
	}
}
