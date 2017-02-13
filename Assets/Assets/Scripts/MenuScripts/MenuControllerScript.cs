/*
 * Author Name: Richmond Goh
 * 
 * ------Function Summary------
 * Controls the operations of the menus such as new game, gallery and credits
 * Plays sounds from sound controller.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour {

    public string GameScene;
    public string GalleryScene;
    public string CreditsScene;
    private DataControllerScript dataController;
    private PlayerProgressScript playerProgress;
    private LoadScript loadController = LoadScript.Instance;
    private MenuButtonSoundScript soundController;

    void OnEnable()
    {
        soundController = GetComponent<MenuButtonSoundScript>();
    }

    public void EntrRespScene(string buttonName)
    {

        soundController.PlayButtonSound();

        if (buttonName == "New Game")
        {
            if(GameScene != null)
            {
                loadController.isLoaded = false;
                SceneManager.LoadScene(GameScene.ToString());
            }
        }

        if(buttonName == "Load")
        {
            if (PlayerPrefs.HasKey("SaveSlot"))
            {

            }
        }

        if (buttonName == "Gallery")
        {
            if (GalleryScene != null)
            {
                SceneManager.LoadScene(GalleryScene.ToString());
            }
        }

        if (buttonName == "Credits")
        {
            if (CreditsScene != null)
            {
                SceneManager.LoadScene(CreditsScene.ToString());
            }
        }

        if (buttonName == "Quit")
        {
            Application.Quit();
        }
    }
}
