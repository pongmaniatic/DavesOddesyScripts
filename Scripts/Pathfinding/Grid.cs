using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	[Tooltip("Add the ground floors")]
	public GameObject[] floors; 
	
	public GameObject player;
	[Tooltip("Layer that the pathfinding algorithm avoid")]
	public LayerMask obstacle;
	// public Vector3 offset;
	[Tooltip("Half the length of the node")]
	public float nodeRadius;
	[Tooltip("Spacing between nodes")]
	public float distance;

	public Vector3 offset;
	// [HideInInspector]
	public List<Node> finalPath;
	private Vector3 gridSize = Vector3.zero;
	private Node[,] grid;
	private Vector3[] floorPositions;
	private Vector3[] floorBounds;
	private Quaternion[] rotation;
	private Vector3 worldPoint;
	private float nodeDiameter;
	public int gridSizeX, gridSizeY;
	private void OnEnable()
	{
		nodeDiameter = nodeRadius * 2;
		GetFloorBounds();
		// for (int i = 0; i < floors.Length; i++)
		// {
		// 	gridSize += floors[i].transform.position + (floorBounds[i]); //To get the size of the floor. This to make the script as malleable as possible 
		// }
		CreateGrid();
	}
	void GetFloorBounds() //To get the floors to set the size of the path finding. 
	{
		floorPositions = new Vector3[4];
		floorBounds = new Vector3[floors.Length];
		rotation = new Quaternion[floors.Length];
		Bounds bounds;
		Vector3 floorCenter = new Vector3();
		for (int i = 0; i < floors.Length; i++)
		{
			bounds = floors[i].GetComponent<BoxCollider>().bounds;
			floorCenter += floors[i].transform.position;
			floorBounds[i] = bounds.extents;

			if (floors[i].transform.position.x + floorBounds[i].x > floorPositions[0].x && floors[i].transform.position.x + floorBounds[i].x > 0)
			{
				floorPositions[0].x = floors[i].transform.position.x + floorBounds[i].x;
			}
			if (floors[i].transform.position.x - floorBounds[i].x < floorPositions[1].x && floors[i].transform.position.x - floorBounds[i].x < 0)
			{
				floorPositions[1].x = floors[i].transform.position.x - floorBounds[i].x;
			}
			if (floors[i].transform.position.z + floorBounds[i].z > floorPositions[2].z && floors[i].transform.position.z + floorBounds[i].z > 0)
			{
				floorPositions[2].z = floors[i].transform.position.z + floorBounds[i].z;
			}
			if (floors[i].transform.position.z - floorBounds[i].z < floorPositions[3].z && floors[i].transform.position.z - floorBounds[i].z < 0)
			{
				floorPositions[3].z = floors[i].transform.position.z - floorBounds[i].z;
			}
			rotation[i] = floors[i].transform.rotation;
			// Handles.DrawWireCube(floors[i].transform.position + floorBounds[i], new Vector3(5, 5, 5));
			

		}
		
		// for (int i = 0; i < floorPositions.Length; i++)
		{
			gridSizeX += Mathf.Abs(Mathf.RoundToInt(Mathf.Abs(floorPositions[0].x - floorPositions[1].x)));
			gridSizeY += Mathf.Abs(Mathf.RoundToInt(Mathf.Abs(floorPositions[2].z - floorPositions[3].z)));
		}

		if (floors.Length > 0) //Only needs center if there are floors, no reason to risk dividing by 0
			transform.position = floorCenter / floors.Length; 
	}
	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 bottomLeft = transform.position - Vector3.one * gridSize.x / 2 - Vector3.one * gridSize.z;
		Debug.Log("Bottom left: "+bottomLeft);
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool wall = !(Physics.CheckSphere(worldPoint + offset, nodeRadius, obstacle));
				grid[x, y] = new Node(wall, worldPoint, x, y);
			}
		}
	}
	public Node NodeFromWorldPosition(Vector3 worldPosition)
	{
		float xpoint = ((worldPosition.x + gridSize.x / 2) / gridSize.x);
		float ypoint = ((worldPosition.z  + gridSize.z / 2) / gridSize.z);
		xpoint = Mathf.Clamp01(xpoint);
		ypoint = Mathf.Clamp01(ypoint);
		int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
		int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);
		return grid[x, y];
	}
	public List<Node> GetNeighboringNodes(Node node)
	{
		List<Node> neighboringNodes = new List<Node>();
		int xCheck;
		int yCheck;
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;
				xCheck = node.gridX + x;
				yCheck = node.gridY + y;
				if (xCheck >= 0 && xCheck < gridSizeX)
				{
					if (yCheck >= 0 && yCheck < gridSizeY)
					{
						neighboringNodes.Add(grid[xCheck, yCheck]);
					}
				}

				if (xCheck >= 0 && xCheck < gridSizeX)
				{
					if (yCheck >= 0 && yCheck < gridSizeY)
					{
						neighboringNodes.Add(grid[xCheck, yCheck]);
					}
				}
			}
		}
		return neighboringNodes;
	}

	public Vector3[] Path()//Get the center of each node between the player and final position
	{
		if (finalPath == null || finalPath.Capacity == 0)
			return null;
		
		Vector3[] pathPoints = new Vector3[finalPath.Capacity];
		for (int i = 0; i < pathPoints.Length; i++)
		{
			pathPoints[i] = finalPath[i].position;
		}
		return pathPoints;
	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.z));
		
		if (grid != null)
		{
			foreach (Node n in grid)
			{
				Gizmos.color = (n.isObstacle) ? Color.white : Color.red; //Check if there's an obstacle in the way

				if (finalPath != null)
				{
					if (finalPath.Contains(n))
					{
						Gizmos.color = Color.magenta;
					}
				}
				Gizmos.DrawCube(transform.position + n.position, Vector3.one * (nodeDiameter - distance));
			}
		}
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(player.transform.position, .5f);
	}
}
