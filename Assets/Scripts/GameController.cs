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

    public GameObject running;
    private GameObject player;
    private GameObject cam;

    public GameObject[] glaseesBlocs;
    public ParticleSystem[] winParticles;

    private void Start()
    {
        GK = GetComponent<GameController>();
        Time.timeScale = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        foreach (Rigidbody go in playerRb)
        {
            go.isKinematic = true;
            go.useGravity = false;
        }

        foreach (ParticleSystem go in winParticles)
        {
            go.Stop();
        }
    }

    public void Loose()
    {
        Invoke(nameof(ShowRestartMenu), 1f);

        player.GetComponent<Control>().enabled = false;
        player.GetComponent<Math>().enabled = false;

        cam.GetComponent<CameraMove>().player = playerRb[0].gameObject;
        cam.GetComponent<CameraMove>().lookAt = true;

        running.GetComponent<Animator>().enabled = false;

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
        ////Change to Win menu//////
        Invoke(nameof(ShowRestartMenu), 1f);

        player.GetComponent<Math>().enabled = false;

        cam.GetComponent<CameraMove>().player = playerRb[0].gameObject;

        running.transform.parent = null;
        /*
        running.AddComponent<CharacterController>().center = new Vector3 (0, 0.5f, 0);
        running.GetComponent<CharacterController>().radius = 0.2f;
        running.GetComponent<CharacterController>().height = 1f;
        running.GetComponent<CharacterController>().Move(new Vector3(0, -1, 0) * Time.deltaTime * 5);
        */
        running.transform.position = new Vector3(running.transform.position.x, 0.33f, running.transform.position.z);
    }

    public void DestroyGlassesBlocks()
    {
        print("destroy");
        if (player.GetComponent<Math>().rollerSizeY > 2f && player.GetComponent<Math>().rollerSizeY <= 4f)
        {
            glaseesBlocs[0].GetComponent<GlassCube>().Destroy();
            glaseesBlocs[1].GetComponent<GlassCube>().Destroy();
        }else if(player.GetComponent<Math>().rollerSizeY > 4f)
        {
            glaseesBlocs[0].GetComponent<GlassCube>().Destroy();
            glaseesBlocs[1].GetComponent<GlassCube>().Destroy();
            glaseesBlocs[2].GetComponent<GlassCube>().Destroy();
        }
        else
            glaseesBlocs[0].GetComponent<GlassCube>().Destroy();

        foreach(ParticleSystem go in winParticles)
        {
            go.Play();
        }
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
