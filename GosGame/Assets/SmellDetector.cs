using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmellDetector : MonoBehaviour
{
    Color smellColor;
    [SerializeField] Image canvasSmellIndicator;

    public Transform smellObjectsParent;
    public SmellObject[] smellObjects;

    void Start()
    {
        smellObjects = smellObjectsParent.GetComponentsInChildren<SmellObject>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            canvasSmellIndicator.gameObject.SetActive(true);
            CalculateSmellColor();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            canvasSmellIndicator.gameObject.SetActive(false);
        }

    }

    public void CalculateSmellColor()
    {
        smellColor = Vector4.zero;

        foreach(SmellObject smellO in smellObjects)
        {
            float dist = Vector3.Distance(transform.position, smellO.transform.position); 

            if(dist < smellO.smellRange)
            {
                //float r = smellO.smellColor.r * Remap(dist, 1, smellO.smellRange, 1, 0);
                //float b = smellO.smellColor.g * Remap(dist, 1, smellO.smellRange, 1, 0);
                //float g = smellO.smellColor.b * Remap(dist, 1, smellO.smellRange, 1, 0);

                //smellColor += new Color(r, b, g, 0.8f);
                smellColor += new Color(smellO.smellColor.r, smellO.smellColor.g, smellO.smellColor.b, Remap(dist, 1, smellO.smellRange, 1, 0));
                //Debug.Log(Remap(dist, 1, smellO.smellRange, 1, 0));
                Debug.Log(smellO.smellColor.b);
            }
        }
        canvasSmellIndicator.color = smellColor;
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
