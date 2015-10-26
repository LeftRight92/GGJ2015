using UnityEngine;
using System.Collections;

public class HelicopterInteraction : MonoBehaviour, InteractableObject {

	public AudioClip heliHit, heliCrash;

	// Use this for initialization
	void Start () {	
		GameController.Register ("Helicopter", transform);
	}

	public void PlayHeliHit(){
		GetComponent<AudioSource>().clip = heliHit;
		GetComponent<AudioSource>().Play ();
	}

	public void PlayHeliCrash(){
		GetComponent<AudioSource>().clip = heliCrash;
		GetComponent<AudioSource>().Play ();
	}
	
	// Update is called once per frame
	void Update () {	
	}
	
	public bool onPush(bool facingRight){
		return true;
	}
	
	public bool onLift(){
		return true;
	}
}
