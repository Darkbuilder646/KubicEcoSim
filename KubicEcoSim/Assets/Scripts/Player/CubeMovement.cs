using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private float snapRange = 1.0f;
    private bool isMoving = false;
    private Vector3 rotationAxis;
    private Vector3 rotationCenter;
    [SerializeField] private GameObject cubeRenderer;

    [SerializeField] private List<Transform> attachedCubes = new List<Transform>();
    private List<Vector3> snapPoints = new List<Vector3>();

    private void Start() {
        cubeRenderer = GetComponentInChildren<BoxCollider>().gameObject;
        
        if(attachedCubes[0] == null) {
            attachedCubes.Add(cubeRenderer.GetComponent<Transform>());    
        }
        UpdateSnapPoints();
    }

    private void Update() {
        if(isMoving) return;

        // Movement
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            StartCoroutine(Roll(Vector3.right));
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            StartCoroutine(Roll(Vector3.left));
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            StartCoroutine(Roll(Vector3.forward));
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            StartCoroutine(Roll(Vector3.back));
        }

        // Snap Action
        if(Input.GetKeyDown(KeyCode.E)) {
            TrySnapToNearbyCube();
        }

        if(Input.GetKeyDown(KeyCode.F)) {
            UnsnapLastCube();
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

        UpdateSnapPoints();
        isMoving = false;
    }

    private Vector3 GetCombinedRotationCenter(Vector3 direction) {
        Vector3 minPos = attachedCubes[0].position;
        Vector3 maxPos = attachedCubes[0].position;

        foreach (Transform cube in attachedCubes) {
            minPos = Vector3.Min(minPos, cube.position);
            maxPos = Vector3.Max(maxPos, cube.position);
        }

        if (direction == Vector3.right || direction == Vector3.left) {
            return new Vector3(direction == Vector3.right ? maxPos.x + 0.5f : minPos.x - 0.5f, minPos.y - 0.5f, minPos.z);
        }
        else {
            return new Vector3(minPos.x, minPos.y - 0.5f, direction == Vector3.forward ? maxPos.z + 0.5f : minPos.z - 0.5f);
        }

    }

    // Try to Snap to nearby cube
    private void TrySnapToNearbyCube() {
        Collider[] hitColliders = Physics.OverlapSphere(cubeRenderer.transform.position, snapRange);

        foreach (var hitCollider in hitColliders) {
            if (hitCollider.transform != transform && !attachedCubes.Contains(hitCollider.transform)) {
                SnapToCube(hitCollider.transform);
                break;
            }
        }
    }

    // Snap on another cube
    public void SnapToCube(Transform newCube) {
        if (!attachedCubes.Contains(newCube)) {
            attachedCubes.Add(newCube);
            UpdateSnapPoints();
        }
    }

    // Unsnap to the last added cube
    private void UnsnapLastCube() {
        if (attachedCubes.Count > 1) {
            Transform lastCube = attachedCubes[attachedCubes.Count - 1];
            attachedCubes.Remove(lastCube);
            UpdateSnapPoints(); 
        }
    }

    // Update list of snap points
    private void UpdateSnapPoints() {
        snapPoints.Clear();
        foreach (Transform cube in attachedCubes) {
            AddSnapPointsForCube(cube);
        }
        RemoveOverlappingSnapPoints();
    }

    // Add all snap point of cube
    private void AddSnapPointsForCube(Transform cube) {
        float halfScale = cube.localScale.x / 2;
        snapPoints.Add(cube.position + Vector3.forward * halfScale);
        snapPoints.Add(cube.position + Vector3.back * halfScale);
        snapPoints.Add(cube.position + Vector3.left * halfScale);
        snapPoints.Add(cube.position + Vector3.right * halfScale);
        snapPoints.Add(cube.position + Vector3.up * halfScale);
        snapPoints.Add(cube.position + Vector3.down * halfScale);
    }

    // Remove overlapping snap points
    private void RemoveOverlappingSnapPoints() {
        List<Vector3> uniquePoints = new List<Vector3>();
        foreach (var point in snapPoints) {
            bool isOverlapping = false;
            foreach (var otherPoint in uniquePoints) {
                if (Vector3.Distance(point, otherPoint) < 0.01f) {
                    isOverlapping = true;
                    break;
                }
            }
            if (!isOverlapping) {
                uniquePoints.Add(point);
            }
        }
        snapPoints = uniquePoints;
    }


    //* Debug & Gizmo
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rotationCenter, 0.15f); //? RotationCenter for all the rotation

        Gizmos.color = Color.cyan;
        foreach (var point in snapPoints) {
            Gizmos.DrawSphere(point, 0.1f); //? All Snap points
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(cubeRenderer.transform.position, snapRange); //? Sphere for snap points
        
    }
    
}
