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
		catAnimator = GameController.Get ("Cat").GetComponent<Animator> ();
		player.canControl = false;
		kidAnimator.SetTrigger ("Reach");
		catAnimator.SetBool ("isScratching", true);
		//Animations
		yield return new WaitForSeconds(2);
		kidAnimator.SetTrigger ("Scratched");
		catAnimator.SetBool ("isScratching", false);
		transform.Translate(new Vector3(0, -3.5f, 0));
		player.canControl = true;
		transform.tag = "Untagged";
		Destroy (this);

	}


	public bool onLift(){
		StartCoroutine ("kidLiftedEvent");
		return true;
	}
}
