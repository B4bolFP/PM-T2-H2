using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI healthText;
    private int maxHealth;

    private void Start()
    {
        maxHealth = health;
    }

    private void Update()
    {
        healthText.text = "Health: " + health;
        if (health > 100) { health = 100; }
    }

    public void heal(int amount)
    {
        health += amount;
    }

    public void damage(int amount)
    {
        health -= amount;
    }
}
