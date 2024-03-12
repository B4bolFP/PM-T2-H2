using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Transform player;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }


    // Update is called once per frame
    void Update()
    {

        Vector3 relativepos = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativepos, Vector3.up);
    }
}
