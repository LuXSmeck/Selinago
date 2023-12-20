using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

   private int x;
   private int y;

   private int gCost;
   private int hCost;
   private int fCost;

   private PathNode origin;

   public PathNode(int x, int y) {
      this.x = x;
      this.y = y;
      gCost  = int.MaxValue;
      hCost  = 0;
      fCost  = int.MaxValue;
      origin = null;
   }

   public void calcualteFCost() {
      fCost = GCost + hCost;
   }

   //************************************************************************************************* Getter & Setters
   public int X { get => x;}
   public int Y { get => y;}
   /// <summary> Represents the real distance from the StartPoint </summary>
   public int GCost { get => gCost; set => gCost = value; }
   /// <summary> Represents the estimated (heuretic) distance to the EndPoint (ignoring obstacles) </summary>
   public int HCost { get => hCost; set => hCost = value; }
   /// <summary> Represents the "cost-value" of the node relative to its G- and H-Cost </summary>
   public int FCost { get => fCost; }
   public PathNode Origin { get => origin; set => origin = value; }

}
