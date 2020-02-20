using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TokenAbilityActive : TokenAbility
{
    public abstract IEnumerator executeAbility(MemoryManager memoryManagerInstance, Token token);
}
