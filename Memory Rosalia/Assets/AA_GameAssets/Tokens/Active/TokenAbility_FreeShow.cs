using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TokenAbility_FreeShow : TokenAbilityActive
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
                c.ShowTokenColor();
                yield return new WaitForSeconds(1f);
                c.HideCard();
                endAbility = true;
            }
            yield return null;
        }

        token.image.color = Color.grey;
        token.gameObject.SetActive(false);
    }
}
