using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CameraController : MonoBehaviour
{
    private Transform startMarker, endMarker;
    public List<Transform> waypoints;
    int currentStartPoint;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    public float smooth = 5.0F;

    public bool isLooping = false;
    [Tooltip("Set this to 0 if you dont want wait time.")]
    public float waitingTime;
 
    void Start()
    {
        //Add an instance of first position as last position so loop can happen
        if(isLooping){
            waypoints.Add(waypoints[0]);
        }
        currentStartPoint = 0;
        SetPoints();
    }
    /// <summary>
    /// Sets the next cube as destination.
    /// </summary>
    void SetPoints()
    {
        startMarker = waypoints[currentStartPoint];
        endMarker = waypoints[currentStartPoint + 1];
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        if (isLooping && currentStartPoint + 2 == waypoints.Count)
        {
            currentStartPoint = -1;
        }
    }
    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        //Starts the lerping with current position and desitnation position.
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        //Starts lerping rotation of the camera from current rotation to destination rotation
        transform.rotation = Quaternion.Lerp(startMarker.rotation, endMarker.rotation, fracJourney);
        if (fracJourney >= 1f && currentStartPoint + 2 < waypoints.Count)
        {
            //Updates the current position
            currentStartPoint++;
            //Will call SetPoints to set the next destination after waitingTime amount of seconds.If waiting time is 0, then will call instantly.
            Invoke("SetPoints",waitingTime);
        }
    }
    /// <summary>
    /// Draws blue line connecting cubes in Editor mode.
    /// </summary>
    [ExecuteInEditMode]
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int x = 0; x < waypoints.Count - 1; x++)
        {
            Vector3 from = new Vector3(waypoints[x].position.x, waypoints[x].position.y, waypoints[x].position.z);
            Vector3 to = new Vector3(waypoints[x + 1].position.x, waypoints[x + 1].position.y, waypoints[x + 1].position.z);
            Gizmos.DrawLine(from, to);
        }

    }
}
