using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string SceneName;

    public void OpenScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
    }
}
