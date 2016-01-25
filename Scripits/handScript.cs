using UnityEngine;
using System.Collections;

public class handScript : MonoBehaviour {

	public bool lasered = false;
	public int health = 55;
	public float damage = 0;

	public bool hurting = false;
	public bool hurt = false;
	private Vector2 startPos;
	private bool dead = false;

	public GameObject lens;
	public GameObject pupil;

	public GameObject graphic;
	public GameObject eye;

	public GameObject monsterFace;

	public bool flipped = false;

	void Awake() {
		monsterFace = GameObject.Find ("/Monster/CyclopsNormal");
	}

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (eyeisDead()){
			if (!flipped)
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (-5.25f, -5f, 0f);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (5.25f, -5f, 0f);
		}
		else if (health <= 0) {
			if (!flipped)
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (-1.25f, -1f, 0f);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (1.25f, -1f, 0f);
		} else if (!lasered){
			if (!flipped)
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (1.25f, 1f, 0f);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (-1.25f, 1f, 0f);
		}
		else {
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, 0f, 0f);
		}
		lasered = false;
		StartCoroutine (checkHurt ());
		hurt = false;
		updateColor ();

		checkDeSpawn ();

		if (!dead && health <= 0) {
			dead = true;
			monsterFace.GetComponent<Animator> ().SetTrigger ("Pain");
			GetComponent<AudioSource> ().Play ();


		}

		if (GetComponent<AudioSource> ().isPlaying) {
			lens.GetComponent<SpriteRenderer> ().color = Color.clear;
			pupil.GetComponent<SpriteRenderer> ().color = Color.clear;
		} else {
			lens.GetComponent<SpriteRenderer> ().color = Color.white;
			pupil.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}

	void checkDeSpawn() {
		if (!flipped) {
			if (transform.position.x < startPos.x) {
				resetPos ();
			}
		} else {
			if (transform.position.x > startPos.x) {
				resetPos ();
			}
		}
	}

	bool eyeisDead(){
		if (eye.activeSelf == true)
			return false;
		return true;
	}

	void updateColor () {
		graphic.GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1 - damage, 1 - damage, 1);
	}

	IEnumerator checkHurt () {
		if (hurt && !hurting) {
			hurting = true;
			yield return new WaitForSeconds (0.1f);
			health--;
			damage = damage + 0.01f;
			hurting = false;
		}
		StopCoroutine (checkHurt ());
	}
		

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerControl> ().hurt (100);
			Debug.Log ("Player got squished");
			Application.LoadLevel("gameover");
		}

		if (col.gameObject.tag == "EyeBall") {
			//Destroy (col.gameObject);
			col.gameObject.SetActive(false);
			Debug.Log ("Eye got squished");
			Application.LoadLevel("gameover");
		}
	}

	public void resetPos () {
		transform.position = startPos;
		health = 55;
		damage = 0;
		dead = false;
		gameObject.SetActive (false);
	}
}
