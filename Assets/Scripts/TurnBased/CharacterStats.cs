using UnityEngine;
using System;

[System.Serializable]
public class CharacterStats : MonoBehaviour , IComparable<CharacterStats>
{

    public new string name;

    public int maxHealth = 100;

    public int currentHealth { get; private set; }

    public Stats baseAttack;

    public Stats baseDefense;

    public Stats baseMagic;

    public Stats baseAgility;

    //status ailments
    public bool isConfused = false;
    public bool isBurned = false;
    public bool isStunned = false;
    public bool isDead = false;


    //ailment counter
    int statusLimit;
    int statusCounter;



    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public int CompareTo(CharacterStats other)
    {
        if (other == null) { return 1; }

        return other.baseAgility.GetValue() - baseAgility.GetValue();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print(transform.name + " takes " + damage + " damage");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        currentHealth = maxHealth;
    }

    public void Confuse()
    {
        isConfused = true;
        CalculateStatusCounter();
    }

    public void Burn()
    {
        isBurned = true;
    }

    public void Stun()
    {
        isStunned = true;
    }

    public void CalculateStatusCounter()
    {
        if(isConfused)
        {
            statusLimit = UnityEngine.Random.Range(1, 3);
        }
    }

    public string GetStatus()
    {
        if(isConfused)
        {
            return transform.name + " is Confuzzled";
        }
        if(isStunned)
        {
            return transform.name + " is Stunned";
        }
        else
        {
            return "This shouldn't show up......";
        }
        
    }

    public bool CheckCanAttack()
    {
        if (isStunned && statusCounter < statusLimit)
        {
            statusCounter += 1;
            return false;
        }
        if (isConfused && statusCounter < statusLimit)
        {
            statusCounter += 1;
            return true;
        }
        else
        {
            statusCounter = 0;
            statusLimit = 0;
            nullifyAilment();
            return true;
        }
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public void nullifyAilment()
    {
        isConfused = false;
        isBurned = false;
        isStunned = false;
    }
}
