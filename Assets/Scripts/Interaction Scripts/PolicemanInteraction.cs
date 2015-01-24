using UnityEngine;
using System.Collections;

public class PolicemanInteraction : MonoBehaviour, InteractableObject {

	private Animator policemanAnimator;
	private bool isBusy; //Used to check whether he's in the middle of stuff or not for idle sounds
	public AudioClip[] idle, lifted, happyIdle;

	// Use this for initialization
	void Start () {
		GameController.Register ("Policeman", transform);
		policemanAnimator = transform.GetComponentInChildren<Animator>();
		isBusy = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool onLift(){

		StartCoroutine ("kidLiftedEvent");
		return true;
	}

	IEnumerator stealCatEvent(){

		isBusy = true;

		// Lock player
		PlayerController player = GameController.Get ("Player").GetComponent<PlayerController>();
		player.canControl = false;
		
		// Gets on the player and grunts
		transform.Translate(new Vector3(0, 3.5f, 0));
		audio.clip = lifted[Random.Range (0,lifted.GetLength (0))];
		audio.Play ();
		yield return new WaitForSeconds(0.5F);


		// gets cat/transforms to happy state
		policemanAnimator.SetTrigger ("Happy");
		Transform cat = GameController.Get ("Cat");
		cat.parent = transform;

		audio.clip = happyIdle[Random.Range (0,happyIdle.GetLength (0))];
		audio.Play ();

		// goes down on the ground
		player.canControl = true;

		// goes to car
		// car leaves
		// kid goes monster mode
		// monster sound
		// gameover.jpeg
		
		
		isBusy = false;

	}


	public bool onPush(bool rightFacing){
	
		return true;
	}
}
