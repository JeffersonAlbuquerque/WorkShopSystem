using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WorkShopSystem : MonoBehaviour
{
    // Referência ao script Data para sistema de salvamento
    private Data saveGame;

    [Header("Wheels Settings")]
    public int Index;
    public GameObject[] WheelFrontLeft;
    public GameObject[] WheelFrontRight;
    public GameObject[] WheelRearLeft;
    public GameObject[] WheekRearRight;

    private void Start()
    {
        saveGame = GetComponent<Data>();

        // Desativa todas as rodas inicialmente
        DeactivateAllWheels();
    }

    // Desativa todas as rodas
    private void DeactivateAllWheels()
    {
        for (int i = 0; i < WheelFrontLeft.Length; i++)
        {
            if (WheelFrontLeft[i] != null)
                WheelFrontLeft[i].SetActive(false);
            if (WheelFrontRight[i] != null)
                WheelFrontRight[i].SetActive(false);
            if (WheelRearLeft[i] != null)
                WheelRearLeft[i].SetActive(false);
            if (WheekRearRight[i] != null)
                WheekRearRight[i].SetActive(false);
        }
    }

    // Ativa as rodas com base no índice
    public void ActivateWheels()
    {
        if (Index >= 0 && Index < WheelFrontLeft.Length)
        {
            if (WheelFrontLeft[Index] != null)
                WheelFrontLeft[Index].SetActive(true);
            if (WheelFrontRight[Index] != null)
                WheelFrontRight[Index].SetActive(true);
            if (WheelRearLeft[Index] != null)
                WheelRearLeft[Index].SetActive(true);
            if (WheekRearRight[Index] != null)
                WheekRearRight[Index].SetActive(true);
        }
        else
        {
            Debug.LogError("Índice de roda fora dos limites: " + Index);
        }
    }

    // Muda para a roda anterior
    public void WheelLeft()
    {
        Index--;
        if (Index < 0)
        {
            Index = WheelFrontLeft.Length - 1;
        }

        // Atualiza as rodas
        DeactivateAllWheels();
        ActivateWheels();

        // Salva os dados
        if (saveGame != null)
        {
            saveGame.Save();
        }
        else
        {
            Debug.LogError("Falha ao salvar: script Data não encontrado.");
        }
    }

    // Muda para a roda seguinte
    public void WheelRight()
    {
        Index++;
        if (Index >= WheelFrontLeft.Length)
        {
            Index = 0;
        }

        // Atualiza as rodas
        DeactivateAllWheels();
        ActivateWheels();

        // Salva os dados
        if (saveGame != null)
        {
            saveGame.Save();
        }
        else
        {
            Debug.LogError("Falha ao salvar: script Data não encontrado.");
        }
    }
}
