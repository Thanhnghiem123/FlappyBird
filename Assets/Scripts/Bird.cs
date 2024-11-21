using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Bird : MonoBehaviour
{
    public float jumpForce = 6f;
    private Rigidbody2D rb;
    private float maxRotation = 25f; // Góc xoay tối đa
    private IAudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<Audios>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || IsTouchingScreen()) // Click mouse left button, press space key, or touch screen to jump
        {
            rb.velocity = Vector2.up * jumpForce;
            audioManager.PlayWingSound();
        }

        RotateBird();
    }

    private bool IsTouchingScreen()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }

    void RotateBird()
    {
        float rotationZ;
        float newRotationZ;

        if (rb.velocity.y > -jumpForce)
        {
            rotationZ = maxRotation;
        }
        else
        {
            rotationZ = Mathf.Lerp(0, -90f, -rb.velocity.y / jumpForce);
        }

        // Lấy giá trị góc quay hiện tại của đối tượng
        float currentRotationZ = transform.rotation.eulerAngles.z;

        // Điều chỉnh góc quay hiện tại để nằm trong khoảng -180 đến 180
        if (currentRotationZ > 180) currentRotationZ -= 360;

        // Tính toán góc quay mới một cách mượt mà
        newRotationZ = Mathf.Lerp(currentRotationZ, rotationZ, 5f * Time.deltaTime);

        // Thiết lập góc quay mới cho đối tượng
        transform.rotation = Quaternion.Euler(0, 0, newRotationZ);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlayHitSound();
        GameObject gameOver = GameObject.FindGameObjectWithTag("End");
        gameOver.SetActiveRecursively(true);
        
        GameObject scoreDisplayObject = GameObject.FindGameObjectWithTag("Result");
        if (scoreDisplayObject != null)
        {
            ScoreDisplay scoreDisplay = scoreDisplayObject.GetComponent<ScoreDisplay>();
            if (scoreDisplay != null)
            {
                scoreDisplay.HighestScore();
                Debug.Log("aaaaaaaaaaaaaaaaaaaaaaa");
            }
        }
        else Debug.LogError("Không tìm thấy component ScoreDisplay trên GameObject.");
        Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioManager.PlayPointSound();
        GameObject scoreDisplayObject = GameObject.FindGameObjectWithTag("ScoreDisplay");
        if (collision.gameObject.CompareTag("Score"))
        {
            // Giả sử GameObject chứa ScoreDisplay có tag là "ScoreDisplay"
            
            if (scoreDisplayObject != null)
            {
                ScoreDisplay scoreDisplay = scoreDisplayObject.GetComponent<ScoreDisplay>();
                if (scoreDisplay != null)
                {
                    scoreDisplay.ScoreExtra();
                }
                else
                {
                    Debug.LogError("Không tìm thấy component ScoreDisplay trên GameObject.");
                }
            }
        }
    }
}

