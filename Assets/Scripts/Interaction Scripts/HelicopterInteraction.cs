using UnityEngine;
using System.Collections;

public class HelicopterInteraction : MonoBehaviour, InteractableObject {

	public AudioClip heliHit, heliCrash;

	// Use this for initialization
	void Start () {	
		GameController.Register ("Helicopter", transform);
	}

	public void PlayHeliHit(){
		audio.clip = heliHit;
		audio.Play ();
	}

	public void PlayHeliCrash(){
		audio.clip = heliCrash;
		audio.Play ();
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
