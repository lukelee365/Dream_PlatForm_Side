using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitChild : MonoBehaviour {
	public GameObject Emoji_Heat;
	public GameObject Emoji_Fruit;
	public float stopDist;
	public GameObject parent1;
	public GameObject parent2;
	private Rigidbody rg;
	private Animator anim;
	private GameObject target;
	private bool showEmojiHeart;
	private int fruitsEaten;
	private GameObject player;
	private bool reachable;
	private bool findParent;
	private bool reachParent;
	private bool checkOnce;
	private bool checkOnce2;
	public GameObject invisbileWall;
	private AudioSource[] rabit_audio;
	public GameObject Emoji_Arrow;
	public GameObject goal;
	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		anim.SetBool ("Sit", true);
		target = null;
		showEmojiHeart = false;
		findParent = false;
		reachParent = false;
		reachable = true;
		checkOnce = false;
		checkOnce2 = false;
		fruitsEaten = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		rabit_audio = GetComponents<AudioSource>();
		Emoji_Heat.SetActive (false);
		Emoji_Fruit.SetActive (true);
		Emoji_Arrow.SetActive (false);
		//
	}
	
	// Update is called once per frame
	void Update () {
		
		if (fruitsEaten > 0) {//Eat 1 Fruit to follow
			Emoji_Fruit.SetActive (false);
			FollowPlayer();
			if (findParent) {
//				invisbileWall.SetActive(false);
				Emoji_Arrow.SetActive (true);
			}
		}

		if (findParent) {
			GoToParent ();
		}

		if (reachParent) {
			gameObject.transform.LookAt (parent1.transform);
		}


		float distBetweenwayPoint = Vector3.Distance (transform.position, player.transform.position);

		if (distBetweenwayPoint < 5f) {
			//Look At player when close by

			gameObject.transform.LookAt (player.transform);
		}



	}
	IEnumerator Eat(){
		Emoji_Fruit.SetActive (false);
		Emoji_Heat.SetActive (true);
		transform.LookAt (target.transform);
		yield return new WaitForSeconds (1.5f);
		anim.SetBool ("Eat", false);
		Emoji_Heat.SetActive (false);
		Emoji_Fruit.SetActive (true);
		if (target != null) {
			Destroy (target);
			fruitsEaten++;
			rabit_audio[0].Play();
		}
	}

	void FollowPlayer(){
		if(player!=null&&reachable){
			rg.isKinematic = false;
			float distBetweenwayPoint = Vector3.Distance (transform.position, player.transform.position);
			if (distBetweenwayPoint < stopDist) {
				rabit_audio[0].Play();
				gameObject.transform.LookAt (player.transform);
				anim.SetBool ("Run", false);
			} else {
				gameObject.transform.LookAt (player.transform);
				float step = 3.5f * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);
				anim.SetBool ("Run", true);
			}
		}
	}

	void GoToParent(){
		if (goal != null) {
			float distBetweenwayPoint = Vector3.Distance (transform.position, goal.transform.position);
			if (distBetweenwayPoint < 2f&&!checkOnce2) {
				gameObject.transform.LookAt (goal.transform);
				anim.SetBool ("Run", false);
				anim.SetBool ("Sit", true);
				checkOnce2 = true;
				if (!showEmojiHeart) {
					// only run once
					StartCoroutine (ShowEmojiHeart ());
					showEmojiHeart = true;
				}
				goal = null;
			} else {

				gameObject.transform.LookAt (goal.transform);
				float step = 5.5f * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, goal.transform.position, step);
				anim.SetBool ("Run", true);
			}
		}
	}

	IEnumerator ShowEmojiHeart(){
		rabit_audio [0].Play ();

		//parent show happy
		parent1.SendMessage ("ActiveEmojiHeart");
		parent2.SendMessage ("ActiveEmojiHeart");
		reachParent = true;
		yield return new WaitForSeconds (3f);
		reachParent = false;
		Emoji_Heat.SetActive (false);
		Emoji_Fruit.SetActive (true);
		parent1.SendMessage ("DeactiveEmojiHeart");
		parent2.SendMessage ("DeactiveEmojiHeart");
		gameObject.transform.LookAt (player.transform);
	}

	public void FruitEnter( GameObject fruit){
		rabit_audio[1].Play();
		target = fruit;
		anim.SetBool ("Sit", false);
		anim.SetBool ("Eat", true);
		StartCoroutine (Eat ());
	}

	public void ParentEnter(){
		if (!checkOnce) {
			Emoji_Heat.SetActive (true);
			rabit_audio [0].Play ();
			findParent = true;
			fruitsEaten = 0;
			checkOnce = true;
		}
	}

}
