using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab5.Helpers
{
    public class FileWriterHelper
    {
        private void InsertLine(string text)
        {
            File.AppendAllText("date.txt",
                    text + Environment.NewLine);
        }
    }
}
