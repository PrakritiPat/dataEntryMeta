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

        // Write the initial row for object name, position X, position Y, position Z
        outStream.WriteLine("Object Name,Position X,Position Y,Position Z");

        // Start tracking position changes
        StartCoroutine(TrackPosition());
    }

    IEnumerator TrackPosition()
    {
        // Track positions indefinitely
        while (true)
        {
            // Get all GameObjects in the scene
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

            // Iterate over each object
            foreach (GameObject obj in allObjects)
            {
                // Get the object's name and position
                string objectName = obj.name;
                Vector3 position = obj.transform.position;

                // Write the object's name and position to the CSV file
                outStream.WriteLine(objectName + "," + position.x + "," + position.y + "," + position.z);
            }

            // Wait for some time before tracking positions again
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

/* original code izzy changed 4/29
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class datacollect : MonoBehaviour
{

    [SerializeField] List<string[]> rowData = new List<string[]>();
    [SerializeField] Transform objectTransform; // Reference to the object's transform
    [SerializeField] List<Transform> transformList;

    // Use this for initialization
    void Start()
    {
        //fill list of all transforms we're tracking

        // Creating First row of titles manually..
        //loop through the list of transforms to see how many inputs we need (position xyz, rotation xyz)

        string[] rowDataTemp = new string[3]; // Modified to hold XYZ coordinates
        rowDataTemp[0] = "X";
        rowDataTemp[1] = "Y";
        rowDataTemp[2] = "Z";
        rowData.Add(rowDataTemp);

    }

    private void Update()
    {
        Save();
    }

    void Save()
    {
        string filePath = getPath();

        // Check if file already exists
        if (File.Exists(filePath))
        {
            // If file exists, do nothing and exit the method
            return;
        }

        string[] rowDataTemp = new string[3];
        // Collect and save the XYZ coordinates
        rowDataTemp = new string[3];
        for (int i = 0; i < transformList.Count; i++)
        {
            Vector3 position = objectTransform.position; // Get object's position
            rowDataTemp[0] = position.x.ToString(); // X coordinate
            rowDataTemp[1] = position.y.ToString(); // Y coordinate
            rowDataTemp[2] = position.z.ToString(); // Z coordinate
            rowData.Add(rowDataTemp);
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
            Debug.Log(rowData[i]);
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        // Write data to file
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
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
} 
 */