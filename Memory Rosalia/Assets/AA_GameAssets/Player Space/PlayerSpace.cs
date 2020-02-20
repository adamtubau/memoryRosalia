using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpace : MonoBehaviour
{
    [SerializeField] Text score;

    [SerializeField] Image turnIndicator;
    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;


    [SerializeField] Transform layoutGroup;

    public void updateScore(int _score)
    {
        score.text = _score.ToString();
    }

    public void parentToLayout(Transform transform)
    {
        transform.SetParent(layoutGroup);
    }

    public void setTurnIndicatorColor(bool active)
    {
        if(active)
        {
            turnIndicator.color = activeColor;
        }
        else
        {
            turnIndicator.color = inactiveColor;
        }
    }
}
