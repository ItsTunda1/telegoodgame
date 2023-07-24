using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orientation1;
    public Transform orientation2;

    [SerializeField] float xRotation1;
    [SerializeField] float yRotation1;
    [SerializeField] float mouseY1;
    [SerializeField] float mouseX1;

    public float xRotation2;
    [SerializeField] float yRotation2;
    [SerializeField] float mouseY2;
    [SerializeField] float mouseX2;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            //Get Mouse Input
            mouseX2 = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            mouseY2 = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation2 += mouseX2;

            xRotation2 -= mouseY2;
            xRotation2 = Mathf.Clamp(xRotation2, -90f, 90f);

            //Rotate Cam and Orientation
            //transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation2.rotation = Quaternion.Euler(0, yRotation2, 0);
        }

        if (Input.GetMouseButton(1))
        {
            //Get Mouse Input
            mouseX1 = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            mouseY1 = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation1 += mouseX1;

            xRotation1 -= mouseY1;
            xRotation1 = Mathf.Clamp(xRotation1, -90f, 90f);

            //Rotate Cam and Orientation
            transform.rotation = Quaternion.Euler(xRotation1, yRotation1, 0);
            orientation1.rotation = Quaternion.Euler(0/*xRotation1*/, yRotation1, 0);
        }
    }

    public void ResetCamera(Quaternion quat)
    {
        transform.rotation = quat;
        orientation1.rotation = quat;
        /*xRotation1 = quat.eulerAngles.x;
        yRotation1 = quat.eulerAngles.y;*/

        //Part 1 Place Holders
        float yRotation;
        float xRotation;

        //Store Part 1
        yRotation = yRotation1;
        xRotation = xRotation1;

        //Switch 1 with 2
        yRotation1 = yRotation2;
        xRotation1 = xRotation2;
        mouseY1 = 0;
        mouseX1 = 0;

        //Switch 2 with 1 (place holder)
        yRotation2 = yRotation;
        xRotation2 = xRotation;
        mouseY2 = 0;
        mouseX2 = 0;
    }
}
