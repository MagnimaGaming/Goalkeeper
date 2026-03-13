using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
  [SerializeField] GameObject PausePannel;
  [SerializeField] GameObject MainMenuPannel;
  [SerializeField] GameObject GameOverPannel;
  [SerializeField] GameObject LivesPannel;
  [SerializeField] GameObject CountdownPannel;
  [SerializeField] TMP_Text CountdownText;
  [SerializeField] GameObject SettingsPannel;
  [SerializeField] GameObject PauseIconPannel;
  [SerializeField] private TextMeshProUGUI score;

  public GameObject LevelsPannel;
  public PlayerMovement player;
  public BallController ballController;

  [SerializeField] GameObject Ball1;
  [SerializeField] GameObject Ball2;
  [SerializeField] GameObject Ball3;

  int goals = 3;
  int points = 0;

  static bool SkipMenu = false;
  static bool SkipLevelPanel = false;

  void Start()
  {
    Time.timeScale = 0f;
    score.text = "Points: 0";

    if (!SkipMenu)
    {
      MainMenuPannel.SetActive(true);
      return;
    }

    MainMenuPannel.SetActive(false);
    SkipMenu = false;

    if (SkipLevelPanel)
    {
      LevelsPannel.SetActive(false);
      SkipLevelPanel = false;
      Time.timeScale = 1f;
    }
    else
    {
      LevelsPannel.SetActive(true);
    }
  }

  public void ScoreIncrease()
  {
    points++;
    score.text = "Points: " + points;
    Debug.Log("Score Increased");
  }

  public void Losegoal()
  {
    goals--;

    if (goals == 2)
    {
      Ball3.SetActive(false);
    }
    else if (goals == 1)
    {
      Ball2.SetActive(false);
    }
    else if (goals == 0)
    {
      Ball1.SetActive(false);
      GameOver();
    }
  }

  void GameOver()
  {
    Debug.Log("Game Over");
    Time.timeScale = 0f;
    GameOverPannel.SetActive(true);
  }

  public void Beginner()
  {
    PlayerPrefs.SetInt("Level", 0);
    LevelsPannel.SetActive(false);
    LivesPannel.SetActive(true);
    PauseIconPannel.SetActive(true);
    Time.timeScale = 1f;
  }

  public void Intermediate()
  {
    PlayerPrefs.SetInt("Level", 1);
    LevelsPannel.SetActive(false);
    LivesPannel.SetActive(true);
    Time.timeScale = 1f;
  }

  public void Difficult()
  {
    PlayerPrefs.SetInt("Level", 2);
    LevelsPannel.SetActive(false);
    LivesPannel.SetActive(true);
    Time.timeScale = 1f;
  }

  public void StartButton()
  {
    SkipMenu = true;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void QuitButton()
  {
    Application.Quit();
  }

  public void ShowPause()
  {
    Debug.Log("Pause Clicked");
    PausePannel.SetActive(true);
    ballController.enabled = false;
    Time.timeScale = 0f;
  }

  public void ResumeButton()
  {
    StartCoroutine(ResumeCountdown());
  }

  public void HomeButton()
  {
    PausePannel.SetActive(false);
    MainMenuPannel.SetActive(true);
  }

  public void BackButton()
  {
    SettingsPannel.SetActive(false);
  }

  public void ShowSettings()
  {
    SettingsPannel.SetActive(true);
  }

  public void Restart()
  {
    SkipMenu = true;
    SkipLevelPanel = true;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    LivesPannel.SetActive(true);
  }

  IEnumerator ResumeCountdown()
  {
    PausePannel.SetActive(false);
    CountdownPannel.SetActive(true);


    Time.timeScale = 1f;
    player.enabled = false;
    CountdownText.text = "3";
    yield return new WaitForSeconds(1f);

    CountdownText.text = "2";
    yield return new WaitForSeconds(1f);

    CountdownText.text = "1";
    yield return new WaitForSeconds(1f);

    CountdownPannel.SetActive(false);
    player.enabled = true;
  }
}
