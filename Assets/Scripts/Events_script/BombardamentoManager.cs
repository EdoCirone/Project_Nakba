using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombardamentoManager : MonoBehaviour
{
    [Header("Intervallo tra i bombardamenti (in secondi)")]
    [SerializeField] float minDelay = 30f;
    [SerializeField] float maxDelay = 90f;

    void Start()
    {
        StartCoroutine(BombardamentoLoop());
    }

    IEnumerator BombardamentoLoop()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            BombardaUnEdificio();
        }
    }

    void BombardaUnEdificio()
    {
        Bombardabile[] tutti = FindObjectsOfType<Bombardabile>();
        List<Bombardabile> validi = new List<Bombardabile>();

        foreach (var b in tutti)
        {
            if (!b.isBombed)
                validi.Add(b);
        }

        if (validi.Count == 0) return;

        int index = Random.Range(0, validi.Count);
        validi[index].TriggerBombardamento();

        Debug.Log(" Bombardamento su: " + validi[index].name);
    }
}
