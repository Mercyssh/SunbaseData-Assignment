using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APICaller : MonoBehaviour
{
    private const string apiUrl = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    private void Start()
    {
        StartCoroutine(GetRequest(apiUrl));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Since retrieval of data is Asynchronous,
            // execution of the current coroutine will halt until this Async Operation is complete.
            yield return webRequest.SendWebRequest();

            // The web request may or may not have failed,
            // so we need to handle all potential failures before doing something with the retrieved data
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error : " + webRequest.error);
                    break;

                case UnityWebRequest.Result.Success:
                    Debug.Log("Data : " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
