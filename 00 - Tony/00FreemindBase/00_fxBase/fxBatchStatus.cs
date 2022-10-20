using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Base
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Candle states.
	/// </summary>
	[DataContract]
	[Serializable]
	public enum fxBatchStatus : byte
	{
		/// <summary>
		/// Initial Request to Live Server for databars missing since Last run
		/// </summary>
		[EnumMember]
		InitialUpdate,

		/// <summary>
		/// Representing the bar pull from Live Server every 30 second
		/// </summary>
		[EnumMember]
		LiveUpdate,


		/// <summary>
		/// Representing the bars pull from Live Server when user selects reloaded Bars from Server
		/// </summary>
		[EnumMember]
		Reloaded,

		/// <summary>
		/// Representing the bars that are from local storage
		/// </summary>
		[EnumMember]
		FromStorage,
	}
}
