﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5.0f;
	private Animator animator;
	public AudioClip[] walkingSounds, liftTrue, liftFalse, pushTrue, pushFalse;
	private AudioSource footstep;
	public bool rightFacing = true;
	public bool canControl = true;

	// Use this for initialization
	void Start () {
		GameController.Register ("Player", transform);
		animator = transform.GetComponentInChildren<Animator>();
		footstep = transform.GetChild (2).GetComponent<AudioSource>();
		InvokeRepeating("WalkingSound",0,0.35f);
	}
	
	// Update is called once per frame
	void Update () {

		//Only execute if the player can be controlled
		if (canControl) {
			// On Left Mouse click
			if (Input.GetButtonDown ("Push")) {
				animator.SetTrigger ("Push");
				// Get the target object
				Transform target = transform.GetComponentInChildren<PushLiftCollider> ().getTarget ();
				if (target != null) {
					// Get the interactableObject script for the target object
					InteractableObject targetScript = (InteractableObject)target.GetComponent (typeof(InteractableObject));
					// Execute on push command
					if (targetScript.onPush (rightFacing)) {
						GetComponent<AudioSource>().clip = pushTrue [Random.Range (0, pushTrue.GetLength (0))];
					} else {
						GetComponent<AudioSource>().clip = pushFalse [Random.Range (0, pushFalse.GetLength (0))];
					}
					GetComponent<AudioSource>().Play ();
				} else {
					GetComponent<AudioSource>().clip = pushFalse [Random.Range (0, pushFalse.GetLength (0))];
					GetComponent<AudioSource>().Play ();
				}
			}	
			// On Right Mouse click
			if (Input.GetButtonDown ("Lift")) {
					
					// Get the target object
					Transform target = transform.GetComponentInChildren<PushLiftCollider> ().getTarget ();
					if (target != null) {
							// Get the interactableObject script for the target object
							InteractableObject targetScript = (InteractableObject)target.GetComponent (typeof(InteractableObject));
							// Execute on lift command
							if (targetScript.onLift ()) {
									GetComponent<AudioSource>().clip = liftTrue [Random.Range (0, liftTrue.GetLength (0))];
							} else {
									GetComponent<AudioSource>().clip = liftFalse [Random.Range (0, liftFalse.GetLength (0))];
							}
							GetComponent<AudioSource>().Play ();
					} else {
							GetComponent<AudioSource>().clip = liftFalse [Random.Range (0, liftFalse.GetLength (0))];
							GetComponent<AudioSource>().Play ();
					}			
			}
			if (Input.GetButton ("Lift"))
			{
				animator.SetBool ("Lift",true);
			}
			else 
			{
				animator.SetBool ("Lift", false);
			}
			// On left right movement
			if (Input.GetAxis ("Horizontal") > 0) {
					transform.Translate (new Vector3 (moveSpeed * Time.deltaTime, 0, 0));
					transform.GetChild (1).localScale = new Vector3 (1, 1, 1);
					rightFacing = true;
					animator.SetBool ("Walking", true);
			} else if (Input.GetAxis ("Horizontal") < 0) {
					transform.Translate (new Vector3 (-moveSpeed * Time.deltaTime, 0, 0));
					transform.GetChild (1).localScale = new Vector3 (-1, 1, 1);
					rightFacing = false;
					animator.SetBool ("Walking", true);
			} else {
					animator.SetBool ("Walking", false);
			}
		}else{
			animator.SetBool("Walking", false);
		}
	}

	void WalkingSound(){
			if(animator.GetBool ("Walking")){
				int file = Random.Range (0, walkingSounds.GetLength(0));
				footstep.clip = walkingSounds[file];
				footstep.Play ();
			}
	}
}


