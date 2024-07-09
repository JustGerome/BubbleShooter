using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoving : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 20;
    public float rotation;
    public GameObject staticObject;
    public HexagonTilleManager _hexagon;
    public GameObject newPosition;

    bool singleInstance;

    void Start()
    {
        singleInstance = false;
        rotation = transform.rotation.z;
        _hexagon = GameObject.Find("TilemapHandler").GetComponent<HexagonTilleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall")) {
            transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z * -1);
        }
        if (collision.gameObject.tag.Equals("Static"))
        {
            GameObject targetPosition;

            //Instantiate(staticObject, transform.position,transform.rotation);

            if (singleInstance == false)
            {
                singleInstance = true;
                targetPosition = _hexagon.FindNearest(transform.position);
                GameObject ballStatic = Instantiate(staticObject, targetPosition.transform);

                ballStatic.GetComponentInChildren<BallStatic>().previouslyMoving = true;
                Destroy(gameObject);
            }
        }

    }
}
