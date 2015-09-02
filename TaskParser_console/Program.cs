using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskParser_lib;
using Task_to_acces;


namespace TaskParser_console
{
    class Program
    {
        static void Main(string[] args) /* arguments [add,new] [database_file] [html_file] */
        {
            TaskParser parser = new TaskParser();
            Console.WriteLine("This software is made by Willem Melis and is only a proof of concept");
            Console.WriteLine("Do not use this software in a production eviroment");
            switch (args[0])
            {
                case "add": /* add more tasks */
                    Console.WriteLine("adding to existing table");
                    parse(parser, args[2]);
                    saveToDatabase(parser, args[1], false);
                    break;
                case "new": /*create new database and add */
                    Console.WriteLine("adding to new table");
                    parse(parser, args[2]);
                    saveToDatabase(parser, args[1], true);
                    break;
                case "help" : /* display help message */
                    Console.WriteLine("HELP:");
                    Console.WriteLine("arguments [add,new] [database_file] [html_file]");
                    break;
                default:
                    Console.WriteLine("invalid arguments maybe try help");
                    break;
            }
            Console.WriteLine("done");
        }
        static void parse(TaskParser parser, string html_path) 
        {
            Console.WriteLine("parsing...");
            parser.AddTasksFromFile(html_path);
            foreach (TaskParser_lib.Task task in parser.Tasks)
            {
                Console.WriteLine("row "+parser.Tasks.IndexOf(task)+" is parsed");
            }   
        }
        static void saveToDatabase(TaskParser parser, string databasepath, bool makeTable)
        {
            Console.WriteLine("saving to access table Task...");
            Task_to_acces.TaskToAcces_2010 tta = new TaskToAcces_2010(databasepath);
            if (makeTable) tta.MakeTable("Tasks",parser.Tasks[0].heads);
            tta.ToAcces("Tasks", parser.Tasks);
        }
        static void DemoParser()  /* function to demonstrate the functionality */
        {
            TaskParser parser = new TaskParser();
            parser.AddTasksFromFile("task_list.html");
            foreach (TaskParser_lib.Task task in parser.Tasks)
            {
                Console.WriteLine(task.data[0]);
            }
            Task_to_acces.TaskToAcces_2010 tta = new TaskToAcces_2010("TestDatabase.accdb");
            tta.ToAcces(parser.nameTable,parser.Tasks);
        }
    }
}
