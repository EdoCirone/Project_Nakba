using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    public enum DeathAction
    {
        None,    
        Disable, 
        Destroy,
        SceneReload
    }

    [SerializeField] private DeathAction onDeath = DeathAction.Destroy;
    [SerializeField] private float maxHP = 100;
    private float currentHP;
    public AudioClip deathSound;
    public HealthBar healthBar;

    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public bool IsAlive => currentHP > 0;

    private void Awake()
    {
        currentHP = maxHP;
        healthBar?.SetMaxHealth(maxHP);
    }

    public float SetHp(float newHp)
    {
        currentHP = Mathf.Clamp(newHp, 0, maxHP);
        healthBar?.SetHealth(currentHP);
        Debug.Log(currentHP);

        if (currentHP <= 0)
        {
            HandleDeath();
        }
        return currentHP;
    }

    public float AddHp(float amount)
    {
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
        healthBar?.SetHealth(currentHP);
        if (amount < 0 && currentHP <= 0)
        {
            //AudioController.Play(deathSound, transform.position, 1);
            HandleDeath();
        }
        return currentHP;
    }

    public void Kill()
    {
        healthBar?.SetHealth(currentHP);
        SetHp(0);
    }

    public void Revive()
    {
        healthBar?.SetMaxHealth(maxHP);
        SetHp(maxHP);
    }

    private void HandleDeath()
    {
        switch (onDeath)
        {
            case DeathAction.None:                
                break;

            case DeathAction.Disable:
                gameObject.SetActive(false);
                break;

            case DeathAction.Destroy:
                Destroy(gameObject);
                break;
            case DeathAction.SceneReload:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }
}