using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour {

	private Rigidbody rb;
	Vector3 dir;
	public Animator anim;
	private Transform graphics;
	private Quaternion facingDir;
	public bool leftSensor, rightSensor, topSensor, bottomSensor;
	 private bool facingTop, facingBottom, facingLeft, facingRight;
	 private float moveSpeed = 20;
	[SerializeField] private GameObject destroyEffect;
	public bool dead;


	// Use this for initialization
	void Start () {
		moveSpeed = 2;
		rb = GetComponent<Rigidbody> ();
		graphics = transform.GetChild (0);
		SetupAnimator ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		MovePlayer ();
		HandleAnimation ();
		HandleRotation ();
		if ( Input.GetKey (KeyCode.Space) )
			Die ();
	}

	[SerializeField] private float hor, ver;

	private void MovePlayer()
	{
		if ( GameManager.Instance.GetGameStarted () )
		{
			hor = CnControls.CnInputManager.GetAxis ("Horizontal");
			ver = CnControls.CnInputManager.GetAxis ("Vertical");

			if ( hor > 0 && rightSensor )
			{
				hor = 0;
			}
			if ( hor < 0 && leftSensor )
			{
				hor = 0;
			}

			if ( ver > 0 && topSensor )
			{
				ver = 0;
			}

			if ( ver < 0 && bottomSensor )
			{
				ver = 0;
			}

			dir = new Vector3 (hor, 0, ver);


			transform.Translate (dir.normalized * moveSpeed * Time.deltaTime, Space.World);
		}
	}

	private void HandleRotation()
	{
		if ( hor > 0 && !facingRight )
		{
			facingRight = true;
			facingLeft = false;
			facingTop = false;
			facingBottom = false;
			graphics.eulerAngles = new Vector3(0,90,0);
		}
		if ( hor < 0 && !facingLeft )
		{
			facingRight = false;
			facingLeft = true;
			facingTop = false;
			facingBottom = false;
			graphics.eulerAngles = new Vector3(0,270,0);
		}

		if ( ver > 0 && !facingTop )
		{
			facingRight = false;
			facingLeft = false;
			facingTop = true;
			facingBottom = false;
		
			graphics.eulerAngles = new Vector3(0,0,0);

		}

		if ( ver < 0 && !facingBottom )
		{
			facingRight = false;
			facingLeft = false;
			facingTop = false;
			facingBottom = true;
			graphics.eulerAngles = new Vector3(0,180,0);


		}
	}

	float parSpeed;
	private void HandleAnimation()
	{
		parSpeed = (hor != 0) ? Mathf.Abs( hor) : Mathf.Abs( ver);
		anim.SetFloat ("Speed", parSpeed);
	}

	private void SetupAnimator()
	{
		anim = GetComponent<Animator> ();
		Animator[] an = GetComponentsInChildren<Animator> ();
		anim.avatar = an[1].avatar;
		Destroy (an[1]);
	}


	public void Die()
	{
		dead = true;
		destroyEffect.SetActive (true);
		destroyEffect.transform.SetParent (null);
		gameObject.SetActive (false);
	}
}
