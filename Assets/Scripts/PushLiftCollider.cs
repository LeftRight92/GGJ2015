using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushLiftCollider : MonoBehaviour {

	//reimpliment as array
	//implement a 'notifyDestroyed()
	//public Transform target;
	private List<Transform> targets;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform getTarget() {
		return target;
	}

	public void notifyDestroy(Transform other){
		if(targets.Contains(other)){
			targets.Remove (other);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Entering "+other.transform);
		if(other.transform.tag == "Interactable"){
			Debug.Log ("Setting current push lift target to "+other.transform);
			//target = other.transform;
			targets.Add(other.transform);
		}
	
	}
	
	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Exiting" +other.transform);
		if(other.transform.tag == "Interactable"){
			//Debug.Log ("Unsetting push lift target");
			//target = null;
			targets.Remove(other);
		}
	}
	
}
