using UnityEngine;
using System.Collections;

public class KidInteraction : MonoBehaviour, InteractableObject {

	private Animator kidAnimator;
	private Animator catAnimator;
	// Use this for initialization
	void Start () {
		kidAnimator = transform.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool onPush(bool facing){
		return true;
	}

	IEnumerator kidLiftedEvent()
	{
		transform.Translate(new Vector3(0, 3.5f, 0));
		PlayerController player = GameController.Get ("Player").GetComponent<PlayerController>();
		player.canControl = false;
		kidAnimator.SetTrigger ("Reach");
		//Animations
		yield return new WaitForSeconds(2);
		kidAnimator.SetTrigger ("Scratched");
		transform.Translate(new Vector3(0, -3.5f, 0));
		player.canControl = true;
	}


	public bool onLift(){
		StartCoroutine ("kidLiftedEvent");
		return true;
	}
}
