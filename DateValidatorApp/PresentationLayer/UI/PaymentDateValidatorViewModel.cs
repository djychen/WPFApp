using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentDateValidator.UI
{
    public class PaymentDateValidatorViewModel : INotifyPropertyChanged
    {
        public PaymentDateValidatorViewModel()
        {
            for (var i = 0; i < 24; i++)
            {
                var year = i / 12 - 1;
                var month = i % 12 + 1;
                var model = new PaymentDateCompliancesModel
                {
                    MonthLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                    DueDate = new DateTime(DateTime.Now.Year + year, month, 1)
                };
                paymentDateCompliances.Add(model);
            }

            commonDueDate = 1;
        }

        public ObservableCollection<PaymentDateCompliancesModel> PaymentDateCompliances => paymentDateCompliances;

        public int CommonDueDate
        {
            get { return commonDueDate; }

            set
            {
                if (value == commonDueDate) return;
                commonDueDate = value;
                RaisePropertyChanged(("CommonDueDate"));
                UpdateAllDueDate(value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ObservableCollection<PaymentDateCompliancesModel> paymentDateCompliances = new ObservableCollection<PaymentDateCompliancesModel>();

        private int commonDueDate;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateAllDueDate(int monthlyDueDate)
        {
            foreach (var paymentDateCompliance in PaymentDateCompliances)
            {
                if (monthlyDueDate < 1 || monthlyDueDate > 31) return;

                var year = paymentDateCompliance.DueDate.Year;
                var month = paymentDateCompliance.DueDate.Month;
                var day = paymentDateCompliance.DueDate.Day;
                var daysInMonth = DateTime.DaysInMonth(year, month);
                day = (monthlyDueDate > daysInMonth) ? daysInMonth : monthlyDueDate;
                paymentDateCompliance.DueDate = new DateTime(year, month, day);
            }
        }
    }
}
