using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour {

	bool isFoundPlayer = false;
	float minRange = 3.0f;
	float maxRange = 10.0f;
	float rangeToFoundPlayer;
	float rangeToStopAtPlayer;
	NavMeshAgent navAgent;
	Animator animator;
	Transform player;

	void Start () {
		animator = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rangeToStopAtPlayer = Random.Range(minRange, maxRange);
		rangeToFoundPlayer = 12.0f;
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
				} else {
					rangeToStopAtPlayer = Random.Range (minRange, maxRange);
					navAgent.SetDestination (player.position);
					navAgent.Resume ();
					animator.SetBool ("shoot", false);
					animator.SetBool ("run", true);
				}
			} else {
				if (location.magnitude <= rangeToFoundPlayer) {
					foundPlayer ();
				}
			}
		} else {
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

	public void foundPlayer () {
		isFoundPlayer = true;
		navAgent.speed = 3.0f;
		animator.SetBool("walk",false);
	}
}
