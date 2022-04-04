public interface IObjectPoolItem<T> {
    public void Init(IObjectPool<T> pool);
    public void OnDestroy();
    public void Activate();
    public void Disable();
    public T GetObject();
}
