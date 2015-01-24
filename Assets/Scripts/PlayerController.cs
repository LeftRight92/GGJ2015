using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5.0f;
	private Animator animator;
	public AudioClip[] walkingSounds, liftTrue, liftFalse, pushTrue, pushFalse;
	private AudioSource footstep;
	public bool rightFacing = true;
	

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();
		footstep = transform.GetComponentInChildren<AudioSource>();
		InvokeRepeating("WalkingSound",0,0.35f);
	}
	
	// Update is called once per frame
	void Update () {		
		// On Left Mouse click
		if(Input.GetButtonDown ("Push")){
			animator.SetTrigger("Push");
			// Get the target object
			Transform target = transform.GetComponentInChildren<PushLiftCollider>().getTarget();
			if(target != null){
				// Get the interactableObject script for the target object
				InteractableObject targetScript = (InteractableObject) target.GetComponent(typeof(InteractableObject));
				// Execute on push command
				if(targetScript.onPush(rightFacing)){
					audio.clip = pushTrue[Random.Range (0, pushTrue.GetLength (0))];
				}else{
					audio.clip = pushFalse[Random.Range (0, pushFalse.GetLength (0))];
				}
				audio.Play();
			}else{
				audio.clip = pushFalse[Random.Range (0, pushFalse.GetLength (0))];
				audio.Play();
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
				if(targetScript.onLift()){
					audio.clip = liftTrue[Random.Range (0, liftTrue.GetLength (0))];
				}else{
					audio.clip = liftFalse[Random.Range (0, liftFalse.GetLength (0))];
				}
				audio.Play();
			}else{
				audio.clip = liftFalse[Random.Range (0, liftFalse.GetLength (0))];
				audio.Play();
			}			
		}
		// On left right movement
		if(Input.GetAxis ("Horizontal") > 0){
			transform.Translate (new Vector3(moveSpeed * Time.deltaTime,0,0));
			transform.GetChild (1).localScale = new Vector3(1,1,1);
			rightFacing = true;
			animator.SetBool("Walking", true);
		}else if(Input.GetAxis ("Horizontal") < 0){
			transform.Translate (new Vector3(-moveSpeed * Time.deltaTime,0,0));
			transform.GetChild (1).localScale = new Vector3(-1,1,1);
			rightFacing = false;
			animator.SetBool("Walking", true);
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


