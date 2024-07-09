using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    float cannonRotation = 0f;
    float smoothing = .7f;
    float horizontal;

    public GameObject[] balls;
    public GameObject[] ballscannon;
    public GameObject[] ballscannonNext;
    public GameObject ballReference;
    public GameManager _gameManager;

    public InputAction inputAction;

    public int currentBall = 1;
    public int nextBall;

    public float shootCooldown = .5f;

    // Start is called before the first frame update
    void Start()
    {
        currentBall = Random.Range(0, 4);
        nextBall = Random.Range(0, 4);
        ballscannon[currentBall].SetActive(true);
        ballscannonNext[nextBall].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        shootCooldown += Time.deltaTime;


        if (Keyboard.current.spaceKey.wasPressedThisFrame && shootCooldown > .5f) {

            shootCooldown = 0;

            Instantiate(balls[currentBall], ballReference.transform.position, ballReference.transform.rotation);

            foreach (GameObject g in ballscannon) {
                g.SetActive(false);
            }

            foreach (GameObject g in ballscannonNext)
            {
                g.SetActive(false);
            }

            currentBall = nextBall;
            nextBall = Random.Range(0, 4);

            ballscannonNext[nextBall].SetActive(true);
            ballscannon[currentBall].SetActive(true);


        }

        cannonRotation -= horizontal * smoothing;
        cannonRotation = Mathf.Clamp(cannonRotation, -60, 60);
        transform.localRotation = Quaternion.Euler(0, 0.0f, cannonRotation);
    }

    public void Move(InputAction.CallbackContext context) {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        
    }

    
}
