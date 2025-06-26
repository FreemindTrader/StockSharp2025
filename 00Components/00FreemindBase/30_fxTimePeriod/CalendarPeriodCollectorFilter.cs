// -- FILE ------------------------------------------------------------------
// name       : CalendarPeriodCollectorFilter.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Collections.Generic; using fx.Collections;

namespace fx.TimePeriod
{

	// ------------------------------------------------------------------------
	public class CalendarPeriodCollectorFilter : CalendarVisitorFilter, ICalendarPeriodCollectorFilter
	{

		// ----------------------------------------------------------------------
		public override void Clear()
		{
			base.Clear();
			collectingMonths.Clear();
			collectingDays.Clear();
			collectingHours.Clear();
		} // Clear

		// ----------------------------------------------------------------------
		public IList<MonthRange> CollectingMonths
		{
			get { return collectingMonths; }
		} // CollectingMonths

		// ----------------------------------------------------------------------
		public IList<DayRange> CollectingDays
		{
			get { return collectingDays; }
		} // CollectingDays

		// ----------------------------------------------------------------------
		public IList<HourRange> CollectingHours
		{
			get { return collectingHours; }
		} // CollectingHours

		// ----------------------------------------------------------------------
		public IList<DayHourRange> CollectingDayHours
		{
			get { return collectingDayHours; }
		} // CollectingDayHours

		// ----------------------------------------------------------------------
		// members
		private readonly PooledList<MonthRange> collectingMonths = new PooledList<MonthRange>();
		private readonly PooledList<DayRange> collectingDays = new PooledList<DayRange>();
		private readonly PooledList<HourRange> collectingHours = new PooledList<HourRange>();
		private readonly PooledList<DayHourRange> collectingDayHours = new PooledList<DayHourRange>();

	} // class CalendarPeriodCollectorFilter

} // namespace fx.TimePeriod
// -- EOF -------------------------------------------------------------------
