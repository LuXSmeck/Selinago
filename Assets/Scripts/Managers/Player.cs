using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private string playerName;
    [SerializeField] private DeckList deck;

    public void Start(){
        Dictionary<String, int> cardLimitations = GameManager.Instance.CardLimitations;
        deck = new DeckList(cardLimitations);
    }
    
    public string PlayerName{
        get => playerName;
        set => playerName = value;
    }

    public DeckList Deck{
        get => deck;
        set => deck = value;
    }
}
