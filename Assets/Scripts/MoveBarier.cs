using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarier : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public float step;

    public float delay = 0.0001f;
    [SerializeField]
    private float progress;
    [SerializeField]
    bool moveTo;
    bool invoke;

    private void Start()
    {
        transform.position = startPosition;
        SetMove();
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

            if (moveTo && !invoke)
                progress += step;
            else if(!invoke)
                progress -= step;

            if ((progress >= 1 || progress <= 0) && !invoke)
            {
                Invoke("SetMove", delay);
                print("Invoke");
                invoke = true;
            }
        }
    }

    private void SetMove()
    {
        if (progress >= 1)
            moveTo = false;
        else if (progress <= 0)
            moveTo = true;
        invoke = false;
    }
}
