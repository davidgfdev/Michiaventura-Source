using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_levelstate : MonoBehaviour
{
    [SerializeField] int levelIndex;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject PauseMenu;

    Vector3 checkPoint;

    int gemsCollected;
    GameObject player;
    GameObject gameController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        checkPoint = player.transform.position;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        Time.timeScale = PauseMenu.activeSelf ? 0 : 1;
    }

    public void setCheckPoint(Vector3 checkPoint)
    {
        this.checkPoint = checkPoint;
    }

    public void OnPlayerHurt()
    {
        StartCoroutine(hurtTransition());
    }

    public void OnPlayerFinish()
    {
        scr_savesystem saveSystem = gameController.GetComponent<scr_savesystem>();
        int[] levels = saveSystem.LoadGame();
        levels[levelIndex] = gemsCollected == 3 ? 3 : 2;
        if (levelIndex != 12) levels[levelIndex + 1] = 1;
        saveSystem.SaveGame(levels);
        StartCoroutine(changeScene("LevelSelect"));
    }

    public void BackToMenu()
    {
        Pause();
        StartCoroutine(changeScene("LevelSelect"));
    }

    public void addGems(int gems)
    {
        gemsCollected += gems;
        UI.GetComponent<scr_UI>().UpdateGemText(gemsCollected);
    }

    int getGemsCollected()
    {
        return gemsCollected;
    }

    IEnumerator changeScene(string sceneName)
    {
        GameObject transition = GameObject.Find("FallInAndFallAway");
        transition.GetComponent<Animator>().SetTrigger("fadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator hurtTransition()
    {
        player.SetActive(false);
        GetComponent<AudioSource>().Play();
        GameObject transition = GameObject.Find("FallInAndFallAway");
        transition.GetComponent<Animator>().SetTrigger("fadeIn");
        yield return new WaitForSeconds(1);
        player.SetActive(true);
        player.transform.position = checkPoint;
        player.GetComponent<scr_hook>().resetValues();
        transition.GetComponent<Animator>().SetTrigger("fadeOut");
    }
}
