using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder {

   private PathNode[,] pathgrid;
   private FieldGrid grid;
   private int width;
   private int height;

   public PathFinder(FieldGrid fieldGrid) {
      grid = fieldGrid;
      width = grid.getWidth();
      height = grid.getHeight();
      resetPathFinder();
   }

   /// <summary>
   /// Searchs Fields in a given Range, that contains a creature.
   /// The Fields dont have to be reachable, but there must be a visible path to them
   /// </summary>
   /// <param name="startX"></param>
   /// <param name="startY"></param>
   /// <param name="range"></param>
   /// <returns> List of Fields including Creatures </returns>
   public List<Field> findCreatureFieldsInRange(Field startField, int range) {
      List<Field> openList = new List<Field> { startField };
      List<Field> closedList = new List<Field>();
      
      Field targetField;
      Vector2 position;
      int x,y;

      //find all non-Blocking-Fields in Range.
      for (int i = range; i > 0; i--) {
         List<Field> nextList = new List<Field>();
         while (openList.Count > 0) {
            Field field = openList[0];
            position = field.getPosition();
            x = (int)position.x;
            y = (int)position.y;

            //LEFT
            if (x - 1 >= 0) {
               targetField = grid.fields[x - 1, y];
               if ((!closedList.Contains(targetField)) && (!targetField.isBlocking())) {
                  nextList.Add(targetField);
               }
            }
            //RIGHT
            if (x + 1 < width) {
               targetField = grid.fields[x + 1, y];
               if ((!closedList.Contains(targetField)) && (!targetField.isBlocking())) {
                  nextList.Add(targetField);
               }
            }
            //TOP
            if (y + 1 < height) {
               targetField = grid.fields[x, y + 1];
               if ((!closedList.Contains(targetField)) && (!targetField.isBlocking())) {
                  nextList.Add(targetField);
               }
            }
            //BOT
            if (y - 1 >= 0) {
               targetField = grid.fields[x, y - 1];
               if ((!closedList.Contains(targetField)) && (!targetField.isBlocking())) {
                  nextList.Add(targetField);
               }
            }

            closedList.Add(field);
            openList.Remove(field);
         }
         
         openList = nextList.Distinct().ToList();
      }

      //Limit reached
      while (openList.Count > 0) {
         closedList.Add(openList[0]);
         openList.Remove(openList[0]);
      }
      closedList.Remove(startField);

      //No more open Nodes
      List<Field> creaturesInRange = new List<Field>();
      foreach (Field field in closedList) {
         if (field.isOccupied()) {
            creaturesInRange.Add(field);
         }
      }
      return creaturesInRange;
   }

   /// <summary>
   /// Searchs all Fields that are reachable in a given Range.
   /// All Fields have to be accessable and free.
   /// </summary>
   /// <param name="startX"></param>
   /// <param name="startY"></param>
   /// <param name="range"></param>
   /// <returns></returns>
   public List<Field> findFreeFieldsInRange(Field startField, int range) {
      List<Field> openList = new List<Field> { startField };
      List<Field> closedList = new List<Field>();
      Field targetField;
      Vector2 position;
      int x,y;

      for (int i = range; i > 0; i--) {
         List<Field> nextList = new List<Field>();
         while (openList.Count > 0) {
            Field field = openList[0];
            position = field.getPosition();
            x = (int)position.x;
            y = (int)position.y;

            //LEFT
            if (x - 1 >= 0) {
               targetField = grid.fields[x - 1, y];
               if ((!closedList.Contains(targetField)) && (targetField.isApproachable())) {
                  nextList.Add(targetField);
               }
            }
            //RIGHT
            if (x + 1 < width) {
               targetField = grid.fields[x + 1, y];
               if ((!closedList.Contains(targetField)) && (targetField.isApproachable())) {
                  nextList.Add(targetField);
               }
            }
            //TOP
            if (y + 1 < height) {
               targetField = grid.fields[x, y + 1];
               if ((!closedList.Contains(targetField)) && (targetField.isApproachable())) {
                  nextList.Add(targetField);
               }
            }
            //BOT
            if (y - 1 >= 0) {
               targetField = grid.fields[x, y - 1];
               if ((!closedList.Contains(targetField)) && (targetField.isApproachable())) {
                  nextList.Add(targetField);
               }
            }

            closedList.Add(field);
            openList.Remove(field);
         }
         openList = nextList;
      }

      //Limit reached
      while (openList.Count > 0) {
         closedList.Add(openList[0]);
         openList.Remove(openList[0]);
      }
      closedList.Remove(startField);
       
      //No more open Nodes
      return closedList;
   }


   /// <summary>
   /// Searchs all Fields in a given Range.
   /// All Fields have to be accessable and free.
   /// </summary>
   /// <param name="startX"></param>
   /// <param name="startY"></param>
   /// <param name="range"></param>
   /// <returns></returns>
   public List<Field> findFieldsInRange(int startX, int startY, int range, bool withStart = false) {
      Field startField = grid.fields[startX, startY];

      List<Field> openList = new List<Field> { startField };
      List<Field> closedList = new List<Field>();
      Field targetField;
      Vector2 position;
      int x,y;

      for (int i = range; i > 0; i--) {
         List<Field> nextList = new List<Field>();
         while (openList.Count > 0) {
            Field field = openList[0];
            position = field.getPosition();
            x = (int)position.x;
            y = (int)position.y;

            //LEFT
            if (x - 1 >= 0) {
               targetField = grid.fields[x - 1, y];
               if (!closedList.Contains(targetField)) {
                  nextList.Add(targetField);
               }
            }
            //RIGHT
            if (x + 1 < width) {
               targetField = grid.fields[x + 1, y];
               if (!closedList.Contains(targetField)) {
                  nextList.Add(targetField);
               }
            }
            //TOP
            if (y + 1 < height) {
               targetField = grid.fields[x, y + 1];
               if (!closedList.Contains(targetField)) {
                  nextList.Add(targetField);
               }
            }
            //BOT
            if (y - 1 >= 0) {
               targetField = grid.fields[x, y - 1];
               if (!closedList.Contains(targetField)) {
                  nextList.Add(targetField);
               }
            }

            closedList.Add(field);
            openList.Remove(field);
         }
         openList = nextList;
      }

      //Limit reached
      while (openList.Count > 0) {
         closedList.Add(openList[0]);
         openList.Remove(openList[0]);
      }

      if (!withStart){
         closedList.Remove(startField);
      }

      //No more open Nodes
      return closedList;
   }

   /// <summary>
   /// Returns the maybe closest, approachable path between start and endpoint. No diagonal movement allowed.
   /// Does not work if there is no valid path at all!
   /// </summary>
   /// <param name="startX"></param>
   /// <param name="startY"></param>
   /// <param name="endX"></param>
   /// <param name="endY"></param>
   /// <returns> A List of the Fields representing the searched path in the right order
   /// OR NULL if the endNode is not reachable. </returns>
   public List<Field> findPath(int startX, int startY, int endX, int endY) {
      resetPathFinder();
      PathNode startNode = pathgrid[startX, startY];
      PathNode endNode   = pathgrid[endX, endY];

      List<PathNode> openList   = new List<PathNode> { startNode }; //This List represents all the Nodes that shoul be checked
      List<PathNode> closedList = new List<PathNode>(); //The nodes on this list, are already optimized and don't need further checks

      startNode.GCost = 0;
      startNode.HCost = calculateDistance(startNode, endNode);
      startNode.calcualteFCost();

      while (openList.Count > 0) {
         PathNode currentNode = findLowestFCostNode(openList);
         if (currentNode == endNode) { //END reached
            openList.Clear();
         } else {
            closedList.Add(currentNode);
            openList.Remove(currentNode);

            //Checks all the adjacent nodes, updates there costs and adds them to the openlist
            List<PathNode> neightbourList = getNeighbourList(currentNode);
            foreach (PathNode node in neightbourList) {
               if (!closedList.Contains(node)) {
                  int tentativeGCost = currentNode.GCost +1;
                  if (tentativeGCost < node.GCost) {
                     node.Origin = currentNode;
                     node.GCost  = tentativeGCost;
                     node.HCost  = calculateDistance(node, endNode);
                     node.calcualteFCost();

                     if (!openList.Contains(node)) {
                        openList.Add(node);
                     }
                  }
               }
            }
         }
      }
      
      //No more open Nodes
      if (endNode.Origin == null){
         return null; //endNode is not reachable
      } else{
         return calculatePath(endNode);
      }
   }

   /// <summary>
   /// Calculates the distance from one Point to another
   /// No Diagonal movement allowed!
   /// </summary>
   /// <param name="startX"></param>
   /// <param name="startY"></param>
   /// <param name="endX"></param>
   /// <param name="endY"></param>
   /// <returns> The number of steps from one to the other node, ignoring anything in between </returns>
   public int calculateDistance(int startX, int startY, int endX, int endY) {
      PathNode startNode = pathgrid[startX, startY];
      PathNode endNode   = pathgrid[endX, endY];

      return calculateDistance(startNode, endNode);
   }

   //******************************************************************************************** Private
   /// <summary>
   /// Returns a list of all approchable nodes adjacent to the given node */
   /// </summary>
   /// <param name="node"></param>
   /// <returns></returns>
   private List<PathNode> getNeighbourList(PathNode node) {
      List<PathNode> neighbourList = new List<PathNode>();
      int x = node.X;
      int y = node.Y;

      //LEFT
      if ((x-1 >= 0) && (grid.fields[x-1,y].isApproachable())) {
         neighbourList.Add(pathgrid[x-1, y]);
      }
      //RIGHT
      if ((x+1 < width) && (grid.fields[x+1, y].isApproachable())) {
         neighbourList.Add(pathgrid[x+1, y]);
      }
      //TOP
      if ((y+1 < height) && (grid.fields[x, y+1].isApproachable())) {
         neighbourList.Add(pathgrid[x, y+1]);
      }
      //BOT
      if ((y-1 >= 0) && (grid.fields[x, y-1].isApproachable())) {
         neighbourList.Add(pathgrid[x, y-1]);
      }

      return neighbourList;
   }

   /// <summary>
   /// Calculates the Path of Fields, from a given PathNode
   /// </summary>
   /// <param name="endNode"></param>
   /// <returns> List of all the Fields from the staringPoint to the endPoint </returns>
   private List<Field> calculatePath(PathNode endNode) {
      List<Field> path = new List<Field>();
      PathNode currentNode = endNode;

      do {
         path.Add(grid.fields[currentNode.X, currentNode.Y]);
         currentNode = currentNode.Origin;

      } while (currentNode.Origin != null);
      path.Reverse();

      return path;
   }

   /// <summary>
   /// Reinitialize the PathNode Grid
   /// </summary>
   private void resetPathFinder() {
      pathgrid = new PathNode[width, height];
      for (int i = 0; i < height; i++) {
         for (int j = 0; j < width; j++) {
            pathgrid[i, j] = new PathNode(i,j);
         }
      }
   }

   /// <summary>
   /// Calculates the distance from one PathNode to another
   /// No Diagonal movement allowed!
   /// </summary>
   /// <param name="startNode"></param>
   /// <param name="endNode"></param>
   /// <returns> The number of steps from one to the other node, ignoring anything in between </returns>
   private int calculateDistance(PathNode startNode, PathNode endNode) {
      int xDistance = Mathf.Abs(startNode.X - endNode.X);
      int yDistance = Mathf.Abs(startNode.Y - endNode.Y);
      return xDistance + yDistance;
   }

   /// <summary>
   /// Returns the PathNode from a given List, that has the lowest F-Cost.
   /// </summary>
   /// <param name="nodeList"></param>
   /// <returns></returns>
   private PathNode findLowestFCostNode(List<PathNode> nodeList) {
      PathNode lowestCostNode = nodeList[0];

      foreach (PathNode node in nodeList) {
         if (node.FCost < lowestCostNode.FCost) {
            lowestCostNode = node;
         }
      }

      return lowestCostNode;
   }


}
