using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonTilleManager : MonoBehaviour
{

    public GameObject[] hexPositions;

    // Start is called before the first frame update
    void Start()
    {
        //InstantiateHexagonTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateHexagonTiles() {
        hexPositions = GameObject.FindGameObjectsWithTag("Hexagons");
    }

    public GameObject FindNearest(Vector3 position) {

        GameObject tMin = null;

        float minDist = Mathf.Infinity;

        Vector3 currentPos = transform.position;
        foreach (GameObject g in hexPositions)
        {
            float dist = Vector3.Distance(g.transform.position, position);
            if (dist < minDist)
            {
                tMin = g;
                minDist = dist;
            }
        }

        return tMin;
    }


}
