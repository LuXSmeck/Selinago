using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Building/Factory", fileName = "Building")]
public class FactoryCard: BuildingCard {
   
   [SerializeField] private List<CreatureType> creatureWhitelist;
   [SerializeField] private List<CreatureType> creatureBlacklist;

   protected override GameObject instanciateInstance(){
      return Instantiate(CardManager.Instance.factoryTemplate, CardManager.Instance.spawnPos);
   }
   
   //************************************************************************************************* Getter & Setters
   public List<CreatureType> CreatureWhitelist => creatureWhitelist;
   public List<CreatureType> CreatureBlacklist => creatureBlacklist;
}
