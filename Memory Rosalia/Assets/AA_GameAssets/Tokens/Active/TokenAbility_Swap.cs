using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TokenAbility_Swap : TokenAbilityActive
{
    public override IEnumerator executeAbility(MemoryManager memoryManagerInstance, Token token)
    {
        bool endAbility = false;
        token.image.color = Color.white;

        while (!endAbility)
        {
            Card c = memoryManagerInstance.RayCastCard();

            if (c != null && c != memoryManagerInstance.cardUp)
            {
                
            }
            yield return null;
        }

        token.gameObject.SetActive(false);
    }
}
