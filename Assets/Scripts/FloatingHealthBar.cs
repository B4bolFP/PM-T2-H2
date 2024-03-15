using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }


    // Update is called once per frame
    void Update()
    {

        Vector3 relativepos = player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativepos, Vector3.up);
    }
}
