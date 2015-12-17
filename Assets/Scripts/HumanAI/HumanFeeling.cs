using UnityEngine;
using System.Collections;

public class HumanFeeling : MonoBehaviour {
	//fleeing
	float amount;
	public GameObject player;
	Vector3 direction;
	Vector3 runPoint;
	Vector3 walkPoint;
	float speed;
	NavMeshAgent enemyAgent;
	float stamina;
	Animator ja;

	//walkingAI
	private Transform playerW;
	private NavMeshAgent myNMagent;
	private float nextTurnTime;
	private Transform startTransform;
	private Transform OtherHuman;

	public float multiplyBy;	





	// Use this for initialization
	void Start () {
		runPoint = transform.position;
		walkPoint = transform.position;
		enemyAgent = GetComponent<NavMeshAgent>();
		ja = GetComponent<Animator> ();
		speed = 15.0f;
		stamina = 100.0f;

		playerW = GameObject.FindGameObjectWithTag ("Player").transform;
		myNMagent = this.GetComponent<NavMeshAgent> ();
		
		//RunFrom (new Vector3(0.1f,0.0f,0.0f));
		RunFrom (1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerW = player.transform;
		if (Time.time > nextTurnTime && amount >= 5) {
			//RunFrom (new Vector3(0.1f,0.0f,0.0f));
			RunFrom (1.0f);
			ja.SetBool ("run",false);
		}
		amount = ((player.transform.position)-(transform.position)).magnitude;
		direction = ((player.transform.position)-(transform.position)).normalized;
		if (amount < 10 && stamina >= 10) {
			stamina -= 2*Time.deltaTime;
			Vector3 fs = player.transform.forward;
			Vector3 fs1 = player.transform.right;
			if (amount >= 9) {
				//normal();
				//enemyAgent.speed = 20.0f;
				//transform.Rotate (0.0f, speed * Time.deltaTime, 0.0f);
				//enemyAgent.speed = 15.0f;
				transform.rotation = Quaternion.LookRotation(transform.position - playerW.position);
				RunFrom (1.5f);
				//RunFrom(new Vector3(0.3f,0.0f,0.0f));
				ja.SetBool("run",true);

			} else if (amount >= 7) {
				//chased();
				//enemyAgent.speed = 20.0f;
				//transform.Rotate (0.0f, 1.1*speed * Time.deltaTime, 0.0f);
				//enemyAgent.speed = 16.0f;
				transform.rotation = Quaternion.LookRotation(transform.position - playerW.position);
				RunFrom (1.6f);
				//RunFrom(new Vector3(0.4f,0.0f,0.0f));
				ja.SetBool("run",true);
			} else {
				//run ();
				//enemyAgent.speed = 20.0f;
				//transform.Translate (1.2f*speed * Time.deltaTime, 0.0f, 0.0f);
				//enemyAgent.speed = 17.0f;
				transform.rotation = Quaternion.LookRotation(transform.position - playerW.position);
				RunFrom (1.8f);
				//RunFrom(new Vector3(0.5f,0.0f,0.0f));
				ja.SetBool("run",true);
			}
		} else if (stamina > 10) {
			stamina += Time.deltaTime;
		}
		if (stamina < 10 && amount > 10) {
			stamina += Time.deltaTime*10;
		}


		//enemyAgent.SetDestination(player);
//		Debug.Log (amount + " w: " + walkPoint + " stamina: "+stamina);
	}
	void normal(){
		if((walkPoint-transform.position).magnitude<5){
			
		}
		else{
			
		}
		
	}
	void chased(){
		transform.LookAt(new Vector3(player.transform.position.x,0,player.transform.position.z));
		transform.position+=-direction*speed*Time.deltaTime;
	}
	void run(){
		if ((runPoint - transform.position).magnitude < 5) {
		} else {
		}
	}
	void RunFrom(float speedo){
		// store the starting transform
		startTransform = transform;
		
		//temporarily point the object to look away from the player

		//transform.rotation = Quaternion.LookRotation(transform.position - playerW.position);

		//Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
		// for this if you want variable results) and store it in a new Vector3 called runTo
		Vector3 runTo = transform.position + transform.forward * multiplyBy * speedo;

		//So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.
		NavMeshHit hit;  // stores the output in a variable called hit
		// 5 is the distance to check, assumes you use default for the NavMesh Layer name
		NavMesh.SamplePosition(runTo, out hit, 15, 1 << NavMesh.GetNavMeshLayerFromName("Default")); 
		nextTurnTime = Time.time + 0.1f;
		transform.position = startTransform.position;
		transform.rotation = startTransform.rotation;
		myNMagent.SetDestination(hit.position);
		
		
	}
	GameObject FindClosestBuilding() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Building");
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
	Vector3 FindClosetBuildingRange(){

		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Building");
		GameObject closest = null;
		Vector3 rangeClosest = new Vector3(0.0f,0.0f,0.0f);
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
				//rangeClosest = distance;
			}
		}
		return rangeClosest;
	}


}
