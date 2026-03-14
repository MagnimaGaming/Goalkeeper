using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
  [SerializeField] private float forwardForce = 15f;
  [SerializeField] private float upwardForce = 7f;
  [SerializeField] private float StartingZPos = 38f;
  Vector3 KickerPosition;
  [SerializeField]private GameObject Kicker;
  [SerializeField] Audiomanager audiomanager;
  [SerializeField] Animator BallKickAnimator;
  float[] lanes = { -2f, -1f, 0f, 1f, 2f };
  Rigidbody Rb;
  bool isResetting = false;
  Vector3 startPosition;



  void Start()
  {
    Rb = GetComponent<Rigidbody>();
    startPosition = new Vector3(0, transform.position.y, StartingZPos);
    KickerPosition=Kicker.transform.position;
    transform.position = startPosition;
    StartCoroutine(BallWait());
  }
  
    void Shoot()
  {
    float laneX = lanes[Random.Range(0, lanes.Length)];
    Vector3 direction = new Vector3(laneX - transform.position.x, 0, 9f).normalized;
    Rb.AddForce(direction * forwardForce, ForceMode.Impulse);
    Rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
    audiomanager.PlayKick();
  }
  IEnumerator BallWait()
  {
    BallKickAnimator.Play("Kick", 0, 0f);
    BallKickAnimator.SetTrigger("Kick");
    yield return new WaitForSeconds(2.1f);
    Shoot();
  }

  void OnCollisionEnter(Collision collision)
  {
    if (isResetting) return;
    if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Goal")
    {
      isResetting = true;
      StartCoroutine(ResetBall());
    }

  }
  IEnumerator ResetBall()
  {
    
    yield return new WaitForSeconds(1f);
    Rb.velocity = Vector3.zero;
    Rb.angularVelocity = Vector3.zero;
    Kicker.transform.position=KickerPosition;
    transform.position = startPosition;
    StopAllCoroutines();
    StartCoroutine(BallWait());
    isResetting = false;
  }
}
