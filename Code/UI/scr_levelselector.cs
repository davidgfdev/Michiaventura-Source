using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_levelselector : MonoBehaviour
{
    GameObject gameController;
    [SerializeField] GameObject[] levelButtons;
    [SerializeField] GameObject endPanel;
    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void Start()
    {
        checkLevelStates();
    }

    public void MainMenu()
    {
        gameController.GetComponent<scr_gamestate>().BackToMenu();
    }

    public void SelectLevel(string levelName)
    {
        StartCoroutine(changeScene(levelName));
    }

    private void checkLevelStates()
    {
        int[] levelStates = gameController.GetComponent<scr_savesystem>().LoadGame();
        if (levelStates != null)
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                switch (levelStates[i])
                {
                    case 0:
                        levelButtons[i].SetActive(false);
                        break;
                    case 1:
                        levelButtons[i].SetActive(true);
                        levelButtons[i].GetComponent<Image>().color = Color.gray;
                        break;
                    case 2:
                        levelButtons[i].SetActive(true);
                        break;
                    case 3:
                        levelButtons[i].SetActive(true);
                        levelButtons[i].GetComponentInChildren<Text>().text = levelButtons[i].GetComponentInChildren<Text>().text + "*";
                        break;
                }
            }

            if (levelStates[12] == 2 || levelStates[12] == 3)
            {
                endPanel.SetActive(true);
            }
        }
        else
        {
            MainMenu();
        }

    }

    IEnumerator changeScene(string sceneName)
    {
        GameObject transition = GameObject.Find("FallInAndFallAway");
        transition.GetComponent<Animator>().SetTrigger("fadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        yield return null;
    }

    public void closeEndPanel()
    {
        endPanel.SetActive(false);
    }
}
