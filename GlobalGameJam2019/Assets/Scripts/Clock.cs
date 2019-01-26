using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        TimeSystem timeSystem = TimeSystem.pInstance;
        System.Action action = null;
        action = () =>
        {
            timeSystem.SubscribeEvent(timeSystem.time.AddMinutes(1), action);
            text.text = timeSystem.time.ToShortTimeString();
        };

        action.Invoke();
    }
}
