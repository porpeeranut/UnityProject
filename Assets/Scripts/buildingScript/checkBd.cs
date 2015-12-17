using UnityEngine;
using System.Collections;

public class checkBd : MonoBehaviour {
	public Vector3 triggerpoint;
	bool enter = false;
	// Use this for initialization
	void Start () {
		triggerpoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider obj){
		enter = true;
		if(obj.gameObject.CompareTag("Human") && enter == true ){
			obj.gameObject.GetComponent<HumanFeeling>().reverseBd(triggerpoint);
			//Debug.Log("testScript" + enter);
			enter = false;
		}
		//Debug.Log("testScript" + enter);
	}
}
