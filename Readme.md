
# How to process Appointment Events and show confirmation dialogs in SchedulerControl

When an end-user adds, updates or deletes an appointment or series of appointments, SchedulerControl raises corresponding events: 

-   AppointmentAdding
-   AppointmentAdded
-   AppointmentsUpdating
-   AppointmentUpdated
-   AppointmentRemoving
-   AppointmentRemoved
-   EditAppointmentDialogShowing
-   RemoveAppointmentDialogShowing

You can handle these events to confirm a certain action or perform your custom logic. 

All these events are asynchronous. However, to show a confirmation dialog, you need to prevent SchedulerControl's internal logic from being executed until your dialog is closed. 

All the **\*ing** events from this list allow you to do this. Their EventArgs are the **DeferredCancelEventArgs** class descendants. This class provides the **GetDeferral** method. When you call this method, it returns an **EventDeferral** object and informs SchedulerControl that it should wait. 

After you complete your logic, call the **EventDeferral** object's **Complete** method to inform SchedulerControl that it can continue its operations: 

````cs
private async void  Scheduler_AppointmentAdding(object sender, AppointmentAddingEventArgs e) {
    var deferral = e.GetDeferral();
    var dialog = new MessageDialog("Do you want to add a new Appointment?", "Confirmation");
    dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
    dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
    var res = await dialog.ShowAsync();

    e.Cancel = (int)res.Id == 1;
    deferral.Complete();
}
````

This example illustrates how to show confirmation dialogs in such events. In the Log list you can also see when each of these events are raised in SchedulerControl.

**NOTE:**
When you get the **EventDeferral** object by calling the **GetDeferral** method, you need to call its **Complete** method after all your logic is completed. Otherwise, you can face unexpected side effects.  