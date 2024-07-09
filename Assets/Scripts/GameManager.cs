using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public Animator _animator;
    public TextMeshProUGUI gameStatus;
    public TextMeshProUGUI scoreTextFinal;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    public GameObject pusher;
    public bool isPlaying;
    public float pusherPosition;
    public int score;

    public float timer;

    public List<GameObject> activBalls;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        StartCoroutine(GetBallsCount());
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5 && isPlaying)
        {
            timer = 0;
            pusher.transform.position = new Vector3(pusher.transform.position.x, pusher.transform.position.y - .5f, pusher.transform.position.z);
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            ResetGame();

        }
    }

    public void AddScore() {
        score += 100;
        scoreText.text = score.ToString("000000");
    }

    IEnumerator GetBallsCount() {

        activBalls.Clear();

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Static")) {

            activBalls.Add(g);
        }

        yield return new WaitForSeconds(.3f);

        if (activBalls.Count <= 0) {
            GameOver("YOU WIN !!");
        }
        else {
            StartCoroutine(GetBallsCount());
        }
        
    }

    public void ResetGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void GameOver(string status) {

        isPlaying = false;
        gameOverPanel.SetActive(true);
        gameStatus.text = status;

        scoreTextFinal.text = "Score: " + score.ToString();

        _animator.Play("Defeat");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Static"))
        {
            GameOver("YOU LOST !!");
        }
    }
}
