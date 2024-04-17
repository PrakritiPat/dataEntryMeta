using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class datacollect : MonoBehaviour
{

    public List<string[]> rowData = new List<string[]>();
    public Transform objectTransform; // Reference to the object's transform

    // Use this for initialization
    void Start()
    {
        objectTransform = GetComponent<Transform>(); // Assign the object's transform
        Save();
    }

    private void Update()
    {
        //Save();
    }

    void Save()
    {
        // Creating First row of titles manually..
        string[] rowDataTemp = new string[3]; // Modified to hold XYZ coordinates
        rowDataTemp[0] = "X";
        rowDataTemp[1] = "Y";
        rowDataTemp[2] = "Z";
        rowData.Add(rowDataTemp);

        // Collect and save the XYZ coordinates
        rowDataTemp = new string[3];
        Vector3 position = objectTransform.position; // Get object's position
        rowDataTemp[0] = position.x.ToString(); // X coordinate
        rowDataTemp[1] = position.y.ToString(); // Y coordinate
        rowDataTemp[2] = position.z.ToString(); // Z coordinate
        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
            Debug.Log(rowData[i]);
        }

        

        /*int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();*/
    }


    // Following method is used to retrieve the relative path as device platform
    private string getPath()
    {
        #if UNITY_EDITOR
                return Application.dataPath + "/CSV/" + "Saved_data.csv";
        #elif UNITY_ANDROID
                return Application.persistentDataPath + "Saved_data.csv";
        #elif UNITY_IPHONE
                return Application.persistentDataPath + "/" + "Saved_data.csv";
        #else
                return Application.dataPath + "/" + "Saved_data.csv";
        #endif
    }
}


