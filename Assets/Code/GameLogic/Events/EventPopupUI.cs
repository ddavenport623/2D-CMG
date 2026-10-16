using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventPopupUI : MonoBehaviour
{
    // UI
    [SerializeField] GameObject popupPanel;
    [SerializeField] TextMeshProUGUI titleText; // Event title
    [SerializeField] TextMeshProUGUI planetText; // Planet Name
    [SerializeField] TextMeshProUGUI descriptionText; // alert message
    [SerializeField] TextMeshProUGUI deadlineText; // days remaining until auto fail
    [SerializeField] Button acknowledgeButton; // continue/close

    // Queue and state
    Queue<ActivePlanetEvent> eventQueue = new();
    bool isShowingEvent = false;
    TimeSpeed prevSpeed; // Speed before popup

    void OnEnable()
    {
        EventManager.OnEventTriggered += HandleEventTriggered;
        acknowledgeButton.onClick.AddListener(OnAcknowledgeClicked);
    }

    void OnDisable()
    {
        EventManager.OnEventTriggered -= HandleEventTriggered;
        acknowledgeButton.onClick.RemoveListener(OnAcknowledgeClicked);
    }

    void HandleEventTriggered(ActivePlanetEvent newEvent)
    {
        if (isShowingEvent)
        {
            eventQueue.Enqueue(newEvent);
        } else
        {
            ShowEvent(newEvent);
        }
    }

    void ShowEvent(ActivePlanetEvent evt)
    {
        if (!isShowingEvent)
        {
            prevSpeed = TimeManager.Instance.CurrentSpeed;        
        }
        isShowingEvent = true;
        TimeManager.Instance.SetSpeed(TimeSpeed.Paused);
        titleText.text = evt.planetEvent.title;
        planetText.text = evt.location.PlanetName;
        descriptionText.text = evt.planetEvent.alertMessage;
        deadlineText.text = evt.planetEvent.durationToRespond.ToString();

        popupPanel.SetActive(true);
    }

    void OnAcknowledgeClicked()
    {
        if(eventQueue.Count > 0)
        {
            ShowEvent(eventQueue.Dequeue());
            return;
        }
        popupPanel.SetActive(false);
        isShowingEvent = false;
        TimeManager.Instance.SetSpeed(prevSpeed);
    }
}
