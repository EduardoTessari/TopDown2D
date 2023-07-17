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
            StartCoroutine(LoadSceneRoutine());
            PlayerControler.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();

        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        UI_Fade.Instance.FadeToClear();
    }


}
