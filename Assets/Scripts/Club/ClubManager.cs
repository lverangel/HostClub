using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClubManager : define
{
	public class ClubNode
	{
		public kCI_Types type = kCI_Types.none;
		public kCC_Types target = kCC_Types.none;
		public Vector2 gridPoint;
		public Vector3 localPosition;
		public GameObject obj;
	}

	public int level = 1;
	public int style = 1;
	public Vector2 gridDistance = new Vector2 (68, 36);
	public Vector2 size = new Vector2 (6, 12);

	public Vector3 OriginPoint {
		get {
			return transform.FindChild ("Zero").localPosition;
		}
	}

	public Dictionary<Vector2,ClubNode> chessBoard = new Dictionary<Vector2,ClubNode> ();

	List<Vector2> _passableList = new List<Vector2>();

	// Use this for initialization
	void Start ()
	{
		buildMap ();

		RefreshUI ();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	void Awake ()
	{
		_gameManager.clubManager = this;
	}

	void RefreshUI ()
	{
		_RefreshUI ();
	}

	void _RefreshUI (Hashtable pData = null)
	{
		AStar astar = new AStar (_passableList, (int)size.x, (int)size.y);
		List<Vector2> path = astar.getPathing (new Vector2(5,4), new Vector2(4,8));
		foreach (var v in path) {
			Debug.Log ("{" + v.x + "," + v.y + "}");
		}
	}

	void buildMap ()
	{
//		GameObject gridMarkerObj;
//		if (transform.FindChild ("GridMarker")) {
//			gridMarkerObj = transform.FindChild ("GridMarker").gameObject;
//			GameObject.DestroyImmediate (gridMarkerObj);
//		}
//		gridMarkerObj = new GameObject ();
//		Canvas canvas = gridMarkerObj.AddComponent<Canvas> ();
//		canvas.overrideSorting = true;
//		canvas.sortingLayerName = "UI";
//		gridMarkerObj.AddComponent<CanvasGroup> ().alpha = 0.4f;
//		gridMarkerObj.name = "GridMarker";
//		gridMarkerObj.transform.SetParent (transform);
//		gridMarkerObj.transform.localScale = Vector3.one;

		chessBoard.Clear ();
		for (int x = 0; x < size.x; x++) {
			for (int y = 0; y < size.y; y++) {
//				GameObject lo = new GameObject ();
//				lo.name = "{" + x + "," + y + "}";
//				lo.AddComponent<RectTransform> ();
//				Text t = lo.AddComponent<Text> ();
//				t.text = "{" + x + "," + y + "}";
//				t.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
//				t.alignment = TextAnchor.MiddleCenter;
//				lo.transform.SetParent (gridMarkerObj.transform);
//				lo.transform.localPosition = new Vector3 (xx, yy, 0);
//				lo.transform.localScale = Vector3.one;

				float xx = GetComponent<ClubManager> ().OriginPoint.x;
				float yy = GetComponent<ClubManager> ().OriginPoint.y;
				if (x > y) {
					xx += (x + y) * gridDistance.x;
					yy += (x - y) * gridDistance.y;
				} else if (x == y) {
					xx += (x + y) * gridDistance.x;
					yy += 0;
				} else {
					xx += (x + y) * gridDistance.x;
					yy += -(y - x) * gridDistance.y;
				}


				ClubNode node = new ClubNode ();
				node.localPosition = new Vector3 (xx, yy, 0);
				node.gridPoint = new Vector2 (x, y);


				chessBoard.Add (new Vector2 (x, y), node);
			}
		}

		for (int i = 0; i < transform.FindChild ("Items").childCount; i++) {
			GameObject obj = transform.FindChild ("Items").GetChild (i).gameObject;
			List<float> tmp = new List<float> ();
			Dictionary<float,ClubNode> pair = new Dictionary<float,ClubNode> ();
			foreach (KeyValuePair<Vector2,ClubNode> v in chessBoard) {
				float dis = TwoPointDistance2D (v.Value.localPosition, obj.transform.localPosition);
				if (!pair.ContainsKey (dis)) {
					pair.Add (dis, v.Value);
				}
				tmp.Add (dis);
			}
			tmp.Sort ();
			obj.transform.localPosition = pair [tmp [0]].localPosition;
			obj.GetComponent<ClubItem> ().grid = pair [tmp [0]].gridPoint;
			ClubNode curNode = chessBoard [pair [tmp [0]].gridPoint];
			curNode.target = obj.GetComponent<ClubItem> ().activeTarget;
			curNode.type = obj.GetComponent<ClubItem> ().type;
			curNode.obj = obj;
		}

		_passableList.Clear ();
		foreach (KeyValuePair<Vector2,ClubNode> v in chessBoard) {
			if (v.Value.type == kCI_Types.none) {
				_passableList.Add (v.Value.gridPoint);
			}
		}
	}

	private float TwoPointDistance2D (Vector2 p1, Vector2 p2)
	{
		return Mathf.Sqrt ((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
	}


}

public class AStar
{
	private class Node
	{
		public bool isWall = false;
		public Vector2 pos;
		public int gCost;
		public int hCost;

		public int fCost {
			get { 
				return gCost + hCost; 
			}
		}

		public int x, y;

		public Node parent;

		public Node (int x, int y, bool isWall = false)
		{
			this.isWall = isWall;
			this.pos = new Vector2 (x, y);
			this.x = x;
			this.y = y;
		}
	}

	private int w;
	private int h;
	private Node[,] _nodes;
	List<Vector2> _passList = new List<Vector2> ();

	public AStar (List<Vector2> pPassList, int w, int h)
	{
		this.w = w;
		this.h = h;
		setMap (pPassList);
	}

	public void setMap (List<Vector2> pPassList)
	{
		_passList = pPassList;
		_nodes = new Node[w, h];
	}

	Node getNode (Vector2 pos)
	{
		return _nodes [(int)pos.x, (int)pos.y];
	}

	Node getNode (int x, int y)
	{
		return _nodes [x, y];
	}

	public List<Vector2> getPathing (Vector2 pStart, Vector2 pEnd)
	{
		for (int x = 0; x < w; x++) {
			for (int y = 0; y < h; y++) {
				_nodes [x, y] = new Node (x, y, !_passList.Contains (new Vector2 (x, y)));
			}
		}

		Node startNode = getNode (pStart);
		Node endNode = getNode (pEnd);

		List<Node> openSet = new List<Node> ();
		HashSet<Node> closeSet = new HashSet<Node> ();
		openSet.Add (startNode);

		int times = 0;
		while (openSet.Count > 0) {
			if (times > 2000) {
				Debug.Log ("错误！");
				return new List<Vector2> ();
			}
			times++;
			Node curNode = openSet [0];

			for (int i = 0, max = openSet.Count; i < max; i++) {
				if (openSet [i].fCost <= curNode.fCost &&
				    openSet [i].hCost < curNode.hCost) {
					curNode = openSet [i];
				}
			}

			openSet.Remove (curNode);
			closeSet.Add (curNode);

			// 找到的目标节点
			if (curNode == endNode) {
				return generatePath (startNode, endNode);
			}

			// 判断周围节点，选择一个最优的节点
			foreach (Node item in getNeighbor(curNode)) {
				// 如果是墙或者已经在关闭列表中
				if (item.isWall || closeSet.Contains (item))
					continue;
				// 计算当前相领节点现开始节点距离
				int newCost = curNode.gCost + getDistanceNodes (curNode, item);
				// 如果距离更小，或者原来不在开始列表中
				if (newCost < item.gCost || !openSet.Contains (item)) {
					// 更新与开始节点的距离
					item.gCost = newCost;
					// 更新与终点的距离
					item.hCost = getDistanceNodes (item, endNode);
					// 更新父节点为当前选定的节点
					item.parent = curNode;
					// 如果节点是新加入的，将它加入打开列表中
					if (!openSet.Contains (item)) {
						openSet.Add (item);
					}
				}
			}
		}

		return generatePath (startNode, null);

	}

	List<Vector2> generatePath (Node startNode, Node endNode)
	{
		List<Vector2> path = new List<Vector2> ();
		if (endNode != null) {
			Node temp = endNode;
			while (temp != startNode) {
				path.Add (new Vector2 (temp.x, temp.y));
				temp = temp.parent;
			}
			// 反转路径
			path.Reverse ();
		}
		return path;
		// 更新路径
		//			grid.updatePath(path);
	}


	List<Node> getNeighbor (Node pBase)
	{
		List<Node> ret = new List<Node> ();
		List<Vector2> tmp = new List<Vector2> ();
		tmp.Add (new Vector2 (pBase.x + 1, pBase.y));
		tmp.Add (new Vector2 (pBase.x - 1, pBase.y));
		tmp.Add (new Vector2 (pBase.x, pBase.y + 1));
		tmp.Add (new Vector2 (pBase.x, pBase.y - 1));

		foreach (var v in tmp) {
			if (v.x < w && v.x >= 0 && v.y < h && v.y >= 0) {
				ret.Add (getNode (v));
			}
		}

		return ret;
	}

	// 获取两个节点之间的距离
	int getDistanceNodes (Node a, Node b)
	{
		return  Mathf.Abs (a.x - b.x) * 10 + Mathf.Abs (a.y - b.y) * 10;

		//			int cntX = Mathf.Abs (a.x - b.x);
		//			int cntY = Mathf.Abs (a.y - b.y);
		//
		//			// 判断到底是那个轴相差的距离更远
		//			if (cntX > cntY) {
		//				return 14 * cntY + 10 * (cntX - cntY);
		//			} else {
		//				return 14 * cntX + 10 * (cntY - cntX);
		//			}
	}
}

