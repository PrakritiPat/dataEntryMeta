using UnityEngine;
using System.Collections;
using System.IO;

/*public class CursorTracker : MonoBehaviour
{
    private StreamWriter csvWriter;
    private string csvFileName = "cursor_coordinates.csv";

    void Start()
    {
        // Create a new CSV file or append to an existing one
        csvWriter = new StreamWriter(Application.dataPath + "/" + csvFileName, true);
        // Write headers if the file is just created
        if (csvWriter.BaseStream.Length == 0)
        {
            csvWriter.WriteLine("Time, X, Y");
        }
    }

    void Update()
    {
        // Get the position of the cursor
        Vector3 cursorPos = Input.mousePosition;
        // Convert screen coordinates to world coordinates if needed
        // cursorPos = Camera.main.ScreenToWorldPoint(cursorPos); 

        // Write the coordinates to the CSV file
        string currentTime = Time.time.ToString();
        string line = currentTime + "," + cursorPos.x.ToString() + "," + cursorPos.y.ToString();
        csvWriter.WriteLine(line);
    }

    void OnDestroy()
    {
        // Close the CSV file when the script is destroyed
        csvWriter.Close();
    }
}




*/



























using UnityEngine;
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
        writer.WriteLine($"{worldPosition.x},{worldPosition.y},{worldPosition.z}, {cursorPosition.x}, {cursorPosition.y}");
        Debug.Log("2D: " + cursorPosition.ToString() + ", 3D: " +worldPosition.x.ToString()+" "+ worldPosition.y.ToString() + " " + worldPosition.z.ToString());
        cursor.transform.position = worldPosition;
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