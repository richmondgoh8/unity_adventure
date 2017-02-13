/*
 * Author Name: Richmond Goh
 * 
 * ------Function Summary------
 * Actscript will contain an Act Name & Act Number in the inspector and contains an array of dialogues
 * whichever that the developer inputs.
 * This is the very first scripts that runs in Persistant Data
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActScript
{
    public string ActName;
    public int ActNumber;
    public DialogueScript[] numberOfDialogues;

}
