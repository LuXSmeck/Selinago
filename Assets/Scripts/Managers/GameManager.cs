using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameManager : MonoBehaviour{
    public static GameManager Instance;
    
    [Header("Templates")]
    public GameObject player1;
    public GameObject player2;
    
    [Header("Constants")] 
    [SerializeField] private int mainDeckMincards  = 10;
    [SerializeField] private int mainDeckMaxcards  = 30;
    [SerializeField] private int mainDeckDoubles   = 3;
    [SerializeField] private int secDeckMincards   = 10;
    [SerializeField] private int secDeckMaxcards   = 30;
    [SerializeField] private int secDeckDoubles    = 3;
    [SerializeField] private int sideDeckMincards  = 0;
    [SerializeField] private int sideDeckMaxcards  = 10;
    [SerializeField] private int sideDeckDoubles   = 2;

    private Dictionary<String, int> cardLimitations;
    private static Random rng = new Random();  


    public void Awake(){
        if(Instance == null){
            Instance = this;
            instantiate();
        }else{
            Destroy(this.gameObject);
        }
    }
    
    private void instantiate(){
        cardLimitations = new Dictionary<string, int>();
        cardLimitations["MainDeck_MinCards"] = mainDeckMincards;
        cardLimitations["MainDeck_MaxCards"] = mainDeckMaxcards;
        cardLimitations["MainDeck_Doubles"]  = mainDeckDoubles;
        cardLimitations["SecDeck_MinCards"]  = secDeckMincards;
        cardLimitations["SecDeck_MaxCards"]  = secDeckMaxcards;
        cardLimitations["SecDeck_Doubles"]   = secDeckDoubles;
        cardLimitations["SideDeck_MinCards"] = sideDeckMincards;
        cardLimitations["SideDeck_MaxCards"] = sideDeckMaxcards;
        cardLimitations["SideDeck_Doubles"]  = sideDeckDoubles;
    }

    public Dictionary<string, int> CardLimitations => cardLimitations;
    
    
    public void shuffleList<T>(List<T> list){
        int maxIndex = list.Count - 1;
        for (int index = maxIndex; index > 0; index--) {
            int rndIndex = rng.Next(maxIndex);  
            T tmpValue = list[index];  
            list[index] = list[rndIndex];  
            list[rndIndex] = tmpValue;  
        }  
    }
}
