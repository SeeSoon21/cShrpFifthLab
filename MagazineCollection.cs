using System;
using System.Collections.Generic;
using System.Linq;


namespace just_try
{
    delegate TKey KeySelector<TKey>(Magazine mg);
    delegate void MagazineChangedHandler<TKey>(object source, MagazinesChangedEventArgs<TKey> args);

    class MagazineCollection<TKey> //TKey–определяет тип ключа в коллекции
    {
        private Dictionary<TKey, Magazine> dictionaryMagazine = new Dictionary<TKey, Magazine>(); //коллекция ключ-magazine
        private KeySelector<TKey> myKeySelector; //экземпляр делегата(то есть, это уже указатель(метод))

        #region event
        public event MagazineChangedHandler<TKey> MagazinesChanged;

        //копируем ссылку во временнную веременную, чтобы какой-нибудь из потоков
        //не удалил делегат из цепочки, присвоив MagazinesChangedEventArgs значение null
        protected virtual void MagazineWasChanged(Update up, string prop, TKey key)
        {
            MagazineChangedHandler<TKey> handler = MagazinesChanged;
            handler?.Invoke(this, new MagazinesChangedEventArgs<TKey>(this.CollectionName, up, prop, key));
        }
        #endregion

        //конструктор(параметр–указатель на функцию, высчитывающую ключ)
        public MagazineCollection(KeySelector<TKey> magazineKey)
        {
            this.myKeySelector = magazineKey;
            //dictionaryMagazine = new Dictionary<TKey, Magazine>();
        }

        public string getKey
        {
            get
            {
                string bigstr = "";
                foreach (var temp in dictionaryMagazine)
                {
                    bigstr += temp.Key.ToString() + '\n';
                }
                return bigstr;
            }
        }

        #region свойства
        public double getMaxAverageRating
        {
            get
            {
                if (dictionaryMagazine.Values.Count > 0)
                    return dictionaryMagazine.Values.Max(m => m.Average_rating);
                return -1;
            }
        }

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> groupingDependsFrequency
        {
            get
            {
                return dictionaryMagazine.GroupBy(m => m.Value.Frequency_of_release);
            }
        }

        public string CollectionName { get; set; }

        #endregion свойства

        #region методы
        //"при подписке на событие PropertyChanged как обработчик события надо использовать метод класса с коллекцией"
        //метод для добавления некоторого числа элементов Magazine для инициализации коллекции по умолчанию
        public void AddDefaults()
        {
            Magazine tempMagazine = new Magazine();
            TKey key = myKeySelector(tempMagazine);
            dictionaryMagazine.Add(key, tempMagazine);

            MagazineWasChanged(Update.Add, "", key);
            tempMagazine.PropertyChanged += PropertyChangedEvent;
            
        }
        
        //"события PropertyChanged, происх в элементах, преобразуются в события класса
        private void PropertyChangedEvent(object sender, EventArgs e)
        {
            Magazine magTemp = (Magazine)sender;
            TKey key = myKeySelector(magTemp);
            MagazineWasChanged(Update.Property, magTemp.Mag_title, key);
        }

        //добавление элементов в коллекцию
        public void AddMagazines(params Magazine[] arrayMagazine)
        {
            TKey key;
            for (int i = 0; i < arrayMagazine.Length; i++)
            {
                key = myKeySelector(arrayMagazine[i]);
                if (key != null)
                {
                    dictionaryMagazine.Add(myKeySelector(arrayMagazine[i]), arrayMagazine[i]);
                    MagazineWasChanged(Update.Add, "", key);

                    arrayMagazine[i].PropertyChanged += PropertyChangedEvent;
                }
                else Console.WriteLine("error!");
            }
        }

        //замена в словаре <Tkey, Magazine> элемента со значением old на элемент со значением mnew
        public bool Replace(Magazine mold, Magazine mnew)
        {
            TKey key;
            foreach (KeyValuePair<TKey, Magazine> keyValue in dictionaryMagazine)
            {
                if (keyValue.Value == mold)
                {
                    key = keyValue.Key;
                    dictionaryMagazine[keyValue.Key] = mnew;
                    MagazineWasChanged(Update.Replace, dictionaryMagazine[keyValue.Key].Mag_title, key);

                    mold.PropertyChanged -= PropertyChangedEvent;
                    mnew.PropertyChanged += PropertyChangedEvent;

                    return true;
                } 
            }
            return false;
        }

        

        public override string ToString()
        {
            string small_string = "";
            foreach(KeyValuePair<TKey, Magazine> keyValue in dictionaryMagazine)
            {
                small_string += dictionaryMagazine[keyValue.Key].ToString();
                small_string += "\n\n";
            }

            return small_string;
        }

        public virtual string ToShortString()
        {
            string small_string = "";
            foreach(KeyValuePair<TKey, Magazine> keyValue in dictionaryMagazine)
            {
                small_string += dictionaryMagazine[keyValue.Key].ToShortString();
                small_string += "\n\n";
            }

            return small_string;
        }

        //возвращает подмножество коллекции с заданной периодичность выхода журнала
        public IEnumerable<KeyValuePair<TKey, Magazine>>FrequencyGroup(Frequency value)
        {
            return dictionaryMagazine.Where( m => m.Value.Frequency_of_release == value);
        }
        #endregion
    }
}
