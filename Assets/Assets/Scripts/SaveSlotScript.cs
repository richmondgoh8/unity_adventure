using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SaveSlotScript : MonoBehaviour {

    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public Text buttonPrefabText;
    private GameControllerScript gameController;
    private SaveSlotClickScript saveController;

    void OnEnable()
    {
        gameController = FindObjectOfType<GameControllerScript>();
    }

    // Use this for initialization
    //Creates buttons when Save Button is Clicked and name them as Empty Save Slots once.
    void Start () 
	{
        for(int i=1;i<4;++i)
        {
            buttonPrefabText.text = "Empty Save Slots " + i;
            GameObject go = Instantiate(buttonPrefab) as GameObject;
            go.transform.SetParent(buttonContainer);
        }
    }

    
}
