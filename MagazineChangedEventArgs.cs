using System;
using System.Collections.Generic;
using System.Text;


namespace just_try
{
    //класс-обработчик
    public class MagazinesChangedEventArgs<TKey>: EventArgs
    {
        #region свойства
        //свойство с название коллекции
        public string CollectionName { get; set; }
        //свойство: чем вызвано событие – добавлением элемента в коллекцию, заменой элемента или измением данных
        public Update TypeEvent { get; set; }
        //свойство с названием свойства Magazine, явл. источником изменения данных элемента.
        //Для события добавления или замены, зн-е свойства – пустая строка 
        public string PropertySourceOfChangeOfItemData { get; set; }
        //свойство с ключом элемента, добавленный в коллекцию/заменивший один из элементов коллекции/или элемента, у которого были изменены данные
        public TKey KeyElement { get; set; }
        #endregion

        public MagazinesChangedEventArgs(string nameMagazine, Update information, string sourceChange, TKey key)
        {
            CollectionName = nameMagazine;
            TypeEvent = information;
            PropertySourceOfChangeOfItemData = sourceChange;
            KeyElement = key;
        }

        public override string ToString()
        {
            return $"Название коллекции: {CollectionName}\n" +
                $"Чем вызвано событие: {TypeEvent}\n" +
                $"Названием свойства Magazine, являющееся источником изменения: {PropertySourceOfChangeOfItemData}\n" +
                $"Свойство с ключом {KeyElement}\n";
        }

    }
}
