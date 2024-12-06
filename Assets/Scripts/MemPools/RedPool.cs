using Zenject;

public class RedPool : MonoMemoryPool<Ball>
{
    protected override void OnDespawned(Ball item)
    {
        base.OnDespawned(item);
        item.Reset();
    }
}