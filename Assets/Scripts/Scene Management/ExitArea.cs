using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitArea : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private string _sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControler>())
        {
            UI_Fade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
            SceneManagement.Instance.SetTransitionName(_sceneTransitionName);

            
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_sceneToLoad);
    }

}
