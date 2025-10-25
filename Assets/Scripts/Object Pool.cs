using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    List<GameObject> pool_objects = null;

    private void Awake()
    {
        if(!instance && instance != this)
        {
            instance = this;
        }
    }

    public List<GameObject> Initializing_Pool_Objects(GameObject object_prefab, int objects_to_pool)
    {
        this.pool_objects = new List<GameObject>();
        List<GameObject> pool_objects = new List<GameObject>();
        for (int i = 0; i < objects_to_pool; i++)
        {
            if (object_prefab)
            {
                GameObject temp = Instantiate(object_prefab);
                temp.SetActive(false);
                this.pool_objects.Add(temp);
                pool_objects.Add(temp);
            }
        }

        return pool_objects;
    }

    public GameObject Activating_Pool_Objects(List<GameObject> pooled_objects,int objects_to_pool) 
    {
        for(int i=0; i < objects_to_pool;++i)
        {
            if (pooled_objects[i] && !pooled_objects[i].activeInHierarchy)
            {
                pooled_objects[i].SetActive(true);
                return pooled_objects[i];
            }
        }
        return null;
    }
}
