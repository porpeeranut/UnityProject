using UnityEngine;
using System.Collections;

public class PlayerTransform : MonoBehaviour {

	// Use this for initialization
	public GameObject playerObject;
	public bool playerTransform;
	public float ChangeTime;
	public GameObject firstBody;
	void Start () {

		playerObject = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {

		if (playerTransform == true && ChangeTime > 0.0f) {
			decreaseTime();
		} 

	}

	public void changeTransform(){

		playerTransform = true;
			ChangeTime = 10.0f;
			


	}


	public void decreaseTime() {
		ChangeTime = ChangeTime - Time.deltaTime;
		Debug.Log (ChangeTime);
		if (ChangeTime <= 0.0f && playerTransform == true ) {
			playerTransform = false;
			playerObject = GameObject.FindGameObjectWithTag("Player");
			GameObject herotem2a=(GameObject) Instantiate (firstBody, playerObject.transform.position, playerObject.transform.rotation);
			Destroy(playerObject);
		}
	}

}
