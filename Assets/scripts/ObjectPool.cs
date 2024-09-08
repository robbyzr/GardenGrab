using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    private GameManager gameManager;
    public static ObjectPool instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 2; //Jumlah objek yang dipool
    private float spawnInterval = 1f; // Interval spawn dalam detik
    private float nextSpawnTime = 0f;

    [SerializeField] private List<GameObject> buahPrefabs; // Daftar prefab buah
    [SerializeField] private List<GameObject> lifePrefabs; // Daftar prefab life
    [SerializeField] private List<GameObject> specialPrefabs; // Daftar prefab special buah
    [SerializeField] private Vector3 minSpawnPos = new Vector3(-28, 55, 20);//minimal posisi spawn
    [SerializeField] private Vector3 maxSpawnPos = new Vector3(22, 55, 20);//maaximal posisis spawn

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); //Pastikan hanya ada satu instance
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //Pool objek untuk setiap prefab yanng ada
        foreach (GameObject prefab in buahPrefabs)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }

    private void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnObject()
    {
        GameObject obj = GetPooledObject();
        if (obj != null)
        {
            GameObject selectedPrefab;

            // Tentukan probabilitas untuk memilih lifePrefab, specialPrefab, atau buahPrefabs
            float spawnChance = Random.value;

            if (gameManager.lives < 3 && spawnChance < 0.1f) // 10% kemungkinan untuk spawn lifePrefab jika lives < 3
            {
                int randomLifeIndex = Random.Range(0, lifePrefabs.Count);
                selectedPrefab = lifePrefabs[randomLifeIndex];
            }
            else if (spawnChance >= 0.1f && spawnChance < 0.15f) // 5% kemungkinan untuk spawn specialPrefabs
            {
                int randomSpecialIndex = Random.Range(0, specialPrefabs.Count);
                selectedPrefab = specialPrefabs[randomSpecialIndex];
            }
            else
            {
                // Jika tidak, pilih buahPrefabs (85% kemungkinan untuk spawn buah biasa atau jika syarat lain tidak terpenuhi)
                int randomFruitIndex = Random.Range(0, buahPrefabs.Count);
                selectedPrefab = buahPrefabs[randomFruitIndex];
            }

            // Pastikan prefab yang diinstansiasi sesuai dengan yang diinginkan
            if (obj.name != selectedPrefab.name)
            {
                obj.SetActive(false); // Nonaktifkan objek sebelum menggantinya
                obj = Instantiate(selectedPrefab); // Buat yang baru
                obj.SetActive(false); // Jangan lupa untuk menonaktifkan sebelum menambah ke pool
                pooledObjects.Add(obj);   
            }

            // Memunculkan pada posisi random yang sudah ditentukan
            obj.transform.position = new Vector3(Random.Range(minSpawnPos.x, maxSpawnPos.x), minSpawnPos.y, minSpawnPos.z);
            obj.SetActive(true);
        }
    }
}
