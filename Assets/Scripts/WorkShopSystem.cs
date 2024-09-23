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
    public GameObject[] FLS;
    public GameObject[] FRS;
    public GameObject[] RLS;
    public GameObject[] RRS;

    private void Start()
    {
        DeactivateAllWheels();
        saveGame = GetComponent<Data>();

        if (saveGame != null)
        {
            Index = saveGame.IndexWheel;
            ActivateWheels();
        }
    }

    private void DeactivateAllWheels()
    {
        for (int i = 0; i < FLS.Length; i++)
        {
            FLS[i].SetActive(false);
            FRS[i].SetActive(false);
            RLS[i].SetActive(false);
            RRS[i].SetActive(false);
        }
    }

    private void ActivateWheels()
    {
        if (Index >= 0 && Index < FLS.Length)
        {
            FLS[Index].SetActive(true);
            FRS[Index].SetActive(true);
            RLS[Index].SetActive(true);
            RRS[Index].SetActive(true);
        }
    }

    public void WheelLeft()
    {
        // Atualizar o Index
        Index--;
        if (Index < 0)
        {
            Index = FLS.Length - 1;
        }

        DeactivateAllWheels();
        ActivateWheels();
        saveGame.Save();
    }

    public void WheelRight()
    {
        Index++;
        if (Index >= FLS.Length)
        {
            Index = 0;
        }
        DeactivateAllWheels();
        ActivateWheels();
        saveGame.Save();
    }
}
