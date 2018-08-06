using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateFitnessDisp : MonoBehaviour {
	public Text text;

	public void updateDisp(float fitness) {
		text.text = "Last Fitness: " + fitness.ToString();
		Debug.Log("gothereatleast");
	}
}
