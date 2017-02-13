using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataControllerScript : MonoBehaviour {

    public ActScript[] noOfActs;
    public string MenuScene;
    private PlayerProgressScript playerProgress;
	// Use this for initialization
	void Start () 
	{
        DontDestroyOnLoad(gameObject);
        LoadPlayerProgress();
        SceneManager.LoadScene(MenuScene.ToString());
	}
	
    public ActScript RetrieveInfo(int ActIndex)
    {
        return noOfActs[ActIndex];
    }

    public void SubmitNewProgress(int newProgress, int newActStatus, int newSaveID)
    {
        Debug.Log("SaveID" + newSaveID);
        playerProgress.progressUpdate[newSaveID] = newProgress;
        playerProgress.actUpdate[newSaveID] = newActStatus;
        playerProgress.SaveSlot[newSaveID] = newSaveID;
        SavePlayerProgress(newSaveID);
    }

    public int GetPlayerProgress(int aNumber)
    {
        if (PlayerPrefs.HasKey("SaveSlot" + aNumber.ToString()))
        {
            playerProgress.UsedSlots[aNumber] = PlayerPrefs.GetInt("UsedSlot" + aNumber);
            return playerProgress.progressUpdate[aNumber];
        }
        return 0;
    }

    public int GetPlayerActIndex(int aNumber)
    {
        return playerProgress.actUpdate[aNumber];
    }

    public int GetPlayerSlotNumber(int aNumber)
    {
        return playerProgress.SaveSlot[aNumber];
    }

    public int GetUsedSlot(int aNumber)
    {
        if (PlayerPrefs.HasKey("SaveSlot" + aNumber.ToString()))
        {
            playerProgress.UsedSlots[aNumber] = PlayerPrefs.GetInt("UsedSlot" + aNumber);
            return playerProgress.UsedSlots[aNumber];
        }
        return 0;
    }

    private void LoadPlayerProgress()
    {
        Debug.Log("RUN");
        playerProgress = new PlayerProgressScript();
        /*
        if (PlayerPrefs.HasKey("SaveSlot"))
        {
            playerProgress.progressUpdate[1] = PlayerPrefs.GetInt("SaveSlot");
            playerProgress.actUpdate[1] = PlayerPrefs.GetInt("SaveSlotIndex");
            playerProgress.SaveSlot[1] = PlayerPrefs.GetInt("SaveSlotNumber"); 
        }
        */
        for(int i = 1; i < 4; ++i)
        {
            if(PlayerPrefs.HasKey("SaveSlot" + i.ToString()))
            {
                playerProgress.progressUpdate[i] = PlayerPrefs.GetInt("SaveSlot" + i);
                playerProgress.actUpdate[i] = PlayerPrefs.GetInt("SaveSlotIndex" + i);
                playerProgress.SaveSlot[i] = PlayerPrefs.GetInt("SaveSlotNumber" + i);
                playerProgress.UsedSlots[i] = PlayerPrefs.GetInt("UsedSlot" + i);
            }
         
        }

        /*for(int i=1;i<4;++i)
        {
            playerProgress.progressUpdate[i] = PlayerPrefs.GetInt("SaveSlot" + i);
            playerProgress.actUpdate[i] = PlayerPrefs.GetInt("SaveSlotIndex" + i);
            playerProgress.SaveSlot[i] = PlayerPrefs.GetInt("SaveSlotNumber" + i);
            playerProgress.UsedSlots[i] = PlayerPrefs.GetInt("UsedSlot" + i);
            Debug.Log(PlayerPrefs.GetInt("SaveSlot" + i));

        }
        *///Debug.Log(playerProgress.progressUpdate[0]);
    }

    private void SavePlayerProgress(int saveID)
    {
        PlayerPrefs.SetInt("SaveSlot" + saveID, playerProgress.progressUpdate[saveID]);
        PlayerPrefs.SetInt("SaveSlotIndex" + saveID, playerProgress.actUpdate[saveID]);
        PlayerPrefs.SetInt("SaveSlotNumber" + saveID, playerProgress.SaveSlot[saveID]);
        PlayerPrefs.SetInt("UsedSlot" + saveID, 1);
    }

}

