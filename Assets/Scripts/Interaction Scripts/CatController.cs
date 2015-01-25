using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour, InteractableObject {

	public AudioClip[] idle;
	public AudioClip attack;
	private bool isBusy;
	public GameObject gravePrefab;
	public GameObject catGhostPrefab;



	// Use this for initialization
	void Start () {
		GameController.Register ("Cat", transform);
		InvokeRepeating ("IdleSound", 0, 4);
		isBusy = false;
	}

	public void playAttack(){
		audio.clip = attack;
		audio.Play ();
	}
	
	void IdleSound(){

		if (!isBusy) {
			audio.clip = idle [Random.Range (0, idle.GetLength (0))];
			audio.Play ();	
		}
		
	}
	
	public void setBusy(bool busy){
		isBusy = busy;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool onPush(bool rightFacing) {

		//Spawn Cat Grave
		Instantiate (gravePrefab, new Vector3 (2, -3, 0), Quaternion.identity);
		tag = "Untagged";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(GameController.Get ("Cat"));

		//Remove Cat
		transform.GetComponent<SpriteRenderer> ().enabled = false;

		//Bury Cat
		StartCoroutine ("catBuriedEvent");

		//End all life
		return true;
	}

	public bool onLift() {

		//Lift Cat
		//Give Cat to Child
		//Child Sacrifices cat to satan
		//Spawn satan
		//Satan interactions
		return true;
	}

	IEnumerator catBuriedEvent () {

		//Spawn Cat Ghost
		Instantiate (catGhostPrefab, new Vector3 (2, -1.96f, 0), Quaternion.identity);
		yield return null;
		Debug.Log (GameController.Get ("GhostCat"));
		GameController.Get ("GhostCat").GetComponentInChildren<SpriteRenderer> ().sortingOrder = 4;

		//Play Ghost sound

		yield return new WaitForSeconds (6.5f);

		//Play Attack sound
		GameController.Get ("GhostCat").GetComponent<AudioSource> ().clip = attack;
		GameController.Get ("GhostCat").GetComponent<AudioSource> ().loop = true;
		GameController.Get ("GhostCat").GetComponent<AudioSource> ().Play ();
		yield return null;

		//Cat Move To Kid
		while (GameController.Get ("GhostCat").position.x > 1.38 || GameController.Get ("GhostCat").position.y < -1.12) {
			if 	(GameController.Get ("GhostCat").position.x > 1.38)
				GameController.Get ("GhostCat").Translate(new Vector3(-2*Time.deltaTime, 0, 0));
			if 	(GameController.Get ("GhostCat").position.y < -1.12)
				GameController.Get ("GhostCat").Translate(new Vector3(0, 2*Time.deltaTime, 0));
			yield return null;
		}

		//Cat swtich to attack face
		GameController.Get ("GhostCat").GetComponentInChildren<Animator> ().SetBool ("Scratch", true);

		//Child crying
		GameController.Get ("Kid").GetComponentInChildren<Animator> ().SetTrigger ("Scratched");
		GameController.Get ("Kid").GetComponentInChildren<KidInteraction> ().setIdle (false);

		//Ghost cat becomes interactabru
		GameController.Get ("GhostCat").tag = "Interactable";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeInteractable(GameController.Get ("GhostCat"));

	}

}
