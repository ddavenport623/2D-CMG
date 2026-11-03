using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    [SerializeField] List<PlanetEventSO> eventPool = new();
    [SerializeField] int MinDaysBtwEvents = 20;
    [SerializeField] int MaxDaysBtwEvents = 70;
    [SerializeField] ImperialDate nextEvent;
    [SerializeField] List<ActivePlanetEvent> activeEvents = new();

    public static event Action<ActivePlanetEvent> OnEventTriggered;

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
        nextEvent = cDate + UnityEngine.Random.Range(MinDaysBtwEvents, MaxDaysBtwEvents);
    }

    // void TriggerRandomEvent(ImperialDate cDate)
    // {
    //     // Pick Planet
    //     List<Planet> availablePlanets = new();
    //     foreach (Planet p in PlanetManager.Instance.AllPlanets)
    //     {
    //         if (!p.HasEvent)
    //         {
    //             availablePlanets.Add(p);
    //         }
    //     }
    //     if (availablePlanets.Count == 0) return;
    //     Planet newPlanet = availablePlanets[UnityEngine.Random.Range(0, availablePlanets.Count)];

    //     PlanetEventSO newEvent = eventPool[UnityEngine.Random.Range(0, eventPool.Count)];

    //     TriggerSpecificEvent(newEvent, newPlanet, cDate);
    // }

    void TriggerRandomEvent(ImperialDate cDate)
    {
        Debug.Log($"[EventManager] Attempting to trigger event on {cDate}...");

        // Pick Planet
        List<Planet> availablePlanets = new();
        Debug.Log($"[EventManager] Total planets in PlanetManager: {PlanetManager.Instance.AllPlanets.Count}");
        foreach (Planet p in PlanetManager.Instance.AllPlanets)
        {
            if (!p.HasEvent)
            {
                availablePlanets.Add(p);
            }
            else
            {
                Debug.Log($"Planet Not available: {p.PlanetName}");
            }
        }

        Debug.Log($"[EventManager] Available planets found: {availablePlanets.Count}");
        if (availablePlanets.Count == 0) return;

        if (eventPool.Count == 0)
        {
            Debug.LogError("[EventManager] Event Pool is empty! Drag an event into the Inspector!");
            return;
        }

        Planet newPlanet = availablePlanets[UnityEngine.Random.Range(0, availablePlanets.Count)];
        PlanetEventSO newEvent = eventPool[UnityEngine.Random.Range(0, eventPool.Count)];

        Debug.Log($"[EventManager] Spawning '{newEvent.title}' on {newPlanet.PlanetName}!");
        TriggerSpecificEvent(newEvent, newPlanet, cDate);
    }

    void TriggerSpecificEvent(PlanetEventSO newEvent, Planet newPlanet, ImperialDate cDate)
    {
        ActivePlanetEvent newAEvent = new(newEvent, newPlanet, cDate);
        activeEvents.Add(newAEvent);
        newPlanet.currentEvent = newAEvent;
        OnEventTriggered?.Invoke(newAEvent);
    }

    void CheckExpirations(ImperialDate cDate)
    {
        for (int i=0; i < activeEvents.Count; i++)
        {
            if (activeEvents[i].IsExpired(cDate))
            {
                TriggerSpecificEvent(activeEvents[i].planetEvent.onCriticalFailEvent, activeEvents[i].location, cDate);
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
