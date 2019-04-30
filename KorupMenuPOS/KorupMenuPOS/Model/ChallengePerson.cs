using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KorupMenuPOS.Model
{
    public class ChallengePerson
    {
        [PrimaryKey, AutoIncrement]
        public int PersonID { get; set; }
        [NotNull]
        public string PersonName { get; set; }
        [NotNull]
        public DateTime ChallengeTime { get; set; }
        public DateTime ChallengeDate { get; set; }
    }
}
