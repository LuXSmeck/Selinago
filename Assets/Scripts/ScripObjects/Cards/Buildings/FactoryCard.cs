using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Building/Factory", fileName = "Building")]
public class FactoryCard: BuildingCard {
   
   [SerializeField] private List<Type> creatureWhitelist;
   [SerializeField] private List<Type> creatureBlacklist;

   protected override GameObject instanciateInstance(){
      return Instantiate(CardManager.Instance.factoryTemplate, CardManager.Instance.spawnPos);
   }
   
   //************************************************************************************************* Getter & Setters
   public List<Type> CreatureWhitelist => creatureWhitelist;
   public List<Type> CreatureBlacklist => creatureBlacklist;
}
