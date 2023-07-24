using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayCastGun : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 1000f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;

    LineRenderer laserLine;
    float fireTimer;

    [Header("Keybinds")]
    public KeyCode fireKey = KeyCode.Mouse0;

    public GameObject playerController;
    PlayerMovement playerMovement;

    PlayerCam playerCam;

    public GameObject cameraHolder;
    MoveCamera moveCamera;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();

        playerMovement = playerController.GetComponent<PlayerMovement>();
        playerCam = FindObjectOfType<Camera>().GetComponent<PlayerCam>();
        moveCamera = cameraHolder.GetComponentInChildren<MoveCamera>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetKey(fireKey) && fireTimer > fireRate)
        {
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = laserOrigin.position;/* = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));*/
            float degToRad = -(playerCam.xRotation2 / 180) * Mathf.PI;   //Invert it   
            Vector3 rayDirection = new Vector3(playerMovement.playerObj.transform.forward.x, Mathf.Sin(degToRad), playerMovement.playerObj.transform.forward.z);
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection/*playerMovement.playerObj.transform.forward/*playerCamera.transform.forward*/, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.transform.gameObject.tag == "Player")
                    SwitchPlayers(hit);
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }



    //Switch the players out (the illusion of body switching)
    void SwitchPlayers(RaycastHit hit)
    {
        //Debug.Log("Player 1: " + player.gameObject.transform.position);
        //Debug.Log("Player 2: " + hit.transform.position);

        //Game Objects
        /*GameObject playerObj = player.gameObject;*/
        GameObject otherPlayerObj = hit.transform.gameObject;

        //Vectors
        /*Vector3 pos1 = playerObj.transform.position;  //This Player
        Vector3 pos2 = otherPlayerObj.transform.position;              //The Player that will be switched

        //Materials
        Material mat1 = playerObj.transform.Find("PlayerObj").GetComponent<MeshRenderer>().material;
        Material mat2 = otherPlayerObj.transform.Find("PlayerObj").GetComponent<MeshRenderer>().material;

        //Orientation
        Quaternion dir1 = playerObj.transform.rotation;
        Quaternion dir2 = otherPlayerObj.transform.rotation;

        //Velocity
        Vector3 vel1 = playerObj.GetComponent<Rigidbody>().velocity;
        Vector3 vel2 = otherPlayerObj.GetComponent<Rigidbody>().velocity;*/



        /*//deactivate the 2nd player to prevent collsions
        otherPlayerObj.SetActive(false);
        //Teleport the 2nd player to the original player
        otherPlayerObj.transform.position = pos1;
        otherPlayerObj.GetComponentInChildren<MeshRenderer>().material = mat1;
        otherPlayerObj.transform.rotation = dir1;
        otherPlayerObj.GetComponent<Rigidbody>().velocity = vel1;

        //Teleport the player to the 2nd pos (special stuff has to happen because it is not static)
        playerObj.transform.position = pos2;
        playerObj.GetComponentInChildren<MeshRenderer>().material = mat2;
        playerCam.ResetCamera(dir2);      //Reset the Camera
        playerObj.GetComponent<Rigidbody>().velocity = vel2;

        //Then reactivate the 2nd player
        otherPlayerObj.SetActive(true);*/



        //Switch where the controls are
        playerCam.ResetCamera(playerMovement.playerObj.transform.rotation);      //Reset the Camera
        moveCamera.cameraPosition = playerMovement.playerObj.transform.Find("CameraPos");
        playerCam.orientation1 = playerMovement.playerObj.transform;
        playerCam.orientation2 = otherPlayerObj.transform;

        playerMovement.rb = otherPlayerObj.GetComponent<Rigidbody>();
        playerMovement.playerObj = otherPlayerObj;
        playerMovement.orientation = otherPlayerObj.transform.Find("Orientation");

        laserOrigin = otherPlayerObj.transform;









    }

    /*//Changes the Attributes to create the ILLUSION of body swapping
    void ChangeAttributes(GameObject inputObj, GameObject outputObj)
    {
        //Input Object 1 mat
        //Make Object 2 have that mat, etc...
        outputObj.GetComponent<MeshRenderer>().material = inputObj.GetComponent<MeshRenderer>().material;


    }*/





}
