using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TokenAbilityInstant : TokenAbility
{
    public abstract IEnumerator executeAbility(MemoryManager memoryManagerInstance, Card card);
}
