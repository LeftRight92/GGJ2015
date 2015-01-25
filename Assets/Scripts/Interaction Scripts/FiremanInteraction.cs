using UnityEngine;
using System.Collections;

public class FiremanInteraction : MonoBehaviour, InteractableObject {

	private Animator firemanAnimator;

	// Use this for initialization
	void Start () {
		Debug.Log ("Fireman Start()");
		// #haxor
		GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
		GameController.Register ("Fireman", transform);
		firemanAnimator = transform.GetComponentInChildren<Animator> ();
		StartCoroutine ("moveToPlace");		
	}

	IEnumerator moveToPlace(){

		firemanAnimator.SetBool ("Walking", true);
		while (transform.position.x > 6F) {
			transform.Translate(new Vector3(-5*Time.deltaTime,0,0));
			yield return null;
		}
		firemanAnimator.SetBool ("Walking", false);
		transform.tag = "Interactable";

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
