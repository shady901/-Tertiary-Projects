using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRNGGameTask3
{
    class ScoreCollection
    {
        Dictionary<int, ScoreData> collection = new Dictionary<int, ScoreData>();
        static int round;
        public void setScoreData(ScoreData data)
        {
            collection.Add(round, data);

            round++;
        }
        public Dictionary<int, ScoreData> GetScoreData()
        {



            return collection;

        }
    }
}
