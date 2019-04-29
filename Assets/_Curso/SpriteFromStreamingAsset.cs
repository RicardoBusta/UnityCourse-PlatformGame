using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFromStreamingAsset : MonoBehaviour
{
    public string FileName;

    public event UnityAction x;

//    private Action x;
//    private Func<bool> y;
//    private UnityAction x;

    private void Start()
    {
        StartCoroutine(GetTexture());
    }

    private IEnumerator GetTexture()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var url = $"{Uri.UriSchemeFile}://{System.IO.Path.Combine(Application.streamingAssetsPath, FileName)}";
        Debug.Log(url);
        var texRequest = UnityWebRequestTexture.GetTexture(url);
        yield return texRequest.SendWebRequest();
        if (texRequest.isHttpError || texRequest.isNetworkError)
        {
            Debug.LogErrorFormat("Failed downloading image: {0}", texRequest.error);
        }
        else
        {
            var tex = DownloadHandlerTexture.GetContent(texRequest);
            Debug.Log(tex);
            var sprite = Sprite.Create(tex, new Rect(0, 0, 20, 9), new Vector2(0.5f, 0f), 16, 0,
                SpriteMeshType.FullRect, Vector4.one);
            //sprite.
            spriteRenderer.sprite = sprite;
        }
    }
}