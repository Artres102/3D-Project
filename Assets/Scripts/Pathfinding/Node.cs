using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    GameObject id;
    public List<Edge> edges = new List<Edge>();

    public float f, g, h;
    public Node cameFrom;

    public Node(GameObject i)
    {
        id = i;
    }

    public GameObject GetID()
    {
        return id;
    }
}