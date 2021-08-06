using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCube : MonoBehaviour
{
    public GameObject[] shards;
    public GameObject mainCube;
    void Start()
    {
        foreach (GameObject go in shards)
        {
            go.SetActive(false);
        }
    }

    public void Destroy()
    {
        mainCube.SetActive(false);
        foreach (GameObject go in shards)
        {
            go.SetActive(true);
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 10f, ForceMode.Impulse);
        }
    }
}
