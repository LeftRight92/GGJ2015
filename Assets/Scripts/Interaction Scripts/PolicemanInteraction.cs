using UnityEngine;
using System.Collections;

public class PolicemanInteraction : MonoBehaviour, InteractableObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool onLift(){
		return true;
	}
	
	public bool onPush(bool rightFacing){
	
		return true;
	}
}
