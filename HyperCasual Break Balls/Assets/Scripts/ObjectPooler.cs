using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ObjectPooler : MonoBehaviour
{
    #region Singleton
    public static ObjectPooler instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }
    #endregion

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

       // SceneManager.LoadScene(1);

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Transform parent = null, bool local = false, bool _default = false)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            print("Pool tag with name " + tag + " doesn't exists!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        
        Rigidbody objectRb = objectToSpawn.GetComponent<Rigidbody>();
        if (objectRb != null)
            objectRb.velocity = Vector3.zero;

        objectToSpawn.transform.SetParent(parent);
        if (!_default)
        {
            if (!local)
            {
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
            }
            else
            {
                objectToSpawn.transform.localPosition = position;
                objectToSpawn.transform.localRotation = rotation;
            }
        }
        
        

        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    
    public void AddToPool(GameObject _obj)
    {
        _obj.transform.SetParent(transform);
        _obj.SetActive(false);
    }
}
