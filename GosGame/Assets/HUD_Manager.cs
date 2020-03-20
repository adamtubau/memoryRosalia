using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Manager : MonoBehaviour
{
    #region Singleton
    private static HUD_Manager instance;

    private void Awake()
    {
        instance = this;
    }
    
    public static HUD_Manager getInstance()
    {
        return instance;
    }
    #endregion

    [SerializeField] Transform popUp_parent;
    [SerializeField] GameObject popUp_pickUpObject;

    GameObject popUp_instantiated;

    public void SpawnPopUpPickObject()
    {
        popUp_instantiated = Instantiate(popUp_pickUpObject, popUp_parent);       
    }

    public void RepositionPopUpPickObject(Vector3 position)
    {
        if(popUp_instantiated != null)
            popUp_instantiated.transform.position = Camera.main.WorldToScreenPoint(position);
    }

    public void DestroyPopUpPickObject()
    {
        Destroy(popUp_instantiated);
    }
}
