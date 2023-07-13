using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string _sceneTransitionName;


    private void Start()
    {
        if(_sceneTransitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerControler.Instance.transform.position = this.transform.position;
        }
    }
}
