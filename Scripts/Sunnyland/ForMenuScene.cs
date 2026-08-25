using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForMenuScene : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _win;
    [SerializeField] GameObject clearPrev;

    public bool level2Access;
    public bool level3Access;
    public bool level4Access;
    public bool level5Access;
    public bool level6Access;
    public int getLevel;

    Coroutine cour;


    public bool _gamePaused;
    private void Start()
    {
      PlayerPrefs.SetInt("LevelCompleted",100);
    }

    public void gotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void gotoLevelSelect()
    {
        Debug.Log(PlayerPrefs.GetInt("LevelCompleted"));
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void gotoLevel1()
    {

        SceneManager.LoadScene(2);
    }
    public void gotoLevel2()
    {
        if( PlayerPrefs.GetInt("LevelCompleted")>=10)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            showCompletePrev();
        }
    }
    public void gotoLevel3()
    {
        if (PlayerPrefs.GetInt("LevelCompleted") >= 20)
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            showCompletePrev();
        }
    }
    public void gotoLevel4()
    {
        if (PlayerPrefs.GetInt("LevelCompleted") >= 30)
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            showCompletePrev();
        }
    }
    public void gotoLevel5()
    {
        if (PlayerPrefs.GetInt("LevelCompleted") >= 40)
        {
            SceneManager.LoadScene(6);
        }
        else
        {
            showCompletePrev();
        }
    }
    public void gotoLevel6()
    {
        if (PlayerPrefs.GetInt("LevelCompleted") >= 50)
        {
            SceneManager.LoadScene(7);
        }
        else
        {
            showCompletePrev();
        }
    }
    public void gotoEndless()
    {
        SceneManager.LoadScene(8);
    }
    public void Modes()
    {
        SceneManager.LoadScene(9);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowAbout()
    {
        text.SetActive(true);
    }
    public void CloseAbout()
    {
        text.SetActive(false);
    }
    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
     Time.timeScale = 0;
        _gamePaused = true;
    }
    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _gamePaused = false;
    }
    public void Win()
    {
        _win.SetActive(true);
    }

    public void determineLevel()
    {
       // PlayerPrefs.SetInt("LevelCompleted", 0);
        if( getLevel> PlayerPrefs.GetInt("LevelCompleted"))
        {
            PlayerPrefs.SetInt("LevelCompleted", PlayerPrefs.GetInt("LevelCompleted")+10);
        }
    }
    void showCompletePrev()
    {
        clearPrev.SetActive(true);
        cour = StartCoroutine(delay());

        IEnumerator delay()
        {
            yield return new WaitForSeconds(1f);
            clearPrev.SetActive(false);

        }
    }
}
