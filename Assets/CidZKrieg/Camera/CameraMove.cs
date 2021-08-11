using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float sensitivity;
    public bool canMove;

    public float xRotation = 0;
    public float yRotation = 0;

    float mouseX;
    float mouseY;

    public float viewValue;

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            canMove = true;
            //Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            canMove = false;
            //Cursor.lockState = CursorLockMode.Confined;
        }



        /*if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.fieldOfView < 100) // back
        {
            Camera.main.fieldOfView += 1;
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.fieldOfView > 25) // forward
        {
            Camera.main.fieldOfView -= 1;
        }*/

        viewValue += (Input.GetAxis("Mouse ScrollWheel") * 200) * Time.deltaTime;

        viewValue = Mathf.Clamp(viewValue, 25, 75);

        Camera.main.fieldOfView = viewValue;

        if (Input.GetKeyDown(KeyCode.Mouse2)) //reset camera zoom
        {
            Camera.main.fieldOfView = 50;
        }



        if (canMove)
        {

            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");


            if (mouseX > 0)
            {
                xRotation += 0.01f * sensitivity;
            }

            else if (mouseX < 0)
            {
                xRotation -= 0.01f * sensitivity;
            }

            if (mouseY > 0)
            {
                yRotation += 0.01f * sensitivity;
            }

            else if (mouseY < 0)
            {
                yRotation -= 0.01f * sensitivity;
            }

            xRotation = Mathf.Clamp(xRotation, -30, 30);
            yRotation = Mathf.Clamp(yRotation, -20, 25);

            transform.localRotation = Quaternion.Euler(-yRotation, xRotation, 0f);
        }


        //Vector3 rotation = new Vector3(0, 0, 0);

        //transform.Rotate(Vector3.up * mouseX);
        
    }
}
