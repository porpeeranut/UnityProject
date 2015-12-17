using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour {

	public GameObject yellArea;
	bool isFoundPlayer = false;
	float minRange = 3.0f;
	float maxRange = 10.0f;
	float walkSpeed = 2.0f;
	float runSpeed = 3.5f;
	float rangeToFoundPlayer = 12.0f;
	float rangeToLostPlayer = 32.0f;
	float rangeToStopAtPlayer;
	NavMeshAgent navAgent;
	Animator animator;
	Transform player;

	void Start () {
		animator = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rangeToStopAtPlayer = Random.Range(minRange, maxRange);
	}

	void Update () {
		if (player != null) {
			Vector3 location = transform.position - player.position;
			if (isFoundPlayer) {
				navAgent.transform.LookAt (player.position);
				if (location.magnitude <= rangeToStopAtPlayer) {
					navAgent.Stop ();
					animator.SetBool ("shoot", true);
					animator.SetBool ("run", false);
					GetComponent<SoldierBulletSpawn>().shoot();
				} else if (location.magnitude <= rangeToLostPlayer){
					rangeToStopAtPlayer = Random.Range (minRange, maxRange);
					navAgent.SetDestination (player.position);
					navAgent.Resume ();
					animator.SetBool ("shoot", false);
					animator.SetBool ("run", true);
				} else {
					foundPlayer (false);
				}
			} else {
				if (location.magnitude <= rangeToFoundPlayer) {
					foundPlayer (true);
				}
				// random walk
			}
		} else {
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

	public void foundPlayer (bool found) {
		if (found) {
			if (!isFoundPlayer) {
				Instantiate(yellArea, transform.position, transform.rotation);
			}
			isFoundPlayer = found;
			navAgent.speed = runSpeed;
			animator.SetBool ("walk", false);
		} else {
			isFoundPlayer = found;
			navAgent.speed = walkSpeed;
			animator.SetBool ("run", false);
			animator.SetBool ("walk", false);
			navAgent.Stop ();
		}
	}
}
