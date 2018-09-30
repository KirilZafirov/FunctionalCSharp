using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Immutability
{
    public class AuditManager
    {
      private readonly int _maxEntriesPerFile;

      public AuditManager(int maxEntriesPerFile)
      {
         _maxEntriesPerFile = maxEntriesPerFile;
      }


      public FileAction AddRecord(FileContent currentFile, string visitorName, DateTime timeOfVisit)
      {
         //string[] lines = File.ReadAllLines(currentFile);
         List<AuditEntry> entries = Parse(currentFile.Content);

         if(entries.Count < _maxEntriesPerFile)
         {
            entries.Add(new AuditEntry(entries.Count+1,visitorName,timeOfVisit));
            string[] newContent = Serialize(entries);
            return new FileAction(currentFile.FileName, ActionType.Update, newContent);

            //int lastIndex = int.Parse(lines.Last().Split(';')[0]);
            //string newLine = (lastIndex + 1) + ";" + visitorName + ";" + timeOfVisit.ToString("s");
            //File.AppendAllLines(currentFile, new[] { newLine });
         }
         else
         {
            var entry = new AuditEntry(1, visitorName, timeOfVisit);
            string[] newContent = Serialize(new List<AuditEntry> { entry });
            string newFileName = GetNewFileName(currentFile.FileName);

            return new FileAction(newFileName, ActionType.Create, newContent);
            //string newLine = "1;" + visitorName + ";" + timeOfVisit.ToString("s");
            //string newFileName = GetNewFileName(currentFile);
            //File.WriteAllLines(newFileName, new[] { newLine });
         }
      }

      private string[] Serialize(List<AuditEntry> entries) => entries
            .Select(entry => entry.Number + ";" + entry.Visitor + ";" + entry.TimeOfVisit.ToString("s"))
            .ToArray();

      private List<AuditEntry> Parse(string[] content)
      {
         var result = new List<AuditEntry>();

         foreach (string line in content)
         {
            string[] data = line.Split(';');
            result.Add(new AuditEntry(int.Parse(data[0]),data[1],DateTime.Parse(data[2])));
         }
         return result;
      }

      private string GetNewFileName(string existingFileName)
      {
         string fileName = Path.GetFileNameWithoutExtension(existingFileName);
         int index = int.Parse(fileName.Split('_')[1]);
         return "Audit_" + (index + 1) + ".txt";
      }

      public void RemoveMentionsAbout(string visitorName, string directoryName)
      {
         foreach (string fileName in Directory.GetFiles(directoryName))
         {
            string tempFile = Path.GetTempFileName();

            List<string> linesToKeep = File
                   .ReadLines(fileName)
                   .Where(line => !line.Contains(visitorName))
                   .ToList();
            if(linesToKeep.Count == 0)
            {
               File.Delete(fileName);
            }
            else
            {
               File.WriteAllLines(tempFile, linesToKeep);
               File.Delete(fileName);
               File.Move(tempFile, fileName);
            }
         }
      }
   }

   public struct AuditEntry
   {
      public readonly int Number;
      public readonly string Visitor;
      public readonly DateTime TimeOfVisit;

      public AuditEntry(int number, string visitor, DateTime timeOfVisit)
      {
         Number = number;
         Visitor = visitor;
         TimeOfVisit = timeOfVisit;
      }
   }

   public struct FileAction
   {
      public readonly string FileName;
      public readonly string[] Content;
      public readonly ActionType Type;

      public FileAction(string fileName, ActionType type, string[] content)
      {
         FileName = fileName;
         Content = content;
         Type = type;
      }
   }

   public enum ActionType
   {
      Create,
      Update,
      Delete
   }

   public struct FileContent
   {
      public readonly string FileName;
      public readonly string[] Content;

      public FileContent(string fileName, string[] content)
      {
         FileName = fileName;
         Content = content;
      }
   }
}
