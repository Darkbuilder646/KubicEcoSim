using System.Collections;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    private bool isMoving = false;

    private Vector3 frontSide, leftSide, rightSide, backSide;

    private GameObject goRenderer;

    private void Start() 
    {
        goRenderer = gameObject.GetComponentInChildren<BoxCollider>().gameObject;    
    }

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

        frontSide = goRenderer.transform.position + Vector3.forward / 2;
        leftSide = goRenderer.transform.position + Vector3.left / 2;
        rightSide = goRenderer.transform.position + Vector3.right / 2;
        backSide = goRenderer.transform.position + Vector3.back / 2;
    }

    IEnumerator Roll(Vector3 direction) {
        isMoving = true; 

        float remainingAngle = 90;
        Vector3 rotationCenter = GetRotationCenter(direction);
        Vector3 rotiationAxis = Vector3.Cross(Vector3.up, direction);
        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            goRenderer.transform.RotateAround(rotationCenter, rotiationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        isMoving = false;
    }

    private Vector3 GetRotationCenter(Vector3 direction) {
        Vector3 halfScale = goRenderer.transform.lossyScale / 2f;
        return goRenderer.transform.position + (direction * halfScale.x) + (Vector3.down * halfScale.y);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.cyan;    
        Gizmos.DrawSphere(frontSide, 0.2f);
        Gizmos.DrawSphere(leftSide, 0.2f);
        Gizmos.DrawSphere(rightSide, 0.2f);
        Gizmos.DrawSphere(backSide, 0.2f);
        
    }
    
}
