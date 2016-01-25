using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject player;
	public GameObject monster;
	public GameObject monsterRoot;

	public GameObject b1;
	public GameObject b2;
	public GameObject b3;

	public GameObject b4;
	public GameObject b5;
	public GameObject b6;

	public GameObject b7;

	public AudioClip clip;

	public int level = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate (){
		if (b1 == null && b2 == null && b3 == null && level == 0) {
			b4.gameObject.SetActive (true);
			b5.gameObject.SetActive (true);
			b6.gameObject.SetActive (true);
			level++;
		}

		if (b4 == null && b5 == null && b6 == null && level == 1) {
			b7.gameObject.SetActive (true);
			level++;
		}

		if (b7 == null && level == 2) {
			increaseLevel ();
			monsterRoot.SetActive (true);
		}
	}

	void increaseLevel (){
		level++;
		if (GetComponent<AudioSource> ().isPlaying)
			GetComponent<AudioSource> ().Stop ();
		GetComponent<AudioSource> ().clip = clip;
		GetComponent<AudioSource> ().Play ();
		monster.GetComponent<monsterScript> ().level++;
	}
}
