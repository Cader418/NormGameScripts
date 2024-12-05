using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;
    public GameObject button;
    private SpriteRenderer buttonSpriteRenderer;
    private PlayerMovement playerScript;
    [SerializeField] Sprite[] buttons;

    private float timer = 60f;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    private void Start()
    {
        SceneManager.LoadScene("Title");
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            button = GameObject.Find("Button_0");
            buttonSpriteRenderer = button.GetComponent<SpriteRenderer>();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == button)
            {
                buttonSpriteRenderer.sprite = buttons[1];
                if (Input.GetMouseButtonDown(0)) SceneManager.LoadScene("Game");
            }
            else buttonSpriteRenderer.sprite = buttons[0];
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            GameObject player = GameObject.FindWithTag("Player");
            playerScript = player.GetComponent<PlayerMovement>();
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer <= 0) SceneManager.LoadScene("Won");
            else if (playerScript.health <= 0) SceneManager.LoadScene("Died");
        }
    }
}
