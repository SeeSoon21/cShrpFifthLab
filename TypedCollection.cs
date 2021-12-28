using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace just_try
{
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
    public class TypedCollection//: INotifyPropertyChanged
    {
        //типизированная коллекция, событиями говорящая, что она изменилась
        //набор ссылок в коллекции изменяется при удалении/добавлении элементов
        //класс сообщает об изменениях с помощью делегат-события
        //при вывозе get элементы изменяются 
        ////элементы сообщают об измениях с помощью PropertyChange
        //нужно подписаться на событие PropertyChanged

        
    }
}
