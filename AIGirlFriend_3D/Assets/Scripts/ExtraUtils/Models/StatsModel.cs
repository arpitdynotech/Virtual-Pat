using System;
using System.Collections.Generic;

namespace ExtraUtils.Models
{
    [Serializable]
    public class Player
    {
        public string ID { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Disqualified { get; set; }
        public string GameplayTokenMd5 { get; set; }
        public int NoOfTurnsTaken { get; set; }
        public List<string> Cards { get; set; }
        public bool HasShown { get; set; }
        public int Life { get; set; }
        public string ProfileImageUrl { get; set; }
        public int Score { get; set; }
        public bool HasSubmitted { get; set; }
        public bool HasDropped { get; set; }
        public bool AutoDrop { get; set; }
        public bool IsEliminated { get; set; }
    }

    [Serializable]
    public class StatsResponse
    {
        public string GameStatus { get; set; }
        public string RoomId { get; set; }
        public string RummyType { get; set; }
        public int Round { get; set; }
        public bool HasShown { get; set; }
        public List<string> OpenDeckLastCards { get; set; }
        public bool HasPicked { get; set; }
        public string JokerCard { get; set; }
        public List<string> JokerCards { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Player> Players { get; set; }
        public int TimeLeft { get; set; }
    }
}