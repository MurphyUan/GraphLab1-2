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
    int furthestX = 0;
    int furthestY = 0;

    // GameObjects
    public GameObject Corridor;
    public GameObject Detector;
    public GameObject Bounds;

    // Start is called before the first frame update
    void Start()
    {
        defineBounds();
    }

    public void DrawMaze()
    {
        // Draw Corridor
        DrawCorridor();
        // Move in direction
        MoveDetector();
        // Increment and Record Distance

        // Check for available directions

        // Move Backwards

        // Decrement Distance
    }

    public void TryDirection(int direction)
    {
        //Rotate 90 degrees randomly

        //Check all directions
        for(int repeat = 0; repeat < 4; repeat++){
            // Check if Detector is out of the screen bounds or touching maze
            if(!(!Detector.GetComponenet<Renderer>().isVisible||Physics2D.OverlapCircle(asVector2(Detector.transform.position)) > 0)){
                //Recursion
                DrawMaze();
            }
            // Turn Right
        }
        // Get Original Direction
    }

    public void RecordDistance()
    {
        if(distance > furthestDistance){
            furthestDistance = distance;
            // Update Furthest X & Y
        }
    }

    public static Vector2 asVector2(Vector3 _v){
        return new Vector2(_v.x, _v.y);
    }

    public int rotateObject(int rotation){
        Detector.transform.eulerAngles = Vector3.forward * (rotation * 90);
        return rotation;
    }

    public void DrawCorridor(){
        // Instantiate Corridor
        Instantiate(Corridor, Detector.transform.position, Quaternion.Euler(0f, 0f, Detector.transform.eulerAngles.z));
    }

    public void MoveDetector(){

    }

    public void defineBounds(){
        Camera.main.orthographicSize = MazeSize / 2;
    }
}
