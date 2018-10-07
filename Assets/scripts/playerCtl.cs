using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCtl : MonoBehaviour {
	private GameObject objSelf;

	// Use this for initialization
	void Start () {
		objSelf = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)) {
			objSelf.GetComponent<vehicleCtl>().accelerate();
		}
		if (Input.GetKey(KeyCode.S))
        {
            objSelf.GetComponent<vehicleCtl>().brake();
        }
		if (Input.GetKey(KeyCode.A))
        {
			objSelf.GetComponent<vehicleCtl>().turnLeft();
        }
		if (Input.GetKey(KeyCode.D))
        {
			objSelf.GetComponent<vehicleCtl>().turnRight();
        }
	}
}
