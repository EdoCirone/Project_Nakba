using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DailyCicleTime : MonoBehaviour
{
    [Header("Durata di ogni fase (in secondi)")]
    public float timePerPhase = 10f;
    private float timer;

    public DayPhase CurrentPhase { get; private set; } = DayPhase.Dawn;
    public static event Action<DayPhase> OnPhaseChanged;

    [Header("Riferimento alla Global Light 2D")]
    [SerializeField] private Light2D globalLight;

    [Header("Hue fisso (colore principale)")]
    [SerializeField, Range(0f, 1f)] private float hue = 0.6f; // Blu
    [SerializeField] private float transitionDuration = 2f;

    [Header("Saturazione per fase")]
    [SerializeField] private float saturationDawn = 0.2f;
    [SerializeField] private float saturationMorning = 0.2f;
    [SerializeField] private float saturationDay = 0.6f;
    [SerializeField] private float saturationAfternoon = 0.2f; 
    [SerializeField] private float saturationEvening = 0.2f;
    [SerializeField] private float saturationNight = 0.05f;

    [Header("Luminosità (Value) per fase")]
    [SerializeField] private float valueDawn = 0.8f;
    [SerializeField] private float valueDay = 1f;
    [SerializeField] private float valueNight = 0.25f;

    private Coroutine transitionCoroutine;

    void Start()
    {
        NotifyPhaseChange();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timePerPhase)
        {
            AdvancePhase();
            timer = 0f;
        }
    }

    void AdvancePhase()
    {
        CurrentPhase = (DayPhase)(((int)CurrentPhase + 1) % Enum.GetNames(typeof(DayPhase)).Length);
        NotifyPhaseChange();
    }

    void NotifyPhaseChange()
    {
        Debug.Log("Nuova fase della giornata: " + CurrentPhase);
        OnPhaseChanged?.Invoke(CurrentPhase);

        float targetS = GetSaturationForPhase(CurrentPhase);
        float targetV = GetValueForPhase(CurrentPhase);

        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(TransitionHSV(targetS, targetV, transitionDuration));
    }

    float GetSaturationForPhase(DayPhase phase)
    {
        return phase switch
        {
            DayPhase.Dawn => saturationDawn,
            DayPhase.Morning => saturationMorning,
            DayPhase.Daytime => saturationDay,
            DayPhase.Afternoon => saturationAfternoon,
            DayPhase.Evening => saturationEvening,
            DayPhase.Night => saturationNight,
            _ => 0.3f,
        };
    }

    float GetValueForPhase(DayPhase phase) //luminosità (Value) per fase
    {
        return phase switch
        {
            DayPhase.Dawn => valueDawn,
            DayPhase.Morning => 0.9f,
            DayPhase.Daytime => valueDay,
            DayPhase.Afternoon => 0.85f,
            DayPhase.Evening => 0.5f,
            DayPhase.Night => valueNight,
            _ => 0.7f,
        };
    }

    IEnumerator TransitionHSV(float targetS, float targetV, float duration)
    {
        float t = 0f;

        Color.RGBToHSV(globalLight.color, out float h0, out float s0, out float v0);

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = Mathf.Clamp01(t / duration);

            float s = Mathf.Lerp(s0, targetS, progress);
            float v = Mathf.Lerp(v0, targetV, progress);

            globalLight.color = Color.HSVToRGB(hue, s, v);
            yield return null;
        }

        globalLight.color = Color.HSVToRGB(hue, targetS, targetV);
    }
}