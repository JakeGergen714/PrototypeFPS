namespace player.health
{
    public interface DamagePublisher
    {
        void subscribe(DamageSubscriber subscriber);

        void notifySubscribers(float damage);
    }
}