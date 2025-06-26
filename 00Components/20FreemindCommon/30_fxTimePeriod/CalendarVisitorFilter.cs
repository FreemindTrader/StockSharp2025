// -- FILE ------------------------------------------------------------------
// name       : CalendarVisitorFilter.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using fx.Collections;

namespace fx.TimePeriod
{

	// ------------------------------------------------------------------------
	public class CalendarVisitorFilter : ICalendarVisitorFilter
	{

		// ----------------------------------------------------------------------
		public virtual void Clear()
		{
			years.Clear();
			months.Clear();
			days.Clear();
			weekDays.Clear();
			hours.Clear();
		} // Clear

		// ----------------------------------------------------------------------
		public ITimePeriodCollection ExcludePeriods
		{
			get { return excludePeriods; }
		} // ExcludePeriods

		// ----------------------------------------------------------------------
		public IList<int> Years
		{
			get { return years; }
		} // Years

		// ----------------------------------------------------------------------
		public IList<YearMonth> Months
		{
			get { return months; }
		} // Months

		// ----------------------------------------------------------------------
		public IList<int> Days
		{
			get { return days; }
		} // Days

		// ----------------------------------------------------------------------
		public IList<DayOfWeek> WeekDays
		{
			get { return weekDays; }
		} // WeekDays

		// ----------------------------------------------------------------------
		public IList<int> Hours
		{
			get { return hours; }
		} // Hours

		// ----------------------------------------------------------------------
		public void AddWorkingWeekDays()
		{
			weekDays.Add( DayOfWeek.Monday );
			weekDays.Add( DayOfWeek.Tuesday );
			weekDays.Add( DayOfWeek.Wednesday );
			weekDays.Add( DayOfWeek.Thursday );
			weekDays.Add( DayOfWeek.Friday );
		} // AddWorkingWeekDays

		// ----------------------------------------------------------------------
		public void AddWeekendWeekDays()
		{
			weekDays.Add( DayOfWeek.Saturday );
			weekDays.Add( DayOfWeek.Sunday );
		} // AddWeekendWeekDays

		// ----------------------------------------------------------------------
		// members
		private readonly TimePeriodCollection excludePeriods = new TimePeriodCollection();
		private readonly PooledList<int> years               = new PooledList<int>();
		private readonly PooledList<YearMonth> months        = new PooledList<YearMonth>();
		private readonly PooledList<int> days                = new PooledList<int>();
		private readonly PooledList<DayOfWeek> weekDays      = new PooledList<DayOfWeek>();
		private readonly PooledList<int> hours               = new PooledList<int>();

	} // class CalendarVisitorFilter

} // namespace fx.TimePeriod
// -- EOF -------------------------------------------------------------------
