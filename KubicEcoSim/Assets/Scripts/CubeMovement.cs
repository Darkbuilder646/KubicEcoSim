using System.Collections;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    private bool isMoving = false;

    private void Update() {
        if(isMoving) return;

        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            StartCoroutine(Roll(Vector3.right));
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            StartCoroutine(Roll(Vector3.left));
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            StartCoroutine(Roll(Vector3.forward));
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            StartCoroutine(Roll(Vector3.back));
        }
    }

    IEnumerator Roll(Vector3 direction) {
        isMoving = true; 

        float remainingAngle = 90;
        Vector3 rotationCenter = GetRotationCenter(direction);
        Vector3 rotiationAxis = Vector3.Cross(Vector3.up, direction);
        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotiationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        isMoving = false;
    }

    private Vector3 GetRotationCenter(Vector3 direction) {
        Vector3 halfScale = transform.localScale / 2f;
        return transform.position + (direction * halfScale.x) + (Vector3.down * halfScale.y);
    }
    
}
