using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data : MonoBehaviour
{
    private string path;

    [Header("WheelSettings")]
    public int IndexWheel;
    private WorkShopSystem workShop;

    [Header("SpeakerSettings")]
    public int IndexSpeak;
    private Speaker speaker;

    [Header("LiningSettings")]
    public int IndexLining;
    private DoorLining Lining;

    [Header("ColorSettings")]
    public float ValueR;
    public float ValueG;
    public float ValueB;
    public float ValueA;
    private CarColor carColor;

    private void Awake()
    {
        // Inicializa as referências
        workShop = GetComponent<WorkShopSystem>();
        speaker = GetComponent<Speaker>();
        carColor = GetComponent<CarColor>();
        Lining = GetComponent<DoorLining>();

        // Define o caminho do arquivo de salvamento
        path = Path.Combine(Application.persistentDataPath, gameObject.name + "_save.txt");
    }

    private void Start()
    {
        // Carrega os dados do jogo
        Invoke("loadGame", 0.5f);
    }

    private void Update()
    {
        // Atualiza os dados com base nas mudanças
        IndexWheel = workShop.Index;
        IndexSpeak = speaker.Index;

        IndexLining = Lining.Index;

        ValueR = carColor.r;
        ValueG = carColor.g;
        ValueB = carColor.b;
        ValueA = carColor.a;
    }

    public void loadGame()
    {
        // Verifica se o arquivo de salvamento existe
        if (File.Exists(path))
        {
            try
            {
                // Lê o conteúdo do arquivo
                string content = File.ReadAllText(path);

                // Sobrescreve os dados do objeto com os dados do arquivo JSON
                JsonUtility.FromJsonOverwrite(content, this);

                Debug.Log("Dados carregados para: " + transform.name);

                // Atualiza as configurações com os dados carregados
                workShop.Index = IndexWheel;
                speaker.Index = IndexSpeak;
                Lining.Index = IndexLining;

                carColor.r = ValueR;
                carColor.g = ValueG;
                carColor.b = ValueB;
                carColor.a = ValueA;

                // Ativa as funcionalidades com base nos dados carregados
                speaker.ActiveSpeakers();
                workShop.ActivateWheels();
                carColor.ActiveColor();
                Lining.ActiveLining();

            }
            catch (System.Exception e)
            {
                Debug.LogError("Erro ao carregar os dados: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("Arquivo de salvamento não encontrado para: " + transform.name);
            // Salva os valores padrão, caso o arquivo não exista
            Save();
        }
    }

    public void Save()
    {
        // Atualiza os índices e as cores atuais antes de salvar
        IndexWheel = workShop.Index;
        IndexSpeak = speaker.Index;
        IndexLining = Lining.Index;
        ValueR = carColor.r;
        ValueG = carColor.g;
        ValueB = carColor.b;
        ValueA = carColor.a;

        // Converte os dados do carro para formato JSON
        string content = JsonUtility.ToJson(this, true);

        // Tenta salvar os dados no arquivo
        try
        {
            File.WriteAllText(path, content);
            Debug.Log("Dados salvos para " + transform.name + " em: " + path);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao salvar os dados: " + e.Message);
        }
    }
}
