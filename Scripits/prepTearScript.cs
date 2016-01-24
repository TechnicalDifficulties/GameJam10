using UnityEngine;
using System.Collections;

public class prepTearScript : MonoBehaviour {
	public Color color = Color.white;
	public int health = 10; 
	public bool hurt = false;
	public bool hurting = false;
	private GameObject monster;

	private GameObject steamResource;
	private GameObject steam;

	public AudioSource audio;

	public Vector2 startPos;

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
			Debug.Log ("OW!");
			hurting = true;
			yield return new WaitForSeconds (0.2f);
			steam = (GameObject)Instantiate (steamResource, transform.position, transform.rotation);
			steam.GetComponent<SpriteRenderer> ().color = color;
			health--;
			hurting = false;
		}
		StopCoroutine (checkHurt ());
	}

	void checkDeath(){
		if (health <= 0){
			//Play Death Animation
			hurting = false;
			steam = (GameObject)Instantiate (steamResource, transform.position, transform.rotation);
			steam.GetComponent<SpriteRenderer> ().color = color;
			gameObject.SetActive(false);
			monster.GetComponent<monsterScript> ().damagedLens ();
		}
	}

	public void resetPos () {
		transform.position = startPos;
		hurting = false;

	}
}
