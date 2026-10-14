using UnityEngine;

[CreateAssetMenu(fileName = "NewPlanetEvent", menuName = "Game/Planet Event")]
public class PlanetEventSO : ScriptableObject
{
    [Header("Event Details")]
    public string eventId;
    public string title;
    [TextArea(3, 6)]
    public string description;

    [Header("Spawning Weights")]
    [Tooltip("Relative weight for random selection")]
    public int spawnWeight = 10;

    [Header("Duration")]
    [Tooltip("How many days the chapter has to respond/resolve this event")]
    public int durationInDays = 30;
}