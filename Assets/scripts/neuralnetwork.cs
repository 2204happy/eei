using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neuralnetwork : MonoBehaviour {
	public int layer1amnt;
	public int layer2amnt;
	public raycast frontSensor;
	public raycast lSen1;
	public raycast rSen1;
	public raycast lSen2;
	public raycast rSen2;
	public vehicleCtl vehicleCtl;
	public float accThreshold;
	public float brakeThreshold;
	public float turnThreshold;
	public float maxMinWeight;
	public float maxMinBias;
	public updateFitnessDisp fitnessDisp;
	private float[] neuronLayer1Biases = new float[32];
	private float[] neuronLayer2Biases = new float[32];
	private float[] outputLayerBiases = new float[4];
	private float[,] neuronLayer1Weights = new float[32, 5];
	private float[,] neuronLayer2Weights = new float[32, 32];
	private float[,] outputLayerWeights = new float[4, 32];
	private float[] neuronLayer1 = new float[32];
	private float[] neuronLayer2 = new float[32];
	private float[] outputLayer = new float[4];
	private float[] inputLayer = new float[5];
	private int upto;
	private int upto2;
	private bool justHeldR;

	// Use this for initialization
	void Start () {
		RandomizeNetwork();
		justHeldR = false;
	}
	public void RandomizeNetwork() {
		upto = 0;
		while(upto < layer1amnt) {
			neuronLayer1Biases[upto] = Random.Range(-maxMinBias, maxMinBias);
			upto2 = 0;
			while (upto2 < 5) {
				neuronLayer1Weights[upto, upto2] = Random.Range(-maxMinWeight, maxMinWeight);
				upto2 += 1;
			}
			upto += 1;
		}
		upto = 0;
		while(upto < layer2amnt) {
			neuronLayer2Biases[upto] = Random.Range(-maxMinBias, maxMinBias);
			upto2 = 0;
			while(upto2 < layer1amnt) {
				neuronLayer2Weights[upto, upto2] = Random.Range(-maxMinWeight, maxMinWeight);
				upto2 += 1;
			}
			upto += 1;
		}
		upto = 0;
		while(upto < 4) {
			outputLayerBiases[upto] = Random.Range(-maxMinBias, maxMinBias);
			upto2 = 0;
			while(upto2 < layer2amnt) {
				outputLayerWeights[upto, upto2] = Random.Range(-maxMinWeight, maxMinWeight);
				upto2 += 1;
			}
			upto += 1;
		}
	}
	public void LoadNetwork(string[] inputFile)
    {
        upto = 0;
		int inputUpto = 0;
        while (upto < layer1amnt)
        {
			neuronLayer1Biases[upto] = float.Parse(inputFile[inputUpto]);
			inputUpto += 1;
            upto2 = 0;
            while (upto2 < 5)
            {
				neuronLayer1Weights[upto, upto2] = float.Parse(inputFile[inputUpto]);
                inputUpto += 1;
                upto2 += 1;
            }
            upto += 1;
        }
        upto = 0;
        while (upto < layer2amnt)
        {
			neuronLayer2Biases[upto] = float.Parse(inputFile[inputUpto]);
            inputUpto += 1;
            upto2 = 0;
            while (upto2 < layer1amnt)
            {
				neuronLayer2Weights[upto, upto2] = float.Parse(inputFile[inputUpto]);
                inputUpto += 1;
                upto2 += 1;
            }
            upto += 1;
        }
        upto = 0;
        while (upto < 4)
        {
			outputLayerBiases[upto] = float.Parse(inputFile[inputUpto]);
            inputUpto += 1;
            upto2 = 0;
            while (upto2 < layer2amnt)
            {
				outputLayerWeights[upto, upto2] = float.Parse(inputFile[inputUpto]);
                inputUpto += 1;
                upto2 += 1;
            }
            upto += 1;
        }
    }

	// Update is called once per frame
	void Update() {
		UpdateNeurons();
		DoOutputs();
		if(Input.GetKey(KeyCode.R) && !justHeldR) {
            justHeldR = true;
			debugResetNetworkandGame();
		}
		if(!Input.GetKey(KeyCode.R) && justHeldR) {
			justHeldR = false;
		}
	}

	public void saveNetandFit(string saveTo) {
		saveNetwork(saveTo);
		string fitToWrite = fitnessDisp.fitness.ToString();
		writetofile.append(fitToWrite, saveTo);
		Debug.Log("Saved Network with score of " + fitToWrite);
	}

	void debugResetNetworkandGame() {
		saveNetandFit("test");
		vehicleCtl.dead = true;
        vehicleCtl.resetVehicle();
        RandomizeNetwork();
	}

	void saveNetwork(string location) {
		bool firstSave = true;
		upto = 0;
		while(upto < layer1amnt) {
			if(firstSave) {
				writetofile.write(neuronLayer1Biases[upto].ToString(), location);
				firstSave = false;
			}
			else {
                writetofile.append(neuronLayer1Biases[upto].ToString(), location);
			}
            upto2 = 0;
			while(upto2 < 5) {
				writetofile.append(neuronLayer1Weights[upto, upto2].ToString(),location);
				upto2 += 1;
			}
			upto += 1;
		}
		upto = 0;
		while(upto < layer2amnt) {
			writetofile.append(neuronLayer2Biases[upto].ToString(), location);
			upto2 = 0;
			while(upto2 < layer1amnt) {
				writetofile.append(neuronLayer2Weights[upto, upto2].ToString(), location);
				upto2 += 1;
			}
			upto += 1;
		}
		upto = 0;
		while(upto < 4) {
			writetofile.append(outputLayerBiases[upto].ToString(), location);
			upto2 = 0;
			while(upto2 < layer2amnt) {
				writetofile.append(outputLayerWeights[upto, upto2].ToString(), location);
				upto2 += 1;
			}
			upto += 1;
		}
	}

	void UpdateNeurons () {
		inputLayer[0] = frontSensor.findist;
		inputLayer[1] = lSen1.findist;
		inputLayer[2] = rSen1.findist;
		inputLayer[3] = lSen1.findist;
		inputLayer[4] = rSen2.findist;
		upto = 0;
		while(upto < layer1amnt) {
			upto2 = 0;
			neuronLayer1[upto] = 0;
			while(upto2 < 5) {
				neuronLayer1[upto] += (neuronLayer1Weights[upto, upto2] * inputLayer[upto2]);
				upto2 += 1;
			}
			neuronLayer1[upto] += neuronLayer1Biases[upto];
			neuronLayer1[upto] = Sigmoid(neuronLayer1[upto]);
			upto += 1;
		}
		upto = 0;
		while(upto < layer2amnt) {
			upto2 = 0;
			neuronLayer2[upto] = 0;
			while(upto2 < layer1amnt) {
				neuronLayer2[upto] += (neuronLayer2Weights[upto, upto2] * neuronLayer1[upto2]);
				upto2 += 1;
			}
			neuronLayer2[upto] += neuronLayer2Biases[upto];
			neuronLayer2[upto] = Sigmoid(neuronLayer2[upto]);
			upto += 1;
		}
		upto = 0;
		while(upto < 4) {
			upto2 = 0;
			outputLayer[upto] = 0;
			while(upto2 < layer2amnt) {
				outputLayer[upto] += (outputLayerWeights[upto, upto2] * neuronLayer2[upto2]);
				upto2 += 1;
			}
			outputLayer[upto] += outputLayerBiases[upto];
			outputLayer[upto] = Sigmoid(outputLayer[upto]);
			upto += 1;
		}
	}

	void DoOutputs() {
		if(outputLayer[0] >= accThreshold) {
			vehicleCtl.accelerate();
		}
		if(outputLayer[1] >= brakeThreshold) {
			vehicleCtl.brake();
		}
		if(outputLayer[2] >= turnThreshold) {
			vehicleCtl.turnLeft();
		}
		if(outputLayer[3] >= turnThreshold) {
			vehicleCtl.turnRight();
		}
	}

	float Sigmoid(float x) {
		float sig = 1 / (1 + Mathf.Exp(-x));
		return sig;
	}
}
