using org.rigpa.wylie;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertUI
{
    public class MainVM : INotifyPropertyChanged
    {
        private string wylie;
        private string tibetan;

        private Wylie converter;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Warnings { get; set; }

        public string Wylie
        {
            get { return wylie; }
            set
            {
                if (wylie == value) return;
                wylie = value;
                OnPropertyChanged("Wylie");
                var warns = new List<string>();
                Tibetan = converter.fromWylie(wylie, warns);
                Warnings.Clear();
                foreach (var warning in warns) Warnings.Add(warning);
            }
        }

        public string Tibetan
        {
            get { return tibetan; }
            set
            {
                if (tibetan == value) return;
                tibetan = value;
                OnPropertyChanged("Tibetan");
            }
        }

        public MainVM()
        {
            Warnings = new ObservableCollection<string>();
            try
            {
                converter = new Wylie();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
