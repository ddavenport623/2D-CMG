using UnityEngine;

public class GameManager : MonoBehaviour
{
    // private
    SMChapter chapter;

    // public
    public Planet startingPlanet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chapter = CreateSpaceMarines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private SMChapter CreateSpaceMarines()
    {
        SMChapterSize ChapterSize = SMChapterSize.full;
        SMChapter newChapter = new(ChapterSize, startingPlanet);
        return newChapter;
    }
}
