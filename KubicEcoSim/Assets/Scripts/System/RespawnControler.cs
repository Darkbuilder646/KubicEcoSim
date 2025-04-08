using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 actifSpawnPoint;
    public Vector3 ActifSpawnPoint { get => actifSpawnPoint; set => ActifSpawnPoint = value; }

    void Start()
    {
        actifSpawnPoint = player.transform.position;
    }

    void Update()
    {
        Debug.Log(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered: " + actifSpawnPoint);
        if(other.gameObject.layer == 7)
        {
            player.transform.position = actifSpawnPoint;
        }
    }



}
