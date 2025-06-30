using UnityEngine;
using UnityEngine.Rendering;

enum warIntensity
{
    Low = 1,
    Medium = 2,
    High = 3,
    Extreme = 4
}

public class WarIntensityController : MonoBehaviour
{
   [SerializeField] warIntensity currentIntensity = warIntensity.Low;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetWarIntensity();
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseWarIntensity();
    }

    private void ResetWarIntensity()
    {
        // Reset the war intensity to Low
        currentIntensity = warIntensity.Low;
        Debug.Log("War intensity reset to: " + currentIntensity);
    }

    private void SetWarIntensity(warIntensity newIntensity)
    {
        // Set the war intensity to the specified level
        currentIntensity = newIntensity;
        Debug.Log("War intensity set to: " + newIntensity);
    }

    private void IncreaseWarIntensity()
    {
        if (currentIntensity < warIntensity.Extreme)
        {
            currentIntensity++;
            Debug.Log("War intensity increased to: " + currentIntensity);
        }
        else
        {
            Debug.Log("War intensity is already at the maximum level: " + currentIntensity);
        }
    }
}
