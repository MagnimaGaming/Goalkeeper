using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
  [SerializeField] AudioSource audioSource;

  [SerializeField] GameObject musicPanel;
  [SerializeField] GameObject instructionsPanel;

  void OnEnable()
  {
    ShowAudio();
  }

  public void setVolume(float value)
  {
    Debug.Log("Volume changed to: " + value);
    audioSource.volume = value;
  }

  public void ShowAudio()
  {
    musicPanel.SetActive(true);
    instructionsPanel.SetActive(false);
  }

  public void ShowHelp()
  {
    musicPanel.SetActive(false);
    instructionsPanel.SetActive(true);
  }
}
