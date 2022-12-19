using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;
public class PlayerMover : MonoBehaviour
{
    //Created by alex
    // This script uses the WASD for steering, and the f key flips the tank

    // and movement rates are specified below.
    public float rotateSpeed = 90.0f;
    public float speed = 5.0f;
   
    public float bulletSpeed = 20.0f;
   
    public GameObject baseT;
  
   
    void Update()
    {
        
        var transAmount = speed * Time.deltaTime;
        var rotateAmount = rotateSpeed * Time.deltaTime;

        if (Input.GetKey("s"))
        {
            baseT.transform.Translate(0, 0, transAmount);
        }
        if (Input.GetKey("w"))
        {
            baseT.transform.Translate(0, 0, -transAmount);
        }
        if (Input.GetKey("a"))
        {
            baseT.transform.Rotate(0, -rotateAmount, 0);
        }
        if (Input.GetKey("d"))
        {
            baseT.transform.Rotate(0, rotateAmount, 0);
        }
        if (Input.GetKey("f"))
        {
            baseT.transform.Rotate(0, 0, 180);
        }
        

    }

   
}


