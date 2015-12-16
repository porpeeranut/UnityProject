using UnityEngine;
using System.Collections;

public class walkingAI : MonoBehaviour {

	// Use this for initialization
	private Transform player;
	private NavMeshAgent myNMagent;
	private float nextTurnTime;
	private Transform startTransform;
	private Transform OtherHuman;

	public float multiplyBy;	
	private int amo;
	void Start () {
		
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		myNMagent = this.GetComponent<NavMeshAgent> ();

		RunFrom ();
		//OtherHuman = GameObject.FindGameObjectsWithTag ("Human");
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextTurnTime) {
			RunFrom ();
		}
	}
	void RunFrom(){
		// store the starting transform
		startTransform = transform;
		
		//temporarily point the object to look away from the player
		transform.rotation = Quaternion.LookRotation(transform.position - player.position);
		
		//Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
		// for this if you want variable results) and store it in a new Vector3 called runTo
		Vector3 runTo = transform.position + transform.forward * multiplyBy;
		
		//So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.
		NavMeshHit hit;  // stores the output in a variable called hit
		// 5 is the distance to check, assumes you use default for the NavMesh Layer name
		NavMesh.SamplePosition(runTo, out hit, 15, 1 << NavMesh.GetNavMeshLayerFromName("Default")); 
		nextTurnTime = Time.time + 0.1f;
		transform.position = startTransform.position;
		transform.rotation = startTransform.rotation;
		myNMagent.SetDestination(hit.position);

		
	}
	GameObject FindClosestEnemy() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy");
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
