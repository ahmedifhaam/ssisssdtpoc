using ClientCreation.connection;
using ClientCreation.ssdt;
using CommandLine;
using System;
using System.Collections.Generic;

namespace Creator
{
    class Program
    {
        public class Options
        {
            [Option('i', "ssispath", Required = true, HelpText = "Please provide the path for the dtsx file of the ssis package.")]
            public string SSISPath { get; set; }

            [Option('d', "dacpacpath", Required = true, HelpText = "Please provide the path for the db creation dacpac.")]
            public string DacpacPath { get; set; }

            [Option('c', "databasename", Required = true, HelpText = "Please provide the name of the database")]
            public string Databasename { get; set; }

            [Option('u', "username", Required = true, HelpText = "Please provide the username for the database server")]
            public string Username { get; set; }

            [Option('p', "password", Required = true, HelpText = "Please provide the password for the database server")]
            public string Password { get; set; }

            [Option('s', "server", Default = "localhost", HelpText = "Please provide server ip or name (Default = localhost)")]
            public string Server { get; set; }

            [Option('x', "xlpath", HelpText = "Please provide the path of the excel file relative to the ssis package")]
            public string ExcelPath { get; set; }


        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }

        static void RunOptions(Options options)
        {
            Console.WriteLine(String.Format("Execution starting on {0} , database {1}, with username {2} and password {3}"
                , options.Server, options.Databasename, options.Username, options.Password));
            Console.WriteLine(String.Format("SSIS package path {0}", options.SSISPath));
            Console.WriteLine(String.Format("Dacpac path {0}", options.DacpacPath));
            Console.WriteLine(options.ToString());

            IConnectionStringBuilder builder = new ConnectionStringBuilder();
            builder.SetServer(options.Server)
                .SetDatabase(options.Databasename)
                .SetUserName(options.Username)
                .SetPassword(options.Password);


            //Create the database from ssdt project
            SSDTPackageLoader pacLoader = new SSDTPackageLoader();

            SSDTExecutor exec = new SSDTExecutor(builder);
            exec.OnStatusUpdate(progressupdate);
            pacLoader.LoadSSDTPackage(options.DacpacPath);
            exec.Publish(pacLoader, options.Databasename);

        }

        private static void progressupdate(object sender, string e)
        {
            Console.WriteLine(e);
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
        }
    }
}
