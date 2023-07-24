using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    //This object
    GameObject door;

    //Requirements
    public Trigger[] triggers;

    //Is open
    public bool isOpen;
    bool previsOpen;









    // Start is called before the first frame update
    void Start()
    {
        door = this.gameObject;



    }

    // Update is called once per frame
    void Update()
    {

        previsOpen = isOpen;        //Set Previous
        isOpen = CheckIfOpened();   //Then Check

        //If they change
        if (previsOpen != isOpen)
        {
            //Open or close
            if (isOpen == true)
            {
                Open();
            }
            else
            {
                //Close();
            }



        }
        




    }





    //Check if opened
    bool CheckIfOpened()
    {
        //Open it
        for (int i = 0; i < triggers.Length; i++)
        {
            //If it ever equals false
            if (triggers[i].isTriggered == false)
            {
                return false;
            }

        }

        //It was always true (same goes for a door with no buttons)
        return true;
    }



    //Open
    void Open()
    {
        //Rotate it open
        door.transform.rotation = new Quaternion(90, 0, 0, 0);
    }

    //Close
    void Close()
    {
        //Rotate it close
        door.transform.rotation = new Quaternion(0, 0, 0, 0);
    }




}
