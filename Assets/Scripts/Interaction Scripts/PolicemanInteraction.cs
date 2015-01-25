using UnityEngine;
using System.Collections;

public class PolicemanInteraction : MonoBehaviour, InteractableObject {

	private Animator policemanAnimator;
	private bool isBusy; //Used to check whether he's in the middle of stuff or not for idle sounds
	private bool isInteractable;
	public AudioClip[] idle, lifted, happyIdle;

	// Use this for initialization
	void Start () {
		GameController.Register ("Policeman", transform);
		policemanAnimator = transform.GetComponentInChildren<Animator>();
		isBusy = false;
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
		GameController.Get ("PoliceCar").tag = "Interactable";

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
		cat.localPosition = new Vector3 (0.35f, 1.39f, 0);

		audio.clip = happyIdle[Random.Range (0,happyIdle.GetLength (0))];

		audio.Play ();
		yield return new WaitForSeconds(0.5F);

		// goes down on the ground
		transform.Translate (new Vector3 (0, -3.5f, 0));
		player.canControl = true;			
		 
		isBusy = false;

		//Move to car
				
		policemanAnimator.SetBool ("Walking", true);
		while (transform.position.x > -3F) {
			transform.Translate(new Vector3(-5*Time.deltaTime,0,0));
			yield return null;
		}

		// Destroy this guy/cat 

		transform.GetChild (0).active = false;
		Destroy (cat.gameObject);

		Transform car = GameController.Get ("PoliceCar");

		car.tag = "Untagged";

		// Change car state
		Animator carAnim = car.GetComponentInChildren<Animator>();
		carAnim.SetTrigger ("WithCat");

		// Move car
		while (car.transform.position.x > -13F) {
			car.transform.Translate(new Vector3(-5*Time.deltaTime,0,0));
			yield return null;
		}

		Destroy (car.gameObject);

		Transform kid = GameController.Get("Kid");
		Animator kidAnim = kid.GetComponentInChildren<Animator>();
		kidAnim.SetTrigger ("Angry");

		KidInteraction kidScript = kid.GetComponent<KidInteraction>();
		kidScript.setBusy (true);
		kidScript.playAngrySound ();

		yield return new WaitForSeconds (1.5f);
		Application.LoadLevel ("game_over");
		//GameOver
		
	}


	public bool onPush(bool rightFacing){
	
		return true;
	}
}
