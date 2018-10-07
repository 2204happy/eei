using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class evolutionManager : MonoBehaviour {
	public int amntPerGen;
	private int currGen;
	private int uptoInGen;
	public neuralnetwork neuralNetwork;
	public float bestScore;
	public int bestScorer;
	public int bestPrevScorer;
	public updateFitnessDisp fitnessDisp;
	// Use this for initialization
	void Start () {
		currGen = 0;
		uptoInGen = 0;
		bestScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void nextAttempt() {
		saveCurrNetwork();
		nextNetwork();
		Debug.Log("called!");
	}
	void saveCurrNetwork() {
		string saveDir = "/networks/gen"+currGen.ToString()+"_try"+uptoInGen.ToString();
		neuralNetwork.saveNetandFit(saveDir);
		if(fitnessDisp.fitness > bestScore) {
			bestScorer = uptoInGen;
		}
	}
	void nextNetwork() {
		uptoInGen += 1;
		if(uptoInGen >= amntPerGen)
        {
            bestPrevScorer = bestScorer;
            bestScore = 0;
            bestScorer = 0;
            uptoInGen = 0;
			currGen += 1;
			Debug.Log("NEW GEN");
        }
		if(currGen == 0) {
			neuralNetwork.RandomizeNetwork();
		}
		if(currGen != 0) {
			string loadDir = "/networks/gen" + (currGen-1).ToString() + "_try" + bestPrevScorer.ToString();
			//string loadDir = "networks/gen0_try0";
            
			string inFile = File.ReadAllText(loadDir);
			string[] inFileArray = inFile.Split(char.Parse("\n"));
			//string[] inFile = {"a","b","c"};
			neuralNetwork.LoadNetwork(inFileArray);
            //modify
		}

	}
}
