using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data : MonoBehaviour
{
    private string path;
    [Header("WorkShop Script And Variable")]
    public int IndexWheel;
    private WorkShopSystem workShop;

    private void Start()
    {
        // Inicializa a referência ao WorkShopSystem
        workShop = GetComponent<WorkShopSystem>();

        // Define o caminho do arquivo de salvamento
        path = "Assets/SaveGame/" + gameObject.name + "_save.txt";

        // Tenta carregar os dados primeiro
        loadGame();

        // Ativa as rodas corretas com base no índice carregado
    }
    private void Update()
    {
        // Atualiza o índice das rodas no WorkShop
        IndexWheel = workShop.Index;
        Save();
    }

    public void loadGame()
    {
        // Verifica se o arquivo de salvamento existe
        if (File.Exists(path))
        {
            // Lê o conteúdo do arquivo
            string content = File.ReadAllText(path);

            // Sobrescreve os dados do objeto com os dados do arquivo JSON
            JsonUtility.FromJsonOverwrite(content, this);

            Debug.Log("Dados carregados para: " + transform.name);
        }
        else
        {
            Debug.LogWarning("Arquivo de salvamento não encontrado para: " + transform.name);
            // Se o arquivo não existir, você pode salvar os valores padrão
            Save();
        }
    }

    public void Save()
    {
        // Atualiza o índice atual do WorkShop no Data
        IndexWheel = workShop.Index;

        // Converte os dados do carro para JSON formatado, arquivo de texto
        var content = JsonUtility.ToJson(this, true);

        // Salva o conteúdo JSON no arquivo especificado
        File.WriteAllText(path, content);

        Debug.Log("Dados salvos para " + transform.name + " em: " + path);
    }
}
