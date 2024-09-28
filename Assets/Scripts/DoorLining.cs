using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLining : MonoBehaviour
{
    public int Index;
    public GameObject[] LiningLeft;
    public GameObject[] LiningRight;

    private Data SaveGame;
    // Start is called before the first frame update
    void Start()
    {
        SaveGame = GetComponent<Data>();

        if (SaveGame != null)
        {
            Index = SaveGame.IndexLining;

            if (Index >=0 && Index < LiningLeft.Length)
            {
                ActiveLining();
                Debug.Log("Forro de Porta ativada com base no índice salvo.");
            }
            else
            {
                Debug.LogWarning("Índice de forro porta inválido ao carregar dados.");
            }
        }
        else
        {
            Debug.LogError("Falha ao obter o script Data.");
        }
    }

    private void DeactiveLining()
    {
        for (int i = 0; i < LiningLeft.Length; i++)
        {
            if (LiningLeft != null)
            {
                LiningLeft[i].SetActive(false);
                LiningRight[i].SetActive(false);
            }
        }
    }
    public void ActiveLining()
    {
        if (Index >= 0 && Index < LiningLeft.Length)
        {
            LiningLeft[Index].SetActive(true);
            LiningRight[Index].SetActive(true);
        }

    }
    public void LeftLining()
    {
        Index--;

        if (Index < 0)
        {
            Index = LiningLeft.Length - 1;
        }
        DeactiveLining();
        ActiveLining();

        if (SaveGame != null)
        {
            SaveGame.Save();
        }
        else
        {
            print("Falha ao Salvar : Script Data não encontrado");
        }
    }

    public void RightLining()
    {
        Index++;

        if (Index >= LiningLeft.Length)
        {
            Index = 0;
        }
        DeactiveLining();
        ActiveLining();

        if (SaveGame != null)
        {
            SaveGame.Save();
        }
        else
        {
            print("Falha ao Salvar : Script Data não encontrado");
        }
    }


}
