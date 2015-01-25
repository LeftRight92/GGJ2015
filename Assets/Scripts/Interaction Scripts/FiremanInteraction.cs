using UnityEngine;
using System.Collections;

public class FiremanInteraction : MonoBehaviour, InteractableObject {

	private Animator firemanAnimator;
	private bool isBusy;

	public AudioClip idle;
	public GameObject hoze;
	public GameObject water;

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

		//spawn water
		// local
		GameObject waterObject = Instantiate (water, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		waterObject.transform.parent = transform;
		waterObject.transform.localPosition = new Vector3 (-4.11f, -1.5f, 0);
		waterObject.GetComponentInChildren<SpriteRenderer> ().sortingOrder = 2;

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

		StartCoroutine ("FallOverAndDie");
		return true;
	}

	IEnumerator FallOverAndDie(){
		//Fall over into the fire

		//Lift the fireman and lock above player
		Transform player = GameController.Get ("Player");
		PlayerController playerController = player.GetComponent<PlayerController> ();
		playerController.canControl = false;
		transform.Translate (new Vector3(0, 3.5f, 0));
		transform.parent = player;

		//Rotate counter-clockwise 90 degrees
		while(player.transform.rotation.eulerAngles.z < 90){

			player.transform.Rotate(Vector3.forward * Time.deltaTime* 120); //it's good enough
			yield return null;

		}
		
		//Put some fire on
		
		//Wait and you ded
		yield return new WaitForSeconds (1);
		Application.LoadLevel ("game_over");

	}
	
		//GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(transform);
}
