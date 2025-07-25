﻿using UnityEngine;

public class Bisogni : MonoBehaviour
{
    LifeController _lifeController;

    [Header("Valori iniziali")]
    [Range(0, 100)] public float fame = 100f;
    [Range(0, 100)] public float sete = 100f;
    [Range(0, 100)] public float salute = 100f;
    [Range(0, 100)] public float morale = 100f; // morale anche, sarebbe meglio usare il teacher o altri eventi, ma non gli oggetti;
    [Range(0, 100)] public float riposo = 100f;
    [Range(0, 100)] public float fede = 50f; // non possiamo usare oggetti per fede, però ci sta l'imam per questo;

    [Header("Degrado (punti al minuto)")]
    [SerializeField] float fameDecay = 2f;
    [SerializeField] float seteDecay = 3f;
    //[SerializeField] float saluteDecay = 1f;
    [SerializeField] float moraleDecay = 1f;
    [SerializeField] float riposoDecay = 2f;
    [SerializeField] float fedeDecay = 0f; // 0 = la fede non cala da sola

    [Header("Perdita di salute in caso di pericolo")]
    [SerializeField] float _lifeDebuff = 10f; // Perdita di salute in caso di pericolo

    float delta; // Delta time in secondi

    void Update()
    {
        float delta = Time.deltaTime / 60f; // da minuti a secondi

        // Degrada i bisogni base
        fame -= fameDecay * delta;
        sete -= seteDecay * delta;
        //salute -= saluteDecay * delta;
        morale -= moraleDecay * delta;
        riposo -= riposoDecay * delta;
        fede -= fedeDecay * delta;

        // Clamp tra 0 e 100
        fame = Mathf.Clamp(fame, 0, 100);
        sete = Mathf.Clamp(sete, 0, 100);
        //salute = Mathf.Clamp(salute, 0, 100);
        morale = Mathf.Clamp(morale, 0, 100);
        riposo = Mathf.Clamp(riposo, 0, 100);
        fede = Mathf.Clamp(fede, 0, 100);

        // Se fame, sete o riposo sono a 0 → salute degrada più velocemente
        HealtLose(_lifeDebuff);
    }

    // Funzioni pubbliche per modificare ogni bisogno
    public void Mangia(float amount) => fame = Mathf.Clamp(fame + amount, 0, 100);
    public void Bevi(float amount) => sete = Mathf.Clamp(sete + amount, 0, 100);
    //public void Cura(float amount) => salute = Mathf.Clamp(salute + amount, 0, 100);
    public void Conforta(float amount) => morale = Mathf.Clamp(morale + amount, 0, 100);
    public void Dormi(float amount) => riposo = Mathf.Clamp(riposo + amount, 0, 100);
    public void Prega(float amount) => fede = Mathf.Clamp(fede + amount, 0, 100);

    // Stati critici
    public bool ÈInPericolo() =>
        fame <= 0 || sete <= 0 || morale <= 0 || riposo <= 0;

    public void HealtLose(float lifeDebuff)
    {
        if (ÈInPericolo() && _lifeController != null)
        {
            // Degrada salute se in pericolo
            _lifeController.AddHp(-lifeDebuff * delta); // Perdita di 10 HP
        }
    }

    public void ApplySadnessEffect(float moraleMultiplier)
    {
        morale -= moraleDecay * moraleMultiplier * (Time.deltaTime / 60f);
        morale = Mathf.Clamp(morale, 0, 100);
    }


}
