using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtnScript : MonoBehaviour {

	public GameObject titlePanel;
	public GameObject loadPanel;
	public GameObject menuPanel;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void HidePanels()
	{
		titlePanel.SetActive (!titlePanel.activeSelf);
		loadPanel.SetActive (!loadPanel.activeSelf);
		menuPanel.SetActive (!menuPanel.activeSelf);
	}
}
