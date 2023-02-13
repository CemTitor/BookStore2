namespace BookStore2.Services{
    public interface ILoggerService{
        public void Write(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}