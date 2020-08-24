using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewEnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 20;
    public float stepDistance = 3;

    public float minDistance;
    float sqrDistFromTarget => Vector2.SqrMagnitude(rb.position - (Vector2)target.position);
    public float accel = 10;
    public Vector2 goalVel;

    public float lookAngle;
    public float turnSpeed = 20;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(FindPath), 0, 0.25f);
    }
    private void FindPath()
    {
        if (sqrDistFromTarget > minDistance * minDistance)
        {
            if (seeker.IsDone())
                seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
        else if (path != null)
        {
            path = null;
        }
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, goalVel, Time.deltaTime * accel);
    }
    void FixedUpdate()
    {
        SearchFOV();
        Pathfind();
    }
    //fix this not working
    private void LookAtPos(Vector3 position) => LookInDir((target.position - position).normalized);
    private void LookInDir(Vector3 dir)
    {
        float newLookAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lookAngle = Mathf.Lerp(lookAngle, newLookAngle, turnSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, 0, lookAngle);
    }

    private void SearchFOV()
    {

    }
    private void Pathfind()
    {
        if (path == null)
        {
            goalVel = Vector2.zero;
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            goalVel = Vector2.zero;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed;

        goalVel = force;

        if (Physics2D.Linecast(transform.position, target.position, LayerMask.NameToLayer("Walls")))
            LookInDir(dir);
        else
            LookAtPos(target.position);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < stepDistance)
            currentWaypoint++;
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

}
