using UnityEngine;

namespace ExtraUtils
{
    /// <summary>
    /// Methods for different logs.
    /// </summary>
    
    public class Logger : MonoBehaviour
    {
        private const bool SocketReceiveLogs = true;
        private const bool AcknowledgementLogs = true;
        private const bool LocalLogs = true;
        private const bool ApiCallLogs = true;

        public static void SocketReceivedLog(string socketName, string data)
        {
            if(SocketReceiveLogs)
                Debug.Log($"<color=lime>[Received Socket - {socketName}] </color> with data " + data);
        }
        
        public static void AcknowledgementLog(string socketName, string data)
        {
            if(AcknowledgementLogs)
                Debug.Log("<color=yellow> Acknowledgement ==>  [Socket - " + socketName + " ] </color>"+" with data " + data);
        }
        
        public static void ApiLog(string apiName,string apiType, string data)
        {
            if(ApiCallLogs)
                Debug.Log("<color=lime> Api Call ==>  [Api - " + apiName + " ] [Type - "+ apiType+" </color>"+" with data " + data);
        }

        public static void LocalLog(string message)
        {
            if(LocalLogs)
                Debug.Log("<color=aqua>"+message+"</color>");
        }
    }
}
