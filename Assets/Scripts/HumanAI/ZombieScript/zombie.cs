using UnityEngine;
using System.Collections;

public class zombie : MonoBehaviour {
	Transform zaombie;
	Transform zombieHost;
	//Animator zpa;
	//
	NavMeshAgent enemyAgent;
	//Transform player;
	Animator animator;
	bool isStopped;
	Collider a;
	Vector3 location;
	public int activeZombie = 0;

	bool checknull = false;

	// Use this for initialization
	void Start () {
		zombieHost = GetComponent<Transform> ();
		animator = GetComponent<Animator> ();
		zaombie = GameObject.FindGameObjectWithTag ("Human").transform;
		enemyAgent = GetComponent<NavMeshAgent>();
		enemyAgent.speed = 0.0f;
		isStopped = false;
		location = transform.position-zaombie.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("TEST");
		//Debug.Log (enemyAgent.destination);
		//location = transform.position-zaombie.position;
		if(FindClosestHuman() != null){
			zaombie = FindClosestHuman ().transform;
			//Debug.Log("TEST");
		} /*
		if (FindClosestHuman () == null) {
			//zaombie.position = (transform.position+new Vector3(Random.Range(-13.0f,13.0f),0,Random.Range(-13.0f,13.0f)));
			animator.SetBool("walk",true);
			animator.SetBool("attack",false);

			enemyAgent.SetDestination(zombieHost.position +new Vector3(Random.Range(-13.0f,13.0f),0,Random.Range(-13.0f,13.0f)));
			//Debug.Log("TEST");

		}*/
		animator.SetBool("attack",false);
		if (activeZombie == 0) {
			animator.SetBool("attack",false);
			//Debug.Log("TEST" + activeZombie);

		}

		if(zaombie != null  ){
			//enemyAgent.transform.LookAt(player.position);
			location = transform.position-zaombie.position;
			//Debug.Log(location);
			//Debug.LogError("distance: " + location.magnitude);
			if(location.magnitude <= 2.0 && activeZombie == 1){
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
				animator.SetBool("attack",false);

				animator.speed = 1.5f;
				enemyAgent.speed = 1.0f;
			} else{
				enemyAgent.speed = 1.0f;
				animator.SetBool("walk",true);
				animator.SetBool("attack",false);
			}
			//animator.SetBool("walk",true);

			enemyAgent.SetDestination(zaombie.position);

			//Debug.Log("kuyyyyyyy" + activeZombie);
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
		if(gos == null){
			return null;
		}
		return closest;
	}
}
