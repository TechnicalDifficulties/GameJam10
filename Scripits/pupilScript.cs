using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pupilScript : MonoBehaviour {

	public GameObject healthbar;
	public GameObject healthFrame;
	public float health = 100;

	public bool dead = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			healthbar.SetActive (true);
			healthFrame.SetActive (true);
			if (health > 0) {
				health -= 50 * Time.deltaTime;
			}
		}
		healthbar.transform.localScale = new Vector3 (health / 100, 1, 1);
		if (health <= 0) {
			Debug.Log ("Victory!");
			Application.LoadLevel("victory");
		}
	}

	public void die(){
		
		dead = true;
	}
}
