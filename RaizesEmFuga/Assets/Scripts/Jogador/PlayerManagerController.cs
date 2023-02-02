using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerController : MonoBehaviour
{
    public bool controlarPlayer = true;

    [Header("Characters")]
    public GameObject[] Players = new GameObject[2];
    [Range(1, 2)]
    public int startPlayer = 1;

    [Header("Virtual Camera do Player")]
    public CinemachineVirtualCamera virtualCamera;

    private void Start()
    {

        AlteraPersonagemStatus(startPlayer - 1);

    }

    private void Update()
    {
        if(!controlarPlayer)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.C)) 
        {
            TrocarPersonagem();
            
        }
    }

    private void TrocarPersonagem()
    {
        int currentPlayer = (startPlayer - 1);
        if(currentPlayer == 0)
        {
            currentPlayer = 1;
            startPlayer = 2;
        } else
        {
            currentPlayer = 0;
            startPlayer = 1;
        }
        AlteraPersonagemStatus(currentPlayer);
    }

    private void AlteraPersonagemStatus(int currentPlayer)
    {
        virtualCamera.Follow = Players[currentPlayer].GetComponent<Jogador>().transform;

        for (int i = 0; i < Players.Length; i++)
        {
            if (i == currentPlayer)
            {
                Players[i].GetComponent<Jogador>().controlarPlayer = true;
                
            }
            else
            {
                Players[i].GetComponent<Jogador>().controlarPlayer = false;
                Players[i].GetComponent<Jogador>().anim.SetBool("walk", false);
            }
        }
    }
}
