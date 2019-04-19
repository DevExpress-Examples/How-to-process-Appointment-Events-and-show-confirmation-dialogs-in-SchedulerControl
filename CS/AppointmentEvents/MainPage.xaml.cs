using DevExpress.Core.Native.Events;
using DevExpress.UI.Xaml.Scheduler;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace AppointmentEvents {
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }


        protected ObservableCollection<Appt> _items;
        public ObservableCollection<Appt> Items
        {
            get
            {
                if(this._items == null)
                {
                    this._items = new ObservableCollection<Appt>();
                    this._items.Add(new Appt() { Start = DateTime.Today, End = DateTime.Today.AddHours(2), Subject = "Appointment1" });
                    this._items.Add(new Appt() { Start = DateTime.Today.AddHours(4), End = DateTime.Today.AddHours(6), Subject = "Appointment2" });
                    this._items.Add(new Appt() { Start = DateTime.Today.AddHours(7), End = DateTime.Today.AddHours(9), Subject = "Recurrent Appt",
                        PatternId = "Pattern1", Type = AppointmentType.Pattern, RecurrenceRule = RecurrenceRule.New().Daily()
                    });
                };

                return this._items;
            }
        }


        protected ObservableCollection<string> _logItems;
        public ObservableCollection<string> LogItems
        {
            get
            {
                if(this._logItems == null)
                {
                    this._logItems = new ObservableCollection<string>();
                }

                return this._logItems;
            }
        }

        private void SchedulerControl_AppointmentAdding(object sender, DevExpress.UI.Xaml.Scheduler.AppointmentAddingEventArgs e) {
            Log("AppointmentAdding");
            if(chkAdding.IsChecked.Value)
            {
                Confirm(e, "Do you want to proceed adding a new appointment?");
            }
        }

        private void SchedulerControl_AppointmentAdded(object sender, DevExpress.UI.Xaml.Scheduler.AppointmentAddedEventArgs e) {
            Log("AppointmentAdded");
        }

        private void SchedulerControl_AppointmentUpdating(object sender, DevExpress.UI.Xaml.Scheduler.AppointmentUpdatingEventArgs e) {
            Log("AppointmentUpdating");
            if(chkUpdating.IsChecked.Value)
            {
                Confirm(e, "Do you want to save changes?");
            }
        }

        private void SchedulerControl_AppointmentUpdated(object sender, DevExpress.UI.Xaml.Scheduler.AppointmentUpdatedEventArgs e) {
            Log("AppointmentUpdated");
        }

        private void SchedulerControl_AppointmentRemoving(object sender, DevExpress.UI.Xaml.Scheduler.AppointmentRemovingEventArgs e) {
            Log("AppointmentRemoving");
            if(chkRemoving.IsChecked.Value)
            {
                Confirm(e, "Do you want to delete this appointment?");
            }
        }

        private void SchedulerControl_AppointmentRemoved(object sender, DevExpress.UI.Xaml.Scheduler.AppointmentRemovedEventArgs e) {
            Log("AppointmentRemoved");
        }

        private void SchedulerControl_RemoveAppointmentDialogShowing(object sender, DevExpress.UI.Xaml.Scheduler.RemoveAppointmentDialogShowingEventArgs e) {
            Log("RemoveAppointmentDialogShowing");
            if(chkEditDialogShowing.IsChecked.Value)
            {
                Confirm(e, "Do you want to show RemoveAppointmentDialog?");
            }
        }

        private void SchedulerControl_EditAppointmentDialogShowing(object sender, DevExpress.UI.Xaml.Scheduler.EditAppointmentDialogShowingEventArgs e) {
            Log("EditAppointmentDialogShowing");
            if(chkRemoveDialogShowing.IsChecked.Value)
            {
                Confirm(e, "Do you want to open EditAppointmentDialog?");
            }
        }

        async void Confirm(DeferredCancelEventArgs args, string message) {
            var deferral = args.GetDeferral();
            var dialog = new MessageDialog(message, "Confirmation");
            dialog.Commands.Add(new UICommand() { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand() { Label = "No", Id = 1 });
            var result = await dialog.ShowAsync();
            args.Cancel = (int) result.Id == 1;
            deferral.Complete();
        }

        void Log(string message) {
            this.LogItems.Insert(0, DateTime.Now.ToString("HH:mm:ss") + " >>> " + message);
        }
    }
}
