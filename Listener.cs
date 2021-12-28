using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace just_try
{
    class Listener
    {
        private List<ListEntry> listEntries = new List<ListEntry>();
        
        public void MagazinesChanged(object sender, MagazinesChangedEventArgs<string> e)
        {
            listEntries.Add(new ListEntry(e.CollectionName, e.TypeEvent, e.PropertySourceOfChangeOfItemData, e.KeyElement));
            //Console.WriteLine("Подписались, значится");
        }

        public override string ToString()
        {
            string bigString = "";
            foreach(ListEntry temp in listEntries)
                bigString += temp.ToString();

            return bigString;
        }
    }
}
