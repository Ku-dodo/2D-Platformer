using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class DialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    private BoxCollider2D _boxCollider;


    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.isTrigger = true;
    }

    public abstract void InteractAction();
  

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
