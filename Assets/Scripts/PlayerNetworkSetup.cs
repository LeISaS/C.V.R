using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    public GameObject LocalXRRigGameObject;
    public GameObject MainAvatarGameObject;


    public GameObject AvatarHeadGameObject;
    public GameObject AvatarBodyGameObject;


    public TextMeshProUGUI PlayerName_Text;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            //Player Local
            LocalXRRigGameObject.SetActive(true);

            SetLayerRecursively(AvatarHeadGameObject,6);
            SetLayerRecursively(AvatarBodyGameObject,7);

            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();

            if(teleportationAreas.Length>0)
            {
                Debug.Log("Found" + teleportationAreas.Length + "teleportation area.");
                foreach(var item in teleportationAreas )
                {
                    item.teleportationProvider = LocalXRRigGameObject.GetComponent<TeleportationProvider>();
                }
            }

            MainAvatarGameObject.AddComponent<AudioListener>();
        }
        else
        {
            //player remote
            LocalXRRigGameObject.SetActive(false);
            SetLayerRecursively(AvatarHeadGameObject, 0);
            SetLayerRecursively(AvatarBodyGameObject, 0);
        }

        if(PlayerName_Text!=null)
        {
            PlayerName_Text.text = photonView.Owner.NickName;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach(Transform trans in go.GetComponentInChildren<Transform>(true))
            {
            transform.gameObject.layer = layerNumber;
        }
    }
}
