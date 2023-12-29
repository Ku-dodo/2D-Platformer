using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;

    public float typeRate;

    public GameObject panelOfDialogue;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        dialogueText.text = string.Empty;

        panelOfDialogue.SetActive(true);

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if(sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = string.Empty;
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeRate);
        }
    }
    public void EndDialogue ()
    {
        panelOfDialogue.SetActive(false);
    }

}
