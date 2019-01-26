using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SunEditor : EditorWindow
{
  AnimationCurve sunCurve = AnimationCurve.Linear(0, 0, 10, 10);

  [MenuItem("Examples/Create Curve For Object")]
  static void Init()
  {
    SunEditor window = (SunEditor)EditorWindow.GetWindow(typeof(SunEditor));
    window.Show();
  }

  void OnGUI()
  {
    sunCurve = EditorGUILayout.CurveField("Sun by Time", sunCurve);

    if (GUILayout.Button("Generate Curve"))
      AddCurveToSelectedGameObject();
  }

  void AddCurveToSelectedGameObject()
  {
    if (Selection.activeGameObject)
    {
      Sun comp = Selection.activeGameObject.AddComponent<Sun>();

      comp.SetCurves(sunCurve);
    }
    else
    {
      Debug.LogError("No Game Object selected for adding an animation curve");
    }
  }
}
