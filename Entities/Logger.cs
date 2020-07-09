//------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) Microsoft Corporation. All Rights Reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.IO;

namespace SensorEventGenerator
{
    public class Logger
    {
        private string path;
        private string id = Guid.NewGuid().ToString();

        public Logger(string path)
        {
            this.path = path;
            var folder = Path.GetDirectoryName(this.path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (Path.GetFileName(path) == string.Empty)
            {
                this.path = Path.Combine(this.path, id);
                this.path = Path.ChangeExtension(this.path, "log");
            }

            Console.WriteLine($"Started Logger {this.id} with path {this.path}");
        }

        public void Write(string log)
        {
            var content = string.Format("[{0}] [Information] {1}{2}", DateTime.UtcNow.ToString("o"), log, Environment.NewLine);
            File.AppendAllText(this.path, content);
        }

        public void Write(Exception e)
        {
            var content = string.Format("[{0}] [Exception] {1}{2}{3}{4}", DateTime.UtcNow.ToString("o"), e.Message, Environment.NewLine, e.StackTrace, Environment.NewLine);
            Console.Write(content);
            File.AppendAllText(this.path, content);
        }
    }
}
