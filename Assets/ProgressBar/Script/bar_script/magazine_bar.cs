using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class magazine_bar : MonoBehaviour {

	public Image magbar;
	private GameObject playerObject;
	private float magratio;
	
	// Use this for initialization
	void Start () 
	{
		playerObject = GameObject.FindGameObjectWithTag("playerstatus");

	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
