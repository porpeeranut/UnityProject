using UnityEngine;
using System.Collections;

public class punchBox : MonoBehaviour {

	float startTime;
	float lifeTime;
	float timer;
	void Start () {
		lifeTime = 1.0f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= (startTime+lifeTime)){
			Destroy(gameObject);
		}
	}
}
