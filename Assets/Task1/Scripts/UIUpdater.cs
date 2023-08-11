using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    //Reference to the container of list
    public GameObject listContainer;

    //We store a local reference to the retrieved data.
    [HideInInspector]
    public Response response;

    //This is a reference to a prefab of a list Item
    public GameObject template;

    public void ShowAll()
    {
        HideAll();

        // Again, since 'Data' class is actually a sequence of manually defined variables
        // We need to manually call the function to return them as an array in a predefined order
        // This would not have to be done if the data object in JSON was presented as an Array, OR 
        // if External libraries were allowed to be used.
        ClientData[] dataArray = response.data.AsArray();

        int index = 0;
        foreach (Client client in response.clients)
        {
            GameObject newItem = Instantiate(template, listContainer.transform);
            ListItem listItem = newItem.GetComponent<ListItem>();

            //Update Label
            listItem.LabelObject.text = "Label : "+client.label;

            //Find appropriate points value for each item
            string pointsValue = "???";
            if (index < dataArray.Length)
                pointsValue = dataArray[index].points.ToString();
            listItem.PointsObject.text = "Points : " + pointsValue;

            //Set animation Delay
            listItem.SetDelay(index * listItem.delay);

            index++;
        }
    }

    private void HideAll()
    {
        foreach(Transform child in listContainer.transform)
        {
            child.GetComponent<ListItem>().PopOut();
        }
    }

    private void ShowManagers()
    {
        HideAll();
    }

    private void ShowNonManager()
    {
        HideAll();
    }

}
