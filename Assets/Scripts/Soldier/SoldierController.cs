using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour {

	public GameObject yellArea;
	bool isFoundPlayer = false;
	float minRangeStopAtPlayer = 3.0f;
	float maxRangeStopAtPlayer = 10.0f;
	float walkSpeed = 1.0f;
	float runSpeed = 3.5f;
	float rangeToFoundPlayer = 12.0f;
	float rangeToLostPlayer = 42.0f;
	float rangeToStopAtPlayer;
	NavMeshAgent navAgent;
	Animator animator;
	Transform player;
	Vector3 randomDestination;
	float timer = 0;
	float randomWalkTime;
	float minTimeRandomWalk = 4.0f;
	float maxTimeRandomWalk = 10.0f;
	float minRangeRandomWalk = 5.0f;
	float maxRangeRandomWalk = 15.0f;

	void Start () {
		animator = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rangeToStopAtPlayer = Random.Range(minRangeStopAtPlayer, maxRangeStopAtPlayer);
		randomWalkTime = Random.Range(minTimeRandomWalk, maxTimeRandomWalk);
	}

	void Update () {
		if (player != null) {
			Vector3 location = transform.position - player.position;
			if (GameObject.FindGameObjectWithTag("playerstatus").GetComponent<PlayerHealth>().isDead) {
				animator.SetBool ("victory", true);
				return;
			}
			if (isFoundPlayer) {
				if (GetComponent<EnemyHealth>().isDead) {
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
					rangeToStopAtPlayer = Random.Range (minRangeStopAtPlayer, maxRangeStopAtPlayer);
					runToDestination(player.position);
				} else {
					foundPlayer (false);
				}
			} else {
				if (location.magnitude <= rangeToFoundPlayer) {
					foundPlayer (true);
				}
				// random walk
				timer += Time.deltaTime;
				if (timer > randomWalkTime) {
					timer = 0;
					randomWalkTime = Random.Range(minTimeRandomWalk, maxTimeRandomWalk);
					float distance = Random.Range(minRangeRandomWalk, maxRangeRandomWalk);
					randomDestination = RandomNavSphere (transform.position, distance, 1 << NavMesh.GetNavMeshLayerFromName("Default"));
					walkToDestination(randomDestination);
				}
				if (transform.position == randomDestination) {
					animator.SetBool ("walk", false);
				}
			}
		} else {
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

	Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask){
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance ;
		randomDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition (randomDirection, out navHit, distance, layermask);
		return navHit.position;
	}

	void runToDestination (Vector3 position) {
		navAgent.SetDestination (position);
		navAgent.Resume ();
		animator.SetBool ("shoot", false);
		animator.SetBool ("run", true);
	}

	void walkToDestination (Vector3 position) {
		navAgent.SetDestination (position);
		navAgent.Resume ();
		animator.SetBool ("shoot", false);
		animator.SetBool ("walk", true);
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
