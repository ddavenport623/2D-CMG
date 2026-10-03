using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Planet : MonoBehaviour
{
    public string PlanetName;
    public TextMeshPro textElement; // Assign in Inspector

    // Warp
    public bool OriginPlanet; // Planet where warp connections originates
    private bool WarpVisited = false;
    public List<Planet> Neighbors; // Planets connected to this one


    public Planet(string name)
    {
        PlanetName = name;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textElement.text = PlanetName;
        if (OriginPlanet)
        {
            GenerateWarpMap();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateWarpMap()
    {
        // Creates Warp Routes along all of it's connected planets
        for (int i=0; i < Neighbors.Count; i++)
        {
            // Only generates if the planet hasn't been visited yet
            if (!Neighbors[i].WarpVisited)
            {
                WarpLine.CreateWarpLine(transform.position, Neighbors[i].transform.position);
            }
        }

        WarpVisited = true; // After all routes have been created or confirmed declare this planet visited 

        // Generate along all paths until every planet has been visited
        for (int i=0; i < Neighbors.Count; i++)
        {
            if (!Neighbors[i].WarpVisited)
            {
                Neighbors[i].GenerateWarpMap();
            }
        }
    }
}
