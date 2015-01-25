using UnityEngine;
using System.Collections;

public class TreeInteraction : MonoBehaviour, InteractableObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool onLift(){
		return false;
	}
	
	public bool onPush(bool right){
		StartCoroutine("TreePushEvent");
		return true;
	}
	
	IEnumerator TreePushEvent(){
		transform.tag = "Untagged";
		Transform kid = GameController.Get ("Kid");
		kid.tag = "Untagged";
		kid.GetComponentInChildren<Animator>().SetTrigger("Cry");
		Transform cat = GameController.Get ("Cat");
		cat.GetComponentInChildren<Animator>().SetBool("isScratching", true);
		transform.GetComponentInChildren<Animator>().SetBool("Shaking", true);
		while(cat.transform.position.y > -3.7){
			cat.transform.Translate(new Vector3(0,-5*Time.deltaTime,0), Space.World);
			cat.transform.Rotate(Vector3.forward * Time.deltaTime* 120); //it's good enough
			yield return null;
		}
		cat.transform.rotation = Quaternion.identity;
		cat.GetComponentInChildren<Animator>().SetBool("isScratching", false);
		cat.GetComponentInChildren<Animator>().SetTrigger("Dead");
		cat.audio.mute = true;
		yield return null;
	}
	
}
