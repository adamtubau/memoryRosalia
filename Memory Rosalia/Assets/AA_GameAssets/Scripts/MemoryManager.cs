using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MemoryManager : MonoBehaviour
{
    #region Singleton
    private static MemoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static MemoryManager getInstance()
    {
        return instance;
    }
    #endregion


    #region Map
    [SerializeField] string map;
    public int sizeX, sizeY;
    #endregion

    [SerializeField] Transform spawnPoint;
    [SerializeField] MusicManager musicManagerInstance;

    [SerializeField] CardInfo[] cardInfos;
    [SerializeField] Card cardPrefab;

    [SerializeField] PlayerSpace playerSpace1;
    [SerializeField] PlayerSpace playerSpace2;

    [SerializeField] GameObject win1;
    [SerializeField] GameObject win2;

    #region Raycast UI Variables
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    #endregion

    bool oneCardUp;
    [HideInInspector]
    public Card cardUp;
    int turn = 1;

    int points1 = 0;
    int points2 = 0;

    [SerializeField] Token tokenPrefab;
    List<Token> passiveTokens = new List<Token>();

    public Card[,] cardMap;


    float xOffset = 80;
    float yOffset = 80;

    void Start()
    {
        #region Raycast UI
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        #endregion

        cardMap = new Card[sizeX, sizeY];

        GenenerateMap(map);
        //RandomizeMap();
        StartCoroutine(turnManager());
    }

    void GenenerateMap(string map)
    {
  
        //Counter variables
        int i = 1;
        int j = 1;
        int count = 0;


        while (map[count] != ';')
        {
            while (map[count] != ',')
            {
                int memoryIndex = (int)char.GetNumericValue(map[count]);

                Card c = Instantiate(cardPrefab, new Vector2(spawnPoint.position.x + i * xOffset, spawnPoint.position.y + j * yOffset), cardPrefab.transform.rotation, transform);
                c.setCardInfo(cardInfos[memoryIndex]);

                c.x = i-1;
                c.y = j-1;

                cardMap[i - 1, j - 1] = c;

                count++;
                i++;

            }
            count++;
            j++;
            i = 1;
        }
    }

    public void SwapCard(int fromX, int fromY, int toX, int toY)
    {
        Card c = cardMap[toX, toY];
        cardMap[toX, toY] = cardMap[fromX, fromY];
        cardMap[fromX, fromY] = c;

        //S'ha de fer be el offset
        cardMap[toX, toY].transform.position = new Vector2((toX+1) * xOffset, (toY+1) * yOffset);
        cardMap[fromX, fromY].transform.position = new Vector2((fromX+1) * xOffset, (fromY+1) * yOffset);

    }

    public void RandomizeMap()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                for (int k = 0; k < sizeY; k++)
                {
                    int randomX = Random.Range(0, sizeX - 1);
                    int randomY = Random.Range(0, sizeX - 1);

                    SwapCard(j, k, randomX, randomY);
                }
            }
        }
    }

    public Token RayCastToken()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "Token")
                {
                    return result.gameObject.GetComponent<Token>();
                }
            }
        }
        return null;
    }

    public Card RayCastCard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "Card")
                {
                    return result.gameObject.GetComponent<Card>();
                }
            }
        }
        return null;
    }

    IEnumerator turnManager()
    {
        bool gameRunning = true;

        while (gameRunning)
        {
            //Set up player space UI
            if (turn == 1)
            {
                playerSpace1.setTurnIndicatorColor(true);
                playerSpace2.setTurnIndicatorColor(false);
            }
            if (turn==2)
            {
                playerSpace1.setTurnIndicatorColor(false);
                playerSpace2.setTurnIndicatorColor(true);
            }


            Token t = RayCastToken();
            Card c = RayCastCard();

            if (c != null)
            {
                yield return StartCoroutine(ClickCard(c));
            }
            else if (t != null)
            {
                yield return StartCoroutine(t.executeActiveHability());
            }
          
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameRunning = false;
            }

            yield return null;
        }

        Application.Quit();
    }

    IEnumerator ClickCard(Card card)
    {
        //Si es la primera carta que destapes
        if (!oneCardUp)
        {
            //Check if instant token
            if (card.getCardInfo().tokenAbility != null && card.getCardInfo().tokenAbility.type == TokenAbility.Type.INSTANT)
            {              
                ((TokenAbilityInstant)card.getCardInfo().tokenAbility).executeAbility(this, card);
            }

            else
            {
                //Es destapa la primera que apretes i sona el fragment.
                //S'espera 1.5s abans de poder apretar la seguent
                card.PlayCardFragment(musicManagerInstance);
                yield return new WaitForSeconds(1.5f);
                cardUp = card;
                oneCardUp = true;
            }
        }

        //Si es la segona carta que destapes
        else
        {
            //Si apretes la mateix carta no ha de passar res
            if (cardUp != card)
            {
                //Si la 2na carta que destapes es un token Instantani
                if (card.getCardInfo().tokenAbility != null && card.getCardInfo().tokenAbility.type == TokenAbility.Type.INSTANT)
                {
                    ((TokenAbilityInstant)card.getCardInfo().tokenAbility).executeAbility(this, card);
                }

                //Si la 2na carta que destapes es una carta normal          
                else
                {
                    //Es destapa la segona que apretes i sona el fragment.
                    //S'espera 1.5s abans de poder apretar la seguent
                    card.PlayCardFragment(musicManagerInstance);
                    yield return new WaitForSeconds(1.5f);

                    //EL player encerta dos cartes iguals
                    if (cardUp.getCardInfo() == card.getCardInfo())
                    {
                        Token t = new Token();

                        if (turn == 1) {

                            points1++;
                            playerSpace1.updateScore(points1);

                            if (card.getCardInfo().tokenAbility != null)
                            {
                                card.ShowTokenColor();
                                cardUp.ShowTokenColor();
                                yield return new WaitForSeconds(2f);
                                t = Instantiate(tokenPrefab, transform.position, Quaternion.identity);
                                playerSpace1.parentToLayout(t.transform);
                                t.ownership = 1;
                            }
                        }
                        else {

                            points2++;
                            playerSpace2.updateScore(points2);

                            if (card.getCardInfo().tokenAbility != null)
                            {
                                card.ShowTokenColor();
                                cardUp.ShowTokenColor();
                                yield return new WaitForSeconds(2f);
                                t = Instantiate(tokenPrefab, transform.position, Quaternion.identity);
                                playerSpace2.parentToLayout(t.transform);
                                t.ownership = 2;
                            }
                        }

                        //Assign token ability
                        if (card.getCardInfo().tokenAbility != null)
                        {
                            t.tokenAbility = card.getCardInfo().tokenAbility;
                            t.initialize();
                            t.setColor(card.getCardInfo().tokenAbility.color);

                            if (t.tokenAbility.type == TokenAbility.Type.PASSIVE)
                            {
                                passiveTokens.Add(t);
                            }
                        }


                        cardUp.gameObject.SetActive(false);
                        card.gameObject.SetActive(false);
                        //Si encertes la carta tornes a tirar
                        turn--;
                    }

                    else
                    {
                        cardUp.HideCard();
                        card.HideCard();
                    }

                    
                    //Check if game has ended
                    if((points1 + points2) == (cardMap.Length/2))
                    {
                        //There is no more cards to play
                        Debug.Log("Game end");

                        if (points1 == points2)
                            foreach (Token token in passiveTokens)
                            {
                                if (token.tokenAbility.tokenName == "Tie Break")
                                {
                                    token.gameObject.SetActive(false);
                                    passiveTokens.Remove(token);

                                    if (token.ownership == 1)
                                    {
                                        win1.SetActive(true);
                                    }
                                    else
                                        win2.SetActive(true);

                                    break;
                                }
                            }
                        else if (points1 > points2)
                        {
                            win1.SetActive(true);
                        }
                        else
                        {
                            win2.SetActive(true);
                        }

                    }                  
                }
                

                foreach (Token t in passiveTokens)
                {
                    if (t.tokenAbility.tokenName == "Extra Turn" && t.ownership == turn)
                    {
                        t.gameObject.SetActive(false);
                        passiveTokens.Remove(t);
                        turn--;
                        break;
                    }
                }

                //End turn
                turn++;
                if (turn == 3)
                    turn = 1;
                

                oneCardUp = false;
                cardUp = null;
            }
        }
    }

    public IEnumerator clockSpin(int x, int y)
    {

        



        yield return null;
    }
}
