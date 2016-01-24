using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	public int health = 10;


	public bool grounded = false;
	public Animator anim;
	public Rigidbody2D rb2d;


	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (this != null) {
			grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

			if (Input.GetButtonDown ("Jump") && grounded) {
				jump = true;
			}
		}
	}

	void FixedUpdate()
	{
		if (this != null) {
			
			float h = Input.GetAxis ("Horizontal");

			anim.SetFloat ("Speed", Mathf.Abs (h));

			if (h * rb2d.velocity.x < maxSpeed)
				rb2d.AddForce (Vector2.right * h * moveForce);

			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

			if (h > 0 && !facingRight)
				Flip ();
			else if (h < 0 && facingRight)
				Flip ();

			if (jump) {
				anim.SetTrigger ("Jump");
				rb2d.AddForce (new Vector2 (0f, jumpForce));
				jump = false;
			}
			checkDeath ();
		}
	}


	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void hurt () {
		Debug.Log ("Player was hurt!");
		health--;
	}

	void checkDeath(){
		if (health <= 0){
			//Play Death Animation
			Destroy (gameObject);
		}
	}
}