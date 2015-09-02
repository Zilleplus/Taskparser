using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskParser_lib;

namespace Task_to_acces
{
    public class TaskToAcces_2010 
    {
        private OleDbConnection cn;
        public TaskToAcces_2010(string PathToAccesDB)
        {
            //make connection
            cn =  new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+PathToAccesDB);
            //cn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\YourFolder\YourAccessDb.mdb");//Password=1L0v3Acce55;
        }
        public bool MakeTable(String TableName,List<string> heads) 
        {
            cn.Open();
            //make a new table
            string queryTable = @"CREATE TABLE "+TableName+@"
                                (
                                ID Autoincrement , ";
            for (int i = 0; i < heads.Count; i++)
            {
                queryTable = queryTable + heads[i] + " varchar(255), ";
            }
            queryTable = queryTable + @"CONSTRAINT id_pk PRIMARY KEY(Id)
                                ); ";
            OleDbCommand cmd = new OleDbCommand(queryTable, cn);
            cmd.ExecuteNonQuery();

            cn.Close();
            return true;
        }
        public bool ToAcces(String TableName,System.Collections.Generic.List<TaskParser_lib.Task> tasks)
        {
            cn.Open();
            OleDbCommand cmd;
            foreach (TaskParser_lib.Task task in tasks)
            {
                cmd = new OleDbCommand(task.MakeQueryTask(TableName), cn);
                cmd.ExecuteNonQuery();
            }
            cn.Close();
            return true; /* succesfully saved the data */
        }
    }
}