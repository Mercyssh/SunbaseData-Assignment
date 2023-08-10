/* 
 * When working with built in Unity JsonUtility
 * We need to define classes, which are similar in structure to the expected JSON
 * with similar names given to variables
 */

// When using JsonUtility, only classes marked with [System.Serializable] are taken into account
[System.Serializable]
public class Response
{
    public Client[] clients;
    public Data data;
    public string label;
}

[System.Serializable]
public class Client
{
    public bool isManager;
    public int id;
    public string label;
}

[System.Serializable]
public class Data
{
    // If there are more entries of ClientData, then we need to add them here manually
    // Or use an external library which supports dynamically sized objects.
    // Or parse the JSON data manually..

    // This is because Data is recieved from the API as a JSON Object, and not an array..
    public ClientData _1;
    public ClientData _2;
    public ClientData _3;

}

[System.Serializable]
public class ClientData
{
    public string address;
    public string name;
    public int points;
}