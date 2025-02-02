using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private bool _canMove;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _health;
    [SerializeField] 
    private int _maxHealth;
    [SerializeField]
    private int _point;
    [SerializeField]
    private Wall wallTilemap;
    [SerializeField]
    private int _WallShowDuration;
    private Rigidbody2D rb;
    private Vector2 movement;
    private UIManager _UI_Manager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2 (-16, -8);
        _UI_Manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        rb = GetComponent<Rigidbody2D>();
        if (_UI_Manager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
        _canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (_canMove == true)
        {
            rb.MovePosition(rb.position + movement * _speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _health--;
            UpdateUI();
            Debug.Log("Menabrak dinding, HP: " + _health);

            if (_health <= 0)
            {
                Invoke("PlayerDead", 1);
            }

        }

        if (collision.gameObject.CompareTag("SavePoint"))
        {
            wallTilemap.ShowWall(_WallShowDuration);
            Scorring(1);
            UpdateUI();
            if (_point == 5)
            {
                PlayerWin();
            }
        }
    }

    void PlayerDead()
    {
        Debug.Log("Game Over!");
        _UI_Manager.UpdateGameOver();
        Destroy(gameObject);
    }

    void PlayerWin()
    {
        _UI_Manager.UpdateGameSolved();
        DisableMove();

    }

    public void PlusHealth(int _extraHealth)
    {
        _health += _extraHealth;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        UpdateUI();
    }

    public void Scorring (int Point)
    {
        _point += Point;
    }

    public void UpdateUI()
    {
        _UI_Manager.updateHealth(_health);
        _UI_Manager.UpdatePoint(_point);
    }

    public void DisableMove()
    {
        _canMove = false;
    }
}
