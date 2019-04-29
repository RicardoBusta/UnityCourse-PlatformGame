using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadAssetBundles : MonoBehaviour
{   
    public void LoadScene2Assets()
    {
        StartCoroutine(LoadBundleAsync());
    }

    private IEnumerator LoadBundleAsync()
    {
        var bundleRequest =
            AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "level2assets"));
        yield return new WaitUntil(  () => bundleRequest.isDone);
        var scenes = bundleRequest.assetBundle.GetAllScenePaths();
        SceneManager.LoadScene(scenes[0]);
    }
}