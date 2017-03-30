namespace GameStore.Services.Interfaces.Exceptions
{
    public class ArgumentExceptionDetails
    {
        public object Value { get; set; }

        public string ArgumentName { get; set; }

        public string MethodName { get; set; }

        public string ClassName { get; set; }

        public string Message { get; set; }
    }
}
