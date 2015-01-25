using UnityEngine;
using System.Collections;

public class GodInteraction : MonoBehaviour, InteractableObject {

	// Use this for initialization
	void Start () {
		StartCoroutine ("youDed");
	}

	IEnumerator youDed() {
		yield return new WaitForSeconds (2);
		Application.LoadLevel ("game_over");
		}

	// Update is called once per frame
	void Update () {
	
	}
	
	public bool onPush(bool facingRight){
	
		return false;
	}
	
	public bool onLift(){
	
		return true;
	}
}
