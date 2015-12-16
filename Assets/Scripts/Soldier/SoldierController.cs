using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour {

	bool isFoundPlayer = true;
	float minRange = 3.0f;
	float maxRange = 10.0f;
	float rangeToPlayer;
	NavMeshAgent navAgent;
	Animator animator;
	Transform player;

	void Start () {
		animator = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rangeToPlayer = Random.Range(minRange, maxRange);
	}

	void Update () {
		if (player != null) {
			if (isFoundPlayer) {
				navAgent.transform.LookAt(player.position);
				Vector3 location = transform.position - player.position;
				if(location.magnitude <= rangeToPlayer){
					navAgent.Stop();
					animator.SetBool("run",false);
					animator.SetBool("shoot",true);
				}else{
					rangeToPlayer = Random.Range(minRange, maxRange);
					navAgent.SetDestination(player.position);
					navAgent.Resume();
					animator.SetBool("run",true);
					animator.SetBool("shoot",false);
				}
			} else {
			}
		}
	}

	public void foundPlayer () {
		isFoundPlayer = true;
		navAgent.speed = 10.0f;
		animator.SetBool("walk",false);
	}
}
