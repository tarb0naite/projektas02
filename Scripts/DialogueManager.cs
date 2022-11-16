using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); 
    }

    public void StartDialogue (Convo dialogue)
    {
        Debug.Log("Starting conversation with" + dialogue.name);
    }
}
