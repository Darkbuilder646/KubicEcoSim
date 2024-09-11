using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float gridSize = 1.0f;
    [SerializeField] private float speed = 10;
    private Vector3 targetPos;
    private Rigidbody rb;
    private bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPos = transform.position;
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                targetPos += new Vector3(0, 0, gridSize);
                isMoving = true;
                rb.useGravity = false;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                targetPos += new Vector3(0, 0, -gridSize);
                isMoving = true;
                rb.useGravity = false;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                targetPos += new Vector3(gridSize, 0, 0);
                isMoving = true;
                rb.useGravity = false;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                targetPos += new Vector3(-gridSize, 0, 0);
                isMoving = true;
                rb.useGravity = false;
            }

        }

    }

    private void FixedUpdate()
    {
        if(isMoving) {
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime * speed));

            if(Vector3.Distance(rb.position, targetPos) < 0.01f) {
                isMoving = false;
                rb.useGravity = true;
            }

        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetPos, 0.2f);
    }


}
