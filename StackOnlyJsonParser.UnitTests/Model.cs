using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.UnitTests
{
	[StackOnlyJsonType]
	internal readonly ref partial struct StackOnlyType
	{
		public bool Bool { get; }
		public byte Byte { get; }
		public DateTime DateTime { get; }
		public DateTimeOffset DateTimeOffset { get; }
		public decimal Decimal { get; }
		public double Double { get; }
		public Guid Guid { get; }
		public short Int16 { get; }
		public int Int32 { get; }
		public long Int64 { get; }
		public sbyte SByte { get; }
		public float Single { get; }
		public string String { get; }
		public ushort UInt16 { get; }
		public uint UInt32 { get; }
		public ulong UInt64 { get; }
		public bool? NullableBool { get; }
		public byte? NullableByte { get; }
		public DateTime? NullableDateTime { get; }
		public DateTimeOffset? NullableDateTimeOffset { get; }
		public decimal? NullableDecimal { get; }
		public double? NullableDouble { get; }
		public Guid? NullableGuid { get; }
		public short? NullableInt16 { get; }
		public int? NullableInt32 { get; }
		public long? NullableInt64 { get; }
		public sbyte? NullableSByte { get; }
		public float? NullableSingle { get; }
		public ushort? NullableUInt16 { get; }
		public uint? NullableUInt32 { get; }
		public ulong? NullableUInt64 { get; }
	}

	[StackOnlyJsonType]
	internal readonly ref partial struct EmptyStackOnlyType
	{ }
}
