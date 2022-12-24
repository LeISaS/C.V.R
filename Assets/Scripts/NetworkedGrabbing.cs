using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkedGrabbing : MonoBehaviour, IPunOwnershipCallbacks
{

    PhotonView m_photonView;
    Rigidbody rb;
    // Start is called before the first frame update

    bool isBeingHeld = false;
    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld)
        {
            //Object is being graabed;
            rb.isKinematic = true;
            gameObject.layer = 11;
        }
        else
        {
            rb.isKinematic = false;
            gameObject.layer = 9;
        }
    }

    private void TransferOwnerShip()
    {
        m_photonView.RequestOwnership();
    }

    public void OnSelectEntered()
    {
        Debug.Log("Graabed");
        m_photonView.RPC("StartNetworkGrabing",RpcTarget.AllBuffered);

        if (m_photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            ;
        }
        else
        {
            TransferOwnerShip();
        }

    }

    public void OnSeletedExisted()
    {
        Debug.Log("Released");
        m_photonView.RPC("StopNetworkGrabbing", RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if(targetView!=m_photonView)
        {
            return;
        }

        Debug.Log("OwnerShip Requested for : " + targetView.name + "from " + requestingPlayer.NickName);
        m_photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("OnOwnerShip Trasnferred to " + targetView.name + "from " + previousOwner.NickName);
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
    }

    [PunRPC]
    public void StartNetworkGrabbing()
    {
        isBeingHeld = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        isBeingHeld = false;
    }
}
