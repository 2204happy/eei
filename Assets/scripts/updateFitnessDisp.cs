using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateFitnessDisp : MonoBehaviour {
	public Text text;
	public float fitness;
	public void updateDisp(float fitnessNew) {
		text.text = "Last Fitness: " + fitnessNew.ToString();
		fitness = fitnessNew;
	}
}
