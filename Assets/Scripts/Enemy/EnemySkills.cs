using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkills : MonoBehaviour
{
    public bool dropsMines;

    public float mineCooldown;
    private float auxMineCooldown = 0;

    private bool canDropMine = true;

    public Transform mineSpawner;
    public GameObject mine;
    void Update()
    {

        auxMineCooldown += Time.deltaTime;

        if (auxMineCooldown >= mineCooldown)
        {
            canDropMine = true;
        }

        if (dropsMines) { dropMine(); }

    }

    private void dropMine()
    {
        if (canDropMine)
        {
            mine.SetActive(true);
            Instantiate(mine, mineSpawner.position, mineSpawner.rotation);
            mine.SetActive(false);
            canDropMine = false;
            auxMineCooldown = 0.0f;
        }
    }
}
