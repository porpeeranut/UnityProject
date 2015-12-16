using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour {

	
	float startTime;
	float lifeTime;
	float timer;
	float initialSpeed;
	// Use this for initialization
	void Start () {
		lifeTime = 5.0f;
		startTime = Time.time;
		initialSpeed = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().velocity = transform.forward * initialSpeed;
		if(Time.time >= (startTime+lifeTime)){
			//Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider obj){

		if(obj.gameObject.CompareTag("Human")){

			obj.GetComponent<humanTransform>().AddTp(2);
}

}

}