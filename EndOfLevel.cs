using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_EndOfLevel : MonoBehaviour
{
    int nextLevel;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadLevel();        
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("LevelTwo");
    }
}
