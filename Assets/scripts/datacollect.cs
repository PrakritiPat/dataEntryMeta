using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class datacollect : MonoBehaviour
{
    string filePath; // Path to the CSV file
    StreamWriter outStream; // Stream writer to write to the file

    // Use this for initialization
    void Start()
    {
        // Get the file path
        filePath = getPath();

        // Check if file already exists
        if (!File.Exists(filePath))
        {
            // If file doesn't exist, create a new one
            CreateNewFile();
        }
        else
        {
            // If file exists, open it in append mode
            outStream = System.IO.File.AppendText(filePath);
        }

        // Write the initial row for object name, position X, position Y, position Z, rotation X, rotation Y, rotation Z
        outStream.WriteLine("Object Name,Position X,Position Y,Position Z,Rotation X,Rotation Y,Rotation Z");

        // Start tracking position and rotation changes
        StartCoroutine(TrackPositionAndRotation());
    }

    IEnumerator TrackPositionAndRotation()
    {
        // Track positions and rotations indefinitely
        while (true)
        {
            // Get all GameObjects in the scene
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

            // Iterate over each object
            foreach (GameObject obj in allObjects)
            {
                // Get the object's name, position, and rotation
                string objectName = obj.name;
                Vector3 position = obj.transform.position;
                Vector3 rotation = obj.transform.eulerAngles;

                // Write the object's name, position, and rotation to the CSV file
                outStream.WriteLine(objectName + "," + position.x + "," + position.y + "," + position.z + "," +
                                    rotation.x + "," + rotation.y + "," + rotation.z);
            }

            // Wait for some time before tracking positions and rotations again
            yield return new WaitForSeconds(1f); // Adjust this time interval as needed
        }
    }

    // Function to create a new CSV file
    void CreateNewFile()
    {
        // Open a new file at the specified path
        outStream = System.IO.File.CreateText(filePath);
    }

    // Function to get the file path based on the platform
    private string getPath()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss"); // Get current timestamp
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "Saved_data_" + timestamp + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath + "Saved_data_" + timestamp + ".csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath + "/" + "Saved_data_" + timestamp + ".csv";
#else
        return Application.dataPath + "/" + "Saved_data_" + timestamp + ".csv";
#endif
    }

    // Function to handle closing the file stream when the application quits
    void OnApplicationQuit()
    {
        // Close the file stream
        if (outStream != null)
        {
            outStream.Close();
        }
    }
}


