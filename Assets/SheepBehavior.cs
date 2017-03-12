using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehavior : MonoBehaviour {
	public GameObject Emoji_Heat;
	public GameObject Emoji_Sick;
	private Rigidbody rg;
	private Animator anim;
	private Transform target;
	private GameObject goat;
	private bool runAndEat;
	private bool followGoat;
	private bool showEmojiHeart;
	private AudioSource [] sheep_audio;
	public GameObject invisableWall;

	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		goat = GameObject.Find ("Goat");
		anim.SetBool ("Sit", true);
		runAndEat = false;
		followGoat = false;
		target = null;
		showEmojiHeart = false;
		Emoji_Heat.SetActive (false);
		Emoji_Sick.SetActive (true);
		sheep_audio = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (runAndEat) {
			ChaseAndEatCue ();
		}
		if (followGoat) {
			invisableWall.SetActive (false);
			runAndEat = false;
			FollowGoat ();
		}
	}

	void ChaseAndEatCue(){
		if (target != null) {
			float distBetweenwayPoint = Vector3.Distance (transform.position, target.position);
			if (distBetweenwayPoint < 2.0f) {
				anim.SetBool ("Sit", false);
				anim.SetBool ("Run", false);
			
				sheep_audio [1].Play ();
					StartCoroutine (EatingClue ());

			} else {
				gameObject.transform.LookAt (target);
				float step = 2 * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, target.position, step);
				anim.SetBool ("Run", true);
			}
		}
	}

	void FollowGoat(){
		if (goat != null) {
			float distBetweenwayPoint = Vector3.Distance (transform.position, goat.transform.position);
			if (distBetweenwayPoint < 6f) {
				gameObject.transform.LookAt (goat.transform);
				anim.SetBool ("Run", false);
			if (!showEmojiHeart) {
					// only run once
					StartCoroutine (ShowEmojiHeart ());
					showEmojiHeart = true;
				}
			} else {
				gameObject.transform.LookAt (goat.transform);
				float step = 2f * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, goat.transform.position, step);
				anim.SetBool ("Run", true);
			}
		}
	}
	IEnumerator EatingClue(){
		yield return new WaitForSeconds (1.5f);
		anim.SetBool ("Eat", false);
		Emoji_Sick.SetActive (false);
		if(target!=null)
		Destroy (target.gameObject);
		followGoat = true;


	}

	public void CueEnter( GameObject cue){
		runAndEat = true;
		target = cue.transform;
		anim.SetBool ("Sit", false);
		anim.SetBool ("Eat", true);
		sheep_audio [1].Play ();
	}

	IEnumerator ShowEmojiHeart(){
		sheep_audio [0].Play ();
		Emoji_Heat.SetActive (true);
		goat.SendMessage ("ActiveEmojiHeart");
		yield return new WaitForSeconds (8f);
		Emoji_Heat.SetActive (false);
		goat.SendMessage ("DeactiveEmojiHeart");
	}

}
