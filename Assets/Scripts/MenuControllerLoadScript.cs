using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerLoadScript : MonoBehaviour {

    public GameObject savePanel;

    void OnEnable()
	{
        savePanel.SetActive(false);
	}

    public void LoadButtonSelected()
    {
        savePanel.SetActive(!savePanel.activeSelf);
    }
}
