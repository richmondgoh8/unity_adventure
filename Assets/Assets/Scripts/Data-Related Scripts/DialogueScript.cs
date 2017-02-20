/*
 * Author Name: Richmond Goh
 * 
 * ------Function Summary------
 * For Each Number of Dialogues input from act script, 1 set of the following class 
 * will be provided. Every Dialogue is given optional decisions which can be any 
 * positive number including 0
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class DialogueScript
{
	public string Title;
    public int DialogueOrder;
    public string location;
    public string DialogueText;
    public string SpeakerName;
    public Sprite SpeakerImage;
    public Sprite BackgroundImage;
	public AudioClip soundToPlay;
    public DecisionScript[] decisions;

}
