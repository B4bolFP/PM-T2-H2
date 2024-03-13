using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    public int healingAmount;
    public int healingMultiply;
    public float healingCooldown;
    private int finalHeal;
    private float auxHealingCooldown;
    private bool canHeal = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (canHeal)
        {
            heal();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.CompareTag("Player")) {
            canHeal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canHeal = false;
        }
    }

    private void heal()
    {
        finalHeal = healingAmount * healingMultiply;
        player.GetComponent<PlayerHealth>().heal(finalHeal);
    }
}
