using UnityEngine;
using System.Collections;

public class TreeInteraction : MonoBehaviour, InteractableObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool onLift(){
		return false;
	}
	
	public bool onPush(bool right){
		return true;
	}
	
}
