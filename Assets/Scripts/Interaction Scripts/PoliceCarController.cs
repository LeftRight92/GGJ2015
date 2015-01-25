using UnityEngine;
using System.Collections;

public class PoliceCarController : MonoBehaviour, InteractableObject {

	private PlayerController player;
	private bool facingRight;
	public AudioClip catDeath, driving, stopping;
	public GameObject firemanPrefab;

	public void playDriving(){
		audio.clip = driving;
		audio.loop = true;
		audio.Play ();
	}

	public void playStopping(){
		audio.clip = stopping;
		audio.loop = false;
		audio.Play ();
	}

	// Use this for initialization
	void Start () {
		GameController.Register ("PoliceCar", transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool onPush (bool right) {
		facingRight = right;
		//Start Push Car event
		StartCoroutine ("PushCarEvent");
		return facingRight;

	}

	public bool onLift () {
		return false;
	}

	IEnumerator PushCarEvent () {

		if (facingRight) {

			//Set Policeman to untagged
			//Set Car to untagged
			GameController.Get ("PoliceCar").tag = "Untagged";
			GameController.Get ("Policeman").tag = "Untagged";

			//Set character to uncontrollable
			PlayerController player = GameController.Get ("Player").GetComponent<PlayerController> ();
			player.canControl = false;
			GameController.Get ("PoliceCar").GetComponentInChildren<SpriteRenderer>().sortingOrder = 4;
			//Move Car To Tree
			while (GameController.Get ("PoliceCar").position.x < 1) {
					GameController.Get ("PoliceCar").transform.Translate (Time.deltaTime, 0, 0);
					yield return null;
			}
			while (GameController.Get ("PoliceCar").position.x > 0.9) {
					GameController.Get ("PoliceCar").transform.Translate (-Time.deltaTime, 0, 0);
					yield return null;
			}

			//Change cat to fallen/dead
			Transform cat = GameController.Get ("Cat");
			cat.GetComponent<AudioSource>().clip = catDeath;
			cat.GetComponent<AudioSource>().Play ();
			cat.GetComponentInChildren<Animator>().SetBool("isScratching", true);
			GameController.Get ("Tree").transform.GetComponentInChildren<Animator>().SetBool("Shaking", true);

			cat.GetComponentInChildren<SpriteRenderer>().sortingOrder = 7;

			while(cat.transform.position.y > -3.7){
				cat.transform.Translate(new Vector3(0,-5*Time.deltaTime,0), Space.World);
				cat.transform.Rotate(Vector3.forward * Time.deltaTime* 120); //it's good enough
				yield return null;
				}

			cat.transform.rotation = Quaternion.identity;
			GameController.Get ("Tree").GetComponentInChildren<Animator>().SetBool("Shaking", false);
			cat.GetComponentInChildren<Animator>().SetBool("isScratching", false);
			cat.GetComponentInChildren<Animator>().SetTrigger("Dead");
			cat.audio.mute = true;
			
			//Change tree to fallen/burning
			Transform tree = GameController.Get ("Tree");
			TreeInteraction treeScript = tree.GetComponent<TreeInteraction>();
			treeScript.playCrash();
			tree.GetComponentInChildren<Animator>().SetTrigger("Burn");


			//Kill Cat,Child,Police Officer
			Destroy(cat.gameObject);
			Destroy(GameController.Get ("Policeman").gameObject);
			Destroy(GameController.Get ("Kid").gameObject);

			//Superposition Tree and player
			GameController.Get ("PoliceCar").GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;			
			GameController.Get ("Tree").GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
			player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
			player.canControl = true;

			//Instantiate Fireman
			Instantiate (firemanPrefab, new Vector3 (12, -1.2f, 0), Quaternion.identity);
			GameController.Get("fireman").GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;

			//Move Fireman in scene
			//Is done in the Fireman script Start()
//			Animator firemanAnimator = GameController.Get("Fireman").transform.GetComponentInChildren<Animator>();

		}
		else
		yield return null;

	}
}
