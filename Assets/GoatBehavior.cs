using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatBehavior : MonoBehaviour {
	public GameObject[] wayPoints;
	private bool pingBool;
	private Rigidbody rg;
	private Animator anim;
	public bool test;
	public GameObject Player;
	public GameObject Emoji_Heart;
	public GameObject Emoji_Fruit;
	public GameObject Emoji_FruitToEat;
	public GameObject Emoji_Flower;
	public GameObject Emoji_Pain;
	public ParticleSystem ps_tree;
	public ParticleSystem ps_self;
	private Quaternion preRotation;
	private bool hittree;
	private bool goBack;
	public bool seeTheCue;
	private bool goNext;
	private int idxForGoToSheep;
	private bool leadToHome;
	private bool getFliped;
	private bool lookAtPlayer;
	private bool playerEnter;
	private bool cueEnter;
	private bool indicateTheFollower;
	private bool lastGuide;
	private bool finishJob;
	private bool chaseAndEat;
	private int EnterFirst;
	private bool cueDestroied;
	private Transform target;
	private int numOfFruits;
	private AudioSource [] sheep_audio;
	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		test = false;
		cueEnter = false;
		hittree = false;
		goBack = false;
		leadToHome = false;
		lastGuide = false;
		lookAtPlayer = false;
		playerEnter = false;
		indicateTheFollower = false;
		finishJob = false;
		chaseAndEat = false;
		cueDestroied = false;
		idxForGoToSheep = 2;
		sheep_audio = GetComponents<AudioSource>();
		preRotation = transform.rotation;
		Emoji_Heart.SetActive (false);
		Emoji_Fruit.SetActive (true);
		Emoji_Flower.SetActive (false);
		Emoji_Pain.SetActive (false);
		Emoji_FruitToEat.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!finishJob) {//Haven't Finish Guiding
			if (EnterFirst == 1) {// first time triger the hit tree thing
				hittree = true;
				EnterFirst++;
			}
			if (hittree) {
				HitTree ();
			}
			if (goBack) {
				GoingBackToOrigin ();
			}
			if (leadToHome) {
				WaitAndGoNext ();
			}
			if (test) {
				FlipGoatBack ();
				test = false;
			}
		
			if (seeTheCue) {
				hittree = false;
				goBack = false;
				FlipGoatBack ();
				leadToHome = true;
			}
			if (indicateTheFollower) {
				IndicateFlower ();
			}
		} else {// finish Guiding Job
			if (chaseAndEat) {
				ChaseAndEatFruit ();

			}
		}
		if (lookAtPlayer) {
			anim.SetBool ("Run", false);
			anim.SetBool ("Walk", false);
			//anim.SetTrigger ("Idle");
			transform.LookAt (Player.transform);
		}

		if (numOfFruits > 2) {
			//follow Player
			FollowPlayer();
			Emoji_FruitToEat.SetActive (false);
			Emoji_Flower.SetActive (false);
		}
	}

	void HitTree(){
		if (Vector3.Distance (wayPoints [1].transform.position, transform.position) < 2f) {
			sheep_audio [0].Play ();
			rg.Sleep ();
			rg.WakeUp ();
			anim.SetBool("AngryRun",false);
			anim.SetBool ("Run",true);
			FlipGoat ();
			Emoji_Fruit.SetActive (false);

		} else {
			Emoji_Fruit.SetActive (true);
			Vector3 forceDir = wayPoints[1].transform.position- transform.position;
			Vector3.Normalize (forceDir);
			//playerRg.velocity = forceDir*speed;
			rg.AddForce(forceDir*6f,ForceMode.Acceleration);
			//where to sop
			anim.SetBool("AngryRun",true);

		}
	}
	void FlipGoat(){
		ps_tree.Stop ();
		ps_tree.Play ();
		ps_self.Stop ();
		ps_self.Play ();
		Emoji_Fruit.SetActive (false);
		Emoji_Pain.SetActive (true);
		rg.constraints = RigidbodyConstraints.None;
		rg.AddTorque(gameObject.transform.right*-850f,ForceMode.Force);
		getFliped = true;
		sheep_audio [0].Play ();
		sheep_audio [0].loop = true;
	}
	public void FlipGoatBack(){
		if (getFliped) {
			ps_self.Stop ();
			sheep_audio [0].loop = false;
			Emoji_Fruit.SetActive (true);
			Emoji_Pain.SetActive (false);
			anim.SetBool("AngryRun",false);
			transform.rotation = preRotation;
			rg.constraints = RigidbodyConstraints.FreezeRotation;
			hittree = false;
			goBack = true;
			getFliped = false;
		}
	}

	void GoingBackToOrigin(){
		anim.SetBool ("Run",false);
		anim.SetBool ("Walk",true);
		float distBetweenwayPoint = Vector3.Distance(transform.position,wayPoints[0].transform.position);
		if (distBetweenwayPoint < 1) {
			//OnReach TurnBack the Origin
		
			hittree = true;
			goBack = false;
			//resetAnim
			anim.SetBool ("Walk",false);
		} else {
			float step =  1.5f*Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position,wayPoints[0].transform.position , step);

		}
	
	}

	void WaitAndGoNext(){
		if (idxForGoToSheep < 4) {
			float distBetweenwayPoint = Vector3.Distance (transform.position, wayPoints [idxForGoToSheep].transform.position);
			if (distBetweenwayPoint < 2.5f) {
				idxForGoToSheep++;
				anim.SetBool("AngryRun",false);
				anim.SetBool ("Run", false);
			
			} else {
				sheep_audio [0].Play ();
				gameObject.transform.LookAt (wayPoints [idxForGoToSheep].transform);
				float step = 3 * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, wayPoints [idxForGoToSheep].transform.position, step);
				anim.SetBool ("Run", true);
			}
		} else if (idxForGoToSheep == 4) {
			Emoji_Fruit.SetActive (false);
			sheep_audio [0].Play ();
			lookAtPlayer = true;
		}
	}

	void IndicateFlower(){
		if (idxForGoToSheep < 6) {
			float distBetweenwayPoint = Vector3.Distance (transform.position, wayPoints [idxForGoToSheep].transform.position);
			if (distBetweenwayPoint < 2.5f) {
				idxForGoToSheep++;
				anim.SetBool ("Run", false);
			} else {
				lookAtPlayer = false;
				gameObject.transform.LookAt (wayPoints [idxForGoToSheep].transform);
				float step = 3 * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, wayPoints [idxForGoToSheep].transform.position, step);
				anim.SetBool ("Run", true);
			}
		} else if (idxForGoToSheep == 6) {
			sheep_audio [0].Play ();
			lookAtPlayer = true;
			Emoji_Flower.SetActive (true);
			finishJob = true;
			Emoji_Fruit.SetActive (false);
			Emoji_FruitToEat.SetActive (false);
		}
	}
	void ChaseAndEatFruit(){
		if (target != null) {
			
			float distBetweenwayPoint = Vector3.Distance (transform.position, target.position);
			if (distBetweenwayPoint < 2.0f) {
				anim.SetBool ("Sit", false);
				anim.SetBool ("Run", false);
				anim.SetBool ("Eat", true);

				StartCoroutine (EatingFruit());

			} else {
				Emoji_FruitToEat.SetActive (false);
				gameObject.transform.LookAt (target);
				float step = 1 * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, target.position, step);
				anim.SetBool ("Run", true);
			}
		}
	}

	IEnumerator EatingFruit(){

		yield return new WaitForSeconds (1.5f);
		anim.SetBool ("Eat", false);
		if (target != null) {
			numOfFruits++;

			Destroy (target.gameObject);
			Emoji_FruitToEat.SetActive (true);
		}
			sheep_audio [0].Play ();
			lookAtPlayer = true;

	}
	void FollowPlayer(){
		if (Player != null) {
			float distBetweenwayPoint = Vector3.Distance (transform.position, Player.transform.position);
			if (distBetweenwayPoint < 4.5f) {
				sheep_audio [0].Play ();
				gameObject.transform.LookAt (Player.transform);
				anim.SetBool ("Run", false);
				lookAtPlayer = true;
			} else {
				
				gameObject.transform.LookAt (Player.transform);
				float step = 3.5f * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, Player.transform.position, step);
				anim.SetBool ("Run", true);
			}
		}
	}

	public void PlayerEnter(){
		playerEnter = true;
		EnterFirst++;
		if (lastGuide) {
			indicateTheFollower = true;

		}
	}
	public void PlayerExit(){
		playerEnter = false;
		if (lastGuide) {
			indicateTheFollower = false;
			lookAtPlayer = true;
		}
	}
	public void CueEnter(){
		Emoji_Fruit.SetActive (false);
		cueEnter = true;
		seeTheCue = true;
		lookAtPlayer = false;
	

	}
	public void CueExit(){
		Emoji_Fruit.SetActive (false);
		cueEnter = false;
		seeTheCue = false;
		leadToHome = false;
	
		if(cueDestroied)
			lookAtPlayer = false;
		else
			lookAtPlayer = true;
	}

	public void FruitEnter(Transform trans){
		sheep_audio [1].Play ();
		chaseAndEat = true;
		target = trans;
	}

	public void ActiveEmojiHeart(){
		Emoji_Heart.SetActive (true);
		cueDestroied = true;
		lookAtPlayer = false;
		transform.LookAt (GameObject.Find("Sheep").transform);
	}

	public void DeactiveEmojiHeart(){
		Emoji_Heart.SetActive (false);
		Emoji_Flower.SetActive (true);
		lookAtPlayer = true;
		lookAtPlayer = false;
		indicateTheFollower = true;
		lastGuide = true;
	}

}
