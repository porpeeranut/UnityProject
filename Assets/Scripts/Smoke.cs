using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour {

	
	float startTime;
	float lifeTime;
	float timer;
	// Use this for initialization
	void Start () {
		lifeTime = 3.0f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= (startTime+lifeTime)){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider obj){

		if(obj.gameObject.CompareTag("Human")){
				//obj.GetComponent<PlayerHealth>().AddHealth(2);
}

}

}