using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class scr_EndScreen : MonoBehaviour
{
    public GameObject EndScreen;
    public int nextLevel;

    private void Start()
    {
        EndScreen.gameObject.SetActive(false);
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            EndScreen.gameObject.SetActive(true);
            LoadLevel();
        }

    }

    public void LoadLevel()
    {
        Thread.Sleep(3000);
        SceneManager.LoadScene(nextLevel);
    }
}
