namespace player.health
{
    public interface DamageSubscriber
    {
        void healthChange(float damage);
    }
}