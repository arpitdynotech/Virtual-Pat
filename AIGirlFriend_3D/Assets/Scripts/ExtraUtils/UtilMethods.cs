using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using DG.Tweening;

namespace ExtraUtils
{
    /// <summary>
    /// Extra Util Methods.
    /// </summary>
    ///
    public class UtilMethods : MonoBehaviour
    {
       /* public static string PrintJsonObject(object data)
        {
           // return (JsonConvert.SerializeObject(data));
        }*/

        public static string RemoveCommas(string data)
        {
            return data.Replace("\"","");
        }

        public static async Task<Texture> DownloadImageUsingLink(string url,string authToken)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            var request = UnityWebRequestTexture.GetTexture(url);
            if(authToken != "")
                request.SetRequestHeader("Authorization",$"Bearer {authToken}");
            var result = request.SendWebRequest();
            Texture finalTexture = null;
            var isTextureLoaded = false;
            
            result.completed += _ =>
            {
                if (result.webRequest.error != null)
                {
                    Debug.Log(request.error);
                    isTextureLoaded = true;
                }
                else
                {
                    try
                    {
                        var downloadedTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                        // var sprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), Vector2.one / 2);
                        finalTexture = downloadedTexture;
                        isTextureLoaded = true;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message +"  IMAGE DOWNLOADER FAILD TO DOWNLOAD !!");
                    }
                }
            };

            while (isTextureLoaded == false)
            {
                await Task.Yield();
            }

            return await Task.FromResult(finalTexture);
        }

        public static void DestroyAllChildren(Transform parentObject)
        {
            foreach (Transform child in parentObject)
            {
                Destroy(child.gameObject);
            }
        }

        public static string ConvertDictionaryToJsonString(Dictionary<string,string> dictionaryData)
        {
            var finalData = "{";

            for (int i = 0; i < dictionaryData.Count; i++)
            {
                var key = dictionaryData.Keys.ToList()[i];
                var value = dictionaryData.Values.ToList()[i];

                var currentData = $"\"{key}\" : \"{value}\"";

                currentData += i < dictionaryData.Count - 1 ? "," : "}";
                finalData += currentData;
            }

            return finalData;
        }
        
        public static string ConvertArrayToJsonString(string[] dictionaryData)
        {
            var finalData = "";

            for (int i = 0; i < dictionaryData.Length; i++)
            {
                // var key = dictionaryData.Keys.ToList()[i];
                // var value = dictionaryData.Values.ToList()[i];

                var currentData = $"\"{dictionaryData[i]}\"";

                currentData += i < dictionaryData.Length - 1 ? "," : "";
                finalData += currentData;
            }

            return finalData;
        }

        
        public static byte[] GetTextureDataInBytes(Texture2D imageToConvert)
        {
            RenderTexture rt = RenderTexture.GetTemporary(imageToConvert.width, imageToConvert.height);
            RenderTexture previous = RenderTexture.active;
            
            Graphics.Blit(imageToConvert, rt);
            RenderTexture.active = rt;
            Texture2D texture2D = new Texture2D(imageToConvert.width, imageToConvert.height);
            texture2D.ReadPixels(new Rect(0, 0, imageToConvert.width, imageToConvert.height), 0, 0);
            texture2D.Apply();

            RenderTexture.active = previous;
            rt.Release();

            return texture2D.EncodeToPNG();
        }

        public static bool ValidateEmailUsingRegex(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        public static async void PerformButtonAnimation(Transform buttonTransform,float animationSpeed,int delayTime,Action callBack)
        {
            buttonTransform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), animationSpeed).SetEase(Ease.Linear);
            await Task.Delay(delayTime);
            buttonTransform.DOScale(new Vector3(1f, 1f, 1f), animationSpeed).SetEase(Ease.Linear);
            await Task.Delay(delayTime);
            
            callBack?.Invoke();
        }
        
    }
}
