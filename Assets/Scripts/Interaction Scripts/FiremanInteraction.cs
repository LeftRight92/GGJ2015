using UnityEngine;
using System.Collections;

public class FiremanInteraction : MonoBehaviour, InteractableObject {

	private Animator firemanAnimator;
	private bool isBusy;

	public AudioClip idle;
	public GameObject hoze;
//	public GameObject hydrant;

	// Use this for initialization
	void Start () {
		isBusy = false;
		// #haxor
		GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
		GameController.Register ("Fireman", transform);
		firemanAnimator = transform.GetComponentInChildren<Animator> ();
		StartCoroutine ("moveToPlace");		
		InvokeRepeating ("IdleSound", 0, 3);
		
	}

	void IdleSound(){
		if (!isBusy) {		
			audio.Play ();		
		}
	}

	IEnumerator moveToPlace(){

		//TODO Firetruck sirens + delay
		firemanAnimator.SetBool ("Walking", true);

		// 4F is the destination of the walking
		while (transform.position.x > 4F) {
			transform.Translate(new Vector3(-5*Time.deltaTime,0,0));
			yield return null;
		}

		//spawn hydrant
		// local
//		GameObject hydrantObject = Instantiate (hydrant, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
//		hydrantObject.transform.parent = transform;
//		hydrantObject.transform.localPosition = new Vector3 (3.25f, 0.03f, 0);
//		hydrantObject.GetComponentInChildren<SpriteRenderer> ().sortingOrder = 2;

		//spawn hoze
		// local x = 0.92 local y = -1.11
		GameObject hozeObject = Instantiate (hoze, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		hozeObject.transform.parent = transform;
		hozeObject.transform.localPosition = new Vector3 (0.92f, -1.11f, 0);	

		hozeObject.GetComponentInChildren<SpriteRenderer> ().sortingOrder = 3;		

		firemanAnimator.SetBool ("Walking", false);
		transform.tag = "Interactable";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeInteractable(transform);

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
	
		//GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(transform);
}
