using System.ComponentModel;
using System.Runtime.Serialization;

namespace App5.Models
{
    [DataContract]
    public class Coin : INotifyPropertyChanged
    {
        public Coin(string logo, string name, int index)
        {
            Logo = logo;
            Name = name;
            Index = index;
        }
        [DataMember]
        public int Index { get; set; }
        [DataMember]
        public string Logo { get; set; }
        [DataMember]
        public string Name { get; set; }
        double min24;
        [DataMember]
        public double Min24 
        { 
            get
            {
                return min24;
            }
            set
            {
                min24 = value;
                OnPropertyChanged("Min24");
            }
        }
        double max24;
        [DataMember]
        public double Max24 
        { 
            get

            {
                return max24;
            }
            set
            {
                max24 = value;
                OnPropertyChanged("Max24");
            }
        }
        double lastprice;
        [DataMember]
        public double Lastprice 
        { 
            get
            {
                return lastprice;
            }
            set
            {
                lastprice = value;
                OnPropertyChanged("Lastprice");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}