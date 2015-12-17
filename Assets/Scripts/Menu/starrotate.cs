using UnityEngine;
using System.Collections;

public class starrotate : MonoBehaviour {
	public float speed;
	public GameObject axisPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround(axisPoint.transform.position,axisPoint.transform.up,speed*Time.deltaTime);

	}
}
