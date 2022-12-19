using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pcamera : MonoBehaviour
{
    //Created by alex
    //had alot of issues with this camera as it was the first time i had made one, at first this camera was moving the tank slightly i wasnt to sure why but it had dissapeared later on in the project after some edits


        //vaibles
    public GameObject target;
    public float rotateSpeed = 5;
    Vector3 offset;
    //will be called on the first frame
    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void Update()
    {
       
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);
    

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);


        transform.LookAt(target.transform.position);
    }
}