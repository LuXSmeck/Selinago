using System;
using UnityEngine;

public class CardManager : MonoBehaviour {
   public static CardManager Instance;
   private GameManager gameManager;

   private const int DIMENSION = 15;
   private const int SLOTS     = 20;
   
   [Header("Fields")]
   [SerializeField] private FieldGrid grid;
   [SerializeField] private Field selectedField;
   public CardSlot[] cardSlots;
   
   [Header("Standards")]
   [SerializeField] public TerrainFeature standardTerrain;

   [Header("Templates")]
   [SerializeField] public GameObject gridTemplate;
   [SerializeField] public GameObject cardSlotTemplate;
   [SerializeField] public GameObject creatureTemplate;
   [SerializeField] public Transform spawnPos;

   void Awake(){
      if(Instance == null){
            Instance = this;
            initializeFIeld();
            initializeCardSlots();
            gameManager = GameManager.Instance;
      }else{
            Destroy(this.gameObject);
      }
   }
        
   public void initializeFIeld(){
      GameObject instance = Instantiate(gridTemplate, transform);
      grid = instance.GetComponentInChildren<FieldGrid>();
      grid.name = "Grid";
      grid.setDimentions(DIMENSION, DIMENSION);
      grid.initializeGrid();
        
      selectedField = grid.fields[0, 0];
   }
   
   public void initializeCardSlots(){
      cardSlots = new CardSlot[SLOTS];
      for (int i = 0; i < SLOTS; i++){
         GameObject instance = Instantiate(cardSlotTemplate, CardManager.Instance.transform.Find("Slots").transform);
         cardSlots[i] = instance.GetComponentInChildren<CardSlot>();
         cardSlots[i].name = "Slot:" + i;
      }
   }

   //*********************************************************************** Card Positioning
   public bool spawnCreature(Field targetField, Creature creature){
      //Field targetField = grid.fields[positionX, positionY];
      int positionX = (int)targetField.getPosition().x;
      int positionY = (int)targetField.getPosition().y;
      if (!targetField.isApproachable()){
         Debug.LogError("Placement failed: Field "+ positionX +"; "+ positionY +" is not Valid");
         return false;
      }else{
         targetField.setCreature(creature);
         creature.setPosition(positionX, positionY);
         Debug.Log("Card "+ creature.getReference().name +" got successfully placed at "+ positionX +" "+ positionY);
         return true;
      }
   }

   //*********************************************************************** Card Interactions
   /// <summary> Checks if there is a creature in both given Fields and
   /// if the attacking Creature is able to attack the defending one. </summary>
   /// <param name="attackerField"> Field of the attacking Creature </param>
   /// <param name="defenderField"> Field of the defending Creature </param>
   /// <returns> 0 if the attack is possible;
   ///           1 if the target is not reachable for the attacker;
   ///           2 if there are not 2 creatures in the given Fields. </returns>
   public int tryAttackingCreatureAt(Field defenderField){
      if (selectedField.isOccupied() && defenderField.isOccupied()){

         grid.resetHighlighting();
         grid.highlightAttackPossibilities(selectedField);
         
         if (grid.isHighlighted(defenderField)){
            return 0;
         } else{
            Debug.LogAssertion("Target out of Range");
            return 1;
         }
      } else{
         Debug.LogAssertion("Invalid Field-Selection");
         return 2;
      }
   }
   
   /// <summary> Moves the Creature from the active Field and places itself on the target location.
   /// There are no validation-checks at all! </summary>
   /// <param name="target"> The targeted Creature </param>
   public void confirmAttack(Creature target) {
      selectedField.getCreature().attack(target);
   }

   /// <summary> Checks if the Creature in the selectedField is able to move towards the given Location.
   /// NO Movement will be performed in any case! </summary>
   /// <param name="targetPositionX"></param>
   /// <param name="targetPositionY"></param>
   /// <returns> 0 if the movement is possible;
   ///           1 if the path is out of range;
   ///           2 if anything else interferes. </returns>
   public int tryMoveabilityCreature(int targetPositionX, int targetPositionY) {
      Field targetField = grid.fields[targetPositionX, targetPositionY];
      if (selectedField.isOccupied() && (selectedField != targetField) && targetField.isApproachable()) {
         if (grid.isHighlighted(targetField)) {
            findPath(targetPositionX, targetPositionY);
            return 0;
         } else {
            grid.resetHighlighting();
            Debug.LogAssertion("Target out of Range");
            return 1;
         }
      } else {
         Debug.LogAssertion("Invalid Field-Selection");
         return 2;
      }
   }

   /// <summary> Moves the Creature from the active Field and places itself on the target location.
   /// There are no validation-checks at all! </summary>
   /// <param name="targetPositionX"></param>
   /// <param name="targetPositionY"></param>
   public void confirmMovement(int targetPositionX, int targetPositionY) {
      selectedField.getCreature().move(getFieldAt(targetPositionX, targetPositionY));
   }

   //************************************************************************************************* Getter & Setters
   public void setSelectedField(int positionX, int positionY, bool highlighting = false) {
      grid.resetHighlighting();
      selectedField = grid.fields[positionX, positionY];
      if (highlighting) {
         grid.highlightPathPossibilities(selectedField);
      }
      grid.highlightField(selectedField, new Color(1f, 0f, 0.03f));
   }

   public void setSelectedField(CardSlot cardSlot, bool highlighting = false){
      setSelectedField((int)cardSlot.fieldReference.getPosition().x, (int)cardSlot.fieldReference.getPosition().y, highlighting);
   }

   public void cancelAction() {
      grid.resetHighlighting();
   }

   public void unselectField() {
      selectedField = null;
      grid.resetHighlighting();
   }
   
   public FieldGrid getFields(){
      return grid;
   }

   public Field getFieldAt(int x, int y){
      return grid.fields[x, y];
   }

   //************************************************************************************************* Privates

   private int findPath(int positionX, int positionY) {
      return grid.highlightShortestPath((int)selectedField.getPosition().x, (int)selectedField.getPosition().y, 
                                         positionX, positionY);
   }

   //************************************************************************************************* Obsolete

   // /// <summary> Changes the TerrainFeature of the given Position, but ignores conflicts with existing entities.
   // /// WARNING: This function uses Debuggingrelated functions and should not be used freely </summary>
   // /// <param name="positionX"> Field x-Coordinate </param>
   // /// <param name="positionY"> Field y-Coordinate </param>
   // /// <param name="terrain"> the new Terrainfeature </param>
   // [Obsolete("uses Obsolete functions")]
   // public void forceTerraformFieldAt(int positionX, int positionY, TerrainFeature terrain) {
   //    grid.fields[positionX, positionY].setTerrain(terrain);
   // }
   //
   // /// <summary> Changes the TerrainFeature of the given Position, if there are no conflicts! </summary>
   // /// <param name="positionX"> Field x-Coordinate </param>
   // /// <param name="positionY"> Field y-Coordinate </param>
   // /// <param name="terrain"> the new Terrainfeature </param>
   // /// <returns> TRUE if the Terrain was changed. FALSE if there was an error. </returns>
   // public bool saveTerraformFieldAt(int positionX, int positionY, TerrainFeature terrain) {
   //    //1st check if the new Terrain is a valid Terrain for existing Entities on that Field
   //    bool validCheck = true;
   //    Field thisField = grid.fields[positionX, positionY];
   //    if (thisField.isOccupied() && !terrain.getFeatures()[0]){
   //       validCheck = false;
   //       Debug.LogError("The position "+ positionX +":"+ positionY +" is already occupied and cant be made non-Accessible");
   //    }
   //    if (thisField.hasInfrastructure() && !terrain.getFeatures()[1]){
   //       validCheck = false;
   //       Debug.LogError("The position "+ positionX +":"+ positionY +" is already a build-ip area and cant be made non-Buildable");
   //    }
   //    
   //    //then the Terrain may be changed
   //    if (validCheck){
   //       thisField.setTerrain(terrain);
   //    }
   //
   //    return validCheck;
   // }

   
   //public bool terraformAccessibilityFieldAt(int positionX, int positionY, bool accessibility) {
   //   return grid.fields[positionX, positionY].makeAccessible(accessibility);
   //}
   //public bool terraformBuildabilityFieldAt(int positionX, int positionY, bool buildability) {
   //   return grid.fields[positionX, positionY].makeBuildable(buildability);
   //}
   //public bool terraformBlockingFieldAt(int positionX, int positionY, bool blocking) {
   //   return grid.fields[positionX, positionY].makeBlocker(blocking);
   //}
}