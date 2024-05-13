using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


namespace AssetsBundle
{
    public class AssetBundleLoader : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI loadingPercentText = null;
        [SerializeField] protected TextMeshProUGUI loadingText = null;
        [SerializeField] protected Slider downloadSlider;
        [SerializeField] protected Canvas assetBundleCanvas = null;
        public static string sceneName;
        public string Path;

        public void Start()
        {
            StartCoroutine(COR_LoadAssetBundleFromStorage(System.IO.Path.Combine(Application.streamingAssetsPath, Path)));
        }

        private IEnumerator COR_LoadAssetBundleFromStorage(string path)
        {
            Debug.Log("goldenRummy AssetBundle Path : " + path);
            AssetBundle.UnloadAllAssetBundles(true);

            var data = AssetBundle.LoadFromFileAsync(path);
            downloadSlider.value = 0;

            while (!data.isDone)
            {
                float v = (data.progress / 1) * 100;
                float fill = v / 100;

                downloadSlider.value = fill;
                loadingText.text = $"Loading";
                loadingPercentText.text = $"{Mathf.RoundToInt(v)}%";

                yield return null;
            }

            downloadSlider.value = 1;
            loadingText.text = $"Loading";
            loadingPercentText.text = $"{100}%";

            Debug.Log(" Data assetbundle "+ data.assetBundle);
            //IntentManager.instance.MyAssetBundle = data.assetBundle;
            if (data.assetBundle.isStreamedSceneAssetBundle)
            {
                Debug.Log("BundleName" + data.assetBundle.name);
                var scenePaths = data.assetBundle.GetAllScenePaths();
                Debug.Log("ScenePaths " + data.assetBundle.GetAllScenePaths());
                foreach (var item in scenePaths)
                {
                    Debug.Log("scene name " + item);
                }
                Debug.Log(" current scene "+ scenePaths[0]);
                sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
                Debug.Log("scene name 1 " + sceneName);
                SceneManager.LoadScene(sceneName);
                /// Once All connection and everything is complete then call this. 
                yield return null;
            }
            else
            {
                Debug.Log($"is not scene asset bundle");
            }
        }
    }
}
