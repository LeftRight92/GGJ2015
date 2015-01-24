using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5.0f;
	private Animator animator;
	public AudioClip[] walkingSounds;
	public bool rightFacing = true;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();
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
				targetScript.onPush(rightFacing);
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
				Debug.Log (file);
				audio.clip = walkingSounds[file];
				Debug.Log(audio.clip);
				audio.Play ();
			Debug.Log ("Is Playing: " + audio.isPlaying);
			}
	}
}


