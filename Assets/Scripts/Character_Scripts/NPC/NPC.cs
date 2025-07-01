using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    public Vector2 direction;
    public Rigidbody2D rb;
    public float speed = 3f;
    public float waitTime = 2f;
    public bool loopWaypoint = true;
    public float detectionRange = 5f;
    public Transform waypointParent;

    protected List<Transform> waypoints = new List<Transform>();
    protected IReadOnlyList<Transform> Waypoints => waypoints;

    protected int currentWaypointIndex = 0;
    protected bool isWaiting = false;

    protected Aid closestAid;

    protected virtual void Start()
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

    protected virtual void Update()
    {
        closestAid = FindClosestAid();

        if (closestAid != null)
        {
            float distanceToAid = Vector2.Distance(transform.position, closestAid.transform.position);

            if (distanceToAid <= detectionRange)
            {
                ChaseAid(closestAid);
                return;
            }
        }

        if (!isWaiting && waypoints.Count > 0)
        {
            MoveToWaypoint();
        }
    }

    protected virtual void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    protected virtual IEnumerator WaitAtWaypoint()
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

    protected virtual void ChaseAid(Aid aid)
    {
        transform.position = Vector2.MoveTowards(transform.position, aid.transform.position, speed * Time.deltaTime);
    }

    protected virtual Aid FindClosestAid()
    {
        Aid[] allAids = Object.FindObjectsByType<Aid>(FindObjectsSortMode.InstanceID);
        Aid closest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Aid aid in allAids)
        {
            float dist = Vector2.Distance(transform.position, aid.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                closest = aid;
            }
        }

        return closest;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Aid>())
        {
            Destroy(other.gameObject);
        }

        if (other.GetComponent<SpecialAid>())
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}



