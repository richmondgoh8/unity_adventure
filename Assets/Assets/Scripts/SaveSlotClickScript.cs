using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SaveSlotClickScript : MonoBehaviour {

    public Text defaultText;
    public int defaultIndex;
    private PlayerProgressScript playerProgress;
    private GameControllerScript gameController;
    private DataControllerScript dataController;
    private MenuControllerScript menuController;
    private LoadScript loadController = LoadScript.Instance;
    public string s1;
    public string s2;
    //public int counter = 1;

    void OnEnable()
	{
        gameController = FindObjectOfType<GameControllerScript>();
        dataController = FindObjectOfType<DataControllerScript>();
        menuController = FindObjectOfType<MenuControllerScript>();
        s1 = defaultText.text;
        s2 = s1.Substring(s1.Length - 1);
        playerProgress = new PlayerProgressScript();
        //Calls a method from dataController Script and then proceed to add in the number in the loaded slot if any.
        if (dataController.GetUsedSlot(int.Parse(s2)) != 0)
        {
            defaultText.text = "Progress - Act " + dataController.GetPlayerActIndex(int.Parse(s2)) + " Slot " + int.Parse(s2);
        }
    }

    public void SaveHandleClick()
    {
        //Get any number after "Save Slot Words"
        gameController.HandleSaveButtonClick(int.Parse(s2));
    }

    public void LoadHandleClick()
    {

        if(defaultText.text.Substring(0,16) != "Empty Save Slots")
        {
            loadController.slotNumber = int.Parse(s2);
            loadController.isLoaded = true;

            SceneManager.LoadScene(menuController.GameScene.ToString());
        }


    }
}
