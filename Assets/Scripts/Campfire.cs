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
    private PlayerHealth playerObj;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerObj = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canHeal && playerObj.health < 100)
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
        playerObj.heal(finalHeal);
    }
}
