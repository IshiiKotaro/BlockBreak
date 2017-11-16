using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//全てのマネージャの親玉

public class SingletonMonoBehaviour<T> : MonoBehaviour where T :MonoBehaviour {

    private static T m_Instance;
    
    //==========================
    //  ゲッター
    //==========================

    public static T GetInstance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (T)FindObjectOfType(typeof(T));

                if (m_Instance == null)
                {
                    Debug.LogError(typeof(T) + "Is Nothing");
                }


            }
            return m_Instance;
        }
    }
}
