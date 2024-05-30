public class FreeState : IPlayerState
{
    private PlayerInteractController playerController;

    public FreeState(PlayerInteractController playerController)
    {
        this.playerController = playerController;
    }

    public void CatchOrKnockback()
    {
    }

    public void CookOrThrow()
    {
    }

    public void PickupOrPlace()
    {
    }
}
