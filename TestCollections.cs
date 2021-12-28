using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace just_try
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    class TestCollection<TKey, TValue>
    {
        //делегат будет ссылаться на метод(статический?), который инициализирует KeyValuePair
        //ключом-значением. Этот метод передается в конструктор(через параметры
        //int–число элементов в коллекции и GenerateElement–метод, который используется
        //для автоматической генерации пары ключ-значение в виде объекта KeyValuePair
        //число элементов в коллекция вводит пользователь(выброс исключений)
        //измерение времени

        List<TKey> listOfTkey;
        List<string> listOfString;
        Dictionary<TKey, TValue> dictionaryOfTKey;
        Dictionary<string, TValue> dictionaryOfString;
        GenerateElement<TKey, TValue> myGenerateElement;

        public TestCollection(int number, GenerateElement<TKey, TValue> generateElement)
        {
            myGenerateElement = generateElement;

            listOfTkey = new List<TKey>();
            listOfString = new List<string>();
            dictionaryOfTKey = new Dictionary<TKey, TValue>();
            dictionaryOfString = new Dictionary<string, TValue>();
            for (int i = 0; i < number; i++)
            { 
                KeyValuePair<TKey, TValue> keyValue = generateElement(i);

                listOfTkey.Add(keyValue.Key);
                listOfString.Add(keyValue.Key.ToString());
                dictionaryOfTKey.Add(keyValue.Key, keyValue.Value);
                dictionaryOfString.Add(keyValue.Key.ToString(), keyValue.Value);
            }
        }


        public void ToMeasureTimeSearchInTKeyList()
        {
            TKey first = listOfTkey[0];
            TKey middle = listOfTkey[(listOfTkey.Count / 2)];
            TKey last = listOfTkey[listOfTkey.Count - 1];
            TKey noneTKey = myGenerateElement(listOfTkey.Count).Key;

            Console.WriteLine("---------------listOfTKey---------------\n");

            Stopwatch watch = Stopwatch.StartNew();
            listOfTkey.Contains(first);
            watch.Stop();
            Console.WriteLine($"Время поиска первого элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            listOfTkey.Contains(middle);
            watch.Stop();
            Console.WriteLine($"Время поиска центрального элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            listOfTkey.Contains(last);
            watch.Stop();
            Console.WriteLine($"Время поиска последнего элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            listOfTkey.Contains(noneTKey);
            watch.Stop();
            Console.WriteLine($"Время поиска не входящего в коллекцию элемента: {watch.Elapsed.Ticks}\n");
        }

        public void toMeasureTimeSearchInStringList()
        {
            string first = listOfString[0];
            string middle = listOfString[listOfString.Count / 2];
            string last = listOfString[listOfString.Count - 1];
            string none = myGenerateElement(listOfString.Count).Key.ToString();

            Console.WriteLine("---------------listOfString---------------\n");

            Stopwatch watch = Stopwatch.StartNew();
            listOfString.Contains(first);
            watch.Stop();
            Console.WriteLine($"Время поиска первого элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            listOfString.Contains(middle);
            watch.Stop();
            Console.WriteLine($"Время поиска центрального элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            listOfString.Contains(last);
            watch.Stop();
            Console.WriteLine($"Время поиска последнего элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            listOfString.Contains(none);
            watch.Stop();
            Console.WriteLine($"Время поиска не входящего в коллекцию элемента: {watch.Elapsed.Ticks}\n");
        }

        public void toMeasureTimeSearchInDictionaryTKey()
        {
            TKey firstTKey = dictionaryOfTKey.ElementAt(0).Key;
            TKey middleTKey = dictionaryOfTKey.ElementAt(dictionaryOfTKey.Count / 2).Key;
            TKey lastTKey = dictionaryOfTKey.ElementAt(dictionaryOfTKey.Count - 1).Key;
            TKey noneTKey = dictionaryOfTKey.ElementAt(dictionaryOfTKey.Count- 1).Key;

            Console.WriteLine("---------------dictionaryOfKey---------------\n");

            Stopwatch watch = Stopwatch.StartNew();
            dictionaryOfTKey.ContainsKey(firstTKey);
            watch.Stop();
            Console.WriteLine($"Время поиска первого элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            dictionaryOfTKey.ContainsKey(middleTKey);
            watch.Stop();
            Console.WriteLine($"Время поиска центрального элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            dictionaryOfTKey.ContainsKey(lastTKey);
            watch.Stop();
            Console.WriteLine($"Время поиска последнего элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            dictionaryOfTKey.ContainsKey(noneTKey);
            watch.Stop();
            Console.WriteLine($"Время поиска не входящего в коллекцию элемента: {watch.Elapsed.Ticks}\n");
        }

        public void toMeasureTimeSearchInDictionaryString()
        {
            string first = dictionaryOfString.ElementAt(0).Key.ToString();
            string middle = dictionaryOfString.ElementAt(dictionaryOfTKey.Count / 2).Key.ToString();
            string last = dictionaryOfString.ElementAt(dictionaryOfTKey.Count - 1).Key.ToString();
            string none = dictionaryOfString.ElementAt(dictionaryOfTKey.Count).Key.ToString();

            Console.WriteLine("---------------dictionaryOfString---------------\n");

            Stopwatch watch = Stopwatch.StartNew();
            dictionaryOfString.ContainsKey(first);
            watch.Stop();
            Console.WriteLine($"Время поиска первого элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            dictionaryOfString.ContainsKey(middle);
            watch.Stop();
            Console.WriteLine($"Время поиска центрального элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            dictionaryOfString.ContainsKey(last);
            watch.Stop();
            Console.WriteLine($"Время поиска последнего элемента: {watch.Elapsed.Ticks}\n");

            watch.Restart();
            dictionaryOfString.ContainsKey(none);
            watch.Stop();
            Console.WriteLine($"Время поиска не входящего в коллекцию элемента: {watch.Elapsed.Ticks}\n");
        }

    }
}
