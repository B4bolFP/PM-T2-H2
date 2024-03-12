using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkills : MonoBehaviour
{
    public bool dropsMines;

    public float mineCooldown;
    private float auxMineCooldown = 0;

    private bool canDropMines = true;

    public Transform mineSpawner;
    public GameObject mine;

    public LayerMask layerSuelo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dropMine();
    }

    private void dropMine()
    {
        Instantiate(mine, mineSpawner);
    }
}
