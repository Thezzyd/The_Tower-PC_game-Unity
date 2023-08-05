using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GraphManager : MonoBehaviour
{
    private LevelManager levelManager;
    private GridGraph enemyBatGridGraph;

    private GridGraph enemySkeletalDragonGridGraph;

    public float updateDistance = 6;
    public Transform target;
    GridNodeBase[] buffer;

	public float pudelko;
	public float pudelko2;

    public bool updatingGraph { get; private set; }

    void Start()
    {
	

		levelManager = FindObjectOfType<LevelManager>();

		updateDistance = 6;
		pudelko = levelManager.highestTowerYaxisValueReached + updateDistance;
		pudelko2 = levelManager.highestTowerYaxisValueReached + updateDistance;

		if (AstarPath.active == null) throw new System.Exception("There is no AstarPath object in the scene");

        enemyBatGridGraph = (GridGraph)AstarPath.active.data.FindGraph(g => g.name == "EnemyBatGridGraph");
		enemySkeletalDragonGridGraph = (GridGraph)AstarPath.active.data.FindGraph(g => g.name == "EnemySkeletalDragonGraph");


		if (enemyBatGridGraph == null) throw new System.Exception("The AstarPath object enemyBatGridGraph has no GridGraph or LayeredGridGraph");
		if (enemySkeletalDragonGridGraph == null) throw new System.Exception("The AstarPath object enemySkeletalDragonGridGraph has no GridGraph or LayeredGridGraph");
      
		UpdateGraph();
    }


	private void FixedUpdate()
    {
   //     var graphCenterInGraphSpace = PointToGraphSpace(enemyBatGridGraph.center);
    //    var targetPositionInGraphSpace = PointToGraphSpace(new Vector3(0f, levelManager.highestTowerYaxisValueReached + 30f, 0f));

	//	if (VectorMath.SqrDistanceXZ(graphCenterInGraphSpace, targetPositionInGraphSpace) > updateDistance * updateDistance)
		if (pudelko <= levelManager.cameraAndEnemiesTargetPoint.position.y)
		{
			pudelko = levelManager.cameraAndEnemiesTargetPoint.position.y + updateDistance;
			pudelko2 = levelManager.cameraAndEnemiesTargetPoint.position.y - updateDistance;

			UpdateGraph();
			enemySkeletalDragonGridGraph.center = new Vector3(0.0f, levelManager.cameraAndEnemiesTargetPoint.position.y, 0.0f);
			//	
		//	Debug.Log("Jak czesto hmm... ");
		/*	for (int i = 0; i < enemySkeletalDragonGridGraph.width; i++)
			{
				for (int j = 0; j < enemySkeletalDragonGridGraph.depth; j++)
				{
					enemySkeletalDragonGridGraph.UpdateTransform();
					enemySkeletalDragonGridGraph.RecalculateCell(i, j, false, false);
					enemySkeletalDragonGridGraph.CalculateConnections(i, j);
				}
			}*/
		}

		else if (pudelko2 >= levelManager.cameraAndEnemiesTargetPoint.position.y)
		{
			pudelko2 = levelManager.cameraAndEnemiesTargetPoint.position.y - updateDistance;
			pudelko = levelManager.cameraAndEnemiesTargetPoint.position.y + updateDistance;

			UpdateGraph();
			enemySkeletalDragonGridGraph.center = new Vector3(0.0f, levelManager.cameraAndEnemiesTargetPoint.position.y, 0.0f);
			//	
			//	Debug.Log("Jak czesto hmm... ");
			/*for (int i = 0; i < enemySkeletalDragonGridGraph.width; i++)
			{
				for (int j = 0; j < enemySkeletalDragonGridGraph.depth; j++)
				{
					enemySkeletalDragonGridGraph.UpdateTransform();
					enemySkeletalDragonGridGraph.RecalculateCell(i, j, false, false);
					enemySkeletalDragonGridGraph.CalculateConnections(i, j);
				}
			}*/
		}
	}



	public void UpdateGraph()
	{
	
		if (updatingGraph)
		{
			return;
		}
		
		updatingGraph = true;

		IEnumerator ie = UpdateGraphCoroutine();
		AstarPath.active.AddWorkItem(new AstarWorkItem(
			(context, force) => {
					
					if (force) while (ie.MoveNext()) { }

					bool done;
				try
				{
					done = !ie.MoveNext();
				}
				catch (System.Exception e)
				{
				    Debug.LogException(e, this);
					done = true;
				}

				if (done)
				{
					updatingGraph = false;
				}
				return done;
			}));
	}

	private Vector3 PointToGraphSpace(Vector3 p)
	{
		return enemyBatGridGraph.transform.InverseTransform(p);
	}

	IEnumerator UpdateGraphCoroutine()
	{
		Vector3 dir = PointToGraphSpace(new Vector3(0f, levelManager.cameraAndEnemiesTargetPoint.position.y, 0f)) - PointToGraphSpace(enemyBatGridGraph.center);

		dir.x = Mathf.Round(dir.x);
		dir.y = 0;
		dir.z = 0;

		if (dir == Vector3.zero) yield break;

		Int2 offset = new Int2(-Mathf.RoundToInt(dir.x), -Mathf.RoundToInt(dir.z));

		enemyBatGridGraph.center += enemyBatGridGraph.transform.TransformVector(dir);
		enemyBatGridGraph.UpdateTransform();

		int width = enemyBatGridGraph.width;
		int depth = enemyBatGridGraph.depth;
		GridNodeBase[] nodes;
		
	//	int layers = enemyBatGridGraph.LayerCount;
		nodes = enemyBatGridGraph.nodes;

		if (buffer == null || buffer.Length != width * depth)
		{
			buffer = new GridNodeBase[width * depth];
		}

		if (Mathf.Abs(offset.x) <= width)
		{
			IntRect recalculateRect = new IntRect(0, 0, offset.x, 0);

			if (recalculateRect.xmin > recalculateRect.xmax)
			{
				int tmp2 = recalculateRect.xmax;
				recalculateRect.xmax = width + recalculateRect.xmin;
				recalculateRect.xmin = width + tmp2;
			}

		/*	if (recalculateRect.ymin > recalculateRect.ymax)
			{
				int tmp2 = recalculateRect.ymax;
				recalculateRect.ymax = depth + recalculateRect.ymin;
				recalculateRect.ymin = depth + tmp2;
			}*/

			var connectionRect = recalculateRect.Expand(1);
			connectionRect = IntRect.Intersection(connectionRect, new IntRect(0, 0, width, depth));

		/*	for (int l = 0; l < layers; l++)
			{*/
				//int layerOffset = l * width * depth;
				for (int y = 0; y < depth; y++)
				{
					int pz = y * width;
					int tz = ((y + depth) % depth) * width;
					for (int x = 0; x < width; x++)
					{
						buffer[tz + ((x + offset.x + width) % width)] = nodes[pz + x];
					}
				}

				yield return null;


				for (int y = 0; y < depth; y++)
				{
					int pz = y * width;
					for (int x = 0; x < width; x++)
					{
						int newIndex = pz + x;
						var node = buffer[newIndex];
						if (node != null) node.NodeInGridIndex = newIndex;
						nodes[newIndex] = node;
					}

					int xmin, xmax;
				/*	if (y >= recalculateRect.ymin && y < recalculateRect.ymax)
					{
						xmin = 0;
						xmax = depth;
					}
					else
					{*/
						xmin = recalculateRect.xmin;
						xmax = recalculateRect.xmax;
				//	}

					for (int x = xmin; x < xmax; x++)
					{
						var node = buffer[(pz + x)];
						if (node != null)
						{
							node.ClearConnections(false);
						}
					}
				}

				yield return null;
			/*}*/

			int yieldEvery = 1000;
			
			int approxNumNodesToUpdate = Mathf.Abs(offset.x) * Mathf.Max(width, depth);
			yieldEvery = Mathf.Max(yieldEvery, approxNumNodesToUpdate / 10);
			int counter = 0;

			for (int y = 0; y < depth; y++)
			{
				int xmin, xmax;
			/*	if (y >= recalculateRect.ymin && y < recalculateRect.ymax)
				{
					xmin = 0;
					xmax = width;
				}*/
				//else
			//	{
					xmin = recalculateRect.xmin;
					xmax = recalculateRect.xmax;
			//	}

				for (int x = xmin; x < xmax; x++)
				{
					enemyBatGridGraph.RecalculateCell(x, y, false, false);
				}

				counter += (xmax - xmin);

				if (counter > yieldEvery)
				{
					counter = 0;
					yield return null;
				}
			}

			for (int z = 0; z < depth; z++)
			{
				int xmin, xmax;
			/*	if (z >= connectionRect.ymin && z < connectionRect.ymax)
				{
					xmin = 0;
					xmax = width;
				}
				else
				{*/
					xmin = connectionRect.xmin;
					xmax = connectionRect.xmax;
				//}

				for (int x = xmin; x < xmax; x++)
				{
					enemyBatGridGraph.CalculateConnections(x, z);
				}

				counter += (xmax - xmin);

				if (counter > yieldEvery)
				{
					counter = 0;
					yield return null;
				}
			}

			yield return null;

			for (int z = 0; z < depth; z++)
			{
				for (int x = 0; x < width; x++)
				{
					if (x == 0 || z == 0 || x == width - 1 || z == depth - 1) enemyBatGridGraph.CalculateConnections(x, z);
				}
			}
		}
		else
		{
			int yieldEvery = Mathf.Max(depth * width / 20, 1000);
			int counter = 0;
			
			for (int z = 0; z < depth; z++)
			{
				for (int x = 0; x < width; x++)
				{
					enemyBatGridGraph.RecalculateCell(x, z);
				}
				counter += width;
				if (counter > yieldEvery)
				{
					counter = 0;
					yield return null;
				}
			}

			for (int z = 0; z < depth; z++)
			{
				for (int x = 0; x < width; x++)
				{
					enemyBatGridGraph.CalculateConnections(x, z);
				}
				counter += width;
				if (counter > yieldEvery)
				{
					counter = 0;
					yield return null;
				}
			}
		}
	}
}

