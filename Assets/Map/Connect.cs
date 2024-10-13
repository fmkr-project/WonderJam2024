using System;
using System.Collections.Generic;
using UnityEngine;

public class Connect : MonoBehaviour
{
    
    public static Connect Instance { get; private set; }
    public GameObject current; 
    public List<GameObject> allObjects; 
    public int numberOfClosest = 3; 
    private List<LineRenderer> lineRenderers = new List<LineRenderer>(); 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this; 
        //DontDestroyOnLoad(gameObject); 
    }

    public void MakeDistance()
    {
        allObjects.ForEach(obj =>
        {
            if(obj.TryGetComponent( out Collider2D col));
            col.enabled = false;
        });
        if (current == null)
        {
            Debug.LogError("The 'current' GameObject is not assigned.");
            return;
        }

        if (allObjects == null || allObjects.Count == 0)
        {
            Debug.LogError("AllObjects list is empty or null.");
            return;
        }

        List<GameObject> closestObjects = GetClosestObjects();

        CreateLineRenderers(closestObjects);
    }
    //for the next function
    private float SqrDistance(Vector3 a, Vector3 b)
    {
        float num1 = a.x - b.x;
        float num2 = a.y - b.y;
        return num1 *  num1 + num2 * num2;
    }
    //get closest objects
    private List<GameObject> GetClosestObjects()
    {
        List<GameObject> closestObjects = new List<GameObject>();

        List<GameObject> sortedObjects = new List<GameObject>(allObjects);
        sortedObjects.Sort((a, b) =>
        {
            float distanceToA = SqrDistance(current.transform.position, a.transform.position);
            float distanceToB = SqrDistance(current.transform.position, b.transform.position);
            return distanceToA.CompareTo(distanceToB);
        });

        for (int i = 1; i < numberOfClosest+1 && i < sortedObjects.Count; i++)
        {
            closestObjects.Add(sortedObjects[i]);
        }

        return closestObjects;
    }

    private void CreateLineRenderers(List<GameObject> closestObjects)
    {
        foreach (var lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }
        lineRenderers.Clear();

        for (int i = 0; i < closestObjects.Count; i++)
        {
            if(closestObjects[i].TryGetComponent( out Collider2D col));
                col.enabled = true;
            GameObject lineObject = new GameObject("LineRenderer_" + i);
            lineObject.transform.parent = current.transform;

            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.material = new Material(Shader.Find("Custom/LineGradientShader")); // Utiliser un shader par défaut

            // Définir les positions du LineRenderer (de `current` à l'objet proche)
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, current.transform.position);
            lineRenderer.SetPosition(1, closestObjects[i].transform.position);

            // Ajouter le LineRenderer à la liste
            lineRenderers.Add(lineRenderer);
        }
    }
}
