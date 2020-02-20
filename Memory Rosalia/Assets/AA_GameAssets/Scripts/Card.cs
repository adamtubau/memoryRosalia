using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [SerializeField] CardInfo cardInfo;

    Image image;
    public int x, y;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public CardInfo getCardInfo()
    {
        return cardInfo;
    }

    public void setCardInfo(CardInfo _cardInfo)
    {
        cardInfo = _cardInfo;
    }

    public void ShowTokenColor()
    {
        image.color = cardInfo.tokenAbility.color;
    }

    public void HideCard()
    {
        image.color = Color.white;
    }

    public void PlayCardFragment(MusicManager musicManagerInstance)
    {
        musicManagerInstance.playFragment(cardInfo.IDSong);
        image.color = Color.gray;
    }
}
