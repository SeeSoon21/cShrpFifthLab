using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace just_try
{
    public enum Frequency { Weekly, Monthly, Yearly };
    [Serializable]
    public class Person
    {
        public string name;
        public string surname;
        public DateTime data;

        public Person()
        {
            name = "Kivi";
            surname = "green";
            data = new DateTime(2001, 12, 6);
        }

        public Person(string nm, string srn, DateTime nums)
        {
            name = nm;
            surname = srn;
            data = nums;
        }

        public Person(Person per)
        {
            this.name = per.Name;
            this.surname = per.Surname;
            this.data = Data;
        }

        //свойства (объектов класса)
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
            name = value;
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }

            set
            {
                surname = value;
            }
        }

        public DateTime Data
        {
            get
            {
                return data;
            }

            set
            {
                //Console.Write("Введите дату: ");
                data = value;
            }
        }

        public int Year
        {
            get
            {
                return data.Year;
            }

            set
            {
                data = new DateTime(value, data.Month, data.Day );
            }
        }

        //перегруженная версия вирт. метода для формирования строки со значениями всех полей класса
        public override string ToString()
        {
            return name + " " + surname + " " + data.ToShortDateString() + '\n';
        }

        public virtual string ToShortString()
        {
            return name + " " + surname;
        }

        //переопределение Equals 
        public override bool Equals(object obj)
        {
            if (obj is Person pers_temp)
            {
                if (this.Name == pers_temp.Name
                    && this.Surname == pers_temp.Surname
                    && this.Data == pers_temp.Data)
                {
                    return true;
                }
                    
                else
                {
                    return false;
                }
            }
            return false;
        }

        //переопределение операций == и !=
        public static bool operator ==(Person p1, Person p2)
        {
            if (p1.Equals(p2))
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Person p1, Person p2)
        {
            if (!p1.Equals(p2))
            {
                return true;
            }

            return false;
        }

        //переопределение GetHashCode()
        public override int GetHashCode()
        {
            int hashcode = name.GetHashCode();
            hashcode = 31 * hashcode + surname.GetHashCode();
            hashcode = 31 * hashcode + data.GetHashCode();

            return hashcode;
        }

        //создание копии DeepCopy
        public object DeepCopy()
        {
            Person other = (Person)this.MemberwiseClone();
            other.Name = this.Name;
            other.Surname = this.Surname;
            other.Data = this.Data;
            return other;
        }

        
        
        

    }
}
