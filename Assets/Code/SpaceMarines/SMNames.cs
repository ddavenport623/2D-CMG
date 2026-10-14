using UnityEngine;

[CreateAssetMenu(fileName = "SMNames", menuName = "Game/SMNames")]
public class SMNames : ScriptableObject
{
    public string[] names;

    public string GetRandomName()
    {
        return names[Random.Range(0, names.Length)];
    }
    
}
