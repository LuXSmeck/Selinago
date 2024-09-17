using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;

public class Player{

    [SerializeField] private string playerName;
    [SerializeField] private DeckList deck;
    [SerializeField] private int energy;
    [SerializeField] private List<AFieldObject> myFieldObjects;
    
    public Player(){
        playerName = "playerX";
        Dictionary<string, int> cardLimitations = GameManager.Instance.CardLimitations;
        deck = new DeckList(cardLimitations);
    }

    public Player(string name){
        playerName = name;
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
    
    //************************************************************** private Methods
    private void startEnergyManagement(){
        List<AFieldObject> consumers = new List<AFieldObject>();
        foreach (AFieldObject fieldObject in myFieldObjects){
            if (fieldObject.EnergyGeneration <0){
                consumers.Add(fieldObject);
            } else{
                energy += fieldObject.EnergyGeneration;
            }
        }

        foreach (AFieldObject consumer in consumers){
            int result = energy + consumer.EnergyGeneration;
            if (result < 0){
                consumer.iniDestroySequence();
            } else{
                energy = result;
            }
        }
    }
}
