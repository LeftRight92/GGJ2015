using UnityEngine;
using System.Collections;

public class PolicemanInteraction : MonoBehaviour, InteractableObject {

	private Animator policemanAnimator;
	private bool isBusy; //Used to check whether he's in the middle of stuff or not for idle sounds
	private bool isHappy; //If isn't busy and is happy then it has the cat and hence should move to the car
	private bool isInteractable;
	public AudioClip[] idle, lifted, happyIdle;

	// Use this for initialization
	void Start () {
		GameController.Register ("Policeman", transform);
		policemanAnimator = transform.GetComponentInChildren<Animator>();
		isBusy = false;
		isHappy = false;
		StartCoroutine ("moveToTree");
	}

	// -6 -> 2.6
	IEnumerator moveToTree(){

		policemanAnimator.SetBool ("Walking", true);
		while (transform.position.x < 2.6F) {
			transform.Translate(new Vector3(5*Time.deltaTime,0,0));
			yield return null;
		}
		policemanAnimator.SetBool ("Walking", false);
		transform.tag = "Interactable";

	}

	// 2.6 -> -6
	IEnumerator moveToCar(){
		while (transform.position.x > -6F) {
			transform.Translate(new Vector3(-5*Time.deltaTime,0,0));
			yield return null;
		}
		// Destroy this guy
		// Change car state
		// Move car
		// make kid kill player
	}

	// Update is called once per frame
	void Update () {
		//Move towards the car
		//If on car invoke stuff on car and despawn
	}
	
	public bool onLift(){
		StartCoroutine ("stealCatEvent");
		return true;
	}

	IEnumerator stealCatEvent(){

		isBusy = true;
		transform.tag = "Untagged";		

		// Lock player
		PlayerController player = GameController.Get ("Player").GetComponent<PlayerController>();
		player.canControl = false;
		
		// Gets on the player and grunts
		transform.Translate(new Vector3(0, 3.5f, 0));
		audio.clip = lifted[Random.Range (0,lifted.GetLength (0))];
		audio.Play ();
		yield return new WaitForSeconds(0.5F);


		// gets cat/transforms to happy state
		policemanAnimator.SetBool ("Happy", true);
		Transform cat = GameController.Get ("Cat");
		cat.parent = transform;
		cat.position = new Vector3 (0, 2.4f, 0);

		audio.clip = happyIdle[Random.Range (0,happyIdle.GetLength (0))];
		audio.Play ();
		yield return new WaitForSeconds(0.5F);

		// goes down on the ground
		transform.Translate (new Vector3 (0, 3.5f, 0));
		isHappy = true;
		player.canControl = true;			
		 
		isBusy = false;

		StartCoroutine ("moveToCar");

	}


	public bool onPush(bool rightFacing){
	
		return true;
	}
}
