using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using static BTX.SERVICES.Enums.Logger.LoggerEnums;

namespace BTX.SERVICES.Helpers.Logger
{
    public static class LoggerHelpers
    {
        public static class Paths
        {
            public static string OutPutTextFilePath = $"{ConfigurationManager.AppSettings["LogFileDirectory"]}/LogFile_{DateTime.Now.ToString("dd_MM_yyyy")}.txt";
        }

        public static class Methods
        {
            public static void LogToDatabase(string message, MessageType type)
            {
                try
                {
                    var connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                    connection.Open();

                    var command = new SqlCommand("Insert into Log Values('" + message + "', " + type.ToString() + ")");
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            public static void LogToFileText(string message, MessageType type)
            {
                if (!File.Exists(Paths.OutPutTextFilePath)) //~~~~~~ se creo una variable estática para la ruta de el archivo de salida, además la validación provoca un error puesto que lee una ruta que no existe.
                    throw new Exception("Invalid output path file.");

                string l = File.ReadAllText(Paths.OutPutTextFilePath);
                l += $"{DateTime.Now.ToShortDateString()} - type: {type.ToString()} - message: {message}";

                File.WriteAllText(Paths.OutPutTextFilePath, l);
            }

            public static void LogToConsole(string message, MessageType type)
            {
                switch (type)
                {
                    case MessageType.Message:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case MessageType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case MessageType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    default: break;
                }

                Console.WriteLine($"{DateTime.Now.ToShortDateString()} {message}");
            }
        }


    }
}
