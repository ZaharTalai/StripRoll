using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusWall : MonoBehaviour
{
    private TextMesh bonusText;
    public float number;

    public enum OperatonType
    {
        multiply,
        plus,
        subtract,
        divide
    }

    public OperatonType type;

    void Start()
    {
        bonusText = GetComponentInChildren<TextMesh>();
        switch (type)
        {
            case OperatonType.plus:
                bonusText.text = "+ " + number;
                break;
            case OperatonType.multiply:
                bonusText.text = "x " + number;
                break;
            case OperatonType.subtract:
                bonusText.text = "- " + number;
                break;
            case OperatonType.divide:
                bonusText.text = "÷ " + number;
                break;
            default:
                break;
        }
    }

    public void Disactivate()
    {
        GetComponent<MeshRenderer>().enabled = false;
        bonusText.gameObject.SetActive(false);
    }
}
