using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TuturialFadeOut : MonoBehaviour {
	private Text i;
	// Use this for initialization
	void Start () {
		i = GetComponent<Text> ();
	}
	void Update(){
		
	}

	public IEnumerator FadeTextToZeroAlpha(string texts,float t)
	{
		i.text = texts;
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}
}
