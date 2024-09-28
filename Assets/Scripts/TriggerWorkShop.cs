using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWorkShop : MonoBehaviour
{
    public HudGame hudgame;
    public FCP_ExampleScript paintChassis;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle")) // Use CompareTag para melhor desempenho
        {
            WorkShopSystem WheelSystem = other.GetComponent<WorkShopSystem>();
            Speaker SpeakerSystem = other.GetComponent<Speaker>();
            CarColor colorCar = other.GetComponent<CarColor>();
            DoorLining Door = other.GetComponent<DoorLining>();

            if (colorCar != null)
            {
                Debug.Log("Detectou o carro com cor válida.");
                paintChassis.material = colorCar.Chassis;

                // Verifica se precisa atualizar a cor inicial com base no material
                if (paintChassis.getStartingColorFromMaterial)
                {
                    paintChassis.fcp.color = paintChassis.material.color;
                }


                // Adiciona o evento de alteração de cor
                paintChassis.fcp.onColorChange.AddListener(paintChassis.OnChangeColor);
            }

            if (WheelSystem != null)
            {
                Debug.Log("Detectado Script das Rodas");
                hudgame.WheelLeftBtn.onClick.AddListener(WheelSystem.WheelLeft);
                hudgame.WheelRightBtn.onClick.AddListener(WheelSystem.WheelRight);
            }
            if (SpeakerSystem != null)
            {
                hudgame.SpeakerLeftBtn.onClick.AddListener(SpeakerSystem.SpeakerLeft);
                hudgame.SpeakerRightBtn.onClick.AddListener(SpeakerSystem.SpeakerRight);
            }
            if (Door != null)
            {
                hudgame.LiningLeftBtn.onClick.AddListener(Door.LeftLining);
                hudgame.LiningRightBtn.onClick.AddListener(Door.RightLining);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            WorkShopSystem WheelSystem = other.GetComponent<WorkShopSystem>();
            Speaker SpeakerSystem = other.GetComponent<Speaker>();
            CarColor colorCar = other.GetComponent<CarColor>();
            DoorLining Door = other.GetComponent<DoorLining>();

            if (colorCar != null)
            {
                // Limpa o material ao sair da trigger
                paintChassis.material = null;
            }

            if (WheelSystem != null)
            {
                Debug.Log("Botões Núlos");
                hudgame.WheelLeftBtn.onClick.RemoveListener(WheelSystem.WheelLeft);
                hudgame.WheelRightBtn.onClick.RemoveListener(WheelSystem.WheelRight);
            }
            if (SpeakerSystem != null)
            {
                hudgame.SpeakerLeftBtn.onClick.RemoveListener(SpeakerSystem.SpeakerLeft);
                hudgame.SpeakerRightBtn.onClick.RemoveListener(SpeakerSystem.SpeakerRight);
            }
            if (Door != null)
            {
                hudgame.LiningLeftBtn.onClick.RemoveListener(Door.LeftLining);
                hudgame.LiningRightBtn.onClick.RemoveListener(Door.RightLining);
            }
        }
    }
}
