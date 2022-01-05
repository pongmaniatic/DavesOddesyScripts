using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Pathfinding : MonoBehaviour
{
	Grid grid;
	public GameObject player;
	private PlayerInput playerInput;
	public Vector3 targetPosition = Vector3.zero;
	private Vector3 newTargetPosition;
	private bool walk;
	private int gCost = 10; // 1 * 10, distance times a constant, 10 is common practice according to https://www.youtube.com/watch?v=-L-WgKMFuhE&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW;
	private int hCost = Mathf.RoundToInt(Mathf.Sqrt(2f) * 10); //the square root of the distance to each side times the constant.
	public int fCost { get { return gCost + hCost; } }

	private void Awake()
	{
		grid = GetComponent<Grid>();
		if (player.GetComponent<PlayerInput>() != null) playerInput = player.GetComponent<PlayerInput>();

		
	}
	private void Update()
	{
		// if (Input.GetMouseButtonDown(0))
		// {
		// 	targetPosition = playerInput.GetTargetPosition();
		// }

		FindPath(player.transform.position, targetPosition);
		// Debug.Log("player path find position: " + player.position);
	}
	void FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Node startNode = grid.NodeFromWorldPosition(startPos);
		// Debug.Log("StartPos: " + startPos);
		Node targetNode = grid.NodeFromWorldPosition(targetPos);
		
		List<Node> openList = new List<Node>();
		HashSet<Node> closedList = new HashSet<Node>();
		openList.Add(startNode);

		while (openList.Count > 0)
		{
			Node currentNode = openList[0];
			for (int i = 0; i < openList.Count; i++)
			{
				if (openList[i].fCost < currentNode.fCost || openList[i].fCost < currentNode.fCost && openList[i].hCost < currentNode.hCost)
				{
					currentNode = openList[i];
				}
			}
			openList.Remove(currentNode);
			closedList.Add(currentNode);

			if (currentNode == targetNode)
			{
				GetFinalPath(startNode, targetNode);
				return;
			}
			foreach (Node neighborNode in grid.GetNeighboringNodes(currentNode))
			{
				if (!neighborNode.isObstacle || closedList.Contains(neighborNode))
				{
					continue;
				}
				int moveCost = currentNode.gCost + GetDistance(currentNode, neighborNode);
				if (moveCost < neighborNode.gCost || !openList.Contains(neighborNode)) 
				{
					neighborNode.gCost = moveCost;
					neighborNode.hCost = GetDistance(neighborNode, targetNode);
					neighborNode.parent = currentNode;

					if (!openList.Contains(neighborNode))
					{
						openList.Add(neighborNode);
					}
				}
			}
		}
	}
	public Vector3[] Path()
	{
		Vector3[] finalPath = grid.Path();
		return finalPath;
	}
	public void SetTarget(Vector3 target)
	{
		targetPosition = target;
	}
	void GetFinalPath(Node startingNode, Node endNode)
	{
		List<Node> finalPath = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startingNode)
		{
			finalPath.Add(currentNode);
			currentNode = currentNode.parent;
		}
		finalPath.Reverse();
		grid.finalPath = finalPath;
	}
	int GetDistance(Node nodeA, Node nodeB)
	{
		int ix = Mathf.Abs(nodeA.gridX - nodeB.gridX); 
		int iy = Mathf.Abs(nodeA.gridY - nodeB.gridY); 

		return ix > iy ? hCost * iy + gCost * (ix - iy) : hCost * ix + gCost * (iy - ix); //Which axis to move diagonally and then the straight forward movement.
	}
}
