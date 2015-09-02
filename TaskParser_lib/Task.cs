using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskParser_lib
{
    public class Task
    {
        public List<string> heads { get; set; }
        public List<string> data { get; set; }

        public Task(List<string> heads) {
            this.heads = heads;
            this.data = new List<string>();
        }


        public string MakeQueryTask(String TableName)
        {
            string headsQuery="";
            for (int i = 0; i < heads.Count-1; i++)
            {
                headsQuery = headsQuery +heads[i] + ",";
            }
            headsQuery = headsQuery + heads[heads.Count - 1] + " ";

            string dataQuery = "";
            for (int i = 0; i < heads.Count - 1; i++)
            {
                dataQuery = dataQuery + "\"" + data[i] + "\"" + ",";
            }
            dataQuery = dataQuery + "\"" + data[data.Count - 1] + "\"" + " ";

            string SQLquery = "INSERT INTO";
            SQLquery = @"INSERT INTO " + TableName + @" (" + headsQuery + @")
            VALUES ("+dataQuery+");";
            return SQLquery;
        }
    }
}
