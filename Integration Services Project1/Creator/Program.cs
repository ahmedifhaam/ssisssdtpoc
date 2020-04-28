using ClientCreation.ssdt;
using ClientCreation.ssis;
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

            ConnectionStringBuilder builder = new ConnectionStringBuilder();
            builder.setServer(options.Server)
                .setDatabase(options.Databasename)
                .setUserName(options.Username)
                .setPassword(options.Password);


            //Create the database from ssdt project
            PackageLoader pacLoader = new PackageLoader();

            SSDTExecutor exec = new SSDTExecutor(builder.build());
            exec.OnStatusUpdate(progressupdate);
            pacLoader.loadDacPac(options.DacpacPath);
            exec.publish(pacLoader, options.Databasename);

            //migrate AE encrypted data
            pacLoader.loadSSISPackage(options.SSISPath);
            var ssisrunner = new SSISExecutor(pacLoader);
            ssisrunner.SetServer(options.Server);
            ssisrunner.SetUser(options.Username);
            ssisrunner.SetPassword(options.Password);
            ssisrunner.SetDatabase(options.Databasename);
            string result = ssisrunner.execute();
            Console.WriteLine(result);
            Console.ReadLine();
        }

        private static void progressupdate(object sender, string e)
        {
            Console.WriteLine(e);
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            //foreach(var error in errs)
            //{
            //    Console.WriteLine(error);
            //}
        }
    }
}
