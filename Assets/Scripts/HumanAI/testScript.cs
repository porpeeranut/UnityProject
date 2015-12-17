using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {
	void Update(){
		float distanceBd = FindClosetBuildingRange();
		GameObject building = FindClosestBuilding();
		
		
		string c = building.name;
		Debug.Log ("Range: " + distanceBd + " BuildingName: "  +c );
	}

	GameObject FindClosestBuilding() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Building");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
	float FindClosetBuildingRange(){
		//var distance = Vector3.Distance(object1.transform.position, object2.transform.position);
		
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Building");
		GameObject closest = null;
		Vector3 rangeClosest = new Vector3(0.0f,0.0f,0.0f);
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
				
				//rangeClosest = distance;
			}
		}
		return distance;
	}
}