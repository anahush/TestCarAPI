using System.ComponentModel;
using System.Globalization;

namespace TestCarAPI.Models.Helper
{
    [TypeConverter(typeof(CollectionSliceConverter))]
    public class CollectionSlice
    {
        public int StartId { get; set; }
        public int EndId { get; set; }

        public static bool TryParse(string s, out CollectionSlice result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 4)
            {
                return false;
            }

            int startId, endId;
            if (int.TryParse(parts[1], out startId) && int.TryParse(parts[3], out endId))
            {
                result = new CollectionSlice()
                {
                    StartId = startId,
                    EndId = endId
                };
                return true;
            }
            return false;
        }
    }

    public class CollectionSliceConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string)
            {
                CollectionSlice slice;
                if (CollectionSlice.TryParse((string)value, out slice))
                {
                    return slice;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
