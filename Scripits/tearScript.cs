using UnityEngine;
using System.Collections;

public class tearScript : MonoBehaviour {
	public Color color = Color.white;
	public int health = 10; 
	public bool hurt = false;
	public bool hurting = false;

	private GameObject steamResource;
	private GameObject steam;

	private GameObject monster;

	private Vector2 startPos;

	void Awake () {
		health = 10;
		monster = GameObject.Find ("/Monster/Eye Ball");
		steamResource = Resources.Load ("Large Steam") as GameObject;
	}

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer> ().color = color;
	}

	void FixedUpdate () {
		StartCoroutine (checkHurt ());
		hurt = false;
		checkDeath ();
	}

	IEnumerator checkHurt () {
		if (hurt && !hurting) {
			hurting = true;
			yield return new WaitForSeconds (0.2f);
			health--;
			hurting = false;
			steam = (GameObject)Instantiate (steamResource, transform.position, transform.rotation);
			steam.GetComponent<SpriteRenderer> ().color = color;
		}
		StopCoroutine (checkHurt ());
	}

	void OnCollisionEnter2D (Collision2D col) {


		if (col.gameObject.tag == "Player") {
			//Destroy (gameObject);
			steam = (GameObject)Instantiate (steamResource, transform.position, transform.rotation);
			steam.GetComponent<SpriteRenderer> ().color = color;
			gameObject.SetActive(false);
			col.gameObject.GetComponent<PlayerControl>().hurt (30);
		}

		if (col.gameObject.tag == "Ground") {
			steam = (GameObject)Instantiate (steamResource, transform.position, transform.rotation);
			steam.GetComponent<SpriteRenderer> ().color = color;
			gameObject.SetActive(false);
		}
	}

	void checkDeath(){
		if (health <= 0){
			//Play Death Animation
			//Destroy (gameObject);
			//hurting = false;
			//gameObject.SetActive(false);
			//monster.GetComponent<monsterScript> ().damagedLens ();


		}
	}

	public void resetPos () {
		transform.position = startPos;
		hurting = false;

	}
}
