using UnityEngine;
using System.Collections;

public class monsterScript : MonoBehaviour {

	public GameObject pupil;
	private GameObject tearResource;
	private GameObject prepTearResource;

	private GameObject lens;

	private bool dropping = false;
	public bool sweating = false;

	public GameObject monsterFace;

	public GameObject tear;
	public GameObject prepTear;

	public GameObject pore0;
	public GameObject pore1;
	public GameObject pore2;
	public GameObject pore3;
	public GameObject pore4;
	public GameObject pore5;
	public GameObject pore6;
	public GameObject pore7;
	public GameObject pore8;
	public GameObject pore9;
	public GameObject pore10;
	public GameObject pore11;
	public GameObject pore12;
	public GameObject pore13;
	public GameObject pore14;
	public GameObject pore15;
	public GameObject pore16;
	public GameObject pore17;

	public GameObject finger0;
	public GameObject finger1;

	public GameObject[] pores = new GameObject[18];
	public GameObject[] fingers = new GameObject[2];

	private Color[] colors = new Color[7];



	public int level = 0;

	void Awake () {
		pores [0] = pore0;
		pores [1] = pore1;
		pores [2] = pore2;
		pores [3] = pore3;
		pores [4] = pore4;
		pores [5] = pore5;
		pores [6] = pore6;
		pores [7] = pore7;
		pores [8] = pore8;
		pores [9] = pore9;
		pores [10] = pore10;
		pores [11] = pore11;
		pores [12] = pore12;
		pores [13] = pore13;
		pores [14] = pore14;
		pores [15] = pore15;
		pores [16] = pore16;
		pores [17] = pore17;

		fingers [0] = finger0;
		fingers [1] = finger1;
		//tearResource = Resources.Load ("Tear") as GameObject;
		//prepTearResource = Resources.Load ("PrepTear") as GameObject;

		//tear = GameObject.Find ("/Monster/Tear");
		//prepTear = GameObject.Find ("/Monster/PrepTear");

		colors [0] = Color.white;
		colors [1] = Color.red;
		colors [2] = Color.green;
		colors [3] = Color.blue;
		colors [4] = Color.cyan;
		colors [5] = Color.magenta;
		colors [6] = Color.yellow;

		lens = GameObject.Find ("/Monster/Lens");
	}
		

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		if (!dropping && level != 0) {
			StartCoroutine (dropTear ());
		}
		//if (Input.GetKeyDown (KeyCode.Y))
		//	StartCoroutine (cascadeSweat (Random.Range(-.5f,.5f)));
		//if (Input.GetKeyDown (KeyCode.U))
		//	StartCoroutine (reverseCascadeSweat (Random.Range(-.5f,.5f)));
		if (!sweating && level > 1 && level < 5)
			StartCoroutine (randomSweat (Random.Range(-.5f,.5f)));

		if (!sweating && level >= 5)
			randomChoice ();
			
	}

	IEnumerator randomSweat (float dir) {
		sweating = true;
		for (int i = 0; i < pores.Length; i++) {
			pores [Random.Range(0,18)].GetComponent<PoreScript>().dropSweat (dir, colors[Random.Range(0,7)]);
			yield return new WaitForSeconds (.25f);
		}
		if (level == 2)
			yield return new WaitForSeconds (5f);
		else if (level == 3)
			yield return new WaitForSeconds (2f);
		
		sweating = false;
		StopCoroutine ("randomSweat");
	}


	IEnumerator cascadeSweat (float dir) {
		sweating = true;
		for (int i = 0; i < pores.Length; i++) {
			pores [i].GetComponent<PoreScript>().dropSweat (dir, colors[Random.Range(0,7)]);
			yield return new WaitForSeconds (.25f);
		}
		sweating = false;
		StopCoroutine ("cascadeSweat");
	}

	IEnumerator reverseCascadeSweat (float dir) {
		sweating = true;
		for (int i = pores.Length - 1; i >= 0; i--) {
			pores [i].GetComponent<PoreScript>().dropSweat (dir, colors[Random.Range(0,7)]);
			yield return new WaitForSeconds (.25f);
		}
		sweating = false;
		StopCoroutine ("reverseCascadeSweat");
	}

	void randomChoice () {
		sweating = true;
		int choice = Random.Range (0, 3);
		switch (choice){
		case 0:
			StartCoroutine (cascadeSweat (Random.Range(-.5f,.5f)));
			break;
		case 1:
			StartCoroutine (reverseCascadeSweat (Random.Range(-.5f,.5f)));
			break;
		default:
			StartCoroutine (randomSweat (Random.Range(-.5f,.5f)));
			break;
		}
	}


	IEnumerator dropTear () {
		dropping = true;
		tear.SetActive (false);
		Color color = colors[Random.Range(0,7)];
		prepTear.GetComponent<prepTearScript> ().resetPos ();
		prepTear.GetComponent<prepTearScript> ().color = color;
		prepTear.GetComponent<prepTearScript> ().health = 10;

		prepTear.SetActive (true);

		yield return new WaitForSeconds (5f);

		tear.GetComponent<tearScript> ().resetPos ();
		tear.GetComponent<tearScript> ().color = color;
		tear.GetComponent<tearScript> ().health = prepTear.GetComponent<prepTearScript> ().health;
		if (prepTear.activeSelf)
			tear.SetActive (true);
		prepTear.SetActive (false);

		yield return new WaitForSeconds (5f);
		dropping = false;
		StopCoroutine (dropTear ());
	}

	public void damagedLens() {
		Debug.Log ("Monster was damaged");
		level++;
		lens.GetComponent<Animator> ().SetInteger ("level", level);
		lens.GetComponent<lensScript> ().hurt ();
		if (level == 6) {
			lens.GetComponent<lensScript> ().die ();
			pupil.GetComponent<CircleCollider2D> ().enabled = true;

		}
		spawnFinger ();
	}

	void spawnFinger() {
		GetComponent<AudioSource> ().Play ();
		monsterFace.GetComponent<Animator> ().SetTrigger ("Scream");
		if(!finger0.activeSelf && !finger1.activeSelf)
			fingers[Random.Range (0, 2)].SetActive(true);
	}
}
