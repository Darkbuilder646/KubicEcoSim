using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCubeControler : MonoBehaviour
{
    private Vector3 frontSide, leftSide, rightSide, backSide;
    private GameObject gameObjectCube;

    void Start()
    {
        gameObjectCube = gameObject.GetComponentInChildren<BoxCollider>().gameObject;    
    }

    private void Update() 
    {
        frontSide = gameObjectCube.transform.position + Vector3.forward / 2;
        leftSide = gameObjectCube.transform.position + Vector3.left / 2;
        rightSide = gameObjectCube.transform.position + Vector3.right / 2;
        backSide = gameObjectCube.transform.position + Vector3.back / 2;
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
