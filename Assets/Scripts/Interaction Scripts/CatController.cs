using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameController.Register ("Cat", transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
