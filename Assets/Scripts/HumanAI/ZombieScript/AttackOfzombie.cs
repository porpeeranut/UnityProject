using UnityEngine;
using System.Collections;

public class AttackOfzombie : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision obj){
		if (obj.gameObject.CompareTag ("Human")) {
			obj.gameObject.GetComponent<HumanFeeling>().onDamge(30.0f);
		}
		//Destroy(obj.gameObject);
	
	}
}
