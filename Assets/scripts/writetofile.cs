using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//Script Courtesy of Joseph Davies

public class writetofile : MonoBehaviour
{

	public static string write(string _input, string _file_path = "/Files/Output.txt")
	{
		using (StreamWriter outputFile = new StreamWriter(Application.dataPath + _file_path))
		{
			outputFile.WriteLine(_input);
		}
		return _input;
	}

	public static string append(string _input, string _file_path = "/Files/Output.txt")
	{
		using (StreamWriter outputFile = new StreamWriter(Application.dataPath + _file_path, true))
		{
			outputFile.WriteLine(_input);
		}
		return _input;
	}
	public static string read(string _file_path = "/Files/Output.txt")
    {
        using (StreamReader inputFile = new StreamReader(Application.dataPath + _file_path))
        {
			string data = inputFile.ReadToEnd();
			return data;
        }
        
    }
}
