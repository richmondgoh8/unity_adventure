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

	private bool contSearch = false;
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
	public GameObject pauseMenu;

	public Sprite ForestSetting2;
	public Sprite ZombieImage;

	public Sprite JacobBasicEmotion01;
	public Sprite JacobBasicEmotion02;
	public Sprite JacobBasicEmotion03;

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

		if(Input.GetKeyDown(KeyCode.Escape) && !savePanel.activeSelf)
        {
			pauseMenu.SetActive(!pauseMenu.activeSelf);
        }

		if(Input.GetKeyDown(KeyCode.Escape) && savePanel.activeSelf)
		{
			savePanel.SetActive(false);
		}
    }

	public void QuitPauseMenu()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
	}

	public void ReturnMenu()
	{
		SceneManager.LoadScene(MenuScene.ToString());
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
				LoadLazyImages(i);
                //ActorPortraitImage.sprite = dialoguePool[i].SpeakerImage;
                backgroundSettingImage.sprite = dialoguePool[i].BackgroundImage;
                SpecialNumber = dataController.GetPlayerProgress(loadController.slotNumber);
				PlaySFX (i);
				LoadLazyImages(i);
				UpdateBackgroundImage ();
                CheckMissing();
            }
        }

        CheckIfAnyDialogue();
    }
    
    private void ShowQuestion()
    {
        RemoveAnswerButtons();
		contSearch = true;
        for (int i = 0; i < dialoguePool.Length; ++i)
        {
			if (dialoguePool[i].DialogueOrder == SpecialNumber && contSearch == true)
            {
				contSearch = false;
                currentText = dialoguePool[i].DialogueText;
                ActorName.text = dialoguePool[i].SpeakerName;
                ActIndex.text = currentRoundData.ActName;
                ActLocation.text = dialoguePool[i].location;
                CheckIfDecisionAvailable(i);
				LoadLazyImages(i);
                //ActorPortraitImage.sprite = dialoguePool[i].SpeakerImage;
                backgroundSettingImage.sprite = dialoguePool[i].BackgroundImage;
				UpdateBackgroundImage ();
				CheckMissing();

				PlaySFX (i);

            }
        }


        CheckIfAnyDialogue();



    }

	private void UpdateBackgroundImage()
	{
		if (SpecialNumber > 50) 
		{
			backgroundSettingImage.sprite = ForestSetting2;
		}
	}

	private void LoadLazyImages(int aNumber)
	{

		ActorPortraitImage.sprite = dialoguePool[aNumber].SpeakerImage;

		if (dialoguePool[aNumber].SpeakerName == "Jacob") 
		{
			ActorPortraitImage.preserveAspect = false;
			ActorPortraitImage.sprite = JacobBasicEmotion01;

			if (currentText.Substring (currentText.Length - 1) == "!") 
			{
				Debug.Log ("EMOTION3");
				ActorPortraitImage.sprite = JacobBasicEmotion03;
			}

			if (currentText.Substring (currentText.Length - 1) == "?") 
			{
				Debug.Log ("EMOTION2");
				ActorPortraitImage.sprite = JacobBasicEmotion02;
			}
				
		} 

		if (dialoguePool [aNumber].SpeakerName == "Zombie") 
		{
			ActorPortraitImage.preserveAspect = true;
			ActorPortraitImage.sprite = ZombieImage;
		}


			
	}

	private void PlaySFX(int i)
	{
		AudioSource audio = GetComponent<AudioSource> ();
		audio.clip = dialoguePool[i].soundToPlay;
		audio.Play ();
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
		pauseMenu.SetActive (!pauseMenu.activeSelf);
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