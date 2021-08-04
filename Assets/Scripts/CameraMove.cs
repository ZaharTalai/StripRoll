using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private bool finish;
    public Transform vinPose;

    public float distance;
    public float heigth;
    public float speed;

    [HideInInspector]
    public bool lookAt;


    void Start()
    {
        finish = false;
       // vinPose.localPosition = transform.localPosition + Vector3.forward;
       // vinPose.localRotation = transform.localRotation * new Quaternion(0, 180, 0, 0);
    }

    void Update()
    {
        if (finish)
        {
            transform.position = Vector3.MoveTowards(transform.position, vinPose.position, Time.deltaTime * 3.5f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, vinPose.rotation, 2f);
        }
        else
        {
            Vector3 pos = player.transform.position;
            pos.y = Mathf.Clamp(pos.y + heigth, 3f, 10f);
            pos.z -= distance;
            pos.x = transform.position.x;
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
        }

        if (lookAt)
            transform.LookAt(player.transform);
    }
}
