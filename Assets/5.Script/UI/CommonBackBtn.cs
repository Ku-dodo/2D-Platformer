using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonBackBtn : MonoBehaviour
{
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(InactiveParentPopup);
    }

    private void InactiveParentPopup()
    {
        gameObject.SetActive(false);
    }
}
