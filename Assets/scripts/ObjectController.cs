using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ObjectController : MonoBehaviour
{
    public float dropVelocity = -1f;
    public float rotationSpeed = 1f;
    public float point;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Perbarui kecepatan jatuh
        dropVelocity += Physics.gravity.y * Time.deltaTime;

        //pindahkan objek sesuai dengan kecepatan jatuh
        transform.position += new Vector3(0, dropVelocity * Time.deltaTime, 0);

        //agar objek berotasi di semua sumbu
        transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed) * Time.deltaTime);

        //jika tidak masuk box dan melewati y -20
        if (transform.position.y < -20)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //cek apakah objek terkena box
        if (other.CompareTag("Box"))
        {
            gameObject.SetActive(false);
            gameManager.AddPoints(point);
            
        }
    }
}
