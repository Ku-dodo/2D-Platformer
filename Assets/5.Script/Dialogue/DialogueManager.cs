using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Typing Delay")]
    public float typeRate;
    [Header("Dialogue Obj")]
    public GameObject panelOfDialogue;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueKeyGuild;

    private Queue<string> sentences;

    private DialogueState _dialogueState;
    private string _sentence;
    #region Unity flow
    private void Start()
    {
        sentences = new Queue<string>();
        _dialogueState = DialogueState.None;
    }
    private void Update()
    {
        //Å¬¸¯ ½Ã 
        if(Input.GetMouseButtonDown(0) && _dialogueState == DialogueState.Typing)
        {
            StopAllCoroutines();
            dialogueText.text = _sentence;
            _dialogueState = DialogueState.Waiting;
        }
        else if (Input.GetMouseButtonDown(0) && _dialogueState == DialogueState.Waiting)
        {
            DisplayNextSentence();
        }
    }
    #endregion

    #region Dialogue flow Method
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
        _sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(_sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        _dialogueState = DialogueState.Typing;
        dialogueText.text = string.Empty;
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeRate);
        }
        _dialogueState = DialogueState.Waiting;
    }
    public void EndDialogue ()
    {
        panelOfDialogue.SetActive(false);
        GameManager.instance.SetPlayerControlAble();
        _dialogueState = DialogueState.None;
    }
    #endregion
}
