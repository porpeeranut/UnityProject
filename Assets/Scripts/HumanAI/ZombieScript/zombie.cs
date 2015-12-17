using UnityEngine;
using System.Collections;

public class zombie : MonoBehaviour {
	Transform zaombie;
	//Animator zpa;
	//
	NavMeshAgent enemyAgent;
	//Transform player;
	Animator animator;
	bool isStopped;
	Collider a;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		zaombie = GameObject.FindGameObjectWithTag ("Human").transform;
		enemyAgent = GetComponent<NavMeshAgent>();
		enemyAgent.speed = 0.0f;
		isStopped = false;
	}
	
	// Update is called once per frame
	void Update () {
		zaombie = FindClosestHuman ().transform;
		if(zaombie != null){
			//enemyAgent.transform.LookAt(player.position);
			Vector3 location = transform.position-zaombie.position;
			//Debug.LogError("distance: " + location.magnitude);
			if(location.magnitude <= 2.0){
				animator.SetBool("walk",true);
				animator.SetBool("attack",true);
				//enemyAgent.Stop();
				enemyAgent.speed = 1.0f;
				//isStopped = true;
			}else if(location.magnitude <= 5.0){
				/*if(isStopped){
					animator.SetBool("walk",true);
					enemyAgent.Resume();
					isStopped = false;
				}*/
				animator.SetBool("walk",true);
				animator.speed = 1.5f;
				enemyAgent.speed = 1.0f;
			} else{
				enemyAgent.speed = 1.0f;
				animator.SetBool("walk",true);
				animator.SetBool("attack",false);
			}
			//animator.SetBool("walk",true);
			enemyAgent.SetDestination(zaombie.position);
		}
	}


	void OnCollisionEnter(Collision obj){

	}




	GameObject FindClosestHuman() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Human");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
}
