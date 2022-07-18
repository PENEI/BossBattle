using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MemoryPool : MonoBehaviour
{
    private class PoolItem
    {
        public bool isActive;           // "GameObject"의 활성화/비활성화 정보
        public GameObject gameObject;   // 화면에 보이는 실제 게임오브젝트
    }

    private int increaseCount = 5;      // 오브젝트가 부족할 때 Instantiate()로 추가 생성되는 오브젝트 개수
    private int maxCount;               // 현재 리스트에 등록되어 있는 오브젝트 개수
    private int activeCount;            // 현재 게임에 사용되고 있는(활성화) 오브젝트 개수

    private GameObject poolObject;      // 오브젝트 풀링에서 관리하는 게임 오브젝트 프리팹
    private List<PoolItem> poolItemList;// 관리되는 모든 오브젝트를 저장하는 리스트
    private GameObject typeObject;

    public MemoryPool(GameObject poolObject)
    {
        typeObject = new GameObject("PoolDB");
        maxCount = 0;
        activeCount = 0;
        this.poolObject = poolObject;
        poolItemList = new List<PoolItem>();

        InstantiateObjects();
    }

    /// <summary>
    /// increaseCount 단위로 오브젝트를 생성
    /// </summary>
    public void InstantiateObjects()
    {
        maxCount += increaseCount;
        // InstantiateObjects 호출 시 maxCount
        // 카운터 만큼 PoolItem을 비활성화로 미리 만들어둠.
        for (int i = 0; i < increaseCount; ++i)
        {
            PoolItem poolItem = new PoolItem();

            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(poolObject);
            poolItem.gameObject.SetActive(false);

            // 생성된 PoolDB오브젝트의 자식으로 등록
            poolItem.gameObject.transform.parent = typeObject.transform;

            poolItemList.Add(poolItem);
        }
    }

    /// <summary>
    /// 현재 관리 중인 모든 오브젝트 삭제
    /// </summary>
    public void DestroyObjects()
    {
        // List가 비어있으면 실행 X
        if (poolItemList == null) return;

        // List에 있는 오브젝트 삭제 후 List 초기화
        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            Destroy(poolItemList[i].gameObject);
        }
        poolItemList.Clear();
    }

    /// <summary>
    /// poolItemList에 저장되어 있는 오브젝트를 활성화해서 사용
    /// 현재 모든 오브젝트가 사용중이면 InstantiateObject()로 추가 생성
    /// </summary>
    public GameObject ActivatePoolItem()
    {
        if (poolItemList == null) return null;

        // 활성화 상태의 PoolItem오브젝트가 maxCount와 같으면
        // PoolItem오브젝트 추가 생성
        if (maxCount == activeCount)
        {
            InstantiateObjects();
        }

        // 현재 생성된 PoolItem오브젝트 중 false인 객체를 활성화
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

    /// <summary>
    /// 현재 사용이 완료된 오브젝트를 비활성화 상태로 설정
    /// </summary>
    public void DeactivatePoolItem(GameObject removeObject)
    {
        if (poolItemList == null || removeObject == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            // 비활성화할 오브젝트를 찾기
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
    /// 게임에 사용중인 모든 오브젝트를 비활성화 상태로 설정
    /// </summary>
    public void DeactivatePoolItems()
    {
        if (poolItemList == null) return;

        // poolItemList의 모든 객체를 비활성화
        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];
            if (poolItem.gameObject != null && poolItem.isActive == true)
            {
                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);
            }
        }
        activeCount = 0;
    }
}