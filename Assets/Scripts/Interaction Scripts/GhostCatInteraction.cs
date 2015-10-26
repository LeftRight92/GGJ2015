using UnityEngine;
using System.Collections;

public class GhostCatInteraction : MonoBehaviour, InteractableObject {

	public AudioClip[] idle;
	public Sprite possessed;
	// Use this for initialization
	void Start () {
		GameController.Register ("GhostCat",transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool onPush(bool facingRight){
		StartCoroutine ("possessedEvent");
			
			return true;
	}

	public bool onLift(){
		transform.parent = GameController.Get ("Player");
		GameController.Get ("Player").GetComponent<PlayerController> ().canControl = false;
		GameController.Get ("Player").GetComponentInChildren<Animator> ().SetBool ("Lift",true);
		transform.localPosition = new Vector3 (0, 2f, 0);
		transform.GetComponentInChildren<Animator> ().SetBool ("Scratch", false);
		idle = GameController.Get ("Cat").GetComponent<CatController> ().idle;		
		
		InvokeRepeating ("IdleSound", 0, 4);
		StartCoroutine ("goingToHeaven");
		return true;
	}



	IEnumerator goingToHeaven(){
		GameController.Get ("Player").GetComponentInChildren<Rigidbody2D> ().gravityScale = 0;
		while (GameController.Get ("Player").transform.position.y < 5) {
			GameController.Get ("Player").Translate(new Vector3(0, 2*Time.deltaTime, 0));
			yield return null;
		}

		Application.LoadLevel ("heaven");
	}

	IEnumerator possessedEvent(){
		//Chase PLayer
		while ((transform.position.x - GameController.Get ("Player").transform.position.x) > 0.1f || (transform.position.x - GameController.Get ("Player").transform.position.x) < -0.1f){
			transform.Translate(new Vector3(-(transform.position.x - GameController.Get ("Player").transform.position.x) * Time.deltaTime,0,0));
			                    yield return null;
			                    }

			//Remove Control from Player
			GameController.Get ("Player").GetComponent<PlayerController> ().canControl = false;
			GameController.Get ("Player").GetComponentInChildren<Animator> ().enabled = false;

			//Change player to possessed
			GameController.Get ("Player").GetComponentInChildren<SpriteRenderer> ().sprite = possessed;

			//Mute and remove ghost cat
			transform.GetComponent<AudioSource>().clip = null;
			transform.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		yield return null;

		idle = GameController.Get ("Cat").GetComponent<CatController> ().idle;		

		InvokeRepeating ("IdleSound", 0, 4);

		GameController.Get ("Player").GetComponentInChildren<Rigidbody2D> ().gravityScale = 0;
		while (GameController.Get ("Player").transform.position.x > -6 && GameController.Get ("Player").transform.position.y < 5) {
			if 	(GameController.Get ("Player").position.x > -10)
				GameController.Get ("Player").Translate(new Vector3(-2*Time.deltaTime, 0, 0));
			if 	(GameController.Get ("Player").position.y < 5)
				GameController.Get ("Player").Translate(new Vector3(0, 2*Time.deltaTime, 0));
			yield return null;
		}

		Application.LoadLevel ("game_over");
		}

	void IdleSound(){
			GetComponent<AudioSource>().clip = idle [Random.Range (0, idle.GetLength (0))];
			GetComponent<AudioSource>().Play ();
	}

}
