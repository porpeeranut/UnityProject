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
	public float stamina = 100.0f;
	Animator ja;
	
	//walkingAI
	private Transform playerW;
	private NavMeshAgent myNMagent;
	private float nextTurnTime;
	private Transform startTransform;
	private Transform OtherHuman;
	
	public float multiplyBy;	
	
	float timewalk;
	//wander AI
	public float speed2 = 5;
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 30;
	

	CharacterController controller;
	float heading;
	Vector3 targetRotation;
	//active human building checker
	int activeBd = 0;
	private Vector3 bdpos = new Vector3(0.0f,0.0f,0.0f);

	//Human Hp
	float HumanHealth = 50.0f;
	public void onDamge(float damage){
		HumanHealth = HumanHealth - damage;
		if (HumanHealth <= 0) {
			//turn on zombie
		}
	}
	void Awake ()
	{
		controller = GetComponent<CharacterController>();
		
		// Set random initial rotation
		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);
		
		StartCoroutine(NewHeading());
	}
	IEnumerator NewHeading ()
	{
		while (true) {
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
	void NewHeadingRoutine ()
	{
		var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
		var ceil  = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
		heading = Random.Range(floor, ceil);
		targetRotation = new Vector3(0, heading, 0);
	}
	void wanderWalk(){
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
		var forward = transform.TransformDirection(Vector3.forward);
		controller.SimpleMove(forward * speed2);
		//Debug.Log (forward * speed2);
	}
	
	
	
	// Use this for initialization
	void Start () {
		runPoint = transform.position;
		walkPoint = transform.position;
		enemyAgent = GetComponent<NavMeshAgent>();
		ja = GetComponent<Animator> ();
		speed = 1.0f;
		stamina = 100.0f;
		timewalk = 0;
		playerW = GameObject.FindGameObjectWithTag ("Player").transform;
		myNMagent = this.GetComponent<NavMeshAgent> ();
		
		//RunFrom (new Vector3(0.1f,0.0f,0.0f));
		//RunFrom (1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerW = player.transform;
		amount = ((player.transform.position)-(transform.position)).magnitude;
		direction = ((player.transform.position)-(transform.position)).normalized;
		float distanceBd = FindClosetBuildingRange();
		GameObject building = FindClosestBuilding();


		string c = building.name;
		//Debug.Log ("Range: " + distanceBd + " BuildingName: "  +c );

		//if (Time.time > nextTurnTime && amount >= 10) {
		if(distanceBd < 10.0f){
		}
		if (amount >= 10 && stamina >= 10) {
			//RunFrom (new Vector3(0.1f,0.0f,0.0f));
			//RunFrom (1.0f);
			//myNMagent.speed = 3.5f;
			//NavMeshAgent.Stop();
			//wanderWalk();
			
			myNMagent.SetDestination(RandomNavSphere (transform.position,15, 1 << NavMesh.GetNavMeshLayerFromName("Default")));
			//randomWalk();
			//transform.Translate(speed*Time.deltaTime,0.0f,0.0f);
			ja.SetBool ("run",false);
			//Debug.Log("aaaaaaaaaa");
		} else if (amount < 10 && stamina >= 10) {
			
			
			stamina -= 6*Time.deltaTime;
			//Vector3 fs = player.transform.forward;
			//Vector3 fs1 = player.transform.right;
			if (amount >= 7) {
				//normal();
				//enemyAgent.speed = 20.0f;
				//transform.Rotate (0.0f, speed * Time.deltaTime, 0.0f);
				//enemyAgent.speed = 15.0f;
				transform.rotation = Quaternion.LookRotation(transform.position - playerW.position);
				RunFrom (15.5f);
				myNMagent.speed = 7.5f;
				//RunFrom(new Vector3(0.3f,0.0f,0.0f));
				ja.SetBool("run",true);
				
			} else if (amount >= 4) {
				//chased();
				//enemyAgent.speed = 20.0f;
				//transform.Rotate (0.0f, 1.1*speed * Time.deltaTime, 0.0f);
				//enemyAgent.speed = 16.0f;
				transform.rotation = Quaternion.LookRotation(transform.position - playerW.position);
				RunFrom (15.6f);
				myNMagent.speed = 7.5f;
				//RunFrom(new Vector3(0.4f,0.0f,0.0f));
				ja.SetBool("run",true);
			} else {
				//run ();
				//enemyAgent.speed = 20.0f;
				//transform.Translate (1.2f*speed * Time.deltaTime, 0.0f, 0.0f);
				//enemyAgent.speed = 17.0f;
				transform.rotation = Quaternion.LookRotation(transform.position - playerW.position);
				RunFrom (15.8f);myNMagent.speed = 7.5f;
				//RunFrom(new Vector3(0.5f,0.0f,0.0f));
				ja.SetBool("run",true);
			}
		} else if (stamina > 10) {
			stamina += Time.deltaTime;
		} 
		if (stamina < 10 && amount > 10) {
			stamina += Time.deltaTime*10;
		}
		if (stamina >= 100) {
			stamina = 100.0f;
		}
		
		//enemyAgent.SetDestination(player);
		//Debug.Log (amount + " w: " + walkPoint + " stamina: "+stamina);

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
		NavMesh.SamplePosition(runTo, out hit, 8, 1 << NavMesh.GetNavMeshLayerFromName("Default")); 
		if (nextTurnTime > Random.Range (3.0f, 7.0f)) {
			//hit.position = runTo;		
		}
		nextTurnTime = Time.time + 0.1f;
		transform.position = startTransform.position;
		transform.rotation = startTransform.rotation;
		myNMagent.SetDestination(hit.position);
		//Debug.Log (nextTurnTime);
		
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
	float FindClosetBuildingRange(){
		//var distance = Vector3.Distance(object1.transform.position, object2.transform.position);
		
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
		return distance;
	}
	int count = 0;
	void randomWalk(){
		float randomTime = Random.Range (5.0f,10.0f);
		
		if (timewalk > randomTime) {
			float randomTheta = Random.Range (-45.0f,45.0f);
			//transform.rotation(Vector3.up,randomTheta);
			
			//transform.rotation = Quaternion.Euler(0.0f,randomTheta,0.0f);
			//transform.eulerAngles = new Vector3 (0.0f,randomTheta,0.0f);
			timewalk = 0;
			count += 1;
			//Debug.Log ("Hiiiiiiii: " + count);
		}
		//transform.forward (Vector3.forward,speed*Time.deltaTime);
		//transform.Translate (Vector3.forward * (speed) * Time.deltaTime);
		
		timewalk += Time.deltaTime;
		//Debug.Log (timewalk);
	}
	//Vector3 randomDirection = new Vector3 (0.0f,0.0f,0.0f);
	Vector3 randomDirection = new Vector3 (0.0f,0.0f,0.0f);
	float y = 3.0f;
	float percentageWalk = 0.0f;
	float countTakeBack = 3.0f;

	public void reverseBd(Vector3 b){
		activeBd = 1;
		bdpos = b;
		countTakeBack = 3.0f;
	}

	Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask){
		timewalk += Time.deltaTime;

		if (timewalk > 3.0f && activeBd == 0){//Random.Range (3.0f,10.0f)) {
			
			randomDirection = UnityEngine.Random.insideUnitSphere * distance ;//
			float testmove =  Mathf.Sqrt(((randomDirection.x)*(randomDirection.x)) + ((randomDirection.z)*(randomDirection.z)));
			float maxmove = 15.0f;
			percentageWalk = (testmove/maxmove)*100;
			timewalk = 0;
			//Debug.Log ("randomValue: " + randomDirection);
			ja.SetBool ("walk",true);
			//Debug.Log ("randomValue: " + randomDirection + " Y: " + y);
			//Debug.Log ("x: "+randomDirection.x + " y: "+randomDirection.z + " testmove: "+testmove + " maxmove: "+maxmove +" per:" + percentageWalk +"%");
			
		}
		Debug.Log (" timeawlk "+timewalk + " counterTakeBack: "+countTakeBack +" percentageWalk: "+ percentageWalk);
		if (activeBd == 1) {
			//Vector3 a = transform.position - bdpos;
			countTakeBack = countTakeBack - Time.deltaTime;

			if( countTakeBack <= 1.0f ){
				activeBd = 0;
				countTakeBack = 3.0f;
				timewalk = 0;

			}

			randomDirection = -1*(randomDirection);
			//Debug.Log(activeBd + " n count: "+countTakeBack);
			//Debug.Log("Helloworld: " + percentageWalk);

		}
		if (percentageWalk >= 50 && percentageWalk < 60) {
			if(timewalk > 2.0f){
				ja.SetBool("walk",false);
			}
			
		}else if (percentageWalk >= 40 && percentageWalk < 50) {
			if(timewalk > 2.0f){
				ja.SetBool("walk",false);
			}
		}else if (percentageWalk >= 30 && percentageWalk < 40) {
			if(timewalk > 1.7f){
				ja.SetBool("walk",false);
			}
		}  
		else if (percentageWalk >= 20 && percentageWalk < 30) {
			if(timewalk > 0.9f){
				ja.SetBool("walk",false);
			}
		} else if (percentageWalk >= 0 && percentageWalk < 20) {
			if(timewalk > 0.6f){
				ja.SetBool("walk",false);
			}
		}

		
		randomDirection += origin;
		
		NavMeshHit navHit;
		
		NavMesh.SamplePosition (randomDirection, out navHit, distance, layermask);
		
		return navHit.position;
	}
	void deStamina(float tempStamina){
		stamina -= tempStamina;
	}
}