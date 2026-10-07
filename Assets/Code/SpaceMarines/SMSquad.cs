using UnityEngine;

public class SMSquad : MonoBehaviour
{
    // Private
    SpaceMarine[] marines;
    int marineNum = 10;

    // Public
    public float power {get;} = 0 ;

    // Accessible by inspector
    [SerializeField] int squadNum;
    [SerializeField] Planet location;

    public SMSquad(SMSquadSize size, Planet startingLocation, float SMPower)
    {
        location = startingLocation;
        marines = new SpaceMarine[marineNum];
        switch (size)
        {
            case SMSquadSize.full:
            for(int i=0; i<marineNum; i++)
                {
                    marines[i] = new(startingLocation, SMPower);
                    power += marines[i].power;
                }
                break;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum SMSquadSize
{
    full = 0,
    empty
}
