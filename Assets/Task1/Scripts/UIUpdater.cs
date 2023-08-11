using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    //Reference to the container of list
    public GameObject listContainer;
    public GameObject filterDropDown;

    //We store a local reference to the retrieved data.
    [HideInInspector]
    public Response response;

    public enum filterType
    {
        All,
        Managers,
        NonManagers
    }

    //This is a reference to a prefab of a list Item
    public GameObject template;

    public void UpdateUI()
    {
        HideAll();

        // Again, since 'Data' class is actually a sequence of manually defined variables
        // We need to manually call the function to return them as an array in a predefined order
        // This would not have to be done if the data object in JSON was presented as an Array, OR 
        // if External libraries were allowed to be used.
        ClientData[] dataArray = response.data.AsArray();

        // Retrieve current filter option from dropdown
        // This will be used afterwards to filter out required cards.
        filterType filter = filterType.All;
        switch (filterDropDown.GetComponent<Dropdown>().value)
        {
            case 0:
                filter = filterType.All;
                break;
            case 1:
                filter = filterType.Managers;
                break;
            case 2: 
                filter = filterType.NonManagers;
                break;
        }

        int index = 0;
        foreach (Client client in response.clients)
        {

            //Check if any filters are in place, and move onto next client as required
            if (filter == filterType.Managers && !client.isManager) continue;
            if (filter == filterType.NonManagers && client.isManager) continue;

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
}
