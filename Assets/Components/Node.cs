using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] parents = new Node[] { };

    public void OnDrawGizmos()
    {
        /*var nodes = GameObject.FindGameObjectsWithTag("Point").ToArray();
        for (var index = 0; index < nodes.Length; index++)
            nodes[index].name = $"Node {index}";*/

        foreach (var parent in parents)
        {
            if (parent == null) continue;
            if (parent.parents.Contains(this))
                Debug.DrawLine(transform.position, parent.transform.position, Color.green);
            else Debug.DrawLine(transform.position, parent.transform.position, Color.red);
        }
    }
}

public class Path
{
    public Node[] Nodes = new Node[] { };

    public float Distance => c();

    public float T => Vector2.Distance(transform.position, Nodes[Nodes.Length - 1].transform.position);

    private Transform transform;

    public Path(Node[] nodes, Transform transform)
    {
        this.transform = transform;
        Nodes = nodes;
    }

    private float c()
    {
        float s = 0;
        for (int i = 1; i < Nodes.Length; i++)
            s += Vector2.Distance(Nodes[i - 1].transform.position, Nodes[i].transform.position);
        return s;
    }

}
