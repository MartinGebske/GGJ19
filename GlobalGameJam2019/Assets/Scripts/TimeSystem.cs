using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimeSystem : MonoBehaviour
{
    public DateTime time
    {
        get;
        private set;
    }
    public float minutesPerDay = 10f;
    private const float MINUTES_IN_A_DAY = 1440;
    private float timeScale
    {
        get
        {
            return MINUTES_IN_A_DAY / minutesPerDay ;
        }
    }

    private List<DateTimeEvent> events = new List<DateTimeEvent>();

    public static TimeSystem pInstance
    {
        get
        {
            if (mInstance == null) {
                mInstance = FindObjectOfType<TimeSystem>();
                if (mInstance == null) {
                    GameObject gO = Instantiate(new GameObject());
                    mInstance = gO.AddComponent<TimeSystem>();
                }
                DontDestroyOnLoad(mInstance);
                mInstance.time = new DateTime(1, 1, 1, 12, 0, 0);
            }
            return mInstance;
        }
    }

    private static TimeSystem mInstance;

    private void Awake()
    {
        pInstance.name = "TIME_SYSTEM";
    }

    private void Update()
    {
        time = time.AddSeconds(Time.deltaTime * timeScale);
        List<DateTimeEvent> toExecute = new List<DateTimeEvent>();
        foreach (DateTimeEvent dateTimeEvent in events.Where<DateTimeEvent>(item => item.triggerTime < time)) {
            toExecute.Add(dateTimeEvent);
        }

        for (int i = 0; i < toExecute.Count; i++) {
            toExecute[i].eventAction.Invoke();
            events.Remove(toExecute[i]);
        }
    }

    public void SubscribeEvent(DateTime dateTime, Action action)
    {
        events.Add(new DateTimeEvent { triggerTime = dateTime, eventAction = action });
    }

    public struct DateTimeEvent
    {
        public DateTime triggerTime;
        public Action eventAction;
    }
}