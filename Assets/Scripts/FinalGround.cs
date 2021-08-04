using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGround : MonoBehaviour
{
    public GameObject greyFlour;
    public GameObject activeFlour;

    public void Activate()
    {
        greyFlour.SetActive(false);
        activeFlour.SetActive(true);
    }
}
