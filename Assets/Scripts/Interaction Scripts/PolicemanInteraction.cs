using UnityEngine;
using System.Collections;

public class PolicemanInteraction : MonoBehaviour, InteractableObject {

	private Animator policemanAnimator;
	private bool isBusy; //Used to check whether he's in the middle of stuff or not for idle sounds
	private float x_threshold = 0.1f; //The threshold after which the kid stops chasing
	public AudioClip[] idle, lifted, happyIdle;

	// Use this for initialization
	void Start () {
		GameController.Register ("Policeman", transform);
		policemanAnimator = transform.GetComponentInChildren<Animator>();
		isBusy = false;
		StartCoroutine ("moveToTree");
		InvokeRepeating ("IdleSound", 0, 2);		
	}

	void IdleSound(){
		if (!isBusy) {
			audio.clip = idle[Random.Range (0,idle.GetLength (0))];
			audio.Play ();
		}
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
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeInteractable(transform);
		GameController.Get ("PoliceCar").tag = "Interactable";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeInteractable(GameController.Get ("PoliceCar"));

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
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(transform);	
			

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
		 

		//Move to car
				
		policemanAnimator.SetBool ("Walking", true);
		while (transform.position.x > -3F) {
			transform.Translate(new Vector3(-5*Time.deltaTime,0,0));
			yield return null;
		}

		// Destroy this guy/cat 

		transform.GetChild (0).gameObject.SetActive(false);
		Destroy (cat.gameObject);

		Transform car = GameController.Get ("PoliceCar");

		car.tag = "Untagged";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(car);

		// Change car state
		Animator carAnim = car.GetComponentInChildren<Animator>();
		carAnim.SetTrigger ("WithCat");
		car.GetChild (0).transform.localScale = new Vector3 (-1, 1, 1);

		car.GetComponentInChildren<PoliceCarController> ().playDriving();

		// Move car
		while (car.transform.position.x > -13F) {
			car.transform.Translate(new Vector3(-3*Time.deltaTime,0,0));
			yield return null;
		}

		Destroy (car.gameObject);

		Transform kid = GameController.Get("Kid");
		Animator kidAnim = kid.GetComponentInChildren<Animator>();
		kidAnim.SetTrigger ("Angry");

		KidInteraction kidScript = kid.GetComponent<KidInteraction>();
		kidScript.setBusy (true);
		kidScript.playAngrySound ();

		int tick = 0;

		while (tick<15) {

			if (Mathf.Abs(kid.transform.position.x - player.transform.position.x) > x_threshold) {
				tick++;
			}

			Vector3 playerPos = player.transform.position;
			Vector3 target = new Vector3(playerPos.x, playerPos.y + 1f, playerPos.z);

			kid.transform.position = Vector3.Lerp (kid.transform.position, target, 10 * Time.deltaTime);
			yield return null;
		}

		player.GetComponentInChildren<Animator> ().SetTrigger("Dead");
		player.canControl = false;
		yield return new WaitForSeconds (1.5f);

		isBusy = false;	

		Application.LoadLevel ("game_over");
		
	}


	public bool onPush(bool rightFacing){
	
		return true;
	}
}
