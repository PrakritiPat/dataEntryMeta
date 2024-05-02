using UnityEngine;
using System.IO;

public class EyeCursorController : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public string folderName = "EyeCursor";
    public string fileName = "EyeGazeData.csv";
    private StreamWriter writer;

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
        writer.WriteLine("X,Y,Z");

        // Check if the main camera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Simulated eye gaze data (replace this with actual eye gaze data)
        Vector2 eyeGazePosition = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        // Convert 2D screen space to a ray in world space
        Ray ray = mainCamera.ScreenPointToRay(eyeGazePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Get the point of intersection
            Vector3 intersectionPoint = hit.point;

            // Convert 3D world space to 2D screen space
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(intersectionPoint);

            // Save data to CSV
            writer.WriteLine($"{intersectionPoint.x},{intersectionPoint.y},{intersectionPoint.z}");
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






