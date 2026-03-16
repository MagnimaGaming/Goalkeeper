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
  [SerializeField] private TextMeshProUGUI score;
  [SerializeField] TextMeshProUGUI finalScoreText;

  public GameObject LevelsPannel;
  public PlayerMovement player;
  public BallController ballController;
  public SettingsManager settingsManager;

  [SerializeField] GameObject Ball1;
  [SerializeField] GameObject Ball2;
  [SerializeField] GameObject Ball3;
  public GameObject Kicker;
  [SerializeField] Audiomanager audiomanager;

  int goals = 3;
  int points = 0;

  static bool SkipMenu = false;
  static bool SkipLevelPanel = false;

  void Start()
  {
    settingsManager.volumeSlider.value = 1;
    Time.timeScale = 0f;
    score.text = "Points: 0";

    if (!SkipMenu)
    {
      MainMenuPannel.SetActive(true);
      LivesPannel.SetActive(false);
      return;
    }

    MainMenuPannel.SetActive(false);
    SkipMenu = false;

    if (SkipLevelPanel)
    {
      LevelsPannel.SetActive(false);
      LivesPannel.SetActive(true);
      SkipLevelPanel = false;
      Time.timeScale = 1f;
    }
    else
    {
      LevelsPannel.SetActive(true);
      LivesPannel.SetActive(false);
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
      ballController.enabled = false;
      player.enabled = false;
      Kicker.SetActive(false);
      StartCoroutine(HoldGame());
    }
  }

  private IEnumerator HoldGame()
  {
    
    yield return new WaitForSeconds(3);

    audiomanager.PlayGameOver();
    GameOver();

  }

  void GameOver()
  {
    Debug.Log("Game Over");
    Time.timeScale = 0f;
    finalScoreText.text = " POINTS: " + points;
    GameOverPannel.SetActive(true);
  }

  public void Beginner()
  {
    PlayerPrefs.SetInt("Level", 0);
    LevelsPannel.SetActive(false);
    LivesPannel.SetActive(true);
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

  public void ShowMenu()
  {
    MainMenuPannel.SetActive(false);
    LevelsPannel.SetActive(true);
  }

  public void QuitButton()
  {
    Application.Quit();
  }

  public void ShowPause()
  {
    Debug.Log("Pause Clicked");
    PausePannel.SetActive(true);
    Time.timeScale = 0f;
    player.enabled = false;
    ballController.enabled = false;

  }

  public void ResumeButton()
  {
    StartCoroutine(ResumeCountdown());
  }

  public void HomeButton()
  {
    PausePannel.SetActive(false);
    SettingsPannel.SetActive(false);
    GameOverPannel.SetActive(false);
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
    points = 0;
    SkipMenu = true;
    SkipLevelPanel = true;

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

  }

  IEnumerator ResumeCountdown()
  {
    PausePannel.SetActive(false);
    CountdownPannel.SetActive(true);
    Time.timeScale = 0f;
    player.enabled = false;
    ballController.enabled = false;


    CountdownText.text = "3";
    yield return new WaitForSecondsRealtime(1f);

    CountdownText.text = "2";
    yield return new WaitForSecondsRealtime(1f);

    CountdownText.text = "1";
    yield return new WaitForSecondsRealtime(1f);

    CountdownPannel.SetActive(false);
    Time.timeScale = 1f;
    ballController.enabled = true;
    player.enabled = true;

  }
}
