using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdemDosLayers : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer MySpriteRenderer;
    private int MyLayer;
    private Transform MyPosition;
    private OrderLayerManager LayerManagerScript;
    private OrdemDosLayers MyScript;

    // Start is called before the first frame update

    void Awake()
    {
        MySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        MyPosition = gameObject.GetComponent<Transform>();
        MyScript = gameObject.GetComponent<OrdemDosLayers>();
        LayerManagerScript = GameObject.FindGameObjectWithTag("LayerManager").GetComponent<OrderLayerManager>();
        LayerManagerScript.CalculatorOfLayer(MyScript, MyPosition.position.y);

    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown("w"))
    //    {
    //        LayerManagerScript.CalculatorOfLayer(MyScript, MyPosition.position.y);
    //    }
    //}

    public void BeginLayerChange()
    {
        LayerManagerScript.CalculatorOfLayer(MyScript, MyPosition.position.y);
    }

    public void ChangeThisLayer(float y)
    {

    }
    public void ChangeMyLayer(int change)
    {
        MySpriteRenderer.sortingOrder = change;
    }

}
