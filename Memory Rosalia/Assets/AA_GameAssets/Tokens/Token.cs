using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Token : MonoBehaviour
{
    MemoryManager memoryManagerInstance;

    public int ownership;

    //[HideInInspector]
    public TokenAbility tokenAbility;
    //[HideInInspector]
    public Image image;
    public Text name;


    void Start()
    {
        memoryManagerInstance = MemoryManager.getInstance();
    }

    public void initialize()
    {
         name.text = tokenAbility.tokenName;

    }

    public void setColor(Color color)
    {
        image.color = color;
    }

    public IEnumerator executeActiveHability()
    {
        if(tokenAbility.type == TokenAbility.Type.ACTIVE)
            yield return StartCoroutine(((TokenAbilityActive)tokenAbility).executeAbility(memoryManagerInstance, this));
    }
}
