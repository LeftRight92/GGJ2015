using UnityEngine;
using System.Collections;

public class PoliceCarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameController.Register ("PoliceCar", transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
