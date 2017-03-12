using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFading : MonoBehaviour {
	public float fadeTime;
	public float idleTime;
	private MeshCollider coll;
	// Use this for initialization
	void Start () {
		StartCoroutine (FadeInOut ());
		coll = GetComponent<MeshCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FadeIn() {
		StartCoroutine (Fade (0f, 1f, fadeTime));
	}

	void FadeOut() {
		StartCoroutine (Fade (1f, 0f, fadeTime));
	}

	IEnumerator Fade(float startAlpha, float endAlpha, float time) {
		float elapseTime = 0;
		Color color = GetComponent<Renderer> ().material.color;
		color.a = startAlpha;

		while (elapseTime < time) {
			color.a += (endAlpha - startAlpha) * Time.deltaTime / time;
			GetComponent<Renderer> ().material.color = color;
			elapseTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	void EnableConnection() {
		gameObject.layer = LayerMask.NameToLayer ("CanConnected");
	}

	void DisableConnection() {
		gameObject.layer = LayerMask.NameToLayer ("Default");
	}

	IEnumerator FadeInOut() {
		while (true) {
			//Debug.Log ("FadeOut");
			FadeOut ();
			yield return new WaitForSeconds (fadeTime);
			//Debug.Log ("Disable");
			DisableConnection ();
			coll.isTrigger = true;
			gameObject.tag = "Untagged";
			yield return new WaitForSeconds (idleTime);
			//Debug.Log ("Enable");
			EnableConnection ();
			//Debug.Log ("FadeIn");
			coll.isTrigger = false;
			gameObject.tag = "Platform";
			FadeIn ();
			yield return new WaitForSeconds (fadeTime + idleTime);
		}
	}
}
