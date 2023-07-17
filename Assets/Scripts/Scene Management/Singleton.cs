using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    public static T Instance { get { return _instance; } }

    protected virtual void Awake()
    {
        // checks if there is already a game object in the new scene, if so destroy it, if no stablish one.
        if (_instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = (T)this;
        }


        // will only destroy the object if it doesn't have a parent
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }

        
    }

}
