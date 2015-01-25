using UnityEngine;
using System.Collections;

public class FloatText : MonoBehaviour {
	private Vector3 locStart, locTarget;
	public float offset, speed;
	private bool goingUp = true;
	// Use this for initialization
	void Start () {
		locStart = transform.position;
		locTarget = new Vector3(locStart.x, locStart.y + offset, locStart.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(goingUp){
			//transform.Translate (Vector3.Lerp (transform.position, locTarget, Time.deltaTime), Space.World);
			transform.position = Vector3.Lerp (transform.position, locTarget, speed*Time.deltaTime);
			if(transform.position.y >= locTarget.y - 0.1f) goingUp = false;
		}else{
			transform.position = Vector3.Lerp (transform.position, locStart, speed*Time.deltaTime);
			if(transform.position.y <= locStart.y + 0.1f) goingUp = true;
		}
	}
}
