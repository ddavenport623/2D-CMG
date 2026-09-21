using UnityEngine;
using TMPro;

public class Planet : MonoBehaviour
{
    public string PlanetName;
    public TextMeshPro textElement; // Assign in Inspector
    private int PlanetID;


    public Planet(string name)
    {
        PlanetName = name;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textElement.text = PlanetName;
        // PlanetID =
    }

    // Update is called once per frame
    void Update()
    {
    }
}
