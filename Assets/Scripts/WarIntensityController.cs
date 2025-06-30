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
        warIntensity currentIntensity = warIntensity.Low;
        Debug.Log("War intensity reset to: " + currentIntensity);
    }

    private void SetWarIntensity(warIntensity newIntensity)
    {
        // Set the war intensity to the specified level
        Debug.Log("War intensity set to: " + newIntensity);
    }

    private void IncreaseWarIntensity()
    {
        // Increase the war intensity by one level
        warIntensity currentIntensity = warIntensity.Low;
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
