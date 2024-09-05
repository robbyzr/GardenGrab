using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private GameManager gameManager;

    public bool isDragging = false;
    private Vector3 offset;

    // Batasan pada sumbu X
    private float minX = -22f;
    private float maxX = 18.5f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = mousePosition + offset;

            // Batasi posisi X agar berada di antara minX dan maxX
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

            // Update posisi objek
            transform.position = newPosition;
        }
    }
    private void OnMouseDown()
    {
        isDragging = true;
        offset = (Vector3)transform.position - (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseUp()
    {
        isDragging = false;
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
