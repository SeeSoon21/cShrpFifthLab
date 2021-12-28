using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


namespace just_try
{
    [Serializable]
    public class Article : IRateAndCopy, IComparable, IComparer<Article>
    {
        public Person Author_data { get; set; }
        public string Article_title { get; set; }
        public double Article_rating { get; set; }

        double IRateAndCopy.Rating
        {
            get
            {
                return Article_rating;
            }
        }

        public object DeepCopy()
        {
            return new Article(this.Author_data, this.Article_title, this.Article_rating);
        }

        public Article()
        {
            Author_data = new Person("Jack", "Tikeson", new DateTime(2001, 5, 17));
            Article_title = "Witcher";
            Article_rating = 10.0;
        }

        public Article(Person p1, string str1, double rate)
        {
            Author_data = p1;
            Article_title = str1;
            Article_rating = rate;
        }

        public Article(Article art_obj)
        {
            this.Author_data = art_obj.Author_data;
            this.Article_title = art_obj.Article_title;
            this.Article_rating = art_obj.Article_rating;
        }

        public override string ToString()
        {
            return Author_data.Name + " " + Author_data.Surname +
                " " + Author_data.data.ToShortDateString() + "; название статьи: " +
                Article_title + ", рейтинг: " + Article_rating + '\n';
        }

        //реализация IComparable для сравнения по названию статьи
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Article otherArticle = obj as Article;
            if (otherArticle != null)
                return this.Article_title.CompareTo(otherArticle.Article_title);
            else
                throw new ArgumentException("Объект не является Article");
        }

        //реализация IComparer<Article> для сравнения по фамилии
        public int Compare(Article artOne, Article artTwo)
        {
            return artOne.Author_data.Surname.CompareTo(artTwo.Author_data.Surname);
        }
    }

    //вспомогательный класс для сравнения Article по рейтингу статьи
    class ToCompareArticleRating : IComparer<Article>
    {
        public int Compare(Article artOne, Article artTwo)
        {
            if (artOne.Article_rating > artTwo.Article_rating)
                return 1;
            else if (artOne.Article_rating == artTwo.Article_rating)
                return 0;
            else
                return -1;
        }
    }

}
