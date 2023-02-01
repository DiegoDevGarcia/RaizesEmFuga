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

        if(Input.GetKeyDown(KeyCode.I)) 
        {
            TrocarPersonagem();
            
        }
    }

    private void TrocarPersonagem()
    {
        int currentPlayer = (startPlayer - 1);
        Debug.Log("antes da logica " + currentPlayer);
        currentPlayer = currentPlayer == 0 ? 1 : 0;
        Debug.Log("Antes da troca " + currentPlayer);
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
            }
        }
        Debug.Log("Depois da troca " + currentPlayer);
    }
}
