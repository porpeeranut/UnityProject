using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	// Use this for initialization
	float startTime;
	float lifeTime;
	float timer;

	void Start () {
		lifeTime = 2.0f;
		startTime = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= (startTime+lifeTime)){
			Destroy(gameObject);
		}
	}
}
