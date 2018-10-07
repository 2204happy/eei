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
	private GameObject fitnessText;
	void Start() {
		coll = this.GetComponent<Collider2D>();
		fitnessText = vehicle.GetComponent<vehicleCtl>().fitnessText;
	}
	void Update()
	{
		if (vehicle.GetComponent<vehicleCtl>().newSeg && vehicle.GetComponent<vehicleCtl>().segment == checkPoint-1)
		{
			//Debug.Log("ENTERED SEGMENT");
			distance2D = coll.Distance(vehicle.GetComponent<Collider2D>());
		}
		if (vehicle.GetComponent<vehicleCtl>().dead && vehicle.GetComponent<vehicleCtl>().segment == checkPoint - 1 && vehicle.GetComponent<vehicleCtl>().noSave == false)
		{
			fitDist = 1 - (curDist / distance2D.distance);
			if(fitDist < 0) {
				fitDist = 0;
			}
			fitDist = fitDist / 9;
			fitDist = fitDist + 9*(checkPoint-1);
			fitTime = Time.time - vehicle.GetComponent<vehicleCtl>().startTime;
			//Debug.Log(fitDist);
			//Debug.Log(fitTime);
			fitSpeed = fitDist/fitTime;
			fitness = fitSpeed * fitDist * fitDist * fitDist;
			if(fitness < 0) {
				fitness = 0;
			}
			fitness = fitness / 9;
			Debug.Log(fitness);
			fitnessText.GetComponent<updateFitnessDisp>().updateDisp(fitness);
			writetofile.append(fitness.ToString(), "_fitnessScores");
		}
		if (vehicle.GetComponent<vehicleCtl>().segment == checkPoint - 1)
        {
            curDist = coll.Distance(vehicle.GetComponent<Collider2D>()).distance;
        }
	}
}
