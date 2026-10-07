using UnityEngine;
using System;

public enum TimeSpeed
{
    Paused = 0,
    Slow = 1,
    Normal = 2,
    Fast = 4
}

public class TimeManager : MonoBehaviour
{
    // Singleton
    public static TimeManager Instance {get; private set;}

    // Controls
    private PlayerControls controls;

    // Starting date
    [Header("Starting Date")] [SerializeField] private int startDay = 1;
    [SerializeField] private int startMonth = 1;
    [SerializeField] private int startYear = 000;
    [SerializeField] private int startMillennium = 35;

    // Speed settings
    [Header("Speed Settings")] [Tooltip("How many real seconds equal 1 in-game day at Normal 1x speed")] 
    [SerializeField] private float realSecondsPerDay = 0.5f; // 2 days per second

    // States
    public ImperialDate CurrentDate {get; private set;}
    public TimeSpeed CurrentSpeed {get; private set;} = TimeSpeed.Normal;
    private TimeSpeed previousSpeed = TimeSpeed.Normal;
    private float dayTimer = 0f; // Count of seconds until next day

    // Events
    public static event Action<ImperialDate> OnDayPassed;
    public static event Action<ImperialDate> OnMonthPassed;
    public static event Action<ImperialDate> OnYearPassed;
    public static event Action<ImperialDate> OnMillenniumPassed;
    public static event Action<TimeSpeed> OnSpeedChanged;

    private void Awake()
    {
        // Make sure only singleton can ever exist
        if (Instance == null)
        {
            Instance = this;
            CurrentDate = new ImperialDate(startDay, startMonth, startYear, startMillennium);
        } else
        {
            Destroy(gameObject);
        }

        controls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        // Do nothing if paused
        if (CurrentSpeed == TimeSpeed.Paused) return;

        // calculate time
        float speedMultiplier = (float)CurrentSpeed / 2;
        dayTimer += Time.unscaledDeltaTime * speedMultiplier;

        // tick
        while (dayTimer >= realSecondsPerDay)
        {
            dayTimer -= realSecondsPerDay;
            AdvanceOneDay();
        }
    }

    private void OnEnable()
    {
        controls.Enable();

        // subscribe to events
        controls.TimeControls.TogglePause.performed += ctx => TogglePause();
        controls.TimeControls.Slow.performed += ctx => SetSpeed(TimeSpeed.Slow);
        controls.TimeControls.Normal.performed += ctx => SetSpeed(TimeSpeed.Normal);
        controls.TimeControls.Fast.performed += ctx => SetSpeed(TimeSpeed.Fast);
    }

    private void OnDisable()
    {
        controls.TimeControls.TogglePause.performed -= ctx => TogglePause();
        controls.TimeControls.Slow.performed -= ctx => SetSpeed(TimeSpeed.Slow);
        controls.TimeControls.Normal.performed -= ctx => SetSpeed(TimeSpeed.Normal);
        controls.TimeControls.Fast.performed -= ctx => SetSpeed(TimeSpeed.Fast);

        controls.Disable();
    }

    public void SetSpeed(TimeSpeed newSpeed)
    {
        if (CurrentSpeed != newSpeed)
        {
            if (CurrentSpeed != TimeSpeed.Paused)
            {
                previousSpeed = CurrentSpeed;
            }
            CurrentSpeed = newSpeed;
            OnSpeedChanged?.Invoke(CurrentSpeed);
        }
    }

    public void TogglePause()
    {
        if (CurrentSpeed == TimeSpeed.Paused) // If paused, set to previous speed
        {
            SetSpeed(previousSpeed == TimeSpeed.Paused ? TimeSpeed.Normal : previousSpeed);
        } else // If not, pause
        {
            SetSpeed(TimeSpeed.Paused);
        }
    }

    private void AdvanceOneDay()
    {
        CurrentDate = CurrentDate.AddDay(out bool monthChanged, out bool yearChanged);

        // Notify subscribers
        OnDayPassed?.Invoke(CurrentDate);

        if (monthChanged)
        {
            OnMonthPassed?.Invoke(CurrentDate);
        }
        if (yearChanged)
        {
            OnYearPassed?.Invoke(CurrentDate);
        }
    }
}
