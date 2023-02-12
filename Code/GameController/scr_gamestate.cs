using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_gamestate : MonoBehaviour
{
    public int[] levels;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameController") != this.gameObject) Destroy(gameObject); else DontDestroyOnLoad(gameObject);
    }

    public void StartNewGame()
    {
        levels = GetComponent<scr_savesystem>().StartNewGame();
        Debug.Log(levels[0]);
        OpenLevelSelect();
    }

    public void LoadGame()
    {
        if (GetComponent<scr_savesystem>().LoadGame() != null)
        {
            OpenLevelSelect();
        }
    }

    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
