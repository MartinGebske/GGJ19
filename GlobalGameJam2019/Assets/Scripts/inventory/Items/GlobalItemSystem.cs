using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalItemSystem : MonoBehaviour
{
    public static GlobalItemSystem pInstance
    {
        get
        {
            if (mInstance == null) {
                mInstance = FindObjectOfType<GlobalItemSystem>();
                if (mInstance == null) {
                    GameObject gO = Instantiate(new GameObject());
                    mInstance = gO.AddComponent<GlobalItemSystem>();
                }
                DontDestroyOnLoad(mInstance);
            }
            return mInstance;
        }
    }

    private static GlobalItemSystem mInstance;

    private void Awake()
    {
        pInstance.name = "TIME_SYSTEM";
    }
}
