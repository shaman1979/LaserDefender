using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField]
    private List<Transform> waypoints;

    [SerializeField]
    private float moveSpeed;

    private int waypointIndex = 0;
    
    void Start()
    {
        transform.position = waypoints[waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementhThisFrame = moveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementhThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
