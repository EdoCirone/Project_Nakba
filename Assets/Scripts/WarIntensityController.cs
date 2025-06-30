using UnityEngine;

public enum WarIntensity
{
    Low = 1,
    Medium = 2,
    High = 3,
    Extreme = 4
}

public class WarIntensityController : MonoBehaviour
{
    public WarIntensity currentIntensity = WarIntensity.Low;
    [SerializeField] private float increaseInterval = 60f; // in secondi
    private float timer;

    void Start()
    {
        ResetWarIntensity();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= increaseInterval)
        {
            IncreaseWarIntensity();
            timer = 0f;
        }
    }

    private void ResetWarIntensity()
    {
        currentIntensity = WarIntensity.Low;
        Debug.Log("🔁 War intensity reset to: " + currentIntensity);
    }

    private void SetWarIntensity(WarIntensity newIntensity)
    {
        currentIntensity = newIntensity;
        Debug.Log("🛠️ War intensity set to: " + currentIntensity);
    }

    private void IncreaseWarIntensity()
    {
        if (currentIntensity < WarIntensity.Extreme)
        {
            currentIntensity++;
            Debug.Log("⚠️ War intensity increased to: " + currentIntensity);
        }
        else
        {
            Debug.Log("🔥 War intensity is already at max: " + currentIntensity);
        }
    }

    public WarIntensity GetCurrentIntensity()
    {
        return currentIntensity;
    }
}
