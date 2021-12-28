using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace just_try //вариант 2 уровня
{
    [Serializable]
    public class Magazine : Edition, IRateAndCopy, INotifyPropertyChanged
    {
        

        private string mag_title; //название журнала
        private Frequency frequency_of_release; //периодичность
        private DateTime date_of_release; //дата выхода
        private int circulation; //тираж
        private List<Person> listEditors; //список редакторов журнала
        private List<Article> listArticle; //список статей

        delegate TKey KeySelector<TKey>(Magazine mg);

        //определение методов для сортировки по статье, фамилии автора и рейтингу
        public void SortByArticleTitle()
        {
            listArticle.Sort();
        }

        //для сортировки по фамилии автора
        public void SortByArticleAuthorSurname()
        {
            listArticle.Sort(new Article());
        }

        //для сортировки по рейтингу
        public void SortByArticleRating()
        {
            listArticle.Sort(new ToCompareArticleRating());
        }

        //реализация интерфейса
        double IRateAndCopy.Rating {
            get {
                return Average_rating;
            }
        }


        /////////////////////////////////////////5-я лаба(1-й вариант)///////////////////////////////////////
        //Magazine DeepCopy – полную копию объекта с исп. сериализации
        public override object DeepCopy()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter(); //сериализует объект в двоичном формате
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin); //SeekOrigin задает начало потока, 0 – смещение 
                return (Magazine)formatter.Deserialize(stream);
            } catch (Exception e)
            {
                Console.WriteLine($"Не удалось провести глубокое копирование: {e}");
                return null; 
            }
                
        }

        //сериализует все данные вызывающего объекта с именем filename
        public bool Save(string filename)
        {
            //если вылетит ошибка, using разберётся, но ошибку нужно обработать
            try {
                using (FileStream fs = new FileStream(filename, FileMode.Create)) {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, this);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }

        //десериализует данные из файла filename и исп. их для инициализации
        public bool Load(string filename)
        {
            Magazine mag = new Magazine();
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    //this = ((Magazine)formatter.Deserialize(fs));
                    mag = ((Magazine)formatter.Deserialize(fs));
                    this.Mag_title = mag.Mag_title;
                    this.Frequency_of_release = mag.Frequency_of_release;
                    this.Date_of_release = mag.Date_of_release;
                    this.Circulation = mag.Circulation;
                    this.ListArticle = mag.ListArticle;
                    this.ListEditors = mag.ListEditors;

                    fs.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"не удалось польностью выполнить десериализацию: {e}");
                return false;
            }

        }


        //те же яйца, только в профиль, как говорится(статические методы Save, Load)
        public static bool Save(string filename, Magazine sourceMag)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, sourceMag);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public static bool Load(string filename, Magazine destMag)
        {
            Magazine mag = new Magazine();
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    //this = ((Magazine)formatter.Deserialize(fs));
                    mag = ((Magazine)formatter.Deserialize(fs));
                    destMag.Mag_title = mag.Mag_title;
                    destMag.Frequency_of_release = mag.Frequency_of_release;
                    destMag.Date_of_release = mag.Date_of_release;
                    destMag.Circulation = mag.Circulation;
                    destMag.ListArticle = mag.ListArticle;
                    destMag.ListEditors = mag.ListEditors;

                    fs.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"не удалось польностью выполнить десериализацию: {e}");
                return false;
            }

        }


        //добавления нового элемента с консоли
        public bool AddFromConsole()
        {
            try
            {
                string title,
                    surname, name;

                Console.WriteLine("Введите название статьи, фамилию, имя, дату рождения(в формате 2021.12.31) и рейтинг через пробел");
                string stringFromConsole = Console.ReadLine();

                string[] dataObtained = stringFromConsole.Split(' ');

                if (dataObtained.Length != 5)
                {
                    Console.WriteLine("Введенное число полей не соответствует требуемому! Введите их снова! ");
                    throw new IfUserWasWrong();
                }

                //преобразуем string-дату в DateTime
                if (!DateTime.TryParse(dataObtained[3], out DateTime date))
                    throw new IfUserWasWrong(dataObtained[3]);

                if (!double.TryParse(dataObtained[4], out double rating))
                    throw new IfUserWasWrong(dataObtained[4]);

                title = dataObtained[0];
                surname = dataObtained[1];
                name = dataObtained[2];

                Article temp = new Article(new Person(name, surname, date), title, rating);
                this.AddArticles(temp);

                return true;

            } catch(IfUserWasWrong e)
            {
                Console.WriteLine($"Ошибка ввода! {e}");
                return false;
            }
        }
   
        //собственные исключения для функции AddFromConsole
        public class IfUserWasWrong: Exception
        {
            public IfUserWasWrong() { }
            public IfUserWasWrong(string message) { Console.WriteLine($"Неверное введена строка: {message}"); }
            public IfUserWasWrong(int number) { Console.WriteLine($"Неверно введено int число: {number}"); }
            public IfUserWasWrong(double number) { Console.WriteLine($"Неверно введено double число: {number}"); }
        }

        
        ////////////////////////////////////////////////////////////////////////////////////////



        object IRateAndCopy.DeepCopy() 
        {
            Magazine copyMag = new Magazine(this.Mag_title, this.Frequency_of_release, 
                this.Date_of_release, this.Circulation);

            copyMag.ListArticle = new List<Article>(this.listArticle);
            copyMag.ListEditors = new List<Person>(this.ListEditors);

            return copyMag;
        }

        //конструкторы
        public Magazine()
        {
            mag_title = "Quantum";
            frequency_of_release = Frequency.Monthly;
            date_of_release = new DateTime(2021, 10, 3);
            circulation = 100;
        }

        public Magazine(string mg_tit, Frequency freq,
            DateTime date1, int edition)
        {
            mag_title = mg_tit;
            frequency_of_release = freq;
            date_of_release = date1;
            circulation = edition;
        }


        //свойства
        public string Mag_title
        {
            get
            {
                return mag_title;
            }

            set
            {
                mag_title = value;
                
            }
        }

        public Frequency Frequency_of_release
        {
            get
            {
                return frequency_of_release;
            }

            set
            {
                frequency_of_release = value;
            }
        }

        public DateTime Date_of_release
        {
            get
            {
                return date_of_release;
            }

            set
            {
                date_of_release = value;
            }
        }

        public int Circulation
        {
            get
            {
                return circulation;
            }

            set
            {
                circulation = value;
            }
        }

        public List<Article> ListArticle
        {
            get
            {
                return this.listArticle;
            }

            set
            {
                this.listArticle = value;
            }
        }

        public List<Person> ListEditors
        {
            get
            {
                return this.listEditors;
            }

            set
            {
                this.listEditors = value;
            }
        }

        //свойства, индексаторы, методы, перегруженные функции
        public double Average_rating
        {
            get
            {
                Article[] mas_art;
                if (listArticle != null)
                {
                    mas_art = new Article[listArticle.Count];
                    for (int i = 0; i < listArticle.Count; i++)
                    {
                        mas_art[i] = new Article();
                    }
                    listArticle.CopyTo(mas_art);
                }
                else
                {
                    return 0;
                }
                double aver_rate = 0;


                for (int i = 0; i < listArticle.Count; i++)
                {
                    aver_rate = aver_rate + mas_art[i].Article_rating;
                }
                return aver_rate / listArticle.Count;
            }
        }


        public bool this[Frequency time] //индексатор совпадения по индексу
        {
            get
            {
                return (time == frequency_of_release);
            }
        }

        

        public void AddArticles(params Article[] mas_art)
        {
            if (listArticle == null)
                listArticle = new List<Article>();

            Article[] temp_mas = new Article[mas_art.Length];
            for (int i = 0; i < mas_art.Length; i++)
            {
                temp_mas[i] = new Article();
            }
            mas_art.CopyTo(temp_mas, 0);

            for (int i = 0; i < temp_mas.Length; i++)
            {
                listArticle.Add(temp_mas[i]);
            }

        }

        public void AddEditors(params Person[] mas_pers)
        {
            if (listEditors == null)
                listEditors = new List<Person>();

            Person[] temp_mas = new Person[mas_pers.Length];
            for (int i = 0; i < mas_pers.Length; i++)
            {
                temp_mas[i] = new Person();
            }
            mas_pers.CopyTo(temp_mas, 0);

            for (int i = 0; i < temp_mas.Length; i++)
            {
                listEditors.Add(temp_mas[i]);
            }

        }



        public override string ToString() //значение всех полей класса + список статей
        {
            
            string very_big_string_arts = "";
            foreach(object o in listArticle)
            {
                very_big_string_arts += o.ToString();
            }

            string string_editors = "";
            foreach(object o in listEditors)
            {
                string_editors += o.ToString();
            }

            return "\n\n\tНазвание журнала: " + mag_title + "\n\tПериодичность: " + frequency_of_release
                + "\n\tДата выхода: " + date_of_release.ToShortDateString() +
                "\n\tТираж: " + circulation + "\n\tСписок статей с их авторами: \n"
                + very_big_string_arts + "\n\tСписок редакторов журнала: \n" + string_editors;
            
            
        }

        public virtual string ToShortString()
        {
            return mag_title + "\t" + frequency_of_release
                + "\t" + date_of_release.ToShortDateString()
                + "\nТираж:" + circulation + ",\t"
                + "Рейтинг:" + Average_rating
                + "\nЧисло редакторов издания: " + ListEditors.Count
                + "\nЧисло статей: " + ListArticle.Count;
        }

        //получение списка статей с рейтингом больше double с помощью форыча
        public IEnumerable GetHighRate(double rate_value)
        {
            Article[] articleRating = new Article[listArticle.Count];
            listArticle.CopyTo(articleRating);

            for (int i = 0; i < articleRating.Length; i++)
            {
                if(articleRating[i].Article_rating > rate_value)
                {
                    Console.Write($"\nСтатья: {articleRating[i].Article_title}, рейтинг: ");
                    yield return articleRating[i].Article_rating;
                }
            }  
        }

        //вывод список статей по заданной строке
        public IEnumerable GetListArticlesByString(String givenStr)
        {
            Article[] articleAllList = new Article[listArticle.Count];
            listArticle.CopyTo(articleAllList);
            
            for (int i = 0; i < articleAllList.Length; i++)
            {
                if (articleAllList[i].Article_title.Contains(givenStr))
                {
                    Console.Write($"\n{i}-я статья, совпадающая по названию: ");
                    yield return articleAllList[i].Article_title;
                }
            }
        }

        


        public IEnumerator GetEnumerator()
        {
            return new MagazineEnumerator(this);
        }

        private class MagazineEnumerator : IEnumerator //вспомогательный класс
        {
            private Magazine mag_obj;
            private Article[] appropriateAuthors; //подходящие авторы
            private int position = -1;

            public MagazineEnumerator(Magazine mag_obj)
            {
                this.mag_obj = mag_obj;
                appropriateAuthors = new Article[mag_obj.listArticle.Count];
                mag_obj.listArticle.CopyTo(appropriateAuthors);
            }

            public bool EqualsEditor(Person authorCheck)
            {
                //прохоидтся по всем элементам listEditors и в случае неудачи возвращает false
                for (int i = 0; i < mag_obj.listEditors.Count; i++) {
                    if (authorCheck.Equals(mag_obj.listEditors[i]))//не входят...
                        return false;
                }
                return true;
            }


            public object Current
            {
                get
                {
                    if (position == -1 || position >= mag_obj.listArticle.Count)
                        throw new InvalidOperationException();
                    return mag_obj.listArticle[position];
                }
            }

            public bool MoveNext() //в форЫче сперва работает эта функция только потом вызывается current
            {
                //инкрементируем, пока не...
                while (++position < mag_obj.listArticle.Count && !EqualsEditor(appropriateAuthors[position].Author_data)) ;
                //возвращаем, в случае если позишн не достиг длины коллекции
                return position < mag_obj.listArticle.Count;
            }

            public void Reset()
            {
                position = -1;
            }

        }

        //определить итератор для перебора статей авторы которых являются редакторами

        public IEnumerable IsAuthorsEqualsEditor()
        {
            Article[] masForNamesAuthor = new Article[listArticle.Count];
            listArticle.CopyTo(masForNamesAuthor);

            for (int i = 0; i < listArticle.Count; i++)
            {
                if (masForNamesAuthor[i].Author_data.Equals(listEditors[i]))
                {
                    //Console.Write($"Статья, автор которой является редактором: ");
                    yield return masForNamesAuthor[i];
                }

            }
            
        }

        //определить итератор для перебора редакторов, у которых нет статей в журнале
        public IEnumerable BrootRedactor()
        {
            Article[] articleAllList = new Article[listArticle.Count];
            listArticle.CopyTo(articleAllList);

            for (int i = 0; i < articleAllList.Length; i++)
            {
                if (!articleAllList[i].Author_data.Equals(this.ListEditors[i]))
                {
                    //Console.Write("Редактор, не имющий статей в журнале: ");
                    yield return listEditors[i];
                }
                
            }
        }

       
    }


}

