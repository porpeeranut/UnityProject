using UnityEngine;
using System.Collections;

public class SoldierBulletSpawn : MonoBehaviour {

	public GameObject bullet;
	bool isShooting = false;
	Transform gunPosition;
	float missAngle = 10;

	public void shoot() {
		if (!isShooting){
			isShooting = true;
			Quaternion rotation = transform.rotation;
			rotation.eulerAngles = new Vector3(
				rotation.eulerAngles.x + Random.Range (-missAngle, missAngle),
				rotation.eulerAngles.y + Random.Range (-missAngle, missAngle),
				rotation.eulerAngles.z
				);

			Instantiate(bullet, transform.position+transform.forward+(transform.up*1.8f), rotation);
			Invoke("stopDelay", 0.5f);
		}
	}

	void stopDelay () {
		isShooting = false;
	}
}
