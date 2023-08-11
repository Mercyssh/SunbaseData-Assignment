using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIUpdater : MonoBehaviour
{
    //Reference to the container of list
    public GameObject listContainer;
    public GameObject filterDropDown;
    public Text fetchingData;

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

        //Hides the "fetching data.." placeholder text
        fetchingData.DOFade(0f, .2f).OnComplete(() =>
        {
            if (fetchingData.gameObject != null)
                Destroy(fetchingData.gameObject);
        });

        // Again, since 'Data' class is actually a sequence of manually defined variables
        // We need to manually call the function to return them as an array in a predefined order
        // This would not have to be done if the data object in JSON was presented as an Array, OR 
        // if External libraries were allowed to be used.
        ClientData[] clientDataArray = response.data.AsArray();

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
            if ((filter == filterType.Managers && !client.isManager) || (filter == filterType.NonManagers && client.isManager)){
                index++;
                continue;
            }

            GameObject newItem = Instantiate(template, listContainer.transform);
            ListItem listItem = newItem.GetComponent<ListItem>();

            //We pass on the ClientData to the newly created list Item.
            //This will be required when the listItem wants to open a popup window.
            if (index < clientDataArray.Length)
                listItem.clientData = clientDataArray[index];

            //Update Label
            listItem.labelObject.text = "Label : "+client.label;

            //Find appropriate points value for each item
            string pointsValue = "???";
            if (index < clientDataArray.Length)
                pointsValue = clientDataArray[index].points.ToString();
            listItem.pointsObject.text = "Points : " + pointsValue;

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
