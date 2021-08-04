using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    Vector2 startPos;
    Camera cam;
    public float sensitive;

    private float targetPosition;

    public float speedMove;
    private float jumpPower;

    private float gravityforce;
    private Vector3 moveVector;

    private CharacterController ch_controller;
    private Animator ch_animator;

    private bool finish;

    void Start()
    {
        cam =GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ch_controller = GetComponent<CharacterController>();

        targetPosition = transform.position.x;
    }

    void Update()
    {
        if (!finish)
            CharacterMove();
        else
            ch_controller.Move(new Vector3(0, gravityforce, speedMove) * Time.deltaTime);
        GamingGravity();
    }

    private void CharacterMove()
    {
        float pos = 0f;
        if (Input.GetMouseButtonDown(0))
        {
            startPos = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            pos = cam.ScreenToViewportPoint(Input.mousePosition).x - startPos.x;
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x + pos * sensitive, -1.25f, 1.25f), transform.position.y, transform.position.z);
            startPos = cam.ScreenToViewportPoint(Input.mousePosition);
            //print(pos * sensitive);
            //targetPosition = Mathf.Clamp(transform.position.x + pos, -1.25f, 1.25f);
        }

        moveVector = Vector3.zero;
        if (transform.position.x > Mathf.Clamp(transform.position.x + pos, -1.25f, 1.25f))
            moveVector.x = -sensitive;
        else if (transform.position.x < Mathf.Clamp(transform.position.x + pos, -1.25f, 1.25f))
            moveVector.x = sensitive;

        moveVector.z = speedMove;

        /*if(Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }*/

        moveVector.y = gravityforce;
        ch_controller.Move(moveVector * Time.deltaTime);
        
        ////////////////////////// move horizontal ////////////////////

       // transform.position = new Vector3(Mathf.Lerp(transform.rotation.x, targetPosition, sensitive * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void GamingGravity()
    {
        if (!ch_controller.isGrounded)
            gravityforce -= 10f * Time.deltaTime;
        else
            gravityforce = -1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Enter");
        if (GetComponent<Control>().enabled) {
            if (other.tag == "FinalGround")
            {
                speedMove += 0.5f;
                if(GetComponent<Math>().rolller.localScale.y > 0.09f)
                    other.GetComponent<FinalGround>().Activate();
            }
            if (other.tag == "Finish")
            {
                finish = true;
                Invoke("DisabelScript", 2f);
                //GetComponent<Control>().enabled = false;
                if (GetComponent<Math>().rolller.localScale.y > 0.09f)
                {
                    GameController.GK.Win();
                    GameController.GK.DestroyGlassesBlocks();
                }
                else
                    GameController.GK.DestroyGlassesBlocks();
            }
            if (other.tag == "DieCollider")
            {
                //GetComponent<Control>().enabled = false;
                GameController.GK.Loose();
            }
            if (other.tag == "Trampoline")
                gravityforce = other.GetComponent<Trampoline>().jumpPower;
        }
    }

    private void DisabelScript()
    {
        GetComponent<Control>().enabled = false;
    }
}
