using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private Sprite hold;
	private Sprite toggle;

	public GameObject holdUI;

	public float health = 100;

	public bool hurting = false;
	public bool regening = false;

	public bool grounded = false;
	public Animator anim;
	public Rigidbody2D rb2d;

	public GameObject healthBar;


	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();


	}

	void Start (){
		//StartCoroutine (regenerate ());

	}

	// Update is called once per frame
	void Update () 
	{
		
		if (this != null) {
			grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

			if (Input.GetButtonDown ("Jump") && grounded || Input.GetKeyDown(KeyCode.UpArrow) && grounded) {
				jump = true;
			}
		}
	}

	void FixedUpdate()
	{
		if (this != null) {
			healthBar.transform.localScale = new Vector3 (1,health / 100, 1);
			if(!regening)
				StartCoroutine(regenerate ());
			if (!grounded) {
				anim.SetBool ("Jump", true);
			} else {
				anim.SetBool ("Jump", false);
			}

	

			float h = Input.GetAxis ("Horizontal");

			if (h * rb2d.velocity.x < maxSpeed)
				rb2d.AddForce (Vector2.right * h * moveForce);

			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

			if (h > 0 && !facingRight)
				Flip ();
			else if (h < 0 && facingRight)
				Flip ();

			if (jump) {
				rb2d.AddForce (new Vector2 (0f, jumpForce));
				jump = false;
			}
			checkDeath ();
		}
	}

	IEnumerator regenerate(){
		regening = true;
		while (health < 100) {
			yield return new WaitForSeconds (1f);
			health++;
		}
		regening = false;
		StopCoroutine ("regenerate");
	}


	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void hurt (int dmg) {
		if (!hurting) {
			StartCoroutine(takeDamage (dmg));
			StartCoroutine(blink ());
		}
	}

	IEnumerator blink(){
		for (int i = 0; i < 10; i++) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, 0);
			yield return new WaitForSeconds (.1f);
			gameObject.GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, 1);
			yield return new WaitForSeconds (.1f);
		}
		StopCoroutine ("blink");
	}

	IEnumerator takeDamage(int dmg){
		hurting = true;
		anim.SetTrigger ("Hurt");
		health -= dmg;
		yield return new WaitForSeconds (3f);
		hurting = false;
		StopCoroutine ("takeDamage");
	}

	void checkDeath(){
		if (health <= 0){
			//Play Death Animation
			Destroy (gameObject);
		}
	}
}