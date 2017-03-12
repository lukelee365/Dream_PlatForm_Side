using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEmoji : MonoBehaviour {
	public GameObject Emoji1;
	public GameObject Emoji2;
	private bool swipe;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("SwitchBetween", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SwitchBetween(){
		if (swipe) {
			Emoji1.SetActive (true);
			Emoji2.SetActive (false);
			swipe = false;
		} else {
			Emoji1.SetActive (false);
			Emoji2.SetActive (true);
			swipe = true;
		}

	}
}
