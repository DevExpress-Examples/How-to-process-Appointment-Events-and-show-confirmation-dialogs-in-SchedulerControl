Imports DevExpress.Core.Native.Events
Imports DevExpress.UI.Xaml.Scheduler
Imports System
Imports System.Collections.ObjectModel
Imports Windows.UI.Popups
Imports Windows.UI.Xaml.Controls

Namespace AppointmentEvents
	Public NotInheritable Partial Class MainPage
		Inherits Page

		Public Sub New()
			Me.InitializeComponent()
			Me.DataContext = Me
		End Sub


		Protected _items As ObservableCollection(Of Appt)
		Public ReadOnly Property Items() As ObservableCollection(Of Appt)
			Get
				If Me._items Is Nothing Then
					Me._items = New ObservableCollection(Of Appt)()
					Me._items.Add(New Appt() With {
						.Start = DateTime.Today,
						.End = DateTime.Today.AddHours(2),
						.Subject = "Appointment1"
					})
					Me._items.Add(New Appt() With {
						.Start = DateTime.Today.AddHours(4),
						.End = DateTime.Today.AddHours(6),
						.Subject = "Appointment2"
					})
					Me._items.Add(New Appt() With {
						.Start = DateTime.Today.AddHours(7),
						.End = DateTime.Today.AddHours(9),
						.Subject = "Recurrent Appt",
						.PatternId = "Pattern1",
						.Type = AppointmentType.Pattern,
						.RecurrenceRule = RecurrenceRule.[New]().Daily()
					})
				End If

				Return Me._items
			End Get
		End Property


		Protected _logItems As ObservableCollection(Of String)
		Public ReadOnly Property LogItems() As ObservableCollection(Of String)
			Get
				If Me._logItems Is Nothing Then
					Me._logItems = New ObservableCollection(Of String)()
				End If

				Return Me._logItems
			End Get
		End Property

		Private Sub SchedulerControl_AppointmentAdding(ByVal sender As Object, ByVal e As DevExpress.UI.Xaml.Scheduler.AppointmentAddingEventArgs)
			Log("AppointmentAdding")
			If chkAdding.IsChecked.Value Then
				Confirm(e, "Do you want to proceed adding a new appointment?")
			End If
		End Sub

		Private Sub SchedulerControl_AppointmentAdded(ByVal sender As Object, ByVal e As DevExpress.UI.Xaml.Scheduler.AppointmentAddedEventArgs)
			Log("AppointmentAdded")
		End Sub

		Private Sub SchedulerControl_AppointmentUpdating(ByVal sender As Object, ByVal e As DevExpress.UI.Xaml.Scheduler.AppointmentUpdatingEventArgs)
			Log("AppointmentUpdating")
			If chkUpdating.IsChecked.Value Then
				Confirm(e, "Do you want to save changes?")
			End If
		End Sub

		Private Sub SchedulerControl_AppointmentUpdated(ByVal sender As Object, ByVal e As DevExpress.UI.Xaml.Scheduler.AppointmentUpdatedEventArgs)
			Log("AppointmentUpdated")
		End Sub

		Private Sub SchedulerControl_AppointmentRemoving(ByVal sender As Object, ByVal e As DevExpress.UI.Xaml.Scheduler.AppointmentRemovingEventArgs)
			Log("AppointmentRemoving")
			If chkRemoving.IsChecked.Value Then
				Confirm(e, "Do you want to delete this appointment?")
			End If
		End Sub

		Private Sub SchedulerControl_AppointmentRemoved(ByVal sender As Object, ByVal e As DevExpress.UI.Xaml.Scheduler.AppointmentRemovedEventArgs)
			Log("AppointmentRemoved")
		End Sub

		Private Sub SchedulerControl_RemoveAppointmentDialogShowing(ByVal sender As Object, ByVal e As DevExpress.UI.Xaml.Scheduler.RemoveAppointmentDialogShowingEventArgs)
			Log("RemoveAppointmentDialogShowing")
			If chkEditDialogShowing.IsChecked.Value Then
				Confirm(e, "Do you want to show RemoveAppointmentDialog?")
			End If
		End Sub

		Private Sub SchedulerControl_EditAppointmentDialogShowing(ByVal sender As Object, ByVal e As DevExpress.UI.Xaml.Scheduler.EditAppointmentDialogShowingEventArgs)
			Log("EditAppointmentDialogShowing")
			If chkRemoveDialogShowing.IsChecked.Value Then
				Confirm(e, "Do you want to open EditAppointmentDialog?")
			End If
		End Sub

		Private Async Sub Confirm(ByVal args As DeferredCancelEventArgs, ByVal message As String)
			Dim deferral = args.GetDeferral()
			Dim dialog = New MessageDialog(message, "Confirmation")
			dialog.Commands.Add(New UICommand() With {
				.Label = "Yes",
				.Id = 0
			})
			dialog.Commands.Add(New UICommand() With {
				.Label = "No",
				.Id = 1
			})
			Dim result = Await dialog.ShowAsync()
			args.Cancel = CInt(Math.Truncate(result.Id)) = 1
			deferral.Complete()
		End Sub

		Private Sub Log(ByVal message As String)
			Me.LogItems.Insert(0, DateTime.Now.ToString("HH:mm:ss") & " >>> " & message)
		End Sub
	End Class
End Namespace
