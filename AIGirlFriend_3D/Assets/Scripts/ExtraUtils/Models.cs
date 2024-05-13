using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ExtraUtils
{
    [System.Serializable]
    public class CardData
    {
        public string cardId;
        public Sprite cardImage;
    }
    
    [System.Serializable]
    public class Group
    {
        public List<string> cards = new List<string>();
    }
    
    [System.Serializable]
    public class SubmitRequest
    {
        public List<Group> groups = new List<Group>();
        public string roomId;
        public string userId;
    }

    public enum ConnectionMode
    {
        Staging,
        Local,
        Production
    }
    
    [System.Serializable]
    public class ValidateRequest
    {
        public List<GroupBasicData> groups = new List<GroupBasicData>();
        public string roomId;
        public string userId;
    }
    
    [System.Serializable]
    public class GroupBasicData
    {
        public string groupId;
        public List<string> cards = new List<string>();
    }

    [System.Serializable]
    public class TournamentEntry
    {
        public string tournamentId;
        public float entryFees;
        public float pointValue;
        public float winningAmount;
    }
    
    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    } 
    
    public enum MainGameType
    {
        OnlineRummy,
        Tournament,
        PlayWithFriends,
        PlayerWithComputer,
    }

    public enum SubGameType
    {
        PointRummy,
        PoolRummy,
        BestOfThree,
        BestOfTwo,
        GunShot
    }

    public enum MaxPlayers
    {
        TwoPlayers,
        SixPlayers
    }

    public enum PointRummyMode
    {
        Point101,
        Point201
    }

    
}