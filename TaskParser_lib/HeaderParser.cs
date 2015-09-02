using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TaskParser_lib
{
    public class HeaderParser
    {
        private XmlTextReader reader;
        public List<string>  heads { get; set; }
        public HeaderParser(){
            heads = new List<string>();
        }
        public Boolean Parse(string pathToFile) {
            /* clear the list */
            heads.Clear();
            /* try opening the file */
            try
            {
                reader = new XmlTextReader(pathToFile);
            }
            catch { return false; }/* failed to read file */

            /* first skip obsolete rows */
            SkipTillCorrectRow();
            /* get the heads out of the table */
            GetHeads();
            reader.Close();
            return true; /* successfull parse */
        }
        private void SkipTillCorrectRow() {
            Boolean notCorrectRow = true;
            int count=0;
            while (reader.Read() && notCorrectRow)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals("tr"))
                        {
                            if(count > 0) notCorrectRow = false;
                            count++;
                        }
                        break;
                }
            }
        }
        private void GetHeads()
        {
            Boolean notEndRow = true;
            while (reader.Read() && notEndRow)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Text:
                        string value = reader.Value.Replace(" ", "_"); /* replace space with underscore to avoid problems in access table later on */
                        value = value.Replace(".", "_"); 
                        value = value.Replace("#", "HASH"); 
                        heads.Add(value);
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name.Equals("tr"))
                            notEndRow = false;
                        break;
                }
            }
        }

    }
}
