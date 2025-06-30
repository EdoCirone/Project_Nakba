using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    public Vector2 direction;
    public Rigidbody2D rb;
    public float speed = 3f;
    public float waitTime = 2f;
    public bool loopWaypoint = true;
    public Transform waypointParent;

    private List<Transform> waypoints = new List<Transform>();
    protected IReadOnlyList<Transform> Waypoints => waypoints;

    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (waypointParent != null)
        {
            foreach (Transform child in waypointParent)
            {
                waypoints.Add(child);
            }
        }
    }

    void Update()
    {
        if (!isWaiting && waypoints.Count > 0)
        {
            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        currentWaypointIndex++;

        if (currentWaypointIndex >= waypoints.Count)
        {
            currentWaypointIndex = loopWaypoint ? 0 : waypoints.Count - 1;
        }

        isWaiting = false;
    }
}

