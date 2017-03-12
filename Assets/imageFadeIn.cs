using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class imageFadeIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(FadeImgToOneAlpha(2f,GetComponent<Image>() ));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator FadeImgToOneAlpha(float t, Image i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a <=1f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}
}
