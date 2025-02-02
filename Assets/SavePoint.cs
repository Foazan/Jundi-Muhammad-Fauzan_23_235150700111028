using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SavePoint : MonoBehaviour
{
    [SerializeField]
    private Wall wallTilemap;
    [SerializeField]
    private Player player; 
    [SerializeField]
    private int _extrahealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.PlusHealth(_extrahealth);
            this.gameObject.SetActive(false);
        }
    }
}
