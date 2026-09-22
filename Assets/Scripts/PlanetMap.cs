using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class PlanetMap
{
    private List<int> PlanetIds = null;
    private List<Vector3> PlanetPositions = null;

    public void AddPlanet(int id, Vector3 PlanetPosition)
    {
        PlanetIds.Add(id);
        PlanetPositions.Add(PlanetPosition);
    }

    public Vector3 PositionById(int id)
    {
        Vector3 returnValue = new(0, 0, 1);
        if (!PlanetIds.Contains(id))
        {
            Debug.Log("Planet not found by id: " + id);
            return returnValue;
        }
        int index = PlanetIds.FindIndex(x => x == id);
        return PlanetPositions.ElementAt(index);
    }
}