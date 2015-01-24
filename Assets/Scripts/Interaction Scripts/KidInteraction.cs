using UnityEngine;
using System.Collections;

public class KidInteraction : MonoBehaviour, InteractableObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool onPush(bool facing){
		return true;
	}
	
	public bool onLift(){
		transform.Translate(new Vector3(0, 3.5f, 0));

		return true;
	}
}
