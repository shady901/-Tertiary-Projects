using UnityEngine;

public class CameraRotation : MonoBehaviour {
    //Created by alex
    // just a simple script that sets up and automatic spin on load screen

    [SerializeField]
    private Transform lookPoint;
    [SerializeField]
    private float rotateSpeed = 12f;

   
    void Update() {
        transform.RotateAround(lookPoint.position, Vector3.up, rotateSpeed * Time.deltaTime);
        transform.LookAt(lookPoint, Vector3.up);
    }

}
