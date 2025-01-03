using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentDateValidator.UI
{
    public class PaymentDateCompliancesModel : INotifyPropertyChanged
    {
        public string MonthLabel { get; set; }

        public DateTime? PaymentDate
        {
            get { return paymentDate; }
            set
            {
                if (value == paymentDate) return;
                paymentDate = value;
                RaisePropertyChanged("PaymentDate");
                UpdateCompliance();
            }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                if (value == dueDate) return;
                dueDate = value;
                RaisePropertyChanged("DueDate");
                UpdateCompliance();
            }
        }

        public int? Compliance
        {
            get { return compliance;}
            set
            {
                if (value == compliance) return;
                compliance = value;
                RaisePropertyChanged("Compliance");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime? paymentDate;

        private DateTime dueDate;

        private int? compliance;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateCompliance()
        {
            if (paymentDate == null) return;

            Compliance = ((DateTime)paymentDate - dueDate).Days;
        }
    }
}
