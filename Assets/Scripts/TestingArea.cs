using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestingArea : MonoBehaviour {

   private List<CreatureCard> myMonsters;
   private List<AFieldCard> fieldCards;
   private List<EquipableCard> equipmentCards;
   
   [Header("PathFinding")]
   [SerializeField] private int myCardSlotId = 0;
   [SerializeField] private int targetCardSlotId = 0;
   [SerializeField] private int targetX = 2;
   [SerializeField] private int targetY = 2;

   CardManager cardManager;

   // Start is called before the first frame update
   void Start() {
      cardManager = CardManager.Instance;
      CardHolder cardHolder = cardManager.GetComponentInChildren<CardHolder>();
      myMonsters     = cardHolder.creatureCards;
      fieldCards     = cardHolder.fieldCards;
      equipmentCards = cardHolder.equipmentCards;
      
      Debug.LogAssertion("***** Testing Start ***** ");
      test();
   }

   [ContextMenu("RunTest_ALL")]
   public void test() {
      testPlacement();
      testTerraforming();
      testSelect();
   }
   
   public void testPlacement() {
      Debug.LogAssertion("***** Creature Placing: RED Labels ***** ");
      cardManager.cardSlots[14].placeCard(fieldCards[4], 2, 1);
      cardManager.cardSlots[0].placeCard(myMonsters[0], 2, 1);
      cardManager.cardSlots[15].placeCard(fieldCards[4], 2, 2);
      cardManager.cardSlots[1].placeCard(myMonsters[1], 2, 2);
      cardManager.cardSlots[16].placeCard(fieldCards[4], 4, 4);
      cardManager.cardSlots[2].placeCard(myMonsters[2], 4, 4);
      cardManager.cardSlots[17].placeCard(fieldCards[4], 3, 1);
      cardManager.cardSlots[3].placeCard(myMonsters[0], 3, 1);
   }

   [ContextMenu("ErrorTest_Placement")]
   private void testErrorPlacement(){
      Debug.LogAssertion("***** Creature Placing: Errors inc? ");
      Debug.Log("Same Slot");
      cardManager.cardSlots[1].placeCard(myMonsters[3], 4, 1);
      cardManager.cardSlots[2].placeCard(myMonsters[3], 4, 2);
      Debug.Log("Same Field");
      cardManager.cardSlots[4].placeCard(myMonsters[4], 2, 2);
      cardManager.cardSlots[5].placeCard(myMonsters[4], 4, 4);
   }

   public void testTerraforming() {
      Debug.LogAssertion("***** Terrain ***** ");
      cardManager.cardSlots[5].placeCard(fieldCards[3], 5, 5);
      cardManager.cardSlots[5].removeCard();
      cardManager.cardSlots[6].placeCard(fieldCards[0], 4, 4);
      cardManager.cardSlots[7].placeCard(fieldCards[2], 3, 6);
   }

   [ContextMenu("ErrorTest_Terraforming")]
   private void testErrorTerraformingt(){
      Debug.Log("provoked Errors start *****");
      cardManager.cardSlots[8].placeCard(fieldCards[3], 2, 2);
      cardManager.cardSlots[9].placeCard(fieldCards[3], 2, 0);
   }

   [ContextMenu("RunTest_Select()")]
   public void testSelect() {
      Debug.LogAssertion("***** Select ***** ");
      cardManager.setSelectedField(cardManager.cardSlots[myCardSlotId]);
   }
   
   [ContextMenu("RunTest_PathFinding")]
   public void testGridPathFind() {
      Debug.LogAssertion("***** PathFind ***** ");
      cardManager.setSelectedField(cardManager.cardSlots[myCardSlotId], true);
      cardManager.tryMoveabilityCreature(targetX, targetY);
   }
   
   [ContextMenu("RunTest_Movement")]
   public void testGridMovement() {
      Debug.LogAssertion("***** Movement ***** ");
      cardManager.setSelectedField(cardManager.cardSlots[myCardSlotId], true);
      int result = cardManager.tryMoveabilityCreature(targetX, targetY);
      switch (result) {
         case 0: Debug.Log("Movement is possible");
                 cardManager.confirmMovement(targetX, targetY);
                 break;
         case 1: Debug.Log("Movement is not possible: Target is out of Range");
                 break;
         default: Debug.Log("Movement is not possible);");
                 break;
      }
   }
   
   [ContextMenu("RunTest_Fighting")]
   public void testFighting() {
      Debug.LogAssertion("***** Fight ***** ");
      cardManager.setSelectedField(cardManager.cardSlots[myCardSlotId]);
      Field defendingField = cardManager.cardSlots[targetCardSlotId].fieldReference;
      
      cardManager.initializeAttack(defendingField);
   }

   
   [ContextMenu("RunTest_Unselect")]
   public void testGridUnselect() {
      Debug.LogAssertion("***** Unselect ***** ");
      cardManager.cancelAction();
   }

   [ContextMenu("RunTest_OnHitEffects")]
   public void testOnHitEffects() {
      Debug.LogAssertion("***** Effects: OnHit ***** ");
      cardManager.cardSlots[10].placeCard(myMonsters[6], 12, 1, true);
      cardManager.cardSlots[11].placeCard(myMonsters[7], 11, 1, true);
      cardManager.cardSlots[12].placeCard(myMonsters[3], 13, 1, true);
      cardManager.cardSlots[13].placeCard(myMonsters[2], 12, 2, true);
      cardManager.setSelectedField(cardManager.cardSlots[10]);
      
      Debug.LogAssertion("Attacking an Enemy with strong DEF No STR with Piece AND Deadly");
      Field defendingField = cardManager.cardSlots[11].fieldReference;
      cardManager.initializeAttack(defendingField);
      
      Debug.LogAssertion("Attacking an Enemy with no DEF with Piece AND Deadly");
      defendingField = cardManager.cardSlots[12].fieldReference;
      cardManager.initializeAttack(defendingField);
      
      
      Debug.LogAssertion("Attacking an Enemy with strong DEF with Piece AND Deadly");
      defendingField = cardManager.cardSlots[13].fieldReference;
      cardManager.initializeAttack(defendingField);

   }

   [ContextMenu("RunTest_Whitelisting")]
   public void testFactoryWhitelisting(){
      Debug.LogAssertion("***** Factory: Whitelisting ***** ");
      cardManager.cardSlots[18].placeCard(fieldCards[5], 12, 12);
      cardManager.cardSlots[19].placeCard(myMonsters[0], 12, 12);
      cardManager.cardSlots[19].placeCard(myMonsters[1], 12, 12);
      
      Debug.LogAssertion("***** Factory: Blacklisting ***** ");
      cardManager.cardSlots[20].placeCard(fieldCards[6], 13, 13);
      cardManager.cardSlots[21].placeCard(myMonsters[2], 13, 13);
      cardManager.cardSlots[21].placeCard(myMonsters[1], 13, 13);
      cardManager.cardSlots[21].placeCard(myMonsters[0], 13, 13);

   }


   [ContextMenu("RunTest_Equpiment")]
   public void testWeapons(){
      Debug.LogAssertion("***** Monster + Weapon ***** ");
      Debug.LogAssertion("Gobo takes 2 Weapons ");
      cardManager.cardSlots[22].placeCard(fieldCards[4], 11, 2);
      cardManager.cardSlots[23].placeCard(myMonsters[5], 11, 2);
      cardManager.cardSlots[23].equipCard(equipmentCards[1]);
      cardManager.cardSlots[23].equipCard(equipmentCards[1]);
      cardManager.cardSlots[23].equipCard(equipmentCards[0]);
      cardManager.cardSlots[23].equipCard(equipmentCards[2]);
      Debug.LogAssertion("Drake can only take 1 ");
      cardManager.cardSlots[25].placeCard(fieldCards[4], 12, 2);
      cardManager.cardSlots[24].placeCard(myMonsters[2], 12, 2);
      cardManager.cardSlots[24].equipCard(equipmentCards[1]);
      cardManager.cardSlots[24].equipCard(equipmentCards[0]);
      cardManager.cardSlots[24].equipCard(equipmentCards[2]);
      
      Debug.LogAssertion("***** Building + Upgrades ***** ");
      cardManager.cardSlots[26].placeCard(fieldCards[7], 11, 3);
      cardManager.cardSlots[27].placeCard(fieldCards[7], 12, 3);
      cardManager.cardSlots[27].equipCard(equipmentCards[3]);
      cardManager.cardSlots[28].placeCard(myMonsters[1], 11, 3);
      cardManager.cardSlots[29].placeCard(myMonsters[1], 12, 3);

   }

   [ContextMenu("RunTest_RemoveEquipment")]
   public void testRemoveWeapons(){
      Debug.LogAssertion("***** Monster + Weapon ***** ");
      Debug.LogAssertion("Remove Poison Daggers ");
      cardManager.cardSlots[23].removeEquipment(equipmentCards[1]);
      cardManager.cardSlots[24].removeEquipment(equipmentCards[1]);
      
   }

}
