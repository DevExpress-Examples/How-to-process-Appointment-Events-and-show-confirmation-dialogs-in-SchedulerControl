Imports DevExpress.Mvvm
Imports DevExpress.UI.Xaml.Scheduler
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace AppointmentEvents
	Public Class Appt
		Inherits BindableBase

		Protected _subject As String
		Public Property Subject() As String
			Get
				Return Me._subject
			End Get
			Set(ByVal value As String)
				Me.SetProperty(Me._subject, value, "Subject")
			End Set
		End Property


		Protected _start As DateTime
		Public Property Start() As DateTime
			Get
				Return Me._start
			End Get
			Set(ByVal value As DateTime)
				Me.SetProperty(Me._start, value, "Start")
			End Set
		End Property


		Protected _end As DateTime
		Public Property [End]() As DateTime
			Get
				Return Me._end
			End Get
			Set(ByVal value As DateTime)
				Me.SetProperty(Me._end, value, "End")
			End Set
		End Property

		Protected _patternId As Object
		Public Property PatternId() As Object
			Get
				Return Me._patternId
			End Get
			Set(ByVal value As Object)
				Me.SetProperty(Me._patternId, value)
			End Set
		End Property

		Protected _recurrenceRule As String
		Public Property RecurrenceRule() As String
			Get
				Return Me._recurrenceRule
			End Get
			Set(ByVal value As String)
				Me.SetProperty(Me._recurrenceRule, value, "RecurrenceRule")
			End Set
		End Property

		Protected _type As AppointmentType
		Public Property Type() As AppointmentType
			Get
				Return Me._type
			End Get
			Set(ByVal value As AppointmentType)
				Me.SetProperty(Me._type, value)
			End Set
		End Property
	End Class
End Namespace
