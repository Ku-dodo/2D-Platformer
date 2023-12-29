using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    private PlayerControlState _playerControlState;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }
    public void SetPlayerControlAble()
    {
        player.GetComponent<PlayerController>().playerControlState = PlayerControlState.Able; 
    }

    public void SetPlayerControlUnable()
    {
        player.GetComponent<PlayerController>().playerControlState = PlayerControlState.Unable;
    }
}
