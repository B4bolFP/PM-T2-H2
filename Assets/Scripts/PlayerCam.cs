using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCam : MonoBehaviour
{
    // Variables de sensibilidad publicas
    // De esta forma podremos cambiarlos desde el menu
    public float sensX;
    public float sensY;

    // Cambiaremos la direccion de la camara con este transform
    public Transform orientation;

    // Variables para rotar la camara
    public float xRotation;
    public float yRotation;

    // Distamcia mira
    public float distance;

    private void Start()
    {
        // Volvemos el cursor invisible y impedimos su movimiento en el primer frame
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        // Obtenemos el input del raton cada frame
        // Lo multiplicamos por deltaTime y la sensibilidad para tener mayor control
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotamos la camara y la orientacion de esta
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}
