using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace just_try
{
    //собирает инф. об изменениях в классе MagCollection. 
    //каждый элемент ListEntry содержит информацию об отдельном изменении объекта
    //MagCollection, в рез-е которого произошло событие MagazinesChanged
    class ListEntry
    { 
        //название коллекции
        public string CollectionName { get; set; }
        //информация о типе события
        public Update TypeEvent { get; set; }
        //название свойства класс Magazine, которое явлиось причиной изменения данных элемента
        public string PropertySourceOfChangeOfItemData { get; set; }
        //string-представление ключа измененного(добавленного, удаленного) элемента
        public string StringKey { get; set; }

        public ListEntry(string collectionName, Update update, string nameProperty, string key)
        {
            CollectionName = collectionName;
            TypeEvent = update;
            PropertySourceOfChangeOfItemData = nameProperty;
            StringKey = key;
        }

        public override string ToString()
        {
            return $"Название коллекции: {CollectionName}\n" +
                $"Чем вызвано событие: {TypeEvent}\n" +
                $"Названием свойства Magazine, являющееся источником изменения: {PropertySourceOfChangeOfItemData}\n" +
                $"Свойство с ключом {StringKey}\n\n";
        }
    }
}
