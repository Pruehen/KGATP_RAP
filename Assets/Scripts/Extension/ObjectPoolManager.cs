using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QueueExtensions
{
    // ������Ʈ Ǯ�� ������Ʈ�� �߰��ϴ� Ȯ�� �޼���
    public static void EnqueuePool(this Queue<GameObject> queue, GameObject item)
    {
        item.gameObject.SetActive(false);
        queue.Enqueue(item);
    }

    // ������Ʈ Ǯ���� ������Ʈ�� �������� Ȯ�� �޼���
    public static GameObject DequeuePool(this Queue<GameObject> queue)
    {
        if (queue.Count == 0)
        {
            Debug.LogWarning("!!!queue.Count == 0!!!");
            return null;
        }

        GameObject item = queue.Dequeue();
        item.gameObject.SetActive(true);
        return item;
    }
}

public class Pool//Ǯ ���� Ŭ����
{
    public Queue<GameObject> queue;
    public int count;
    public Transform transform;

    public Pool(Transform transform)
    {
        queue = new Queue<GameObject>();
        count = 0;
        this.transform = transform;
    }
}

public class ObjectPoolManager : SceneSingleton<ObjectPoolManager>
{
    //������Ʈ�� Ǯ�� �������� ����� �� �ֵ��� �����ִ� �̱��� Ŭ����.
    private Dictionary<string, Pool> objectPools = new Dictionary<string, Pool>();
    //������ ������ Ÿ���� string���� �˻���.
    //�̸��� ������ ������ ���������� ����ϱ� ������ �̸� ������ ������ ��.

    public void CreatePool(GameObject prefab, int count = 10)//Ǯ�� count��ŭ ����.
    {
        string itemType = prefab.name;
        if (!objectPools.ContainsKey(itemType))//Ű�� ���� ���
        {
            //Ǯ�� ������ Ʈ�������� �߰��ϰ�, ������ Ǯ�� ��ųʸ��� �߰���.
            GameObject pool = new GameObject();
            pool.transform.SetParent(this.transform);
            pool.name = prefab.name + "Pool";
            objectPools.Add(itemType, new Pool(pool.transform));
        }

        for (int i = 0; i < count; i++)//count��ŭ ������ ������Ʈ�� �����ؼ� ��ť.
        {
            GameObject item = Instantiate(prefab, objectPools[itemType].transform);
            item.name = itemType;
            objectPools[itemType].queue.EnqueuePool(item);
            objectPools[itemType].count++;
        }
    }
    public void EnqueueObject(GameObject item)//�� �� �� ť�� �ٽ� ����. Destroy�� ��ü��.
    {
        string itemType = item.name;
        if (!objectPools.ContainsKey(itemType))//Ű�� ���� ���
        {
            CreatePool(item);//�ڵ����� Ǯ�� ����
        }
        item.transform.SetParent(objectPools[itemType].transform);
        objectPools[itemType].queue.EnqueuePool(item);
    }
    public void AllDestroyObject(GameObject prefab)//prefab�� ���� Ÿ���� ��� ������Ʈ�� ť�� �ٽ� ����.
    {
        string itemType = prefab.name;
        if (!objectPools.ContainsKey(itemType))//Ű�� ���� ���
        {
            CreatePool(prefab);//�ڵ����� Ǯ�� ����
        }

        for (int i = 0; i < objectPools[itemType].transform.childCount; i++)//Ű���� �ش��ϴ� Ʈ�������� ��� ������ �˻�
        {
            GameObject item = objectPools[itemType].transform.GetChild(i).gameObject;
            if (item.activeSelf)
            {
                EnqueueObject(item);//Ȱ��ȭ�Ǿ����� ��� ��ť.
            }
        }
    }

    public GameObject DequeueObject(GameObject prefab)//�� �� ��ȯ��. Instantiate�� ��ü��.
    {
        string itemType = prefab.name;
        if (!objectPools.ContainsKey(itemType))//Ű�� ���� ���
        {
            CreatePool(prefab);//�ڵ����� Ǯ�� ����
        }
        GameObject dequeneObject = objectPools[itemType].queue.DequeuePool();
        //��ť �õ�. ť�� �ִ� ��� �������� ������ϰ�� null�� ��ȯ��.
        if (dequeneObject != null)//ť�� ���빰�� ���� ���
        {
            //Debug.Log(objectPools[itemType].queue.Count);
            return dequeneObject.gameObject;//�ش� ������Ʈ�� ��ȯ
        }
        else//ť�� ���빰�� ���� ���
        {
            CreatePool(prefab, objectPools[itemType].count);//Ǯ Ȯ��.
            return DequeueObject(prefab);//�߰��� Ǯ���� ��ť.
        }
    }

    /// <summary>
    /// �ð��� �����ؼ� ��ȯ��
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="time"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void EnqueueObject(GameObject item, float time)
    {
        StartCoroutine(DelayedEnqueu(item, time));
    }
    private IEnumerator DelayedEnqueu(GameObject item, float time)
    {

        Debug.Assert(item != null, "delayed returning pool item is null");
        yield return new WaitForSeconds(time);
        EnqueueObject(item);
    }
}