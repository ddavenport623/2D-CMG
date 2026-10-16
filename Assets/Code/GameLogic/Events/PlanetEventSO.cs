using UnityEngine;

[CreateAssetMenu(fileName = "NewPlanetEvent", menuName = "Game/Planet Event")]
public class PlanetEventSO : ScriptableObject
{
    // Details for code
    [Header("Event Details")]
    public string eventId;
    public string title;
    [Tooltip("Only for developer")]
    [TextArea(3, 6)]
    public string devDescription;
    public bool canSpawnNaturally;

    // Messages that will be seen by the user
    [Header("Messages")]
    [Tooltip("Message Player receives for Event before sending SM")]
    [TextArea(3, 6)]
    public string alertMessage;

    [Tooltip("Message Player receives when SM arrives")]
    [TextArea(3, 6)]
    public string arriveMessage;

    [Tooltip("Message Player receives when no SM are sent")]
    [TextArea(3, 6)]
    public string noArriveMessage;

    [Tooltip("Message Player receives when event crit suceeds")]
    [TextArea(3, 6)]
    public string critSucceedMessage;

    [Tooltip("Message Player receives when event suceeds with injuries")]
    [TextArea(3, 6)]
    public string easySucceedMessage;

    [Tooltip("Message Player receives when event suceeds but was hard won")]
    [TextArea(3, 6)]
    public string HardSucceedMessage;

    [Tooltip("Message Player receives when event fails but was close to succeeding")]
    [TextArea(3, 6)]
    public string HardFailMessage;

    [Tooltip("Message Player receives when event fails and wasn't close")]
    [TextArea(3, 6)]
    public string easyFailMessage;

    [Tooltip("Message Player receives when event crit fails")]
    [TextArea(3, 6)]
    public string critFailsMessage;

    // Numbers for difficulty of mission
    [Header("Difficulty numbers")]
    [Tooltip("Power needed for success with no consequences")]
    public int critSuccess = 150;

    [Tooltip("Power needed for mission to succeed but with minor consequences")]
    public int success = 15;

    [Tooltip("Power needed to not auto fail")]
    public int fail = 10;

    [Tooltip("Minimum power needed to not critical Fail")]
    public int critFail = 5;

    // Time
    [Header("Duration")]
    [Tooltip("How many days the chapter has to respond/resolve this event")]
    public int durationToRespond = 60;

    [Header("Chain Events")]
    public PlanetEventSO onFailEvent;
    public PlanetEventSO onCriticalFailEvent;
    public PlanetEventSO onSuccessEvent;
}