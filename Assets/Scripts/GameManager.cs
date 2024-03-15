using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI enemiesLeftText;

    public GameObject player;

    public GameObject[] enemies;
    public GameObject[] enemySpawners;

    public int currentRound;
    private int enemiesLeft;

    private void Start()
    {
        spawnEnemies(currentRound);

        enemiesLeft = GameObject.FindGameObjectsWithTag("enemy").Length;

        actualizarhud();
    }

    private void Update()
    {

        enemiesLeft = GameObject.FindGameObjectsWithTag("enemy").Length;

        if (currentRound == 4) { SceneManager.LoadScene(2); }

        if (enemiesLeft <= 0 && currentRound < 4)
        {
            nextRound();
        }

        actualizarhud();
    }

    private void spawnEnemies(int round)
    {
        if (currentRound == 1)
        {
            foreach (var spawner in enemySpawners)
            {
                Instantiate(enemies[0], spawner.transform);
                Instantiate(enemies[0], spawner.transform);
                Instantiate(enemies[0], spawner.transform);
                Instantiate(enemies[0], spawner.transform);
            }
        } else if (currentRound == 2)
        {
            foreach (var spawner in enemySpawners)
            {
                Instantiate(enemies[0], spawner.transform);
                Instantiate(enemies[1], spawner.transform);
                Instantiate(enemies[0], spawner.transform);
                Instantiate(enemies[1], spawner.transform);
            }
        } else if (currentRound == 3)
        {
            foreach (var spawner in enemySpawners)
            {
                Instantiate(enemies[0], spawner.transform);
                Instantiate(enemies[1], spawner.transform);
                Instantiate(enemies[2], spawner.transform);
                Instantiate(enemies[0], spawner.transform);
                Instantiate(enemies[1], spawner.transform);
                Instantiate(enemies[2], spawner.transform);
            }
        }
    }

    private void nextRound()
    {
        this.currentRound++;
        spawnEnemies(currentRound);
        
    }

    private void actualizarhud()
    {
        enemiesLeftText.text = "Enemies " + enemiesLeft;
        if (currentRound == 3)
        {
            roundText.text = "Final Round";
        } else
        {
            roundText.text = "Round " + currentRound;
        }
    }

}
