using UnityEngine;
using System.Collections;

public class destroyHP : MonoBehaviour {
	float startTime;
	public float lifeTime;
	float timer;
	// Use this for initialization
	void Start () {
		//lifeTime = 2.0f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= (startTime+lifeTime)){
			Destroy(gameObject);
		}
	}
}
