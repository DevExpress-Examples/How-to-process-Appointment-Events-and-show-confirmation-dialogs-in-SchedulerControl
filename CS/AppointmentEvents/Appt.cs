using DevExpress.Mvvm;
using DevExpress.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentEvents {
    public class Appt : BindableBase {

        protected string _subject;
        public string Subject
        {
            get { return this._subject; }
            set { this.SetProperty(ref this._subject, value, "Subject"); }
        }


        protected DateTime _start;
        public DateTime Start
        {
            get { return this._start; }
            set { this.SetProperty(ref this._start, value, "Start"); }
        }


        protected DateTime _end;
        public DateTime End
        {
            get { return this._end; }
            set { this.SetProperty(ref this._end, value, "End"); }
        }

        protected object _patternId;
        public object PatternId {
            get { return this._patternId; }
            set { this.SetProperty(ref this._patternId, value); }
        }

        protected string _recurrenceRule;
        public string RecurrenceRule
        {
            get { return this._recurrenceRule; }
            set { this.SetProperty(ref this._recurrenceRule, value, "RecurrenceRule"); }
        }

        protected AppointmentType _type;
        public AppointmentType Type {
            get { return this._type; }
            set { this.SetProperty(ref this._type, value); }
        }
    }
}
