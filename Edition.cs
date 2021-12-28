using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace just_try
{
    [Serializable]
    public class Edition: INotifyPropertyChanged
    {
        //происходит при изменении свойств 
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected string editionTitle;
        private DateTime dateEdition;
        private int editionCirculation;
        

        public string EditionTitle {
            get { return this.editionTitle; }
            set {
                if (value != this.editionTitle)
                {
                    this.editionTitle = value;
                    NotifyPropertyChanged(this.editionTitle);
                }
            }
        }

        public DateTime DateEdition {
            get { return this.dateEdition; }
            set {
                if (this.dateEdition != value)
                {
                    this.dateEdition = value;
                    NotifyPropertyChanged(this.dateEdition.ToString());
                }
            }
        }

        public int EditionCirculation {
            get { return this.editionCirculation; }
            set {
                try
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    if (this.editionCirculation != value)
                    {
                        this.editionCirculation = value;
                        NotifyPropertyChanged(editionTitle.ToString());
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Вызвано исключение при присваивании {value}: " + ex);
                    Console.WriteLine("Допустимое значение >= 0");
                }
            }
        }

       
        public Edition()
        {
            editionTitle = "empty edition";
            dateEdition = new DateTime(2020, 3, 3);
            editionCirculation = 1;
        }

        public Edition(string title, DateTime date, int circulation)
        {
            editionTitle = title;
            dateEdition = date;
            editionCirculation = circulation;

        }

        event System.ComponentModel.PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public virtual object DeepCopy()
        {
            return new Edition(this.EditionTitle, this.DateEdition,
                this.EditionCirculation);
        }

        public override string ToString()
        {
            return "Название издания: " + EditionTitle +
                "\tДата выхода издания: " + DateEdition.ToShortDateString() +
                "\tТираж издания: " + EditionCirculation + '\n';
        }

        public override bool Equals(object obj)
        {
            if (obj is Edition edit)
            {
                if (this.EditionTitle == edit.EditionTitle &&
                    this.DateEdition == edit.DateEdition &&
                    this.EditionCirculation == edit.EditionCirculation)
                {
                    return true;
                }

                return false;
            }
            return false;
        }

        public static bool operator==(Edition ed1, object ob2)
        {
            if (ob2 is Edition editTwo)
            {
                if (ed1.Equals(editTwo))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool operator !=(Edition ed1, object ob2)
        {
            if (ob2 is Edition editTwo)
            {
                if (!ed1.Equals(editTwo))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 31 * this.EditionTitle.GetHashCode() +
                31 * this.DateEdition.GetHashCode() +
                this.EditionCirculation.GetHashCode();
        }


    }

    
}

