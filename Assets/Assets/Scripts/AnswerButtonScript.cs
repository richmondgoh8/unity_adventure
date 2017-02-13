/*
 * Author Name: Richmond Goh
 * 
 * ------Function Summary------
 * When button is created, it will take data from the function 
 * it was called from and fill it with the accordingly data. Click
 * will receive the information about the clicked button.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtonScript : MonoBehaviour {

    public Text answerText;
    private DecisionScript decisionData;
    private GameControllerScript gameController;    

    void OnEnable()
	{
        gameController = FindObjectOfType<GameControllerScript>();
    }


    public void Setup(DecisionScript decision)
    {
        decisionData = decision;
        answerText.text = decisionData.answerText;
    }

    public void HandleClick()
    {
        gameController.AnswerButtonClick(decisionData.decisionTree);
    }


}
