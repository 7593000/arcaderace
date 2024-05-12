using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneAsync( string sceneName )
    {

        StartCoroutine( LoadSceneAsyncCoroutine( sceneName ) );
    }
    private IEnumerator LoadSceneAsyncCoroutine( string sceneName )
    {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync( sceneName );

        while ( !asyncOperation.isDone )
        {

            float progress = Mathf.Clamp01( asyncOperation.progress / 0.9f );
            Debug.Log( " Сцена загружена на " + sceneName + ": " + ( progress * 100 ) + "%" );
            if ( progress * 100 == 100 )
            {
                Debug.Log( "Сцена " + sceneName + " загружена полностью." );
            }
            yield return null;
        }


    }
}
