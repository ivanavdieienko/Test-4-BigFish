using Zenject;

public class YellowPool : MonoMemoryPool<Ball>
{
    protected override void OnDespawned(Ball item)
    {
        base.OnDespawned(item);
        item.Reset();
    }
}