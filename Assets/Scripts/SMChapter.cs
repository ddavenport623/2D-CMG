using UnityEngine;

public class SMChapter : MonoBehaviour
{
    // private
    int CompanyNum = 10; // Total
    SMCompany [] companies;
    int power;

    // public
    public Planet location;

    public SMChapter(SMChapterSize size, Planet startingPlanet)
    {
        // test.AddComponent(script);
        location = startingPlanet;
        companies = new SMCompany[CompanyNum];
        switch (size)
        {
            case SMChapterSize.full:
                for(int i=0; i<CompanyNum; i++)
                {
                    companies[i] = new SMCompany(SMCompanySize.full, startingPlanet);
                }
                break;
            default:
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

public enum SMChapterSize
{
    full = 0,
    empty
}
