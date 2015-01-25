using UnityEngine;
using System.Collections;

public class PoliceCarController : MonoBehaviour, InteractableObject {

	private PlayerController player;

	// Use this for initialization
	void Start () {
		GameController.Register ("PoliceCar", transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool onPush (bool right) {

		//Start Push Car event
		StartCoroutine ("PushCarEvent");
		return true;

	}

	public bool onLift () {
		return false;
	}

	IEnumerator PushCarEvent () {

		//Set Policeman to untagged
		//Set Car to untagged
		GameController.Get ("PoliceCar").tag = "Untagged";
		GameController.Get ("PoliceMan").tag = "Untagged";
		//Set character to uncontrollable
		PlayerController player = GameController.Get ("Player").GetComponent<PlayerController>();
		player.canControl = false;
		//Move Car To Tree
		while (GameController.Get ("PoliceCar").position.x < 3) {
						GameController.Get ("PoliceCar").transform.Translate (Time.deltaTime, 0, 0);
				}
		//Change cat to fallen/dead
		//Change tree to fallen
		//Change tree to burning


		yield return null;

	}
}
