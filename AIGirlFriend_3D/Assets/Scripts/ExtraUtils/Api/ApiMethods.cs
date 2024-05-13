using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

namespace ExtraUtils.Api
{
    /// <summary>
    /// Api Methods to call Get,Post and Delete Apis.
    /// </summary>
    
    public class ApiMethods : MonoBehaviour
    {
        public static async Task<string> CallGetApi(string uri,string apiName,string authToken = "")
        {
            var finalUrl = "";
            
            var request = UnityWebRequest.Get(finalUrl);
            request.certificateHandler = new BypassCertificate();
            if(authToken != "")
                request.SetRequestHeader("Authorization",$"Bearer {authToken}");
            
            Logger.LocalLog($"API url of {apiName} is {finalUrl}");
            
            request.SendWebRequest();

            while (request.isDone == false)
            {
                await Task.Yield();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Logger.ApiLog(apiName,"Get",request.error);
                return request.error;
            }

            var allData = request.downloadHandler.text;
            Logger.ApiLog(apiName,"Get",allData);
            return allData;
        }
        
        public static async Task<string> CallPostApi(string uri,string apiName,bool jsonType = false,WWWForm form = null,string authorizationToken = null,bool showLoader = true)
        {
            //if(showLoader)
              //  UIManager.Instance.OpenLoaderPopUp("Sending data.");
            
            var finalUrl =" ConfigManager.Instance.baseURL + uri";
            
            Logger.LocalLog($"API url of {apiName} is {finalUrl}");
            
            var request = UnityWebRequest.Post(finalUrl,form);
            request.timeout = 5000;
            request.certificateHandler = new BypassCertificate();
            request.disposeUploadHandlerOnDispose = true;
            request.disposeDownloadHandlerOnDispose = true;

            if(authorizationToken != null)
                request.SetRequestHeader("Authorization",$"Bearer {authorizationToken}");

            if (jsonType)
            {
                request.SetRequestHeader("Content-Type", "application/json");
                // request.SetRequestHeader("Accept", "application/json");
            }
            
            request.SendWebRequest();

            while (request.isDone == false)
            {
                await Task.Yield();
            }
            
           // if(showLoader)
              //  UIManager.Instance.HideLoaderPopUp();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Logger.ApiLog(apiName, "Post", request.error);
               // UIManager.Instance.OpenLoaderPopUp("Failed to get data,Please try again.");
                await Task.Delay(2000);
              //  UIManager.Instance.HideLoaderPopUp();
                return request.error;
            }
            
            var allData = request.downloadHandler.text;
            request.Dispose();
            Logger.ApiLog(apiName, "Post", allData);
            return allData;
        }
        
        public static async Task<string> CallDeleteApi(string uri,string apiName,string authorizationToken = null)
        {
            var finalUrl =" ConfigManager.Instance.baseURL + uri";
            var request = UnityWebRequest.Delete(finalUrl);
            request.certificateHandler = new BypassCertificate();

            if(authorizationToken != null)
                request.SetRequestHeader("Authorization",$"Bearer {authorizationToken}");
            
            request.SendWebRequest();

            while (request.isDone == false)
            {
                await Task.Yield();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Logger.ApiLog(apiName, "Delete", request.error);
                return request.error;
            }

            var allData = "success";
            Logger.ApiLog(apiName, "Delete", allData);
            return allData;
        }
    }
}
