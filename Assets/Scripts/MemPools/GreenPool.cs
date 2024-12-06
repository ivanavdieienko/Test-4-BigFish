using Zenject;

public class GreenPool : MonoMemoryPool<Ball>
{
    protected override void OnDespawned(Ball item)
    {
        base.OnDespawned(item);
        item.Reset();
    }
}