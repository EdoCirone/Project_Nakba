using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;

    void OnEnable()
    {
        DailyCicleTime.OnDayChanged += UpdateDayText; 
    }

    void OnDisable()
    {
        DailyCicleTime.OnDayChanged -= UpdateDayText;
    }

    void UpdateDayText(int newDay)
    {
        dayText.text = $"Giorno {newDay}";
    }
}
