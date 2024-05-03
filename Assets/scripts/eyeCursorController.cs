
/*
using UnityEngine;
using System.Collections;
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
        writer = new StreamWriter(filePath);
        //record how wide and tall the screen is
        writer.WriteLine("3dX,3dY,3dZ,2dX.2dY");

        // Check if the main camera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Get the cursor position in screen space
        Vector3 cursorPosition = Input.mousePosition;

        // Convert screen space position to world space
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(cursorPosition.x, cursorPosition.y, mainCamera.nearClipPlane));

        // Save data to CSV
        //if(writer!=null)
            writer.WriteLine($"{worldPosition.x},{worldPosition.y},{worldPosition.z}, {cursorPosition.x}, {cursorPosition.y}");
        Debug.Log("2D: " + cursorPosition.ToString() + ", 3D: " +worldPosition.x.ToString()+" "+ worldPosition.y.ToString() + " " + worldPosition.z.ToString());
        //cursor.transform.position = worldPosition;
        // Check if the cursor GameObject is not null before accessing it
        if (cursor != null)
        {
            cursor.transform.position = worldPosition;
        }
    }

    void OnDestroy()
    {
        // Close the StreamWriter when the script is destroyed
        if (writer != null)
        {
            writer.Close();
        }
    }
}



*/

using UnityEngine;
using System.Collections;
using System.IO;

public class EyeCursorController : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public string folderName = "EyeCursor";
    public string fileName = "EyeGazeData.csv";
    private StreamWriter writer;
    public GameObject cursor;
    public LayerMask raycastMask; // Specify the layers the raycast should hit

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
        writer = new StreamWriter(filePath);
        // Record how wide and tall the screen is
        writer.WriteLine("3dX,3dY,3dZ,2dX.2dY");

        // Check if the main camera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Cast a ray from the camera to determine the gaze direction
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastMask))
        {
            // Save data to CSV
            Vector3 worldPosition = hit.point;
            Vector2 cursorPosition = Input.mousePosition;
            writer.WriteLine($"{worldPosition.x},{worldPosition.y},{worldPosition.z}, {cursorPosition.x}, {cursorPosition.y}");

            // Move the cursor to the hit point
            if (cursor != null)
            {
                cursor.transform.position = worldPosition;
            }
        }
    }

    void OnDestroy()
    {
        // Close the StreamWriter when the script is destroyed
        if (writer != null)
        {
            writer.Close();
        }
    }
}
