using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Wall : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private bool isHidden = false;
    [SerializeField]
    private int _showDuration;
    

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        Invoke("HideWall", 10f); 
    }

    void HideWall()
    {
        tilemapRenderer.enabled = false;
        isHidden = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isHidden)
        {
            ShowWallCollision(_showDuration);
        }
    }

    public void ShowWall(int _duration)
    {
        
        StartCoroutine(ShowWallTemporarily());
        IEnumerator ShowWallTemporarily()
        {
            tilemapRenderer.enabled = true;
            yield return new WaitForSeconds(_duration);
            tilemapRenderer.enabled = false;
        }
    }

    public void ShowWallCollision(int _duration)
    {
        StartCoroutine(ShowWallTemporarily());
        IEnumerator ShowWallTemporarily()
        {
            tilemapRenderer.enabled = true;
            yield return new WaitForSeconds(_duration);
            tilemapRenderer.enabled = false;
        }
    }
}
