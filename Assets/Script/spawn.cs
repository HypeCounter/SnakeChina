using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics2D.Raycast (transform.position, Vector3.up, 10)){
			Debug.DrawRay (transform.position, Vector3.up, Color.green);
			print ("there is something");
		}

	}

}
