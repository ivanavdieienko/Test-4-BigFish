public class CollectWinWithMultiplierCommand
{
    private UserData data;

    public CollectWinWithMultiplierCommand(UserData data)
    {
        this.data = data;
    }

    public void Execute(CollectWinWithMultiplierSignal signal)
    {
        data.Balance += (int) (data.Bet * signal.Multiplier * 100);
    }
}
