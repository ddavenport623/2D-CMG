using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] List<PlanetEventSO> eventPool = new();
    [SerializeField] int MinDaysBtwEvents = 20;
    [SerializeField] int MaxDaysBtwEvents = 70;
    [SerializeField] ImperialDate nextEvent;
    [SerializeField] List<ActivePlanetEvent> activeEvents = new();

    void Start()
    {
        ScheduleNextEvent(TimeManager.Instance.CurrentDate);
    }

    void OnEnable()
    {
        TimeManager.OnDayPassed += HandleDayPassed;
    }

    void OnDisable()
    {
        TimeManager.OnDayPassed -= HandleDayPassed;
    }

    void HandleDayPassed(ImperialDate cDate)
    {
        if (cDate >= nextEvent)
        {
            TriggerRandomEvent(cDate);
            ScheduleNextEvent(cDate);
        }

        CheckExpirations(cDate);
    }

    void ScheduleNextEvent(ImperialDate cDate)
    {
        nextEvent = cDate + Random.Range(MinDaysBtwEvents, MaxDaysBtwEvents);
    }

    void TriggerRandomEvent(ImperialDate cDate)
    {
        // Pick Planet
        List<Planet> availablePlanets = new();
        foreach (Planet p in PlanetManager.Instance.AllPlanets)
        {
            if (!p.HasEvent)
            {
                availablePlanets.Add(p);
            }
        }
        if (availablePlanets.Count == 0) return;
        Planet newPlanet = availablePlanets[Random.Range(0, availablePlanets.Count)];

        PlanetEventSO newEvent = eventPool[Random.Range(0, eventPool.Count)];

        ActivePlanetEvent newAEvent = new(newEvent, newPlanet, cDate);
        activeEvents.Add(newAEvent);
        newPlanet.currentEvent = newAEvent;
    }

    void CheckExpirations(ImperialDate cDate)
    {
        for (int i=0; i < activeEvents.Count; i++)
        {
            if (activeEvents[i].IsExpired(cDate))
            {
                // TODO PENALTIES
                activeEvents[i].location.currentEvent = null;
                activeEvents.RemoveAt(i);
                i--;
            }
        }
    }

    public bool PlanetHasEvent(Planet p)
    {
        return activeEvents.Exists(e => e.location == p);
    }
}
