namespace SandBoxEngine
{
    public class LogEventArgs
    {
        public string Message { get; set; }

        public LogEventArgs(string message)
        {
            Message = message;
        }
    }
}