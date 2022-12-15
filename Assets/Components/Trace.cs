using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trace : MonoBehaviour
{
    public Transform Player;
    public Node Node;

    public Node[] currentPath = new Node[] { };
    public Node[] showPath = new Node[] { };

    void Start()
    {
        UpdateCurrentPath();
        UpdateShowPath();
    }

    void Update()
    {
        UpdateShowPath();
        var g = new List<Vector3>(showPath.Select(x => x.transform.position)) { Player.position }.ToArray();
        GetComponent<LineRenderer>().widthMultiplier = 0.125f;
        GetComponent<LineRenderer>().positionCount = g.Length;
        GetComponent<LineRenderer>().SetPositions(g);
    }

    public void UpdateCurrentPath()
    {
        currentPath = GetCurrentPath();
    }

    public void UpdateShowPath()
    {
        if (currentPath.Length == 0) return;
        showPath = currentPath.Take(Array.IndexOf(currentPath, currentPath.Where(x => x.gameObject.activeInHierarchy)
            .OrderBy(x => Vector2.Distance(x.transform.position, Player.transform.position)).First()) + 1)
            .Where(x => x.gameObject.activeInHierarchy).ToArray();
    }

    private Path[] GetPath(Node node, List<Node> buffer = null)
    {
        var s = new List<Path>();
        buffer ??= new();

        buffer.Add(node);

        foreach (var parent in node.parents)
        {
            if (buffer.Contains(parent)) continue;
            // if (!parent.gameObject.activeInHierarchy) continue;
            s.AddRange(GetPath(parent, new(buffer)));
        }

        s.Add(new Path(buffer.ToArray(), Player));

        return s.ToArray();
    }

    private Node[] GetCurrentPath()
    {
        if (Node == null)
        {
            GetComponent<LineRenderer>().positionCount = 0;
            return new Node[] { };
        }
        var nodes = GameObject.FindGameObjectsWithTag("Point").Where(x => x.activeInHierarchy).Select(x => x.GetComponent<Node>()).ToList();
        var s = GetPath(Node);
        s = s.Where(x => x.Nodes[^1].gameObject.activeInHierarchy).ToArray();
        s = s.OrderBy(x => x.T).ThenBy(x => x.Distance).ToArray();
        if (s.Length == 0)
        {
            GetComponent<LineRenderer>().positionCount = 0;
            return new Node[] { };
        }
        return s[0].Nodes;
    }
}
