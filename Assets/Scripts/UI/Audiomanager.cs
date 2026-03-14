using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
  public AudioSource audioSource;

  public AudioClip kickSound;
  public AudioClip saveSound;
  public AudioClip goalSound;
  public AudioClip gameOverSound;

  public void PlayKick()
  {
    audioSource.PlayOneShot(kickSound);
  }

  public void PlaySave()
  {
    if (saveSound == null) return;
    audioSource.PlayOneShot(saveSound);
  }

  public void PlayGoal()
  {
    audioSource.PlayOneShot(goalSound);
  }

  public void PlayGameOver()
  {
    audioSource.PlayOneShot(gameOverSound);
  }
}
