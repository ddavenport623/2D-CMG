using UnityEngine;

[System.Serializable]
public class ActivePlanetEvent
{
    public PlanetEventSO planetEvent;
    public Planet location;
    public ImperialDate startDate;
    public ImperialDate endDate;

    public ActivePlanetEvent(PlanetEventSO pEvent, Planet p, ImperialDate cDate)
    {
        planetEvent = pEvent;
        location = p;
        startDate = cDate;
        endDate = startDate.AddDays(pEvent.durationInDays);
    }

    public bool IsExpired(ImperialDate cDate)
    {
        return cDate >= endDate;
    }

    public int TimeTillExpire(ImperialDate cDate)
    {
        return Mathf.Max(0, endDate - cDate);
    }
}
