namespace WaxWelio.Common.Object
{
    public class KeyValueResult
    {
        public KeyValueResult(object key, object value = null)
        {
            Key = key;
            Value = value;
        }

        public object Key { get; set; }

        public object Value { get; set; }
    }
}