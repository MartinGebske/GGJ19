using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
  private Light light;

  public AnimationCurve intensityCurve;

  public void SetCurves(AnimationCurve sunCurve)
  {
    this.intensityCurve = sunCurve;
  }

  void Start()
  {
    this.light = GetComponent<Light>();
  }

  void Update()
  {
    this.light.intensity = intensityCurve.Evaluate(
        ((float)TimeSystem.pInstance.time.TimeOfDay.TotalMinutes % 1440) / 1440);
  }
}
