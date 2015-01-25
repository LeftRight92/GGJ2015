using UnityEngine;
using System.Collections;

public class KidInteraction : MonoBehaviour, InteractableObject {

	private Animator kidAnimator;
	private Animator catAnimator;
	private bool isIdle; //If is not idle, then is hurt (when considering the new sound in looping
	private bool isBusy; //Used to check if the idle sounds can be played
	public GameObject policeCarPrefab;
	public GameObject policeManPrefab;
	private Transform policeCar;
	public AudioClip[] pushed, lifted, idle, satan, hurt, monster;

	// Use this for initialization
	void Start () {
		GameController.Register ("Kid", transform);	
		kidAnimator = transform.GetComponentInChildren<Animator>();
		InvokeRepeating ("IdleSound", 0, 6);
		isIdle = true;
	}

	public void setBusy(bool busy){
		isBusy = busy;
	}

	public void setIdle(bool idle){
		isIdle = idle;
	}

	public void playAngrySound(){
		audio.clip = monster[Random.Range (0,monster.GetLength (0))];
		audio.Play ();
	}

	void IdleSound(){
		if (!isBusy) {
						if (isIdle) {
								audio.clip = idle [Random.Range (0, idle.GetLength (0))];
								audio.Play ();	
						} else {
								audio.clip = hurt [Random.Range (0, hurt.GetLength (0))];
								audio.Play ();
						}
				}

	}

	// Update is called once per frame
	void Update () {

	}
	
	public bool onPush(bool facing){
		audio.clip = pushed[Random.Range (0,pushed.GetLength (0))];
		audio.Play ();
		return true;
	}

	IEnumerator kidLiftedEvent()
	{
		GameObject.FindWithTag("TutCanvas").GetComponent<FadeCanvas>().FadeOut();
		isBusy = true;
		isIdle = false;
		GameController.Get ("Tree").tag = "Untagged";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(GameController.Get ("Tree"));
		transform.Translate(new Vector3(0, 3.5f, 0));
		PlayerController player = GameController.Get ("Player").GetComponent<PlayerController>();
		Transform cat = GameController.Get ("Cat");
		catAnimator = cat.GetComponent<Animator> ();

		player.canControl = false;
		kidAnimator.SetTrigger ("Reach");
		audio.clip = lifted[Random.Range (0,lifted.GetLength (0))];
		audio.loop = false;
		audio.Play ();
		yield return new WaitForSeconds(1);

		CatController catScript = cat.GetComponent<CatController> ();
		catScript.setBusy (true);
		catScript.playAttack ();
		catAnimator.SetBool ("isScratching", true);
		audio.clip = hurt[Random.Range (0,hurt.GetLength (0))];
		audio.Play ();
		//Animations
		yield return new WaitForSeconds(2);
		catScript.setBusy (false);

		kidAnimator.SetTrigger ("Scratched");
		catAnimator.SetBool ("isScratching", false);
		transform.Translate(new Vector3(0, -3.5f, 0));
		transform.tag = "Untagged";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(transform);
		isBusy = false;
		player.canControl = true;
		Instantiate (policeCarPrefab, new Vector3 (-12, -1.2f, 0), Quaternion.identity);
		yield return null;
		policeCar = GameController.Get("PoliceCar");
		policeCar.tag = "Untagged";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(policeCar);
		Debug.Log ("ASD" + policeCar);

		PoliceCarController carScript = policeCar.GetComponent<PoliceCarController>();		
		carScript.playDriving ();
		Debug.Log ("YOYOYOYO");

		while (policeCar.position.x < -3.35f) {
						policeCar.transform.Translate (new Vector3 (Time.deltaTime, 0, 0));
				yield return null;
				}

		carScript.playStopping ();		
		Debug.Log ("YOYOYOYO");
		

		yield return new WaitForSeconds (1);

		GameController.Get ("PoliceCar").tag = "Interactable";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeInteractable(GameController.Get ("PoliceCar"));
		
		policeCar.GetComponentInChildren<Animator> ().SetTrigger ("Empty");
		Instantiate (policeManPrefab, new Vector3 (-3, -1.9f, 0), Quaternion.identity);

	}


	public bool onLift(){
		StartCoroutine ("kidLiftedEvent");
		return true;
	}
}

