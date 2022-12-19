using Photon.Pun;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

[RequireComponent(typeof(FirstPersonController))]

public class PlayerNetworkMover : MonoBehaviourPunCallbacks, IPunObservable {
    //Created by alex
    // this had the majority of issues with photon call backs ect as tanks wouldnt spawn or glitch around
    [SerializeField]
    private GameObject cameraObject;
    [SerializeField]

    private GameObject playerObject;
    [SerializeField]
    

    private Vector3 position;
    private Quaternion rotation;
   
    private float smoothing = 10.0f;
    PhotonView photonView;

    /// <summary>
    /// Move game objects to another layer.
    /// </summary>
    void MoveToLayer(GameObject gameObject, int layer) {
        gameObject.layer = layer;
        foreach(Transform child in gameObject.transform) {
            MoveToLayer(child.gameObject, layer);
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        photonView = GetComponentInChildren<PhotonView>();
        if (photonView.IsMine)
        {
            cameraObject.SetActive(true);
        }
        else
        {
            cameraObject.SetActive(false);
            
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        if (photonView.IsMine) {
            GetComponent<PlayerMover>().enabled = true;

            GetComponent<FirstPersonController>().enabled = true;
            MoveToLayer(playerObject, LayerMask.NameToLayer("Hidden"));
     
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
           
        } else {
            position = transform.position;
            rotation = transform.rotation;
            GetComponent<PlayerMover>().enabled = false;

            GetComponent<FirstPersonController>().enabled = false;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
           
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if (!photonView.IsMine) {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing);
        }
    }

   

    /// <summary>
    /// Used for synchronization of variables in a script watched by a photon network view.
    /// </summary>
    /// <param name="stream">The network bit stream.</param>
    /// <param name="info">The network message information.</param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        } else {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
        }
    }

}
