using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    public KeyCode killKey = KeyCode.K;

    public Animator animacion;
    public Transform tr;

    // Update is called once per frame
    void Update()
    {
        MyInput();
        tr.position = new Vector3(-149.51f, 2.55f, 119.21f);
    }

    private void MyInput()
    {
        if (Input.GetKey(killKey))
        {
            animacion.Play("DieAnimation", 0, 0.0f);
        }
    }
}
