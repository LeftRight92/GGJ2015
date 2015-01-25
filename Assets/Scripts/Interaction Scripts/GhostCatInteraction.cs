using UnityEngine;
using System.Collections;

public class GhostCatInteraction : MonoBehaviour, InteractableObject {

	// Use this for initialization
	void Start () {
		GameController.Register ("GhostCat",transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool onPush(bool facingRight){
	
		return true;
	}
	
	public bool onLift(){
	
		return true;
	}
}
