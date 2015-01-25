using UnityEngine;
using System.Collections;

public class FloatText : MonoBehaviour {
	private Vector3 locStart, locTarget;
	public int offset;
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
			transform.Translate (Vector3.Lerp (new Vector3(0,0,0), new Vector3(0,offset,0), 0.5f));
		}else{
		
		}
	}
}
