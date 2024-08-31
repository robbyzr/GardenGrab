using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float minX; // Koordinat X minimum
    public float maxX; // Koordinat X maksimum
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //input untuk tombol A dan D
        float moveDirection = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1f; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1f;
        }

        // Hitung posisi baru berdasarkan arah dan kecepatan
        float newX = transform.position.x + moveDirection * moveSpeed * Time.deltaTime;

        // membatasi posisi X agar tidak melewati batas
        newX = Mathf.Clamp(newX, minX, maxX);

        // Terapkan posisi baru
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

    }
    void OnTriggerEnter(Collider other)
    {
        //mengecek apakah objek terkena obstacle lalu mengurangi nyawa
        if (other.CompareTag("Obstacle"))
        {
            gameManager.ReduceLives(1);
        }
        if (other.CompareTag("Life"))
        {
            gameManager.IncreaseLives(1);
        }
    }
    
}
