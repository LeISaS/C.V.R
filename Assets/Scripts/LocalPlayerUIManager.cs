using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocalPlayerUIManager : MonoBehaviour
{

    [SerializeField]
    GameObject Back_Button;

    // Start is called before the first frame update
    void Start()
    {
        Back_Button.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndLoadHomeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}