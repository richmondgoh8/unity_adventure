using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerLoadScript : MonoBehaviour {

    public GameObject savePanel;
	public GameObject loadPanel;
	public GameObject titlePanel;

    void OnEnable()
	{
		savePanel.SetActive (false);
	}

    public void LoadButtonSelected()
    {
        savePanel.SetActive(!savePanel.activeSelf);
		loadPanel.SetActive (!loadPanel.activeSelf);
		titlePanel.SetActive (!titlePanel.activeSelf);
    }
}
