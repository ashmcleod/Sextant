using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Sextant.Domain.Entities
{
    [DelimitedRecord(",")]
    [IgnoreFirst]
    public class SpanshLine
    {
        [FieldQuoted]
        public string SystemName;
        [FieldQuoted]
        public string BodyName;
        [FieldQuoted]
        public string Subtype;

        [FieldConverter(ConverterKind.Boolean, "Yes", "No")]
        [FieldQuoted]
        public bool Terraformable;

        [FieldConverter(ConverterKind.UInt32)]
        [FieldQuoted]
        public UInt32 DTA;
        [FieldConverter(ConverterKind.UInt32)]
        [FieldQuoted]
        public UInt32 ScanValue;
        [FieldConverter(ConverterKind.UInt32)]
        [FieldQuoted]
        public UInt32 MappingValue;
        [FieldConverter(ConverterKind.UInt16)]
        [FieldQuoted]
        public UInt16 Jumps;

    }
}
