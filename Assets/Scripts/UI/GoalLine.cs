using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLine : MonoBehaviour
{
  [SerializeField] private UIManager uiManager;
  public Audiomanager audioManager;
  public BallController ballController;

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Ball")
    {
      uiManager.Losegoal();
      audioManager.PlayGoal();
      ballController.isResetting=true;
      StartCoroutine(ballController.ResetBall());
    }
  }
}
