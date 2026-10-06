using UnityEngine;

public class SMCompany : MonoBehaviour
{
    // private    
    SMSquad[] squads;
    int squadNum = 10;

    // public
    public int CompanyNum; // Company Number
    public Planet location;

    public SMCompany(SMCompanySize size, Planet startingPlanet)
    {
        location = startingPlanet;
        squads = new SMSquad[squadNum];
        switch (size)
        {
            case SMCompanySize.full:
                for(int i=0; i<squadNum; i++)
                {
                    squads[i] = new(SMSquadSize.full, startingPlanet);
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

public enum SMCompanySize
{
    full = 0,
    empty
}
