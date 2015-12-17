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
	float rangeToLostPlayer = 42.0f;
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
				if (GetComponent<EnemyHealth>().isDead) {
					return;
				}
				if (GameObject.FindGameObjectWithTag("playerstatus").GetComponent<PlayerHealth>().isDead) {
					animator.SetBool ("victory", true);
					return;
				}
				if (location.magnitude <= rangeToStopAtPlayer) {
					float deltaY = player.transform.position.y - transform.position.y;
					if (deltaY < 2f) {
						navAgent.transform.LookAt (player.position);
						RaycastHit hit1;
						RaycastHit hit2;
						Vector3 soldierPosition1 = transform.position+(transform.right*0.8f)+(transform.up*1.8f);
						Vector3 soldierPosition2 = transform.position-(transform.right*0.8f)+(transform.up*1.8f);
						if (Physics.Raycast(soldierPosition1, player.transform.position - soldierPosition1, out hit1, rangeToStopAtPlayer)) {
							if (Physics.Raycast(soldierPosition2, player.transform.position - soldierPosition2, out hit2, rangeToStopAtPlayer)) {
								if(hit1.transform == player && hit2.transform == player) {
									navAgent.Stop ();
									animator.SetBool ("shoot", true);
									animator.SetBool ("run", false);
									GetComponent<SoldierBulletSpawn>().shoot();
								} else {
									Vector3 pos = transform.position-(transform.right*3);
									runToDestination(pos);
								}
							}
						}
					} else {
						navAgent.Stop ();
						animator.SetBool ("shoot", false);
						animator.SetBool ("run", false);
					}
				} else if (location.magnitude <= rangeToLostPlayer){
					rangeToStopAtPlayer = Random.Range (minRange, maxRange);
					runToDestination(player.position);
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

	void runToDestination (Vector3 position) {
		navAgent.SetDestination (position);
		navAgent.Resume ();
		animator.SetBool ("shoot", false);
		animator.SetBool ("run", true);
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
