using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    public GameObject list;

    //This is a reference to a prefab of a list Item
    public GameObject template;

    public void UpdateUI(Response response)
    {
        // Again, since 'Data' class is actually a sequence of manually defined variables
        // We need to manually call the function to return them as an array in a predefined order
        // This would not have to be done if the data object in JSON was presented as an Array, OR 
        // External libraries were allowed to be used.
        ClientData[] dataArray = response.data.AsArray();

        int index = 0;
        foreach (Client client in response.clients)
        {
            GameObject newItem = Instantiate(template, list.transform);
            newItem.GetComponent<ListItem>().LabelObject.text = "Label : "+client.label;

            //Find appropriate points value for each item
            string pointsValue = "???";
            if (index < dataArray.Length)
                pointsValue = dataArray[index].points.ToString();
            newItem.GetComponent<ListItem>().PointsObject.text = "Points : " + pointsValue;

            index++;
        }
    }
}
