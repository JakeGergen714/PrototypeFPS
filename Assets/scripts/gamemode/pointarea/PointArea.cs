using System.Collections.Generic;
using player;

namespace gamemode.pointarea
{
    public interface PointArea
    {
        int getNumberOfPlayersOnpoint(ICollection<Player> players);
    }
}