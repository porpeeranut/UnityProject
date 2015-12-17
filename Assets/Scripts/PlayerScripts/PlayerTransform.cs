using UnityEngine;
using System.Collections;

public class PlayerTransform : MonoBehaviour {

	// Use this for initialization
	public GameObject playerObject;
	public bool playerTransform;
	public float ChangeTime;
	public GameObject firstBody;
	public float maxTime;
	public bool waitTransform;


	void Awake () {
		ChangeTime = 0.0f;
		maxTime = 10.0f;
		waitTransform = false;
		playerTransform = false;
		playerObject = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
	
		if (playerTransform == true && ChangeTime > 0.0f) {
			decreaseTime();
		} 

		if (ChangeTime < maxTime && playerTransform == false)
			cooldown ();

	}

	public void changeTransform(){

		playerTransform = true;
		//ChangeTime = 10.0f;
	}

	public void addTime(float time){
		ChangeTime += time;
		if (ChangeTime > maxTime)
			ChangeTime = maxTime;


	}

	public void decreaseTime() {

		ChangeTime = ChangeTime - 0.01f;
		if (ChangeTime <= 0.0f && playerTransform == true ) {
			ChangeTime  = 0.0f;
			playerTransform = false;
			waitTransform = false;
			playerObject = GameObject.FindGameObjectWithTag("Player");
			GameObject herotem2a=(GameObject) Instantiate (firstBody, playerObject.transform.position, playerObject.transform.rotation);
			Destroy(playerObject);
		}
	}
	

	void cooldown(){

		ChangeTime += 0.005f; 
		if (ChangeTime > maxTime) {
			ChangeTime = maxTime;
			waitTransform = true;
		}

	}

	
}
