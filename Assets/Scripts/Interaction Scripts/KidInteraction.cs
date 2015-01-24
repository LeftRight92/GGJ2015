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
		return true;
	}
}
