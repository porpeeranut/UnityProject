﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public float walkSpeed = 0.15f;
	public float runSpeed = 1.0f;
	public float sprintSpeed = 2.0f;
	public float flySpeed = 4.0f;

	public float turnSmoothing = 3.0f;
	public float aimTurnSmoothing = 15.0f;
	public float speedDampTime = 0.1f;

	public float jumpHeight = 5.0f;
	public float jumpCooldown = 1.0f;

	private float timeToNextJump = 0;
	
	private float speed;

	private Vector3 lastDirection;

	private Animator anim;
	private int speedFloat;
	private int jumpBool;
	private int hFloat;
	private int vFloat;
	private int aimBool;
	private int flyBool;
	private int groundedBool;
	private int punchBool;
	private Transform cameraTransform;

	private float h;
	private float v;



	private bool run;
	private bool sprint;

	private bool isMoving;
	private bool isPunch;

	// fly
	private bool fly = false;
	private float distToGround;
	private float sprintFactor;

	//transform
	public bool chkPlayerTransform;
	public GameObject Hero2;
	public GameObject herotem2a;

	//haveGun
	public bool aim;
	private bool shoot =false;
	public bool haveGun = true;
	private int shootBool;
	//punch

	public bool punch;
	public GameObject punchObject;

	//spread

	private int spreadBool;
	public GameObject  greenSmoke;

	void Start() ///  old is Awake
	{	

		chkPlayerTransform = GameObject.Find ("playerStatus").GetComponent<PlayerTransform>().playerTransform;
		anim = GetComponent<Animator> ();
		cameraTransform = Camera.main.transform;
		punchObject = GameObject.FindGameObjectWithTag("punch");


		speedFloat = Animator.StringToHash("Speed");
		jumpBool = Animator.StringToHash("Jump");
		hFloat = Animator.StringToHash("H");
		vFloat = Animator.StringToHash("V");
		aimBool = Animator.StringToHash("Aim");
		punchBool = Animator.StringToHash ("Punch");
		shootBool = Animator.StringToHash ("Shooting");
		spreadBool =  Animator.StringToHash ("Spread");
		// fly 
		flyBool = Animator.StringToHash ("Fly");
		groundedBool = Animator.StringToHash("Grounded");
		distToGround = GetComponent<Collider>().bounds.extents.y;
		sprintFactor = sprintSpeed / runSpeed;



	}

	bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}


	void Update()
	{	//Debug.Log (shoot);
		if (Input.GetButtonDown ("Shoot"))
			shoot = !shoot;
		if (chkPlayerTransform == true) {
			if (Input.GetButtonDown ("Fly"))
				fly = !fly;
		} else {
			if (Input.GetButtonDown ("change")) {
				if(GameObject.Find("playerStatus").GetComponent<PlayerTransform>().waitTransform){
				chkPlayerTransform = true;
				GameObject.Find("playerStatus").GetComponent<PlayerTransform>().changeTransform();
				herotem2a=(GameObject) Instantiate (Hero2, transform.position, transform.rotation);
				Destroy(gameObject);
				}
				
			}
		}


		aim = Input.GetButton ("Aim");
		punch = Input.GetButton ("Punch");
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
		run = Input.GetButton ("Run");
		sprint = Input.GetButton ("Sprint");
		isMoving = Mathf.Abs (h) > 0.1 || Mathf.Abs (v) > 0.1;
		//isPunch = Input.GetButton ("Punch");

	
	}
	
	void FixedUpdate()
	{
	
		anim.SetBool (aimBool, IsAiming());

		anim.SetFloat(hFloat, h);
		anim.SetFloat(vFloat, v);
		
		// Fly
		anim.SetBool (flyBool, fly);
		GetComponent<Rigidbody>().useGravity = !fly;
		anim.SetBool (groundedBool, IsGrounded ());
		if (fly) {
			FlyManagement (h, v);
		}
		else
		{
			MovementManagement (h, v, run, sprint);
			JumpManagement ();
			AttackManagement ();
			SpreadMangement ();
		
		}
	}

	// fly
	void FlyManagement(float horizontal, float vertical)
	{
		Vector3 direction = Rotating(horizontal, vertical);
		GetComponent<Rigidbody>().AddForce(direction * flySpeed * 100 * (sprint?sprintFactor:1));
	}

	void JumpManagement()
	{
		if (GetComponent<Rigidbody>().velocity.y < 10) // already jumped
		{
			anim.SetBool (jumpBool, false);
			if(timeToNextJump > 0)
				timeToNextJump -= Time.deltaTime;
		}
		if (Input.GetButtonDown ("Jump"))
		{
			anim.SetBool(jumpBool, true);
			if(speed > 0 && timeToNextJump <= 0 && !aim)
			{
				GetComponent<Rigidbody>().velocity = new Vector3(0, jumpHeight, 0);
				timeToNextJump = jumpCooldown;
			}
		}
	}
	void SpreadMangement()
	{
	
		if (Input.GetButtonDown ("Spread") && !isMoving) {
		
		
			anim.SetBool (spreadBool, true);
			Instantiate(greenSmoke,transform.position,transform.rotation);
		
		} else {
		
			anim.SetBool (spreadBool, false);

		}
	
	
	
	
	
	}


	void AttackManagement()
	{


	/*	if (!haveGun) { // chek already gun
			if (Input.GetButtonDown ("Shoot")) {
				anim.SetBool (punchBool, true);
				
			} else {
				anim.SetBool (punchBool, false);
			}

	
		} else {
			if (shoot) {

				anim.SetBool (shootBool, true);
				
			}else {
				anim.SetBool (shootBool, false);

			}
		
		}*/

		if (punch && !aim) {
			Debug.Log("punch");
			punchObject.GetComponent<punch>().punched();
			anim.SetBool (punchBool, true);
			
		} else {
			anim.SetBool (punchBool, false);
		}
		if (aim) { // if(shoot)
			
			anim.SetBool (shootBool, true);
			
		}else {
			anim.SetBool (shootBool, false);
			
		}

	}
	
	

	void MovementManagement(float horizontal, float vertical, bool running, bool sprinting)
	{
		Rotating(horizontal, vertical);

		if(isMoving && !aim)
		{
			if(sprinting)
			{
				speed = sprintSpeed;
			}
			else if (running)
			{
				speed = runSpeed;
			}
			else
			{
				speed = walkSpeed;
			}

			anim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
		}
		else
		{
			speed = 0f;
			anim.SetFloat(speedFloat, 0f);
		}
		GetComponent<Rigidbody>().AddForce(Vector3.forward*speed);
	}

	Vector3 Rotating(float horizontal, float vertical)
	{
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		if (!fly)
			forward.y = 0.0f;
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		Vector3 targetDirection;

		float finalTurnSmoothing;

		if(IsAiming())
		{
			targetDirection = forward;
			finalTurnSmoothing = aimTurnSmoothing;
		}
		else
		{
			targetDirection = forward * vertical + right * horizontal;
			finalTurnSmoothing = turnSmoothing;
		}

		if((isMoving && targetDirection != Vector3.zero) || IsAiming())
		{
			Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
			// fly
			if (fly)
				targetRotation *= Quaternion.Euler (90, 0, 0);

			Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, finalTurnSmoothing * Time.deltaTime);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
			lastDirection = targetDirection;
		}
		//idle - fly or grounded
		if(!(Mathf.Abs(h) > 0.9 || Mathf.Abs(v) > 0.9))
		{
			Repositioning();
		}

		return targetDirection;
	}	

	private void Repositioning()
	{
		Vector3 repositioning = lastDirection;
		if(repositioning != Vector3.zero)
		{
			repositioning.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation (repositioning, Vector3.up);
			Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
		}
	}

	public bool IsFlying()
	{
		return fly;
	}

	public bool IsAiming()
	{
		return aim && !fly;
	}

	public bool isSprinting()
	{
		return sprint && !aim && (isMoving);
	}
}
