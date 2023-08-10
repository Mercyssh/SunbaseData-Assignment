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

    //Sends webRequest to fetch API data. If successful, updates the UI to represent the data.
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
    
                    // Retrieved data has spaces and newlines before the actual JSON content starts,
                    // We first sanitize this using the .Trim() method
                    string data = webRequest.downloadHandler.text.Trim();

                    // Then we Serialize it to our defined Response Class
                    Response response = JsonUtility.FromJson<Response>(data);

                    //And if the UI updater class is present, then prompt it to update the Canvas Gameobject
                    UIUpdater uiUpdater = GetComponent<UIUpdater>();
                    if (uiUpdater == null) break;
                    else uiUpdater.UpdateUI(response);
                    break;
            }
        }
    }
}
