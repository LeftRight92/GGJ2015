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
	public AudioClip[] pushed, lifted, idle, satan, hurt;

	// Use this for initialization
	void Start () {
		kidAnimator = transform.GetComponentInChildren<Animator>();
		InvokeRepeating ("IdleSound", 0, 6);
		isIdle = true;
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
		isBusy = true;
		isIdle = false;

		transform.Translate(new Vector3(0, 3.5f, 0));
		PlayerController player = GameController.Get ("Player").GetComponent<PlayerController>();
		catAnimator = GameController.Get ("Cat").GetComponent<Animator> ();
		player.canControl = false;
		kidAnimator.SetTrigger ("Reach");
		audio.clip = lifted[Random.Range (0,lifted.GetLength (0))];
		audio.loop = false;
		audio.Play ();
		yield return new WaitForSeconds(1);
		catAnimator.SetBool ("isScratching", true);
		audio.clip = hurt[Random.Range (0,hurt.GetLength (0))];
		audio.Play ();
		//Animations
		yield return new WaitForSeconds(2);
		kidAnimator.SetTrigger ("Scratched");
		catAnimator.SetBool ("isScratching", false);
		transform.Translate(new Vector3(0, -3.5f, 0));
		transform.tag = "Untagged";
		isBusy = false;
		player.canControl = true;
		Instantiate (policeCarPrefab, new Vector3 (-12, -1.2f, 0), Quaternion.identity);
		yield return null;
		policeCar = GameController.Get("PoliceCar");
		Debug.Log ("ASD" + policeCar);
		while (policeCar.position.x < -6.35f) {
						policeCar.transform.Translate (new Vector3 (5 * Time.deltaTime, 0, 0));
				yield return null;
				}
		policeCar.GetComponentInChildren<Animator> ().SetTrigger ("Empty");
		Instantiate (policeManPrefab, new Vector3 (-6, -1.9f, 0), Quaternion.identity);
	}


	public bool onLift(){
		StartCoroutine ("kidLiftedEvent");
		return true;
	}
}

