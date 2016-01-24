using UnityEngine;
using System.Collections;

public class prepTearScript : MonoBehaviour {
	public Color color = Color.white;
	public int health = 10; 
	public bool hurt = false;
	public bool hurting = false;

	void Awake () {
		health = 10;
	}

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
			Debug.Log ("OW!");
			hurting = true;
			yield return new WaitForSeconds (0.2f);
			health--;
			hurting = false;
		}
	}

	void checkDeath(){
		if (health <= 0){
			//Play Death Animation
			Destroy (gameObject);
		}
	}
}
