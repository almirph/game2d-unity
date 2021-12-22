using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room Camera
    //[SerializeField] private float speed;
    //[SerializeField] private int ortogonalSize;
    //private float currentPosX;
    //private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform player;

    private void Update()
    {
        //Room Camera
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }

    //public void MoveToNewRoom(Transform _newRoom)
    //{
    //    currentPosX = _newRoom.position.x;
    //}
}
