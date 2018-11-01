using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.ObjectModel;
using Java.Util;
using Com.Syncfusion.Schedule;
using Android.Graphics;

namespace RecurrenceExceptions
{
    [Activity(Label = "RecurrenceExceptions", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        ObservableCollection<Meeting> scheduleAppointmentCollection = new ObservableCollection<Meeting>();
        Calendar currentDate = Calendar.Instance;
        Button AddExceptionDates;
        Button RemoveExceptionDates;
        Button AddExceptionAppointment;
        Button RemoveExceptionAppointment;
        Calendar exceptionDate3 = Calendar.Instance;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            LinearLayout linearLayout = new LinearLayout(this);
            linearLayout.Orientation = Orientation.Vertical;
            this.AddButton(linearLayout);
            //Creating an instance for SfSchedule Control
            SfSchedule schedule = new SfSchedule(this);
            schedule.ScheduleView = Com.Syncfusion.Schedule.Enums.ScheduleView.WeekView;

            // Creating an instance for schedule appointment Collection

            Calendar startTime = (Calendar)currentDate.Clone();

            //setting start time for the event
            startTime.Set(2017, 08, 03, 10, 0, 0);
            Calendar endTime = (Calendar)currentDate.Clone();

            //setting end time for the event
            endTime.Set(2017, 08, 03, 12, 0, 0);

            // move to date.
            schedule.MoveToDate = startTime;

            // Set exception dates.
            var exceptionDate1 = Calendar.Instance;
            exceptionDate1.Set(2017, 08, 03);
            var exceptionDate2 = Calendar.Instance;
            exceptionDate2.Set(2017, 08, 05);
            exceptionDate3 = Calendar.Instance;
            exceptionDate3.Set(2017, 08, 07);

            AppointmentMapping mapping = new AppointmentMapping();
            mapping.Subject = "EventName";
            mapping.StartTime = "From";
            mapping.EndTime = "To";
            mapping.Color = "Color";
            mapping.RecurrenceId = "RecurrenceID";
            mapping.ExceptionOccurrenceActualDate = "ActualDate";
            mapping.RecurrenceExceptionDates = "RecurrenceExceptionDates";
            mapping.RecurrenceRule = "RecurrenceRule";
            schedule.AppointmentMapping = mapping;

            Meeting recurrenceAppointment = new Meeting();
            recurrenceAppointment.From = startTime;
            recurrenceAppointment.To = endTime;
            recurrenceAppointment.EventName = "Daily Occurs";
            recurrenceAppointment.Color = Color.Blue;
            recurrenceAppointment.RecurrenceRule = "FREQ=DAILY;COUNT=20";
            recurrenceAppointment.RecurrenceExceptionDates = new ObservableCollection<Calendar> { exceptionDate1, exceptionDate2, exceptionDate3 };
            scheduleAppointmentCollection.Add(recurrenceAppointment);

            //Adding schedule appointment collection to SfSchedule appointments
            schedule.ItemsSource = scheduleAppointmentCollection;
            linearLayout.AddView(schedule);
            SetContentView(linearLayout);
        }

        private void AddButton(LinearLayout linearLayout)
        {
            AddExceptionDates = new Button(this);
            AddExceptionDates.Text = "AddExceptionDates";
            AddExceptionDates.Click += AddExceptionDates_Click;
            linearLayout.AddView(AddExceptionDates);

            RemoveExceptionDates = new Button(this);
            RemoveExceptionDates.Text = "RemoveExceptionDates";
            RemoveExceptionDates.Click += RemoveExceptionDates_Click;
            linearLayout.AddView(RemoveExceptionDates);

            AddExceptionAppointment = new Button(this);
            AddExceptionAppointment.Text = "AddExceptionAppointment";
            AddExceptionAppointment.Click += AddExceptionAppointment_Click;
            linearLayout.AddView(AddExceptionAppointment);

            RemoveExceptionAppointment = new Button(this);
            RemoveExceptionAppointment.Text = "RemoveExceptionAppointment";
            RemoveExceptionAppointment.Click += RemoveExceptionAppointment_Click;
            linearLayout.AddView(RemoveExceptionAppointment);
        }

        private void RemoveExceptionAppointment_Click(object sender, System.EventArgs e)
        {
            if (scheduleAppointmentCollection.Count > 1)
            {
                var exceptionAppointment = scheduleAppointmentCollection[1];
                scheduleAppointmentCollection.Remove(exceptionAppointment);
            }
        }

        private void AddExceptionAppointment_Click(object sender, System.EventArgs e)
        {
            var startTime1 = Calendar.Instance;
            startTime1.Set(2017, 08, 07, 13, 0, 0);
            var endTime1 = Calendar.Instance;
            endTime1.Set(2017, 08, 07, 14, 0, 0);

            var recurrenceAppointment = scheduleAppointmentCollection[0];
            Meeting exceptionAppointment = new Meeting();
            exceptionAppointment.From = startTime1;
            exceptionAppointment.To = endTime1;
            exceptionAppointment.EventName = "Daily Occurs";
            exceptionAppointment.Color = Color.Red;
            exceptionAppointment.RecurrenceID = recurrenceAppointment;
            exceptionAppointment.ActualDate = exceptionDate3;
            scheduleAppointmentCollection.Add(exceptionAppointment);
        }

        private void RemoveExceptionDates_Click(object sender, System.EventArgs e)
        {
            scheduleAppointmentCollection[0].RecurrenceExceptionDates.RemoveAt(0);
        }


        private void AddExceptionDates_Click(object sender, System.EventArgs e)
        {
            Calendar exceptionDate = (Calendar)currentDate.Clone();
            exceptionDate.Set(2017, 08, 08, 10, 0, 0);
            scheduleAppointmentCollection[0].RecurrenceExceptionDates.Add(exceptionDate);
        }
    }

    /// <summary>   
    /// Represents custom data properties.   
    /// </summary> 
    public class Meeting
    {
        public string EventName { get; set; }
        public Calendar From { get; set; }
        public Calendar To { get; set; }
        public int Color { get; set; }
        public Calendar ActualDate { get; set; }
        public string RecurrenceRule { get; set; }
        public object RecurrenceID { get; set; }
        public ObservableCollection<Calendar> RecurrenceExceptionDates { get; set; } = new ObservableCollection<Calendar>();
    }
}

