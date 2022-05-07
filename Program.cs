using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Management;

namespace Remote_AdminSurface
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int oldCount = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("______                     _               ___ _________  ________ _   _  _____ ");
            Console.WriteLine("| ___ \\                   | |             / _ \\|  _  \\  \\/  |_   _| \\ | |/  ___|");
            Console.WriteLine("| |_/ /___ _ __ ___   ___ | |_ ___ ______/ /_\\ \\ | | | .  . | | | |  \\| |\\ `--. ");
            Console.WriteLine("|    // _ \\ '_ ` _ \\ / _ \\| __/ _ \\______|  _  | | | | |\\/| | | | | . ` | `--. \\");
            Console.WriteLine("| |\\ \\  __/ | | | | | (_) | ||  __/      | | | | |/ /| |  | |_| |_| |\\  |/\\__/ /");
            Console.WriteLine("\\_| \\_\\___|_| |_| |_|\\___/ \\__\\___|      \\_| |_/___/ \\_|  |_/\\___/\\_| \\_/\\____/ ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                                                                by @ScarredMonk");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[+] Fetched local Admins on Crowns Jewel Machine without admin privileges:\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            GetRemoteLocalGroupMembers();

            while (true)
            {
                ChangedCount(oldCount);
                Thread.Sleep(5000);
            }


            int GetRemoteLocalGroupMembers()
            {
                StringBuilder result = new StringBuilder();
                ConnectionOptions options = new ConnectionOptions();
                options.Impersonation = System.Management.ImpersonationLevel.Impersonate;
                ManagementScope scope = new ManagementScope("\\\\ADSQL01\\root\\cimv2", options);
                scope.Connect();

                //Query system for local administrators
                ObjectQuery query = new ObjectQuery("select * from Win32_GroupUser WHERE GroupComponent = 'Win32_Group.Domain=\"ADSQL01\",Name=\"Administrators\"'");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    ManagementPath path = new ManagementPath(m["PartComponent"].ToString());
                    {
                        String[] names = path.RelativePath.Split(',');
                        result.Append(names[0].Substring(names[0].IndexOf("=") + 1).Replace("\"", " ").Trim() + "\\");
                        result.AppendLine(names[1].Substring(names[1].IndexOf("=") + 1).Replace("\"", " ").Trim());

                    }
                    oldCount++;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Count is " + oldCount + "\n");
                return oldCount;
            }

            void ChangedCount(int count)
            {
                int newCount = 0;
                StringBuilder result = new StringBuilder();
                ConnectionOptions options = new ConnectionOptions();
                options.Impersonation = System.Management.ImpersonationLevel.Impersonate;
                ManagementScope scope = new ManagementScope("\\\\ADSQL01\\root\\cimv2", options);
                scope.Connect();

                //Query system for local administrators
                ObjectQuery query = new ObjectQuery("select * from Win32_GroupUser WHERE GroupComponent = 'Win32_Group.Domain=\"ADSQL01\",Name=\"Administrators\"'");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    ManagementPath path = new ManagementPath(m["PartComponent"].ToString());
                    {
                        String[] names = path.RelativePath.Split(',');
                        result.Append(names[0].Substring(names[0].IndexOf("=") + 1).Replace("\"", " ").Trim() + "\\");
                        result.AppendLine(names[1].Substring(names[1].IndexOf("=") + 1).Replace("\"", " ").Trim());

                    }
                    newCount++;
                }
                if (oldCount != newCount)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("DETECTED CHANGE IN LOCAL ADMIN MEMBERS\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.ToString());
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("New count is " + newCount);
                    oldCount = newCount;
                }

            }
        }



    }
}