using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math : MonoBehaviour
{
    private BonusWall[] bonusWalls = new BonusWall[2];

    public Transform rolller;
    public Transform rollerCylinder;
    public Transform player;
    public GameObject text;

    public float rollerSizeY;

    public float sizeSpeed;

    private bool fall;
    [SerializeField]
    private bool finish;

    void Start()
    {
        rollerSizeY = rolller.localScale.y;
    }

    void Update()
    {
        if (bonusWalls[0] != null)
        {
            CompareResults();
        }

        if (rolller.localScale.y - 0.5f != rollerSizeY + 0.5f) {
            float rollerY = Mathf.Lerp(rolller.localScale.y, rollerSizeY + 0.5f, Time.deltaTime * (fall ? sizeSpeed / 2: sizeSpeed));
            rolller.localScale = new Vector3(1f, rollerY, rollerY);
            rolller.localPosition = new Vector3(0f, (rollerY - 1) / 2f, 0f);
            rollerCylinder.localPosition = rolller.localPosition;
            player.localPosition = new Vector3(0f, rollerCylinder.localPosition.y + (rollerY / 2f), 0f);
            text.transform.localPosition = new Vector3(0f, rolller.localPosition.y, -(rollerY / 2f));
            text.GetComponent<TextMesh>().text = string.Format("{0:N1}", rollerY - 0.5f); 
        }

        if (rolller.localScale.y - 0.45f <= 0.09 && !finish)
            //print("Loose");
            GameController.GK.Loose();
        else if (rolller.localScale.y - 0.45f <= 0.09f && finish)
        {
            print("Stop");
            GameController.GK.Win();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Math>().enabled)
        {
            if (other.tag == "MathTab")
            {
                if (bonusWalls[0] == null)
                    bonusWalls[0] = other.GetComponent<BonusWall>();
                else
                    bonusWalls[1] = other.GetComponent<BonusWall>();
            }
            if (other.tag == "MathFall")
            {
                rollerSizeY = Mathf.Clamp(rollerSizeY - other.GetComponent<BonusWall>().number, 0f, 10f);
                fall = true;
            }
            if (other.tag == "FinalGround")
            {
                finish = true;
                rollerSizeY = Mathf.Clamp(rollerSizeY - 0.8f, 0f, 10f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MathFall")
            fall = false;
    }

    private void CompareResults()
    {
        if(bonusWalls[1] == null)
        {
            AddResults(bonusWalls[0]);
        }
        else if (bonusWalls[0].type < bonusWalls[1].type)
        {
            AddResults(bonusWalls[0]);
        }
        else if (bonusWalls[0].type > bonusWalls[1].type)
        {
            AddResults(bonusWalls[1]);
        }
        else if (bonusWalls[0].type == bonusWalls[1].type)
        {
            switch (bonusWalls[0].type)
            {
                case BonusWall.OperatonType.plus:
                    if (bonusWalls[0].number > bonusWalls[1].number)
                        AddResults(bonusWalls[0]);
                    else
                        AddResults(bonusWalls[1]);
                    break;
                case BonusWall.OperatonType.multiply:
                    if (bonusWalls[0].number > bonusWalls[1].number)
                        AddResults(bonusWalls[0]);
                    else
                        AddResults(bonusWalls[1]);
                    break;
                case BonusWall.OperatonType.subtract:
                    if (bonusWalls[0].number > bonusWalls[1].number)
                        AddResults(bonusWalls[1]);
                    else
                        AddResults(bonusWalls[0]);
                    break;
                case BonusWall.OperatonType.divide:
                    if (bonusWalls[0].number > bonusWalls[1].number)
                        AddResults(bonusWalls[1]);
                    else
                        AddResults(bonusWalls[0]);
                    break;
                default:
                    break;
            }
        }
        ////////////////////////////////////////////
        bonusWalls[0] = null;
        bonusWalls[1] = null;
    }

    private void AddResults(BonusWall bonusWall)
    {
        switch (bonusWall.type)
        {
            case BonusWall.OperatonType.plus:
                print("plus");
                rollerSizeY = Mathf.Clamp(rollerSizeY + bonusWall.number, 0, 10);
                bonusWall.Disactivate();
                break;
            case BonusWall.OperatonType.multiply:
                print("multiply");
                rollerSizeY = Mathf.Clamp(rollerSizeY * bonusWall.number, 0, 10);
                bonusWall.Disactivate();
                break;
            case BonusWall.OperatonType.subtract:
                print("substract");
                rollerSizeY = Mathf.Clamp(rollerSizeY - bonusWall.number, 0, 10);
                bonusWall.Disactivate();
                break;
            case BonusWall.OperatonType.divide:
                print("Divide");
                rollerSizeY = Mathf.Clamp(rollerSizeY / bonusWall.number, 0, 10);
                bonusWall.Disactivate();
                break;
            default:
                break;
        }
    }
}
