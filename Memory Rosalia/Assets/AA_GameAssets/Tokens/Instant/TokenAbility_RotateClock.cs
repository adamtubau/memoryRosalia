using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TokenAbility_Clock : TokenAbilityInstant
{
    public override IEnumerator executeAbility(MemoryManager memoryManagerInstance, Card card)
    {
        bool endAbility = false;

        while (!endAbility)
        {

            if (card != null && card != memoryManagerInstance.cardUp)
            {

                Debug.Log("x: " + card.x);
                Debug.Log("y: " + card.y);

                /*for (int i = 0; i < memoryManagerInstance.sizeX; i++)
                {
                    for (int j = 0; j < memoryManagerInstance.sizeY; j++)
                    {
                        memoryManagerInstance.cardMap[i, j].ShowCard();
                    }
                }*/
                //c.x -= 1;

                //map position random
                //recolocar
                memoryManagerInstance.cardMap[card.x + 1, card.y + 1].ShowTokenColor();       
                memoryManagerInstance.cardMap[card.x + 1, card.y].ShowTokenColor();

                memoryManagerInstance.SwapCard(card.x + 1, card.y + 1, card.x + 1, card.y);
                yield return new WaitForSeconds(2f);
                memoryManagerInstance.cardMap[card.x + 1, card.y].HideCard();
                memoryManagerInstance.cardMap[card.x + 1, card.y - 1].ShowTokenColor();

                memoryManagerInstance.SwapCard(card.x + 1, card.y + 1, card.x + 1, card.y - 1);
                yield return new WaitForSeconds(2f);
                memoryManagerInstance.cardMap[card.x + 1, card.y - 1].HideCard();
                memoryManagerInstance.cardMap[card.x, card.y - 1].ShowTokenColor();

                memoryManagerInstance.SwapCard(card.x + 1, card.y + 1, card.x, card.y - 1);
                yield return new WaitForSeconds(2);
                memoryManagerInstance.cardMap[card.x, card.y - 1].HideCard();
                memoryManagerInstance.cardMap[card.x-1, card.y - 1].ShowTokenColor();         
                memoryManagerInstance.SwapCard(card.x + 1, card.y + 1, card.x - 1, card.y - 1);
                yield return new WaitForSeconds(2);
                memoryManagerInstance.cardMap[card.x - 1, card.y - 1].HideCard();
                memoryManagerInstance.cardMap[card.x - 1, card.y].ShowTokenColor();

                memoryManagerInstance.SwapCard(card.x + 1, card.y + 1, card.x - 1, card.y);
                yield return new WaitForSeconds(2);
                memoryManagerInstance.cardMap[card.x - 1, card.y].HideCard();
                memoryManagerInstance.cardMap[card.x - 1, card.y+1].ShowTokenColor();

                memoryManagerInstance.SwapCard(card.x + 1, card.y + 1, card.x - 1, card.y + 1);
                yield return new WaitForSeconds(2);
                memoryManagerInstance.cardMap[card.x - 1, card.y + 1].HideCard();
                memoryManagerInstance.cardMap[card.x, card.y + 1].ShowTokenColor();

                memoryManagerInstance.SwapCard(card.x + 1, card.y + 1, card.x, card.y + 1);

                endAbility = true;
                break;
            }
            yield return null;
        }
    }
}
