using UnityEngine;
using System.Collections;

public class TreeInteraction : MonoBehaviour, InteractableObject {

	public AudioClip[] crash;
	public AudioClip catDeath;

	// Use this for initialization
	void Start () {
		GameController.Register("Tree", transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playCrash(){
		GetComponent<AudioSource>().clip = crash [Random.Range(0, crash.GetLength (0))];
		GetComponent<AudioSource>().Play ();
	}
	
	public bool onLift(){
		return false;
	}
	
	public bool onPush(bool right){
		StartCoroutine("TreePushEvent");
		return true;
	}
	
	IEnumerator TreePushEvent(){
		GameObject.FindWithTag("TutCanvas").GetComponent<FadeCanvas>().FadeOut();
		transform.tag = "Untagged";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(transform);
		Transform kid = GameController.Get ("Kid");
		kid.tag = "Untagged";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeUninteractable(kid);
		kid.GetComponentInChildren<Animator>().SetTrigger("Cry");
		Transform cat = GameController.Get ("Cat");
		cat.GetComponent<AudioSource>().clip = catDeath;
		cat.GetComponent<AudioSource>().Play ();
		cat.GetComponentInChildren<Animator>().SetBool("isScratching", true);
		transform.GetComponentInChildren<Animator>().SetBool("Shaking", true);
		while(cat.transform.position.y > -3.7){
			cat.transform.Translate(new Vector3(0,-5*Time.deltaTime,0), Space.World);
			cat.transform.Rotate(Vector3.forward * Time.deltaTime* 120); //it's good enough
			yield return null;
		}
		cat.transform.rotation = Quaternion.identity;
		transform.GetComponentInChildren<Animator>().SetBool("Shaking", false);
		cat.GetComponentInChildren<Animator>().SetBool("isScratching", false);
		cat.GetComponentInChildren<Animator>().SetTrigger("Dead");
		cat.tag = "Interactable";
		GameController.Get("Player").GetComponentInChildren<PushLiftCollider>().becomeInteractable(cat);
		cat.GetComponent<AudioSource>().mute = true;
		yield return null;
	}
	
}
