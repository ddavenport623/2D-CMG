using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager Instance {get; private set;}

    List<Planet> planets = new();
    public IReadOnlyList<Planet> AllPlanets => planets;

    void Awake()
    {
        // Make sure only singleton can ever exist
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        planets.AddRange(FindObjectsByType<Planet>());
    }

    public Planet GetRandomPlanet()
    {
        if (planets.Count == 0) return null;
        return planets[Random.Range(0, planets.Count)];
    }

    public Planet GetPlanetByName(string name)
    {
        for (int i=0; i < planets.Count; i++)
        {
            if (planets[i].PlanetName == name)
            {
                return planets[i];
            }
        }
        return null;
    }

    public void RegisterPlanet(Planet planet)
    {
        planets.Add(planet);
    }

    public void UnregisterPlanet(Planet planet)
    {
        planets.Remove(planet);
    }
}
