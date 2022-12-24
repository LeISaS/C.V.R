using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;

    public TextMeshProUGUI occupancyRateText_OutDoor;
    public TextMeshProUGUI occupancyRateText_city;


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnectedAndReady) 
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Servers again");
        PhotonNetwork.JoinLobby();
    }


    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_CityScene()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_CITYSCENE;
        ExitGames.Client.Photon.Hashtable CoustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR } };
        PhotonNetwork.JoinRandomRoom(CoustomRoomProperties, 0);
    }

    public void OnEnterButtonClicked_OutDoor()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        ExitGames.Client.Photon.Hashtable CoustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR } };
        PhotonNetwork.JoinRandomRoom(CoustomRoomProperties,0);
    }



    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is Create with te name : " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Local Player : " + PhotonNetwork.NickName + "join To" + PhotonNetwork.CurrentRoom.Name + "Player Count : " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY)) 
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType)) 
            {
                Debug.Log("Joined room with the map: " + (string)mapType);
                if((string)mapType==MultiplayerVRConstants.MAP_TYPE_VALUE_CITYSCENE)
                {
                    //Load the CityScene
                    PhotonNetwork.LoadLevel("CityScene");
                }
                else if((string)mapType ==MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR)
                {
                    //Load the outdoor
                    PhotonNetwork.LoadLevel("OutDoor");
                }
            }
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log( newPlayer.NickName + "Player Count : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_"  + mapType+ Random.Range(0, 1000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        //æ¿¿Ã∏ß
        //1.CityScene = "cityScene
        //2.OutDoor = "outdoor"


        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
        
        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;
        

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined is Lobby");
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(roomList.Count == 0)
        {
            //no room
            occupancyRateText_OutDoor.text = 0 + " /  " + 20;
            occupancyRateText_city.text = 0 + " /  " + 20;
        }

        foreach(RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if(room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR))
            {
                //Update OutDoor room occ
                Debug.Log("Room is OutDoor map Player Count : " + room.PlayerCount);
                occupancyRateText_OutDoor.text = room.PlayerCount + " / " + 20;
            }
            else if(room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_CITYSCENE))
            {
                Debug.Log("Room is City map Player Count : " + room.PlayerCount);
                occupancyRateText_city.text = room.PlayerCount + " / " + 20;
            }
            
        }


    }




}
