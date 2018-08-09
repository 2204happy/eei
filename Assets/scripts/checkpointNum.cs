using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointNum : MonoBehaviour {
    //just an identifier
	public int checkPoint;
	public GameObject vehicle;
	private float fitDist;
	private Collider2D coll;
	private ColliderDistance2D distance2D;
	private float curDist;
	private float fitTime;
	private float fitSpeed;
	public float fitness;
	private bool dontRecord;
	private GameObject fitnessText;
	void Start() {
		coll = this.GetComponent<Collider2D>();
		fitnessText = vehicle.GetComponent<vehicleCtl>().fitnessText;
	}
	void Update()
	{
		dontRecord = false;
		if (vehicle.GetComponent<vehicleCtl>().newSeg && vehicle.GetComponent<vehicleCtl>().segment == checkPoint-1)
		{
			Debug.Log("ENTERED SEGMENT");
			distance2D = coll.Distance(vehicle.GetComponent<Collider2D>());
		}
		if (vehicle.GetComponent<vehicleCtl>().dead && vehicle.GetComponent<vehicleCtl>().segment == checkPoint-1)
		{
			fitDist = 1 - (curDist / distance2D.distance);
			fitDist = fitDist + checkPoint-1;
			fitTime = Time.time - vehicle.GetComponent<vehicleCtl>().startTime;
			//Debug.Log(fitDist);
			//Debug.Log(fitTime);
			fitSpeed = fitDist/fitTime;
			fitness = fitSpeed * fitDist*fitDist;
			fitnessText.GetComponent<updateFitnessDisp>().updateDisp(fitness);
			Debug.Log(fitness);
			if((fitness > 100 && checkPoint < 6) || fitness < 0) {
				dontRecord = true;
				Debug.Log("NOT RECORDING VALUE");
			}
			if (!dontRecord)
			{
				writetofile.append(fitness.ToString(), "_fitnessScores");
			}
			else {
				writetofile.append("null", "_fitnessScores");
			}
		}
		if (vehicle.GetComponent<vehicleCtl>().segment == checkPoint - 1)
        {
            curDist = coll.Distance(vehicle.GetComponent<Collider2D>()).distance;
        }
	}
}
