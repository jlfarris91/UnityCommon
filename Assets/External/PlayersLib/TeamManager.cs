namespace PlayersLib
{
    using System.Collections.Generic;
    using System.Linq;
    using CommonLib.Behaviours;

    public class TeamManager : SingletonBehaviour<TeamManager>
    {
        public Team[] Teams;

        private Dictionary<Player, Team> assignedTeams;

        public Player[] GetTeamPlayers(Team team)
        {
            return this.assignedTeams.Where(_ => _.Value == team).Select(_ => _.Key).ToArray();
        }

        public Team GetPlayersTeam(Player player)
        {
            Team assignedTeam;
            this.assignedTeams.TryGetValue(player, out assignedTeam);
            return assignedTeam;
        }

        public void AddPlayerToTeam(Team team, Player player)
        {
            this.RemovePlayerFromCurrentTeam(player);

            this.assignedTeams[player] = team;
        }

        private void RemovePlayerFromCurrentTeam(Player player)
        {
            this.assignedTeams[player] = null;
        }

        private void Start()
        {
            this.assignedTeams = new Dictionary<Player, Team>();
        }
    }
}