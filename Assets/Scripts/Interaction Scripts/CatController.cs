using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	public AudioClip[] idle;
	private bool isBusy;

	// Use this for initialization
	void Start () {
		GameController.Register ("Cat", transform);
		InvokeRepeating ("IdleSound", 0, 5);
		isBusy = false;
	}
	
	
	void IdleSound(){

		if (!isBusy) {
			audio.clip = idle [Random.Range (0, idle.GetLength (0))];
			audio.Play ();	
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
