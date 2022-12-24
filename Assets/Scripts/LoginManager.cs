using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class LoginManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField Playername_InputName;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ConnectAnnoymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        if(Playername_InputName!=null)
        {
            PhotonNetwork.NickName = Playername_InputName.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }


    #region Photon Callback Methods
    public override void OnConnected()
    {
        Debug.Log("OnConnected is called");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnected is MasterServer");
        PhotonNetwork.LoadLevel("MyHomeScene");
    }

    #endregion

}
