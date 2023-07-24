using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Trigger
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }








    //Touches button
    void OnTriggerEnter(Collider other)
    {
        //Touched the button
        Pressed();
    }

    //Leaves button
    void OnTriggerExit(Collider other)
    {
        //Left the button
        Leave();
    }

    //Stay on button
    //Prevents player1 from walking on it, but then player2 walking on it and leaving.
    //Even though player1 is still on it, it deactivates because player2 left.
    void OnTriggerStay(Collider other)
    {
        Pressed();
    }



    //Walked into button
    void Pressed()
    {
        isTriggered = true;
    }

    //Walked away from button
    void Leave()
    {
        isTriggered = false;
    }





}
