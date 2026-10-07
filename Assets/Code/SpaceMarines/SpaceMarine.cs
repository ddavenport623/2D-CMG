using UnityEngine;

public class SpaceMarine : MonoBehaviour
{
    // Private
    [SerializeField] string SMName;
    SMNames nameList;

    // public
    public Planet location;
    public float power {get;} = 0;

    public SpaceMarine(Planet startingPlanet, float startingPower)
    {
        location = startingPlanet;
        power = startingPower;
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
