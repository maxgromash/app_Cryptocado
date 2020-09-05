using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace App5.Models
{
    [DataContract]
    public class User : INotifyPropertyChanged
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Avatar { get; set; }

        double sumBalance;
        [DataMember]
        public double SumBalance
        {
            get
            {
                return sumBalance;
            }
            set
            {
                sumBalance = value;
                OnPropertyChanged("SumBalance");
            }
        }



        double balance;
        [DataMember]
        public double Balance
        {
            get
            {
                return balance;
            }
            set
            {
                if (value != balance)
                {
                    balance = value;
                    OnPropertyChanged("Balance");
                }
            }
        }
        [DataMember]
        public double[] Amounts { get; set; }
        [DataMember]
        public ObservableCollection<Operation> Operations { get; set; }
        [DataMember]
        public int CurrentTask { get; set; }


        public User(double balan)
        {
            Balance = balan;
            Avatar = "testavatar.png";
            Amounts = new double[6];
            Name = "";
            Operations = new ObservableCollection<Operation>();
            CurrentTask = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}