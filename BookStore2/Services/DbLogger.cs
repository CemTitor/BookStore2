namespace BookStore2.Services{
    public class DbLogger : ILoggerService{
        public void Write(string message){
            Console.WriteLine("[DbLogger] " + message);
        }
        public void LogInformation(string message){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void LogWarning(string message){
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void LogDebug(string message){
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void LogError(string message){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}