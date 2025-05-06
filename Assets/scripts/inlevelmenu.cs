using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inlevelmenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void menu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void level_menu()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void next_level()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void prev_level()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex -1);
    }
    public void OpenLevel(int levelid)
    {
        SceneManager.LoadScene(levelid);

    }
    public void StopGame()
    {
        Application.Quit();
    }


}
