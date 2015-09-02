using System;
namespace Taks_To_Acces
{
    interface ITaskToAcces
    {
        bool ToAcces(string nameTable, System.Collections.Generic.List<TaskParser_lib.Task> tasks);
    }
}
