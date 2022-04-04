using UnityEngine;

public class ObjectPoolItem : MonoBehaviour, IObjectPoolItem<GameObject> {
    private IObjectPool<GameObject> _pool;

    public void OnDestroy() {
        _pool.ReturnItem(this);
    }

    public void Activate() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    public void Init(IObjectPool<GameObject> pool) {
        _pool = pool;
    }

    public GameObject GetObject() {
        return gameObject;
    }
}
