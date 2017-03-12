using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitBeHavior : MonoBehaviour {
	public GameObject Emoji_Heat;
	public GameObject Emoji_Fruit;
	public float stopDist;
	private Rigidbody rg;
	private Animator anim;
	private GameObject target;
	private bool showEmojiHeart;
	private int fruitsEaten;
	private GameObject player;
	private bool reachable;
	public GameObject invisbileWall;
	private AudioSource[] rabit_audio;
	public GameObject Emoji_Arrow;
	//public GameObject Emoji_Worry;
	//public GameObject Rabbit_Child;
	private bool worried;
	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		anim.SetBool ("Sit", true);
		target = null;
		showEmojiHeart = false;
		reachable = true;
		worried = true;
		fruitsEaten = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		rabit_audio = GetComponents<AudioSource>();
		Emoji_Heat.SetActive (false);
		Emoji_Fruit.SetActive (true);
		Emoji_Arrow.SetActive (false);
//		Emoji_Worry.SetActive (false);
//
	}
	
	// Update is called once per frame
	void Update () {
		
		if (fruitsEaten > 0) {//Eat two Fruit to follow
			
			invisbileWall.SetActive(false);
			Emoji_Fruit.SetActive (false);
			FollowPlayer();
			Emoji_Arrow.SetActive (true);
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
			rabit_audio[0].Play();
			//if (!worried) {
				fruitsEaten++;
			//}
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

	public void FruitEnter( GameObject fruit){
		rabit_audio[1].Play();
		target = fruit;
		anim.SetBool ("Sit", false);
		anim.SetBool ("Eat", true);
		StartCoroutine (Eat ());
	}

	public void ActiveEmojiHeart(){
//		Emoji_Worry.SetActive (false);
		Emoji_Heat.SetActive (true);
		//transform.LookAt (Rabbit_Child.transform);
	}

	public void DeactiveEmojiHeart(){
		Emoji_Heat.SetActive (false);
		Emoji_Fruit.SetActive (true);
		transform.LookAt (player.transform);
		worried = false;
	}
}
