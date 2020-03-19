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

    HUD_Manager hudInstance;

    bool isSmellIndicatorHudActivated;

    void Start()
    {
        smellObjects = smellObjectsParent.GetComponentsInChildren<SmellObject>();
        hudInstance = HUD_Manager.getInstance();
    }

    void Update()
    {
        if(isSmellIndicatorHudActivated)
            CalculateSmellColor();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (!isSmellIndicatorHudActivated)
            {
                canvasSmellIndicator.gameObject.SetActive(true);
                isSmellIndicatorHudActivated = true;
            }

            else
            {
                canvasSmellIndicator.gameObject.SetActive(false);
                isSmellIndicatorHudActivated = false;
            }
        }
    }

    public void CalculateSmellColor()
    {
        smellColor = Vector4.zero;

        int smellNum = 0;
        foreach(SmellObject smellO in smellObjects)
        {
            float dist = Vector3.Distance(transform.position, smellO.transform.position); 

            if(dist < smellO.smellRange)
            {
                float a = Remap(dist, 1, smellO.smellRange, 1, 0);
                //float r = smellO.smellColor.r * a;
                //float b = smellO.smellColor.g * a;
                //float g = smellO.smellColor.b * a;

                //smellColor += new Color(r, b, g, 0.8f);
                smellColor += new Color(smellO.smellColor.r, smellO.smellColor.g, smellO.smellColor.b, a);
                //Debug.Log(Remap(dist, 1, smellO.smellRange, 1, 0));
                Debug.Log(smellO.smellColor.b);
                smellNum++;            }
        }
       
        canvasSmellIndicator.color = new Vector4(smellColor.r/smellNum, smellColor.g/smellNum, smellColor.b/smellNum, smellColor.a);
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Object")
        {
            hudInstance.SpawnPopUpPickObject();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            //Y offset a pelo
            hudInstance.RepositionPopUpPickObject(other.gameObject.transform.position + new Vector3(0, 0.5f, 0));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            hudInstance.DestroyPopUpPickObject();
        }
    }
}
