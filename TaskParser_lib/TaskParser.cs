using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TaskParser_lib
{
    public class TaskParser
    {
        private XmlTextReader reader;
        public enum Read_states { collum_read,row_read };
        public List<Task> Tasks { get; set; }
        public String nameTable { get; set; }


        private HeaderParser Hp;
        public TaskParser() 
        {
            Tasks = new List<Task>();
             nameTable = "NoName";
            Hp = new HeaderParser();
        }

        public Boolean AddTasksFromFile(string pathToFile)
        {
            /* read out the heads */
            Hp.Parse(pathToFile);
            try
            {
                reader = new XmlTextReader(pathToFile);
            }
            catch { return false; }//failed to read file
            
            //skip over headersand get table name
            int rows = 0;
            while (reader.Read() && rows < 2)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Text:
                        if (rows == 0) nameTable = reader.Value;
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        if (reader.Name == "tr") rows++;
                        break;
                }
            }
            //remove the spaces in the name
            nameTable = nameTable.Replace(" ", "_");
            nameTable = nameTable.Replace("<", "");
            nameTable = nameTable.Replace(">", "");
            nameTable = nameTable.Replace("(", "");
            nameTable = nameTable.Replace(")", "");
            nameTable = nameTable.Replace("'", "");

            //read the data
            bool notEndOfTable = true;
            Task taskToAdd = new Task(Hp.heads); /* containts temporary data */

            while (reader.Read() && notEndOfTable)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: 
                        if (reader.Name == "tr") { 
                            //Console.WriteLine("--------------------- start ---------------------"); 
                        }
                        break;
                    case XmlNodeType.Text: 
                        //builder.AddProp(reader.Value);
                        string data = reader.Value;
                        taskToAdd.data.Add(data);
                        break;
                    case XmlNodeType.EndElement: 
                        if (reader.Name == "tr")
                        {
                            //Console.WriteLine("--------------------- build ---------------------");
                            //Tasks.Add(builder.Build());
                            Tasks.Add(taskToAdd);
                            taskToAdd = new Task(Hp.heads);
                        }
                        if (reader.Name == "table") 
                        {
                            notEndOfTable = false;
                        }
                        break;
                }
            }
            reader.Close();
            return true; //succesfull in reading of file
        }
    }
}
