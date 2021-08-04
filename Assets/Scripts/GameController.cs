using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public static GameController GK;

    public GameObject RestartButton;
    public GameObject StartButton;

    public Rigidbody[] playerRb;
    public GameObject[] borders;

    private void Start()
    {
        GK = GetComponent<GameController>();
        Time.timeScale = 0f;
    }

    public void Loose()
    {
        Invoke(nameof(ShowRestartMenu), 1f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<Control>().enabled = false;
        player.GetComponent<Math>().enabled = false;


        GameObject cam =  GameObject.FindGameObjectWithTag("MainCamera");
        cam.GetComponent<CameraMove>().player = playerRb[0].gameObject;
        cam.GetComponent<CameraMove>().lookAt = true;

        foreach (GameObject go in borders)
        {
            go.SetActive(false);
        }
        foreach (Rigidbody go in playerRb)
        {
            go.isKinematic = false;
            go.useGravity = true;
        }
        playerRb[0].AddForce(Vector3.forward * 100f, ForceMode.Impulse);
        print("Looose");
    }

    public void Win()
    {
        RestartButton.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        StartButton.SetActive(false);
    }

    private void ShowRestartMenu()
    {
        RestartButton.SetActive(true);
    }
}
