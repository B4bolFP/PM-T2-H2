using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int mineDamage;
    public float mineSpeed;
    public ParticleSystem eeexplosion;

    private void OnTriggerEnter(Collider other)
    {
        blowMine(other);
    }

    IEnumerator destroyMine()
    {
        yield return new WaitForSeconds(mineSpeed);
        Destroy(gameObject);
    }

    public void blowMine(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerHealth>().damage(50);
            eeexplosion.Play();
            StartCoroutine("destroyMine");
        }
    }

    public void shootMine()
    {
        eeexplosion.Play();
        StartCoroutine("destroyMine");
    }
}
