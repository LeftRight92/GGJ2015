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

		//Remove Cat
		transform.GetComponent<SpriteRenderer> ().enabled = false;

		//Spawn Cat Ghost
		Instantiate (catGhostPrefab, new Vector3 (2, -3, 0), Quaternion.identity);
		
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



}
