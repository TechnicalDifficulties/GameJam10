using UnityEngine;
using System.Collections;

public class sweatScript : MonoBehaviour {

	private float fallDir = 0;

	private GameObject gun;
	private GameObject laser;

	public Color color = Color.white;

	// Use this for initialization
	void Start () {
		gun = GameObject.Find ("/Gun");
		laser = GameObject.Find ("/Gun/Laser");
		Destroy (gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void FixedUpdate () {
		GetComponent<SpriteRenderer> ().color = color;
		GetComponent<Rigidbody2D>().velocity = new Vector3(fallDir, -1f, 0);
	}

	public void setFallDir (float dir) {
		fallDir = dir;
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			Destroy (gameObject);
			col.gameObject.GetComponent<PlayerControl>().hurt ();
		}
	}

	public void hitByLaser (Color c) {

	}
	/*
	Vector2 CartesianToPolar (Vector2 point) {
		Vector2 polar;

		polar.y = Mathf.Atan2 (point.x, point.y);
		polar.x = point.magnitude;

		return polar;
	}

	bool findLaserTriangle () {
		Vector2 GunPos = new Vector2 (laser.transform.position.x, laser.transform.position.y);
		Vector2 hitPoint = new Vector2 (gun.GetComponent<Gun> ().hitPoint.x, gun.GetComponent<Gun> ().hitPoint.y);
		Vector2 thisPos = transform.position;


		GunPos = CartesianToPolar(shiftOrigin (GunPos, GunPos));
		hitPoint = CartesianToPolar(shiftOrigin (hitPoint, GunPos));
		thisPos = CartesianToPolar(shiftOrigin (thisPos, GunPos));

		GunPos = shiftTheta (GunPos, hitPoint.y);
		hitPoint = shiftTheta (hitPoint, hitPoint.y);
		thisPos = shiftTheta (thisPos, hitPoint.y);

		float width = gun.GetComponent<Gun> ().curLaserWidth;

		float alpha = Mathf.Atan2(width, hitPoint.x);
		float beta = -alpha;
	
		return thisPos.x <= hitPoint.x && thisPos.y >= beta && thisPos.y <= alpha;

	}

	Vector2 shiftOrigin (Vector2 point, Vector2 newOrigin) {
		Vector2 newPoint = new Vector2 (point.x - newOrigin.x, point.y - newOrigin.y);
		return newPoint;
	}

	Vector2 shiftTheta (Vector2 point, float theta) {
		Vector2 newPoint = new Vector2 (point.x, point.y - theta);
		return newPoint;
	}*/


}
