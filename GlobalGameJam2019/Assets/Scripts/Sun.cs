using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
  private Light light;

  public AnimationCurve sunCurve;

  public void SetCurves(AnimationCurve sunCurve)
  {
    this.sunCurve = sunCurve;
  }

  void Start()
  {
    this.light = GetComponent<Light>();
  }

  void Update()
  {
    this.light.intensity = sunCurve.Evaluate(
        ((float)TimeSystem.pInstance.time.TimeOfDay.TotalMinutes % 1440) / 1440);
  }
}
