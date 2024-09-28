using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColor : MonoBehaviour
{
    //public Color cor;
    public Material Chassis;
    public Renderer ChassiRenderer;
    public float r, g, b, a;
    private Data SaveGame;

    // Start is called before the first frame update
    void Start()
    {
        SaveGame = GetComponent<Data>();

    }

    // Update is called once per frame
    void Update()
    {
        //Chassis.color = new Color(r,g,b,a);

        r = Chassis.color.r;
        g = Chassis.color.g;
        b = Chassis.color.b;
        a = Chassis.color.a;
    }
    public void ActiveColor()
    {
        Chassis.color = new Color(SaveGame.ValueR, SaveGame.ValueG, SaveGame.ValueB, SaveGame.ValueA);

    }
    public void SaveColor()
    {
        r = Chassis.color.r;
        g = Chassis.color.g;
        b = Chassis.color.b;
        a = Chassis.color.a;
        SaveGame.Save();
    }


}
