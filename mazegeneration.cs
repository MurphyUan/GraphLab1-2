using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    // Maze Variables
    public int MazeSize = 8;

    // Player Spawn / Max Distance Maze Reaches
    int distance = 0;
    int furthestDistance = 0;
    float furthestX = 0;
    float furthestY = 0;

    // GameObjects
    public GameObject Corridor;
    public GameObject Detector;
    public GameObject Bounds;


    // Start is called before the first frame update
    void Start()
    {
        // Define Play Area
        DefineEnvironment();

        // Pick Random Direction
        RotateDetector(Random.Range(-1,3) * 90);

        // Start Recursive Draw
        DrawMaze();

        // Draw X @ end locaion

        // Make Game Playable
    }

    public void DrawMaze()
    {
        // Draw Corridor
        DrawCorridor();

        // Move in direction
        MoveDetector(1);

        // Increment and Record Distance
        RecordDistance();

        // Check for available directions
        TryDirection(Detector.transform.eulerAngles.z);

        // Move Backwards
        MoveDetector(-1);

        // Decrement Distance
        distance--;
    }

    public void TryDirection(float direction)
    {
        //Rotate 90 degrees randomly
        RotateDetector((Random.Range(-1,2) * 90));

        //Check all directions
        for(int repeat = 0; repeat < 4; repeat++){

            Collider2D collider = Physics2D.OverlapCircle(asVector2(Detector.transform.GetChild(0).transform.position),0.5f);
            // Check if Detector is out of the screen bounds or touching maze
            if(!(collider == null || collider.tag == "maze")){
                //Recursion
                DrawMaze();
            }
            // Turn Right
            RotateDetector(Detector.transform.eulerAngles.z - 90);
        }
        // Get Original Direction
        RotateDetector(direction);
    }

    public void RecordDistance()
    {
        distance++;
        if(distance > furthestDistance){
            furthestDistance = distance;
            furthestX = Detector.transform.position.x;
            furthestY = Detector.transform.position.y;
        }
    }

    public static Vector2 asVector2(Vector3 _v){
        return new Vector2(_v.x, _v.y);
    }

    public void RotateDetector(float rotation){
        Detector.transform.eulerAngles = new Vector3(0, 0, rotation);
    }

    public void DrawCorridor(){
        // Instantiate Corridor
        Instantiate(Corridor, Detector.transform.position, Quaternion.Euler(0f, 0f, Detector.transform.eulerAngles.z));
    }

    public void MoveDetector(int direction){
        Detector.transform.Translate((Vector3.right * direction) * 3);
    }

    public void DefineEnvironment(){
        // Define new Bounds
        Bounds = Instantiate(Bounds);
        Camera.main.orthographicSize = MazeSize;

        float boundsHeight = Camera.main.orthographicSize * 2f;
        float boundsWidth = Camera.main.aspect * boundsHeight;

        Bounds.transform.localScale = new Vector3(boundsWidth - 4, boundsHeight - 4,0);

        //Define new Detector
        Detector = Instantiate(Detector);
    }
}
