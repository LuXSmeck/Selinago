using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldGrid : MonoBehaviour {

   [SerializeField] private int width;
   [SerializeField] private int height;
   [SerializeField] private int fieldSize = 50;
   [SerializeField] private int textSize = 25;

   [SerializeField] private GameObject fieldTemplate;
   private List<Field> highlightedFields;
   public Field[,] fields;
   public PathFinder pathFinder;
   
   public void initializeGrid() {
      fields = new Field[width, height];
      for (int i = 0; i < height; i++) {
         for (int j = 0; j < width; j++) {
            GameObject instance = Instantiate(fieldTemplate, CardManager.Instance.transform.Find("Grid").transform);
            instance.transform.position = getWorldPosition(i, j);
            Field fieldInstance = instance.GetComponent<Field>();
            fieldInstance.name = "Field_" + i + ";" + j;
            fieldInstance.initialize(textSize, fieldSize, new Vector2(i, j));

            fields[i, j] = fieldInstance;
            Debug.DrawLine(getWorldPosition(i, j), getWorldPosition(i, j + 1), Color.green, 100f);
            Debug.DrawLine(getWorldPosition(i, j), getWorldPosition(i + 1, j), Color.green, 100f);
         }
      }
      Debug.DrawLine(getWorldPosition(0, height), getWorldPosition(width, height), Color.green, 100f);
      Debug.DrawLine(getWorldPosition(width, 0), getWorldPosition(width, height), Color.green, 100f);

      pathFinder        = new PathFinder(this);
      highlightedFields = new List<Field>();
   }

   //******************************************************************************** cardCalls
   public void terraformArea(int affectionRange, CardSlot cardSlot, TerrainFeature terrain){
      int positionX = (int)cardSlot.fieldReference.getPosition().x;
      int positionY = (int)cardSlot.fieldReference.getPosition().y;
      Field startField = fields[positionX, positionY];
      
      List<Field> fieldsInRange = pathFinder.findFieldsInRange(positionX, positionY, affectionRange);
      fieldsInRange.Add(startField);
      foreach (Field field in fieldsInRange) {
         int distanceToCenter = CardManager.Instance.getFields().pathFinder.calculateDistance(positionX, positionY, (int)field.getPosition().x, (int)field.getPosition().y);
         if (distanceToCenter <= field.getArea().getRange()) {
            if (field.isOccupied() && !terrain.getFeatures()[0]){
               forceMoveCreature(field, startField);
            }
            field.getArea().initialize(cardSlot, distanceToCenter, terrain);
            field.display();
         }
      }
   }

   /// <summary> Moves the Creature in the starField to a free Field. The creature will be exhausted after the movement.
   /// The free Field is chosen by the closest Distance compared to the Startfield.
   /// If multiple free fields are available, the furthest of those is chosen.
   /// If there is again a tie, the target field will be chosen randomly. </summary>
   /// <param name="startField"> The Field in which the creature was located. </param>
   /// <param name="pushingField"> The Field that tries to push the creature away from it. </param>
   private void forceMoveCreature(Field startField, Field pushingField){
      //Seaches the closest Fields compared to the startField.
      int range = 1;
      List<Field> fieldsInRange = new List<Field>();
      while (fieldsInRange.Count == 0){
         fieldsInRange = pathFinder.findFreeFieldsInRange(startField, range);
         range++;
      }

      //Searches the furthest Field compared to the pushingField
      GameManager.Instance.shuffleList(fieldsInRange);
      Field furthestField = fieldsInRange[0];
      int furthestDistance = 0;
      foreach (var field in fieldsInRange){
         int distance = pathFinder.calculateDistance((int)field.getPosition().x, (int)field.getPosition().y,
                                                     (int)pushingField.getPosition().x, (int)pushingField.getPosition().y);
         if (furthestDistance < distance){
            furthestField = field;
            furthestDistance = distance;
         }
      }
      
      //Moves the Creature
      Creature creature = startField.getCreature(); 
      creature.move(furthestField);
      creature.increaseStun();
      
   }
   
   //******************************************************************************** highlighting
   /// <summary>
   /// Colors all reachable Fields from the starting coords as StartingPoint.
   /// If a creature is on the SelectedField, the Range is defined from it's movecapability,
   /// if not, the default range will be 4 Fields
   /// </summary>
   /// <param name="startX"></param>
   /// <param name="startY"></param>
   public void highlightPathPossibilities(Field startField) {
      int range = 0;
      if (startField.isOccupied()) {
         Creature creature = startField.getCreature();
         range = creature.getMovement();
      }
      List<Field> reachableFields = pathFinder.findFreeFieldsInRange(startField, range);
      foreach (var field in reachableFields) {
         field.highlight(new Color(1f, 0.69f, 0.07f));
         highlightedFields.Add(field);
      }
   }
   
   public void highlightAttackPossibilities(Field startField) {
      int range = 1;
      if (startField.isOccupied()) {
         Creature creature = startField.getCreature();

         AAttackEffect effect = creature.getAttackEffects(AttackEffectTypeEnum.RANGE);
         if (effect != null){
            RangeEffect rangeEffect = (RangeEffect)effect; 
            range = rangeEffect.Value;
         }
      }
      List<Field> attackableFields = pathFinder.findCreatureFieldsInRange(startField, range);
      foreach (var field in attackableFields) {
         field.highlight(new Color(1f, 0.61f, 0.08f));
         highlightedFields.Add(field);
      }
   }

   public void highlightField(Field field, Color color) {
      field.highlight(color);
      highlightedFields.Add(field);
   }

   public bool isHighlighted(Field field) {
      return highlightedFields.Contains(field);
   }

   public void resetHighlighting() {
      foreach (Field field in highlightedFields) {
         field.display();
      }
      highlightedFields.Clear();
   }

   public int highlightShortestPath(int startX, int startY, int endX, int endY) {
      List<Field> path = pathFinder.findPath(startX, startY, endX, endY);
      if (path != null){
         foreach (Field field in path) {
            field.highlight(new Color(0.48f, 0.93f, 1f));
            highlightedFields.Add(field);
         }
         return path.Count;
      } else{
         return -1;
      }
   }


   //************************************************************************************************* Getter & Setters
   private Vector3 getWorldPosition(int x, int y) {
      Vector3 position = new Vector3(x, y) * fieldSize;
      return position;
   }

   private Vector2 getXY(Vector3 worldposition) {
      int x = Mathf.FloorToInt(worldposition.x / fieldSize);
      int y = Mathf.FloorToInt(worldposition.y / fieldSize);
      return new Vector2(x, y);
   }
   
   public void setDimentions(int width, int height) {
      this.width = width;
      this.height = height;
   }

   public int getWidth() {
      return width;
   }

   public int getHeight() {
      return height;
   }

}