using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
	public GameObject Emoji_Heart;
	public GameObject Emoji_Rose;
	public GameObject Emoji_Rose2;
	public GameObject Emoji_Rose3;
	// Use this for initialization
	void Start () {
		Emoji_Rose.SetActive (true);
		Emoji_Rose2.SetActive (true);
		Emoji_Rose3.SetActive (true);
		Emoji_Heart.SetActive (false);
		Invoke ("DisableEmoji_Rose", 6f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (GameObject.FindGameObjectWithTag("MainCamera").transform);
	}

	void DisableEmoji_Rose(){
		Emoji_Rose.SetActive (false);
		Emoji_Rose2.SetActive (false);
		Emoji_Rose3.SetActive (false);
	}
	public void EnableEmoji_Heart(){
		Emoji_Heart.SetActive (true);
	}
}
