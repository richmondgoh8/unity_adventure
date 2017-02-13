/*
 * Author Name: Richmond Goh
 * 
 * ------Function Summary------
 * Heart of the game, reused for all game content and decisions.
 * Flexible and Dynamic
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{

    private DataControllerScript dataController;
    private ActScript currentRoundData;
    private DialogueScript[] dialoguePool;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private int SpecialNumber = 0;
    private int DialogueLeftCounter;
    private PlayerProgressScript playerProgress;
    public GameObject namePanel;
    public GameObject actorPotratitPanel;
    //Auto Function
    public bool autoFunction = false;

    private int actIndex = 0;

    private float checkRate = 0.01f;
    private float nextCheck;
    private string currentText;
    private int textCounter = 0;
    private int NoOfDecisionAvailable;
    private SoundScript soundController;
    private LoadScript loadController = LoadScript.Instance;

    private bool activateSound = false;
    private bool isTheEnd;
    public Transform answerButtonParent;
    public SimpleObjectPoolScript answerButtonObjectPool;
    public Text questionDisplayText;
    public GameObject NextButton;
    public Text NextButtonText;
    public string MenuScene;
    public Text ActorName;
    public Text ActIndex;
    public Text ActLocation;
    public Image ActorPortraitImage;
    public Image backgroundSettingImage;
    public GameObject savePanel;

    void OnEnable()
    {
        soundController = FindObjectOfType<SoundScript>();
        dataController = FindObjectOfType<DataControllerScript>();
        SoftReset();
        savePanel.SetActive(false);

        if (loadController.isLoaded == true)
        {
            actIndex = dataController.GetPlayerActIndex(loadController.slotNumber);
            SoftReset();
            LoadCurrentStory();
        }

        else
        {
            ShowQuestion();
        }


    }

    void Update()
    {
        if (Time.time > nextCheck && textCounter < currentText.Length)

        {
            nextCheck = Time.time + checkRate;
            textCounter++;
            questionDisplayText.text = currentText.Substring(0, textCounter);

            if (activateSound == false)
            {
                activateSound = true;
                soundController.PlaySoundEffect();
            }

        }

        if (textCounter == currentText.Length && activateSound == true)
        {
            activateSound = false;
            soundController.StopSoundEffectIfPlaying();
        }

        if (textCounter == currentText.Length && autoFunction == true && NoOfDecisionAvailable == 0 && NextButtonText.text != "End")
        {
            SpecialNumber++;
            ResetInstance();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (NextButtonText.IsActive() != false)
            {
                nextButtonClick();
            }

        }

        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(MenuScene.ToString());
        }
    }

    private void SoftReset()
    {
        currentRoundData = dataController.RetrieveInfo(actIndex);
        dialoguePool = currentRoundData.numberOfDialogues;
        DialogueLeftCounter = dialoguePool.Length;

    }
    
    private void LoadCurrentStory()
    {
       
        for (int i = 0; i < dialoguePool.Length; ++i)
        {
            if (dialoguePool[i].DialogueOrder == dataController.GetPlayerProgress(loadController.slotNumber))
            {
                currentText = dialoguePool[i].DialogueText;
                ActorName.text = dialoguePool[i].SpeakerName;
                ActIndex.text = currentRoundData.ActName;
                ActLocation.text = dialoguePool[i].location;
                CheckIfDecisionAvailable(i);
                ActorPortraitImage.sprite = dialoguePool[i].SpeakerImage;
                backgroundSettingImage.sprite = dialoguePool[i].BackgroundImage;
                SpecialNumber = dataController.GetPlayerProgress(loadController.slotNumber);

                CheckMissing();
            }
        }

        CheckIfAnyDialogue();
    }
    
    private void ShowQuestion()
    {
        RemoveAnswerButtons();

        for (int i = 0; i < dialoguePool.Length; ++i)
        {
            if (dialoguePool[i].DialogueOrder == SpecialNumber)
            {
                currentText = dialoguePool[i].DialogueText;

                ActorName.text = dialoguePool[i].SpeakerName;
                ActIndex.text = currentRoundData.ActName;
                ActLocation.text = dialoguePool[i].location;
                CheckIfDecisionAvailable(i);
                ActorPortraitImage.sprite = dialoguePool[i].SpeakerImage;
                backgroundSettingImage.sprite = dialoguePool[i].BackgroundImage;

                CheckMissing();


            }
        }


        CheckIfAnyDialogue();



    }

    private void CheckMissing()
    {
        if (ActorPortraitImage.sprite == null)
        {
            actorPotratitPanel.SetActive(false);
        }

        if (ActorPortraitImage.sprite != null)
        {
            actorPotratitPanel.SetActive(true);
        }

        if (ActorName.text == "")
        {
            namePanel.SetActive(false);
        }

        if (ActorName.text != "")
        {
            namePanel.SetActive(true);
        }
    }

    private void CheckIfAnyDialogue()
    {
        int tmpSpec = 0;
        NextButtonText.text = "End";
        tmpSpec = SpecialNumber;
        tmpSpec++;

        for (int i = 0; i < dialoguePool.Length; ++i)
        {
            if (dialoguePool[i].DialogueOrder == tmpSpec)
            {
                NextButtonText.text = "Next";
            }

        }


    }
    private void CheckIfDecisionAvailable(int aNumber)
    {
        DialogueScript questionData = dialoguePool[aNumber];
        NoOfDecisionAvailable = questionData.decisions.Length;

        for (int i = 0; i < questionData.decisions.Length; ++i)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);
            AnswerButtonScript answerButton = answerButtonGameObject.GetComponent<AnswerButtonScript>();
            answerButton.Setup(questionData.decisions[i]);
        }

        if (questionData.decisions.Length == 0)
        {
            DialogueLeftCounter--;
            NextButton.SetActive(true);
        }

        else
        {
            DialogueLeftCounter -= 2;
            NextButton.SetActive(false);
        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void nextButtonClick()
    {

        int tmpActIndex = actIndex;

        if (NextButtonText.text == "End")
        {
            if (++tmpActIndex < dataController.noOfActs.Length)
            {
                actIndex++;
                SoftReset();
                SpecialNumber = 0;
                ResetInstance();
            }

            else
            {
                SceneManager.LoadScene(MenuScene.ToString());
            }
        }

        else if (textCounter != currentText.Length)
        {
            SkipTypeWriting();
        }

        else
        {
            SpecialNumber++;
            ResetInstance();
        }

    }

    public void AnswerButtonClick(int myNumber)
    {

        if (textCounter != currentText.Length)
        {
            SkipTypeWriting();
        }

        else
        {
            SpecialNumber = myNumber;
            ResetInstance();
        }
    }

    private void SkipTypeWriting()
    {
        textCounter = currentText.Length;
        questionDisplayText.text = currentText.Substring(0, currentText.Length);
    }

    private void ResetInstance()
    {
        textCounter = 0;
        ShowQuestion();
    }

    public void SaveCurrentStatus()
    {
        bool tmptool = savePanel.activeSelf;
        savePanel.SetActive(!tmptool);

    }

    public void HandleSaveButtonClick(int buttonIndex)
    {
        for (int i = 0; i < dialoguePool.Length; ++i)
        {
            if (dialoguePool[i].DialogueOrder == SpecialNumber)
            {
                dataController.SubmitNewProgress(dialoguePool[i].DialogueOrder, actIndex,buttonIndex);
            }
        }
        SaveCurrentStatus();
    }
}