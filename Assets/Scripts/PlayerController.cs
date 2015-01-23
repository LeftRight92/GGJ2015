using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// On Left Mouse click
		if(Input.GetButtonDown ("Push")){
			//Debug.Log ("Pushing!");

			// Get the target object
			Transform target = transform.GetComponentInChildren<PushLiftCollider>().getTarget();

			// Get the interactableObject script for the target object
			InteractableObject targetScript = (InteractableObject) target.GetComponent(typeof(InteractableObject));

			//Debug.Log (target);
			//Debug.Log (targetScript != null);

			// Execute on push command
			targetScript.onPush();

		}	
		// On Right Mouse click
		if(Input.GetButtonDown ("Lift")){
			//Debug.Log ("Do you maybe lift?");

			// Get the target object
			Transform target = transform.GetComponentInChildren<PushLiftCollider>().getTarget();

			// Get the interactableObject script for the target object
			InteractableObject targetScript = (InteractableObject) target.GetComponent(typeof(InteractableObject));

			//Debug.Log (target);
			//Debug.Log (targetScript != null);

			// Execute on lift command
			targetScript.onLift();
		}

		// On left right movement
		if(Input.GetAxis ("Horizontal") > 0){
			transform.Translate (new Vector3(moveSpeed * Time.deltaTime,0,0));
		}else if(Input.GetAxis ("Horizontal") < 0){
			transform.Translate (new Vector3(-moveSpeed * Time.deltaTime,0,0));
		}
	}
}
