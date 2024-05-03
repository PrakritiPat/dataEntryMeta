using UnityEngine;
using System;
using System.IO;

public class EyeCursorController : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public string folderName = "EyeCursor";
    public string fileName = "EyeGazeData.csv";
    private StreamWriter writer;
    public GameObject cursor;

    void Start()
    {
        // Create the directory if it doesn't exist
        string directoryPath = Path.Combine(Application.dataPath, folderName);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Create/Open the CSV file for writing
        string filePath = Path.Combine(directoryPath, fileName);
        //  writer = new StreamWriter(filePath);
        Debug.Log("File Path: " + filePath);
        writer = new StreamWriter(filePath, true);

        // Record screen dimensions
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        // Record screen dimensions and write header to CSV
        writer.WriteLine($"Timestamp,3dX,3dY,3dZ,2dX,2dY,ScreenWidth,ScreenHeight");
        writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}......" +
                         $"{screenWidth},{screenHeight}");
        writer.Flush();
        // Check if the main camera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Get the current timestamp
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        // Perform raycast from camera into the scene
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            Vector3 gazePosition = hit.point;

            // Save data to CSV with timestamp
            writer.WriteLine($"{timeStamp},{gazePosition.x},{gazePosition.y},{gazePosition.z},{Input.mousePosition.x},{Input.mousePosition.y}," +
                             $"{Screen.width},{Screen.height}");

            Debug.Log($"Timestamp: {timeStamp}, 2D: {Input.mousePosition}, 3D: {gazePosition}");

            // Update cursor position
            if (cursor != null)
            {
                cursor.transform.position = gazePosition;
            }
        }
    }

    void OnDestroy()
    {
        // Close the StreamWriter when the script is destroyed
        if (writer != null)
        {
            writer.Flush();
            writer.Close();
        }
    }
}

