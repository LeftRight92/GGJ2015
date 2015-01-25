using UnityEngine;
using System.Collections;

public class FiremanInteraction : MonoBehaviour, InteractableObject {

	private Animator firemanAnimator;
	private bool isBusy;
	private GameObject waterObject;

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
		waterObject = Instantiate (water, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
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
	
	public bool onPush(bool rightFacing){

		StartCoroutine ("BoomHeadshot");
		return true;
	}
	
	IEnumerator BoomHeadshot(){

		GameController.Get ("Fireman").tag = "Untagged";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(GameController.Get ("Fireman"));
		
		//Rotate clockwise 90 degrees
		while(Mathf.Abs(transform.rotation.eulerAngles.z - 270) > 3){				
			transform.Rotate(Vector3.back * Time.deltaTime* 120); //it's good enough
			yield return null;			
		}

		// Play fireman crazy sounds
		// stretch the water 

		Transform water_sprite = waterObject.transform.GetChild (0);
		SpriteRenderer renderer = waterObject.GetComponentInChildren<SpriteRenderer>();
		Debug.Log ("spriteRenderer " + renderer.ToString ());

//		Debug.Log ("1");		
		while(water_sprite.transform.localScale.x < 4){				
//			Debug.Log ("Current scale: " + water_sprite.transform.localScale.ToString() );		
//			Debug.Log ("New scale: " + Vector3.Lerp(water.transform.localScale, new Vector3(2,1,1), Time.deltaTime * 10).ToString());  

			water_sprite.transform.localScale = new Vector3(water_sprite.transform.localScale.x + 0.1f, 1, 1);
			//			water_sprite.transform.localScale = Vector3.MoveTowards(water.transform.localScale, new Vector3(2,1,1), Time.deltaTime);

			yield return null;			
		}
		Debug.Log ("3");		
		

		// Crash the copter
		// Reduce the hoze
		// Change them to background layer?
		
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
