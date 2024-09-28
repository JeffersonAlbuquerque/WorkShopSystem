using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    private Data SaveGame;
    [Header("Speaker Settings")]
    public int Index;
    public GameObject[] Speakers;

    private void Start()
    {
        SaveGame = GetComponent<Data>();

        // Verifica se o saveGame foi carregado corretamente
        if (SaveGame != null)
        {
            // Atribui o valor salvo ao índice das rodas
            Index = SaveGame.IndexSpeak;

            // Certifica-se de que o índice seja válido antes de ativar as rodas
            if (Index >= 0 && Index < Speakers.Length)
            {
                // Ativa as rodas corretas após carregar os dados
                ActiveSpeakers();
                Debug.Log("Caixa de som ativada com base no índice salvo.");
            }
            else
            {
                Debug.LogWarning("Índice de roda inválido ao carregar dados.");
            }
        }
        else
        {
            Debug.LogError("Falha ao obter o script Data.");
        }
    }

    private void DeactivateAllSpeakers()
    {
        for (int i = 0; i < Speakers.Length; i++)
        {
            if (Speakers[i] != null)
            {
                Speakers[i].SetActive(false);
            }
        }
    }
    public void ActiveSpeakers()
    {
        if (Index >= 0 && Index < Speakers.Length)
        {
            if (Speakers[Index] != null)
            {
                Speakers[Index].SetActive(true);
            }
        }
    }

    public void SpeakerLeft()
    {
        Index--;
        if (Index < 0)
        {
            Index = Speakers.Length - 1;
        }
        DeactivateAllSpeakers();
        ActiveSpeakers();

        // Salva os dados
        if (SaveGame != null)
        {
            SaveGame.Save();
        }
        else
        {
            Debug.LogError("Falha ao salvar: script Data não encontrado.");
        }

    }

    public void SpeakerRight()
    {
        Index++;
        if (Index >= Speakers.Length)
        {
            Index = 0;
        }
        DeactivateAllSpeakers();
        ActiveSpeakers();

        if (SaveGame != null)
        {
            SaveGame.Save();
        }
        else
        {
            Debug.LogError("Falha ao salvar: script Data não encontrado.");
        }


    }




}
