## 1) Revision de Snippet

Luego de revisar el siguiente snippet, podemos resaltar las siguientes obesrvaciones y sugerencias.

1. Es una variable private y deberia por convención debería escribirse (_logToDatabase)
2. Existen 2 parametros con el mismo nombre, esto provocará un error, además procedi a reemplazar las variables booleanas por un Enum.
3. Realizar un .Trim() antes de validar si es null, arrojará una excepción.
4. Esta validación debe realizarse antes del .Trim().
5. Al cambiar po un Enum, podemos evitar la validación de todos los bool anteriores.
6. Los métodos para realizar el log en los diferentes ambientes deben factorizarse y aislarse en métodos aparte para luego ser invocados en el método principal.
7. Podemos emplear la directiva "using" para indicar el Path de origen System.Data.SqlClient, en lugar de escribirla completa, teniendo una linea de código menos extensa, además podriamos usar una tipificación explicita como "var" al momento de declarar el objeto.
8. Tener una variable local no inicializada arrojará error al momento de leerla en un ámbito mas proximo como en el punto (9), dicho método .ToString() arrojará error en el intellisense.
9. Usar Interpolación para concatenar strings.
10. Usar la directiva "using" en lugar de todo el namespace completo.
10. Se podría crear una constante para el path.
10. Usar Interpolación para concatenar strings.
10. El método DateTime.Now.ToShortDateString() contiene caracteres no admitidos para archivos, por lo que emplearlo en el método .File.Exists() conllevará a que nunca se encuentre dicho path.
11. Usar Interpolación para concatenar strings
12. Usar la directiva "using" en lugar de todo el namespace completo.

## 2) Reescribir el código
Siguiendo con el punto 2. del ejercicio, reescribí el codigo, definí una estructura así como las pruebas unitarias para llevar acabo la tarea.

```
using System;
using System.Linq;
using System.Text;

public class JobLogger
{
    private static bool _logToFile;
    private static bool _logToConsole;
    private static bool _logMessage;
    private static bool _logWarning;
    private static bool _logError;
    private static bool LogToDatabase; // 1. 
    private bool _initialized;
    public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool
    logMessage, bool logWarning, bool logError)
    {
        _logError = logError;
        _logMessage = logMessage;
        _logWarning = logWarning;
        LogToDatabase = logToDatabase;
        _logToFile = logToFile;
        _logToConsole = logToConsole;
    }
    public static void LogMessage(string message, bool message, bool warning, bool error) // 2.
    {
        message.Trim(); // 3.
        if (message == null || message.Length == 0) // 4. 
        {
            return;
        }
        if (!_logToConsole && !_logToFile && !LogToDatabase)
        {
            throw new Exception("Invalid configuration");
        }
        if ((!_logError && !_logMessage && !_logWarning) || 
            (!message && !warning && !error)) // 5. 
        {
            throw new Exception("Error or Warning or Message must be specified");
        }
        
        // 6.
        
        System.Data.SqlClient.SqlConnection connection = new
        System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);  // 7.
        connection.Open();
        int t; // 8.
        if (message && _logMessage)
        {
            t = 1;
        }
        if (error && _logError)
        {
            t = 2;
        }
        if (warning && _logWarning)
        {
            t = 3;
        }
        System.Data.SqlClient.SqlCommand command = new
        System.Data.SqlClient.SqlCommand("Insert into Log Values('" + message + "', " +
        t.ToString() + ")"); // 9.
        command.ExecuteNonQuery();
        string l;
        if (!System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"))  // 10.
        {
            l =
            System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"); // 10.

        }
        if (error && _logError)
        {
            l = l + DateTime.Now.ToShortDateString() + message; // 11.
        }
        if (warning && _logWarning)
        {
            l = l + DateTime.Now.ToShortDateString() + message; // 11.
        }
        if (message && _logMessage)
        {
            l = l + DateTime.Now.ToShortDateString() + message;  // 11.
        }

        System.IO.File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt", l); // 12.
        if (error && _logError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        if (warning && _logWarning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        if (message && _logMessage)
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine(DateTime.Now.ToShortDateString() + message);
    }
}

```
