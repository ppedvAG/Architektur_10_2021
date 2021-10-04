namespace HalloSingelton
{
    class Logger
    {
        private static Logger _instance;
        private static object _instanceLock = new object();

        public static Logger Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                        _instance = new Logger();
                }

                return _instance;
            }
        }

        static int instCounter = 0;
        private Logger()
        {
            Console.WriteLine("CTOR Singelton");
            instCounter++;
        }

        public void Log(string msg)
        {
            Console.WriteLine($"({instCounter}) [{DateTime.Now}] {msg}");
        }
    }
}
