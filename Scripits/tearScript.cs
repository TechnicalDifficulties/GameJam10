using UnityEngine;
using System.Collections;

public class tearScript : MonoBehaviour {
	public Color color = Color.white;
	public int health = 10; 
	public bool hurt = false;
	public bool hurting = false;
	// Use this for initialization
	void Start () {
	
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
		}
	}

	void OnCollisionEnter2D (Collision2D col) {


		if (col.gameObject.tag == "Player") {
			Destroy (gameObject);
			col.gameObject.GetComponent<PlayerControl>().hurt ();
		}

		if (col.gameObject.tag == "Ground") {
			Destroy (gameObject);
		}
	}

	void checkDeath(){
		if (health <= 0){
			//Play Death Animation
			Destroy (gameObject);
		}
	}


}
