using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;

    [SerializeField] GameObject laserPrefabs;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    [SerializeField]
    private float projctileSpeed = 10f;
    [SerializeField]
    private float projectileFiringPeriod = 0.1f;

    private Coroutine firingCoroutine;
    void Start()
    {
        SetUpMoveBoundaries();
    }

 

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                laserPrefabs,
                transform.position,
                Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projctileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXpos, newYpos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(Vector3.zero).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(Vector3.right).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(Vector3.zero).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(Vector3.up).y - padding;
    }

}
