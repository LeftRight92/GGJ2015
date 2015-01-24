using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5.0f;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
		{		
		// On Left Mouse click
		if(Input.GetButtonDown ("Push")){
			animator.SetTrigger("Push");
			// Get the target object
			Transform target = transform.GetComponentInChildren<PushLiftCollider>().getTarget();
			if(target != null){
				// Get the interactableObject script for the target object
				InteractableObject targetScript = (InteractableObject) target.GetComponent(typeof(InteractableObject));
				// Execute on push command
				targetScript.onPush();
			}
		}	
		// On Right Mouse click
		if(Input.GetButtonDown ("Lift")){
			animator.SetTrigger ("Lift");
			// Get the target object
			Transform target = transform.GetComponentInChildren<PushLiftCollider>().getTarget();
			if(target != null){
				// Get the interactableObject script for the target object
				InteractableObject targetScript = (InteractableObject) target.GetComponent(typeof(InteractableObject));
				// Execute on lift command
				targetScript.onLift();
			}			
		}
		// On left right movement
		if(Input.GetAxis ("Horizontal") > 0){
			transform.Translate (new Vector3(moveSpeed * Time.deltaTime,0,0));
			transform.GetChild (1).localScale = new Vector3(1,1,1);
			animator.SetBool("Walking", true);
		}else if(Input.GetAxis ("Horizontal") < 0){
			transform.Translate (new Vector3(-moveSpeed * Time.deltaTime,0,0));
			transform.GetChild (1).localScale = new Vector3(-1,1,1);
			animator.SetBool("Walking", true);
		}else{
			animator.SetBool("Walking", false);
		}
	}
}
