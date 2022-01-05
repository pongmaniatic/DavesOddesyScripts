using UnityEngine;

public class Node
{
	public int gridX;
	public int gridY;

	public bool isObstacle;
	public Vector3 position;
	public Node parent;
	public int gCost;
	public int hCost; 
	public int fCost { get { return gCost + hCost; } }

	public Node(bool isObstacle, Vector3 position, int gridX, int gridY)
	{
		this.isObstacle = isObstacle;
		this.position = position;
		this.gridX = gridX;
		this.gridY = gridY;
	}
}
