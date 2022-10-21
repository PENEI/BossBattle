using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MemoryPool : MonoBehaviour
{
    private class PoolItem
    {
        public bool isActive;           // "GameObject"�� Ȱ��ȭ/��Ȱ��ȭ ����
        public GameObject gameObject;   // ȭ�鿡 ���̴� ���� ���ӿ�����Ʈ
    }

    private int increaseCount = 5;      // ������ ������Ʈ ����
    private int maxCount = 0;           // ���� ����Ʈ�� ��ϵǾ� �ִ� ������Ʈ ����
    private int activeCount = 0;        // ���� Ȱ��ȭ�� ������Ʈ ����

    private GameObject poolObject;      // ������Ʈ Ǯ������ �����ϴ� ���� ������Ʈ ������

    private List<PoolItem> poolItemList;// �����Ǵ� ��� ������Ʈ�� �����ϴ� ����Ʈ
    private GameObject typeObject;      // ������Ʈ Ǯ������ ������ ������Ʈ�� ���� �θ� ������Ʈ

    public MemoryPool(GameObject obj)
    {
        typeObject = new GameObject("PoolDB");
        this.poolObject = obj;
        poolItemList = new List<PoolItem>();

        InstantiateObjects();
    }

    /// <summary>
    /// increaseCount ������ ������Ʈ�� ����
    /// </summary>
    public void InstantiateObjects()
    {
        maxCount += increaseCount;
        // InstantiateObjects ȣ�� �� maxCount
        // ī���� ��ŭ PoolItem�� ��Ȱ��ȭ�� �̸� ������.
        for (int i = 0; i < increaseCount; ++i)
        {
            PoolItem poolItem = new PoolItem();

            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(poolObject);
            poolItem.gameObject.SetActive(false);

            // ������ PoolDB������Ʈ�� �ڽ����� ���
            poolItem.gameObject.transform.parent = typeObject.transform;

            poolItemList.Add(poolItem);
        }
    }

    /// <summary>
    /// ���� ���� ���� ��� ������Ʈ ����
    /// </summary>
    public void DestroyObjects()
    {
        // List�� ��������� ���� X
        if (poolItemList == null) return;

        // List�� �ִ� ������Ʈ ���� �� List �ʱ�ȭ
        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            Destroy(poolItemList[i].gameObject);
        }
        Destroy(typeObject);
        poolItemList.Clear();
    }

    /// <summary>
    /// poolItemList�� ����Ǿ� �ִ� ������Ʈ�� Ȱ��ȭ�ؼ� ���
    /// ���� ��� ������Ʈ�� ������̸� InstantiateObject()�� �߰� ����
    /// </summary>
    public GameObject ActivatePoolItem()
    {
        if (poolItemList == null) return null;

        // Ȱ��ȭ ������ PoolItem������Ʈ�� maxCount�� ������
        // PoolItem������Ʈ �߰� ����
        if (maxCount == activeCount)
        {
            InstantiateObjects();
        }

        // ���� ������ PoolItem������Ʈ �� false�� ��ü�� Ȱ��ȭ
        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            PoolItem poolItem = poolItemList[i];
            if (poolItem.isActive == false)
            {
                activeCount++;

                poolItem.isActive = true;
                poolItem.gameObject.SetActive(true);

                return poolItem.gameObject;
            }
        }
        return null;
    }
    public GameObject ActivatePoolItem(Vector3 pos)
    {
        GameObject obj = ActivatePoolItem();
        // Ȱ��ȭ�Ǿ� ��ȯ�� ������Ʈ�� ��ġ ����
        if (obj != null)
        {
            obj.transform.position = pos;
            return obj;
        }
        return null;
    }
    public GameObject ActivatePoolItem(Vector3 pos, Quaternion rot)
    {
        GameObject obj = ActivatePoolItem(pos);
        if (obj != null)
        {
            obj.transform.rotation = rot;
            return obj;
        }
        return null;
    }

    /// <summary>
    /// ���� ����� �Ϸ�� ������Ʈ�� ��Ȱ��ȭ ���·� ����
    /// </summary>
    public void DeactivatePoolItem(GameObject removeObject)
    {
        if (poolItemList == null || removeObject == null) 
            return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            // ��Ȱ��ȭ�� ������Ʈ�� ã��
            if (poolItem.gameObject == removeObject)
            {
                activeCount--;
                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);

                return;
            }
        }
    }

    /// <summary>
    /// ���ӿ� ������� ��� ������Ʈ�� ��Ȱ��ȭ ���·� ����
    /// </summary>
    public void DeactivatePoolItems()
    {
        if (poolItemList == null) return;

        // poolItemList�� ��� ��ü�� ��Ȱ��ȭ
        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];
            if (poolItem.gameObject != null &&
                poolItem.isActive == true)
            {
                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);
            }
        }
        activeCount = 0;
    }
}