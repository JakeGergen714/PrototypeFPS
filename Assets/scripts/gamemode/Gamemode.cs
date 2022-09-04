namespace gamemode
{
    public interface Gamemode
    {
        void startGame(); //start the game

        void stopGame(); //end the game

        void tick(); //process game tick

        bool isGameOver();

    }
}