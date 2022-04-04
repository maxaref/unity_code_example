using UnityEngine;
using System.Collections.Generic;

public class ObjectsPool : MonoBehaviour, IObjectPool<GameObject> {

    [SerializeField] private GameObject _itemPrefab;

    private Queue<IObjectPoolItem<GameObject>> _pool = new Queue<IObjectPoolItem<GameObject>>();

    public void ReturnItem(IObjectPoolItem<GameObject> item) {
        item.Disable();
        _pool.Enqueue(item);
    }

    public GameObject GetItem() {
        if (_pool.Count == 0) {
            CreateItem();
        }

        var item = _pool.Dequeue();
        item.Activate();

        return item.GetObject();
    }

    private void CreateItem() {
        GameObject instance = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
        var poolItem = instance.GetComponent<IObjectPoolItem<GameObject>>();
        poolItem.Init(this);
        _pool.Enqueue(poolItem);
    }
}
