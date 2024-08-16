using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 2; //Jumlah objek yang dipool
    private float spawnInterval = 1f; // Interval spawn dalam detik
    private float nextSpawnTime = 0f;

    [SerializeField] private List<GameObject> buahPrefabs; // Daftar prefab buah
    [SerializeField] private Vector3 minSpawnPos = new Vector3(-10, 20, 15);//minimal posisi spawn
    [SerializeField] private Vector3 maxSpawnPos = new Vector3(10, 20, 15);//maaximal posisis spawn

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
            //Pilih random prefab dari daftar
            int randomIndex = Random.Range(0, buahPrefabs.Count);
            GameObject selectedPrefab = buahPrefabs[randomIndex];

            // Pastikan prefab yang diinstansiasi sesuai dengan yang diinginkan
            if (obj.name != selectedPrefab.name)
            {
                Destroy(obj); // Hapus objek yang tidak sesuai
                obj = Instantiate(selectedPrefab); // Buat yang baru
                pooledObjects.Add(obj);
            }
            //Memunculkan pada posisi random yang sudah ditentukan
            obj.transform.position = new Vector3(Random.Range(minSpawnPos.x, maxSpawnPos.x), minSpawnPos.y, minSpawnPos.z);
            obj.SetActive(true);
        }
    }
}
