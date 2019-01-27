using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
  public void SwitchTo(string sceneName)
  {
    StartCoroutine(SwitchToAsync(sceneName, 1f));
  }

  IEnumerator SwitchToAsync(string sceneName, float delay)
  {
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene(sceneName);
  }
}
