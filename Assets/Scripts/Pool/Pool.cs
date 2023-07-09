using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    [SerializeField]
    private T _prefab { get; }
    [SerializeField]
    private Transform _container { get; }

    private List<T> _pool;

    public Pool(T prefab, int count, Transform parent)
    {
        _prefab = prefab;
        _container = parent;

        CreatePool(count);
    }
    private void CreatePool(int defaultCount)
    {
        _pool = new();
        for (int i = 0; i < defaultCount; i++)
            CreateObject();
    }
    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);
        return createdObject;
    }

    public bool IsFreeElementAvailable(out T element)
    {
        foreach (T obj in _pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                element = obj;
                obj.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (IsFreeElementAvailable(out T element))
            return element;

        return CreateObject(true);
    }

    public void ClearPool()
    {
        _pool.Clear();
    }
}
