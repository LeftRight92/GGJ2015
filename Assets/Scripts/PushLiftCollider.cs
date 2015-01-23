using UnityEngine;
using System.Collections;

public class PushLiftCollider : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Entering "+other.transform);
		if(other.transform.tag == "Interactable"){
			Debug.Log ("Setting current push lift target to "+other.transform);
			target = other.transform;
		}
	
	}
	
	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Exiting" +other.transform);
		if(other.transform.tag == "Interactable"){
			Debug.Log ("Unsetting push lift target");
			target = null;
		}
	}
	
}
