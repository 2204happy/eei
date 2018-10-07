using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleCtl : MonoBehaviour {
	private GameObject objSelf;
	public float turnModif;
	public float moveModif;
	public float friction;
	public float topSpeed;
	public float acceleration;
	public float brakeSpeed;
	public GameObject childObj;
	public int segment;
	private float speed;
	private Vector3 initPos;
	private Quaternion initRot;
	public bool dead = false;
	public bool newSeg;
	public float startTime;
	private float fitDist;
	private float fitTime;
    private float fitSpeed;
    public float fitness;
	public int checkpointAmnt;
	public GameObject fitnessText;
	private bool started;
	public bool noSave;
	public bool isAi;
	public neuralnetwork neuralNetwork;
	public evolutionManager evolutionManager;
	// Use this for initialization
	void Start () {
		noSave = false;
		objSelf = this.gameObject;
		speed = 0;
		initPos = objSelf.transform.position;
		initRot = objSelf.transform.rotation;
		segment = 0;
		newSeg = true;
		startTime = Time.time;
		fitDist = 0;
		fitTime = 0;
		fitSpeed = 0;
		fitness = 0;
		started = true;
	}

	// Update is called once per frame
	void Update() {
		if (speed > topSpeed) {
			speed = topSpeed;
		}
		if (speed < 0) {
			speed = 0;
		}
		if (!started) {
			startTime = Time.time;
		}
		childObj.transform.localPosition = new Vector3(0, speed, 0);
		objSelf.transform.position = childObj.transform.position;
		speed -= friction;
		if(checkpointAmnt == segment) {
			fitDist = 9*checkpointAmnt;
            fitTime = Time.time - startTime;
            //Debug.Log(fitDist);
            //Debug.Log(fitTime);
            fitSpeed = fitDist / fitTime;
			fitness = fitSpeed * fitDist * fitDist * fitDist;
			fitness = fitness / 9;
			fitnessText.GetComponent<updateFitnessDisp>().updateDisp(fitness);
			writetofile.append(fitness.ToString(),"_fitnessScores");
            Debug.Log(fitness);
			resetVehicle();
			dead = true;
		}
		if(Time.time - startTime > 15) {
			Debug.Log("took to long");
			dead = true;
			resetVehicle();
		}
	}
	public void turnLeft() {
		objSelf.transform.Rotate(new Vector3(0,0,turnModif));
		if(!started) {
			started = true;
		}
	}

    public void turnRight()
    {
        objSelf.transform.Rotate(new Vector3(0, 0, -turnModif));
        if (!started)
        {
            started = true;
        }
    }
	public void accelerate() {
		speed += acceleration;
        if (!started)
        {
            started = true;
        }
	}
	public void brake() {
		speed -= brakeSpeed;
        if (!started)
        {
            started = true;
        }
	}
	void OnCollisionEnter2D(Collision2D col) {
		dead = true;
		resetVehicle();

	}
	void OnTriggerEnter2D(Collider2D col) {
		GameObject objCol = col.gameObject;
		if (objCol.GetComponent<checkpointNum>().checkPoint == segment + 1)
		{
			segment += 1;
			newSeg = true;
		}
		Debug.Log(segment);
	}
	void LateUpdate()
	{
        if (dead)
        {
            segment = 0;
			startTime = Time.time;
			//started = false;
			if (isAi)
            {
				Debug.Log("calling evolution manager");
				evolutionManager.nextAttempt();
            }
        }
		dead = false;
		newSeg = false;
	}
	public void resetVehicle() {
		objSelf.transform.position = initPos;
        speed = 0;
        objSelf.transform.rotation = initRot;
		fitDist = 0;
        fitTime = 0;
        fitSpeed = 0;
        fitness = 0;

	}
}
