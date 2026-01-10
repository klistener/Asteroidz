using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float thrustForce = 1f;
    [SerializeField] float maxSpeed = 5f;
    GameObject boosterFlame;
    private float elapsedTime = 0f;
    private float score = 0f;
    [SerializeField] float scoreMultiplier = 10f;
    UIDocument uiDocument;
    private Label scoreText;
    Rigidbody2D rb;
    GameObject explosionEffect;
    private Button restartButton;

    public float Score { get { return score; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boosterFlame = GameObject.Find("BoosterFlame");
        boosterFlame.SetActive(false);
        uiDocument = GameObject.Find("GameUI").GetComponent<UIDocument>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        explosionEffect = Resources.Load<GameObject>("Prefabs/ExplosionEffect");

        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
    }

    void UpdateScore() {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;
    }

    void MovePlayer() {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;
            transform.up = direction;
            rb.AddForce(direction * thrustForce);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        MovePlayer();   
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
        Destroy(gameObject);
    }
}
