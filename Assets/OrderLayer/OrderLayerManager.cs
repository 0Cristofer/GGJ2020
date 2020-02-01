using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLayerManager : MonoBehaviour
{


    public void CalculatorOfLayer(OrdemDosLayers ScriptAtual, float y)
    {
        int OrderAtual;
        y *= 100;
        float ValorDoLayer = 1000 - y;

        OrderAtual = (int)ValorDoLayer;

        ScriptAtual.ChangeMyLayer(OrderAtual);
    }

}
