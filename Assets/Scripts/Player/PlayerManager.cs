using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    
    enum PlayerType
    {
        WARRIOR = 0,
        MAGE = 1,
        ARCHER = 2,
        TOGGLE
    }

    PlayerType currentType = PlayerType.WARRIOR;

    static GameObject currentPlayer;

	// Use this for initialization
	void Start () {
        currentPlayer = transform.GetChild((int)currentType).gameObject;

        currentPlayer.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 temp = currentPlayer.transform.position;

        //currentPlayer.transform.position = Vector3.zero;
        //transform.position += temp;

        if (Input.GetKeyDown(KeyCode.Alpha1) && currentType != PlayerType.WARRIOR)
        {
            SwitchPlayer(PlayerType.WARRIOR);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentType != PlayerType.MAGE)
        {
            SwitchPlayer(PlayerType.MAGE);
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            SwitchPlayer(PlayerType.TOGGLE);
        }
    }

    private void SwitchPlayer(PlayerType pt)
    {
        if (pt == PlayerType.TOGGLE)
        {
            pt = (PlayerType)(((int)currentType + 1) % 2);
        }
        Vector3 temp = currentPlayer.transform.position;

        currentType = pt;
        currentPlayer.SetActive(false);
        currentPlayer = transform.GetChild((int)currentType).gameObject;
        currentPlayer.transform.position = temp;
        currentPlayer.SetActive(true);
    }
}
