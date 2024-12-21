using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverUI;
    public GameObject gameoverUI1;
    public bool game = false;
    public void gameover()
    {
        gameoverUI.SetActive(true);
        gameoverUI1.SetActive(false);
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
