public interface IObjectPool<T> {
    public void ReturnItem(IObjectPoolItem<T> item);
    public T GetItem();
}
