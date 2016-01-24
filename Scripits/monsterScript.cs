using UnityEngine;
using System.Collections;

public class monsterScript : MonoBehaviour {

	private GameObject tearResource;
	private GameObject prepTearResource;
	private GameObject tear;
	private GameObject prepTear;

	private bool created = false;

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

	public GameObject[] pores = new GameObject[18];

	private Color[] colors = new Color[7];

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
		tearResource = Resources.Load ("Tear") as GameObject;
		prepTearResource = Resources.Load ("PrepTear") as GameObject;

		colors [0] = Color.white;
		colors [1] = Color.red;
		colors [2] = Color.green;
		colors [3] = Color.blue;
		colors [4] = Color.cyan;
		colors [5] = Color.magenta;
		colors [6] = Color.yellow;
	}
		

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.T) && created == false) {
			created = true;
			StartCoroutine (dropTear ());
		}
		if (Input.GetKeyDown (KeyCode.Y))
			StartCoroutine (cascadeSweat (Random.Range(-.5f,.5f)));
		if (Input.GetKeyDown (KeyCode.U))
			StartCoroutine (reverseCascadeSweat (Random.Range(-.5f,.5f)));
		if (Input.GetKeyDown (KeyCode.I))
			StartCoroutine (randomSweat (Random.Range(-.5f,.5f)));
	}

	IEnumerator randomSweat (float dir) {
		for (int i = 0; i < pores.Length; i++) {
			pores [Random.Range(0,18)].GetComponent<PoreScript>().dropSweat (dir, colors[Random.Range(0,7)]);
			yield return new WaitForSeconds (.25f);
		}
	}


	IEnumerator cascadeSweat (float dir) {
		for (int i = 0; i < pores.Length; i++) {
			pores [i].GetComponent<PoreScript>().dropSweat (dir, colors[Random.Range(0,7)]);
			yield return new WaitForSeconds (.25f);
		}
	}

	IEnumerator reverseCascadeSweat (float dir) {
		for (int i = pores.Length - 1; i >= 0; i--) {
			pores [i].GetComponent<PoreScript>().dropSweat (dir, colors[Random.Range(0,7)]);
			yield return new WaitForSeconds (.25f);
		}
	}


	IEnumerator dropTear () {
		Color color = colors[Random.Range(0,7)];
		if (tear) {
			Destroy (tear);
		} else {
			if (!created)
				StopCoroutine ("dropTear");
		}

		if (prepTear) {
			Destroy (prepTear);
		} else {
			if (!created)
				StopCoroutine ("dropTear");
		}
		created = false;
		prepTear = (GameObject)Instantiate (prepTearResource, new Vector3(transform.position.x,transform.position.y - 2, transform.position.z), transform.rotation);
		prepTear.GetComponent<prepTearScript> ().color = color;
		prepTear.GetComponent<prepTearScript> ().health = 10;

		yield return new WaitForSeconds (5f);

		if (tear) {
			Destroy (tear);
		} 
		int prepTearHealth;
		if (prepTear) {
			prepTearHealth = prepTear.GetComponent<prepTearScript> ().health;
			Destroy (prepTear);
			tear = (GameObject)Instantiate (tearResource, new Vector3 (transform.position.x, transform.position.y - 2, transform.position.z), transform.rotation);
			tear.GetComponent<tearScript> ().color = color;
			tear.GetComponent<tearScript> ().health = prepTearHealth;
			StopCoroutine ("dropTear");
		} else {
			if (tear) {
				Destroy (tear);
			}
			StopCoroutine ("dropTear");
		}
	}
}
