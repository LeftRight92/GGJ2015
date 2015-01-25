using UnityEngine;
using System.Collections;

public class FadeCanvas : MonoBehaviour {

	public float fadeSpeed = 1f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void FadeOut(){
		StartCoroutine ("DoFade");
	}
	
	IEnumerator DoFade(){
		CanvasGroup group = gameObject.GetComponent<CanvasGroup>();
		while(group.alpha > 0.01f){
			group.alpha -= Time.deltaTime * fadeSpeed;
			yield return null;
		}
		Destroy (gameObject);
		yield return null;
	}
}
