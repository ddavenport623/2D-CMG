using UnityEngine;

public class SpaceMarine : MonoBehaviour
{
    // Private
    string SMName;
    int power;
    SMNames nameList;

    // public
    public Planet location;

    public SpaceMarine(Planet startingPlanet)
    {
        location = startingPlanet;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SMName = nameList.GetRandomName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
