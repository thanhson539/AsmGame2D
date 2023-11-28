using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform playerTranform;

    public Vector3 playerCameraVector3;
    void Start()
    {
        //Dùng player làm tọa độ gốc
        playerTranform = GameObject.Find("player").transform;
        playerCameraVector3 = playerTranform.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTranform.transform.position - playerCameraVector3;
    }
}
