using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    private bool isMoving = false;

    [SerializeField] private List<Transform> attachedCubes = new List<Transform>();
    private Vector3 rotationAxis;
    private Vector3 rotationCenter;

    // private GameObject goRenderer;

    private void Start() {
        // goRenderer = gameObject.GetComponentInChildren<BoxCollider>().gameObject;
        // attachedCubes.Add(gameObject.GetComponentInChildren<Transform>().transform);    
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

    }

    IEnumerator Roll(Vector3 direction) {
        isMoving = true; 

        rotationCenter = GetCombinedRotationCenter(direction);
        rotationAxis = Vector3.Cross(Vector3.up, direction);
        float remainingAngle = 90;

        while (remainingAngle > 0) {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            
            foreach (Transform cube in attachedCubes) {
                cube.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            }
            remainingAngle -= rotationAngle;
            yield return null;
        }

        isMoving = false;
    }

    private Vector3 GetCombinedRotationCenter(Vector3 direction) {
        // Vector3 combinedCenter = Vector3.zero;
        // foreach (Transform cube in attachedCubes) {
        //     Vector3 halfScale = cube.lossyScale / 2f;
        //     combinedCenter += cube.position + (direction * halfScale.x) + (Vector3.down * halfScale.y);
        // }
        // return combinedCenter / attachedCubes.Count;

        Vector3 minPos = attachedCubes[0].position;
        Vector3 maxPos = attachedCubes[0].position;

        foreach (Transform cube in attachedCubes) {
            minPos = Vector3.Min(minPos, cube.position);
            maxPos = Vector3.Max(maxPos, cube.position);
        }

        // Si on bouge vers la droite ou gauche, on ajuste le centre sur la face gauche ou droite
        if (direction == Vector3.right || direction == Vector3.left) {
            return new Vector3(direction == Vector3.right ? maxPos.x + 0.5f : minPos.x - 0.5f, minPos.y - 0.5f, minPos.z);
        }
        // Si on bouge vers l'avant ou l'arrière, on ajuste le centre sur la face avant ou arrière
        else {
            return new Vector3(minPos.x, minPos.y - 0.5f, direction == Vector3.forward ? maxPos.z + 0.5f : minPos.z - 0.5f);
        }

    }

    public void SnapToCube(Transform newCube) {
        if (!attachedCubes.Contains(newCube)) {
            attachedCubes.Add(newCube);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rotationCenter, 0.15f); //? RotationCenter for all the rotation
        
    }
    
}
