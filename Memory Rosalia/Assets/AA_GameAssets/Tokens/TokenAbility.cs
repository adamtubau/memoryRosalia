using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TokenAbility : ScriptableObject
{
    public enum Type { ACTIVE, PASSIVE, INSTANT};
    public Type type;
    public string tokenName;
    public Color color;
}
