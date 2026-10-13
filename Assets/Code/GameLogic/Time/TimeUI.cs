using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] TextMeshProUGUI speedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeManager.OnDayPassed += UpdateDateDisplay;
        TimeManager.OnSpeedChanged += UpdateSpeedDisplay;

        if(TimeManager.Instance != null)
        {
            UpdateDateDisplay(TimeManager.Instance.CurrentDate);
            UpdateSpeedDisplay(TimeManager.Instance.CurrentSpeed);
        }
    }

    void OnDestroy()
    {
        TimeManager.OnDayPassed -= UpdateDateDisplay;
        TimeManager.OnSpeedChanged -= UpdateSpeedDisplay;
    }

    void UpdateDateDisplay(ImperialDate date)
    {
        if(dateText != null)
        {
            dateText.text = date.ToString();
        }
    }

    void UpdateSpeedDisplay(TimeSpeed speed)
    {
        if(speedText == null) return;

        speedText.text = speed switch
        {
            TimeSpeed.Paused => "PAUSED",
            TimeSpeed.Normal => "1x",
            TimeSpeed.Slow   => "1/2x",
            TimeSpeed.Fast   => "2x",
            _                => "Err"
        };
    }

    public void OnPausedClicked()
    {
        TimeManager.Instance?.TogglePause();
    }

    public void OnSlowerSpeedClicked()
    {
        switch (TimeManager.Instance.CurrentSpeed)
        {
            case TimeSpeed.Paused:
                TimeManager.Instance?.SetSpeed(TimeSpeed.Slow);
                break;
            case TimeSpeed.Slow:
                break; // If already slow, ignore
            case TimeSpeed.Normal:
                TimeManager.Instance?.SetSpeed(TimeSpeed.Slow);
                break;
            case TimeSpeed.Fast:
                TimeManager.Instance?.SetSpeed(TimeSpeed.Normal);
                break;
        }        
    }

    public void OnFasterSpeedClicked()
    {
        switch (TimeManager.Instance.CurrentSpeed)
        {
            case TimeSpeed.Paused:
                TimeManager.Instance?.SetSpeed(TimeSpeed.Fast);
                break;
            case TimeSpeed.Slow:
                TimeManager.Instance?.SetSpeed(TimeSpeed.Normal);
                break; // If already slow, ignore
            case TimeSpeed.Normal:
                TimeManager.Instance?.SetSpeed(TimeSpeed.Fast);
                break;
            case TimeSpeed.Fast:
                break; // If already fast, ignore
        }
    }
    
}
