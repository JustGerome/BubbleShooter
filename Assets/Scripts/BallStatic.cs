using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStatic : MonoBehaviour
{
    // Start is called before the first frame update

    public string myColor;
    public bool previouslyMoving = false;
    public bool isTriggered = false;
    public bool isConnectedAbove;
    public CircleCollider2D _circleCollider;
    public GameManager _gameManager;
    public GameObject vfx;


    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.enabled = true;

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered) {
            StartCoroutine(TriggerCheck());

            isTriggered = false;
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(ChainReaction(collision));
    }

    IEnumerator ChainReaction(Collider2D collision) {

        yield return null;

        if (previouslyMoving)
        {
            //Debug.Log("Previously Moving");

            if (collision.gameObject.tag.Equals("Circle") && _circleCollider != null)
            {
                if (collision.gameObject.GetComponent<BallStatic>().myColor.Equals(myColor))
                {
                    isTriggered = true;
                    collision.gameObject.GetComponent<BallStatic>().previouslyMoving = true;
                    collision.gameObject.GetComponent<BallStatic>().isTriggered = true;
                }
                else {
                    if (transform.parent.position.y > collision.transform.parent.position.y && isTriggered) {

                        collision.gameObject.GetComponent<BallStatic>().previouslyMoving = true;
                        collision.gameObject.GetComponent<BallStatic>().isTriggered = true;
                    }
                }
            }
        }

        yield return null;
    }

    IEnumerator TriggerCheck() {
        gameObject.transform.parent.Translate(new Vector3(0, Time.deltaTime * -.0001f), 0);

        yield return new WaitForSeconds(.1f);
        _gameManager.AddScore();
        Instantiate(vfx,transform.position,transform.rotation);
        Destroy(gameObject.transform.parent.gameObject);
    }
    /*
    IEnumerator ChainReaction(Collider2D collision)
    {

        yield return null;

        if (previouslyMoving)
        {
            if (collision.gameObject.tag.Equals("Circle"))
            {
                if (collision.gameObject.GetComponent<BallStatic>().myColor.Equals(myColor))
                {
                    yield return null;
                    Debug.Log("Previously Moving");
                    isTriggered = true;
                    collision.gameObject.GetComponent<BallStatic>().previouslyMoving = true;
                    collision.gameObject.GetComponent<BallStatic>().isTriggered = true;

                    previouslyMoving = false;
                }
            }
        }
        yield return null;
    }
    */



}
