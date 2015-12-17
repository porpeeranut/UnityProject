using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mpbar_script : MonoBehaviour {

	public Image mpbar;
	private GameObject playerObject;
	private float mpratio;

	// Use this for initialization
	void Start () 
	{
		playerObject = GameObject.FindGameObjectWithTag("playerstatus");
		mpratio = playerObject.GetComponent<PlayerTransform>().maxTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		mpbar.fillAmount = playerObject.GetComponent<PlayerTransform> ().ChangeTime / mpratio;
		if (mpbar.fillAmount <= 0.0f) 
		{
			mpbar.fillAmount=0.0f;
		}
	}
}
