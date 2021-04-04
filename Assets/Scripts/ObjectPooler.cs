using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public Transform parent;
        public int maxSize;
    }

    [SerializeField] List<Pool> objPools;
    Dictionary<string, Queue<GameObject>> poolDictionary;
    GameObject objToSpawn;

    [HideInInspector] public static ObjectPooler instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in objPools)
        {
            Queue<GameObject> objectQue = new Queue<GameObject>();

            for (int i = 0; i < pool.maxSize; i++)
            {
                GameObject obj;
                if(pool.parent!=null)
                    obj = Instantiate(pool.prefab, pool.parent);
                else
                    obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectQue.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectQue);
        }
    }

    public GameObject SpawnPoolObj(string tag, Vector3 position, Quaternion rotation) {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("<color=red> Object pool with " + tag + " tag does not exist </color>");
            return null;
        }

        objToSpawn = poolDictionary[tag].Dequeue();

        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        objToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

    public GameObject SpawnPoolObj(string tag, Vector3 position, Quaternion rotation, Transform parent) {
        objToSpawn = SpawnPoolObj(tag, position, rotation);
        if(objToSpawn.transform.parent != parent)
            objToSpawn.transform.parent = parent;

        return objToSpawn;
    }
}