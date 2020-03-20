using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellObject : MonoBehaviour
{
    public Color smellColor;
    public int smellRange;

    private void OnDrawGizmos()
    {
        Gizmos.color = smellColor;
        Gizmos.DrawWireSphere(transform.position, smellRange);
    }
}
