using UnityEngine;
using System.Collections;

public class PilotInteraction : MonoBehaviour, InteractableObject {

	public AudioClip[] agony;
	private bool isIdle;

	// Use this for initialization
	void Start () {
		isIdle = true;
		InvokeRepeating ("IdleSound", 0, 6);	
	}

	IEnumerator IdleSound(){
		if (isIdle) {
			audio.clip = agony [Random.Range (0, agony.GetLength (0))];
			audio.Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public bool onPush(bool rightFacing) {	
		StartCoroutine ("PilotDeath");
		return true;
	}
	
	public bool onLift() {
		StartCoroutine ("PilotDeath");		
		return true;
	}

	public void PilotDeath(){
		isIdle = false;

		// Fly to blade
		// Die
	}

}
