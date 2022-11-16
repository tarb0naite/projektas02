using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoTrigger : MonoBehaviour
{
    public Convo dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
