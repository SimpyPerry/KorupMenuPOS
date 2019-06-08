using KorupMenuPOS.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace KorupMenuPOS.Data
{
    public class LeaderBoardDB
    {
        readonly SQLiteAsyncConnection _database;
       

        public LeaderBoardDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ChallengePerson>().Wait();
            
        }

        public Task<int> AddTimeToLeaderBoard(string challengeTime, string challengerName)
        {
            ChallengePerson result = new ChallengePerson();

            string time = "0001/01/01,"+ challengeTime;
            
            
            result.ChallengeDate = DateTime.UtcNow;
            result.ChallengeTime = Convert.ToDateTime(time);
            result.PersonName = challengerName;

            if(result.PersonID != 0)
            {
                return _database.UpdateAsync(result);
            }
            else
            {
                return _database.InsertAsync(result);
            }

            
        }

        public Task<List<ChallengePerson>> GetLeaderBoardTimes()
        {
            
            return  _database.Table<ChallengePerson>().OrderBy(time => time.ChallengeTime).ToListAsync();


            //List<HighScore> people = new  List<HighScore>();

            //foreach(ChallengePerson cp in list)
            //{
            //    new HighScore
            //    {
            //        ChallengerName = cp.PersonName,
            //        HighScoreTime = cp.ChallengeTime.ToLongTimeString(),
            //        ChallengeDate = cp.ChallengeDate
            //    };
            //}

            //return people;
            
        }

        public async void DeleteThisScore(int challengenPersonId)
        {
            ChallengePerson cp = await _database.Table<ChallengePerson>().Where(x => x.PersonID == challengenPersonId).FirstOrDefaultAsync();
            await _database.DeleteAsync(cp);
        }
    }
}
