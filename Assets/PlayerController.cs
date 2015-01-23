using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Push")){
			Debug.Log ("Pushing!");
		}
		if(Input.GetButtonDown ("Lift")){
			Debug.Log ("Do you maybe lift?");
		}
		if(Input.GetAxis ("Horizontal") > 0){
			transform.Translate (new Vector3(moveSpeed * Time.deltaTime,0,0));
		}else if(Input.GetAxis ("Horizontal") < 0){
			transform.Translate (new Vector3(-moveSpeed * Time.deltaTime,0,0));
		}
	}
}
