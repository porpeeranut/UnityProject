using UnityEngine;
using System.Collections;

public class punch : MonoBehaviour {

	// Use this for initialization

	private GameObject player;
	public GameObject punchHit;
	bool isPunching = false;


	void Start () {


	}

	public void punched () {

		if (!isPunching) {
			isPunching = true;
			Instantiate  (punchHit, transform.position, transform.rotation);
			Invoke("stopPunch",1f);
			//Invoke("Punching",0.5f);
		}

	}

	void Punching(){




	}

	void stopPunch(){
		isPunching = false;
	}
}
