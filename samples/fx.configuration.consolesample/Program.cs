﻿/* The MIT License (MIT)
*
* Copyright (c) 2014 FriendlyX
* Permission is hereby granted, free of charge, to any person obtaining a copy of
* this software and associated documentation files (the "Software"), to deal in
* the Software without restriction, including without limitation the rights to
* use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
* the Software, and to permit persons to whom the Software is furnished to do so,
* subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
* FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
* COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
* IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
* CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using Autofac;
using Newtonsoft.Json;

namespace FX.Configuration.ConsoleSample
{
    /// <summary>
    /// A startup class
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            //MainJsonConfiguration configuration = new MainJsonConfiguration
            //{
            //    AppVersion = "0.3.0",
            //    ComplexDetails = new ConfigurationComplexDetails
            //    {
            //        DisplayName = "some-json-with-display-name",
            //        Expiration = new DateTime(2099, 11, 1),
            //        SomeDetails = new SomeSubDetails
            //        {
            //            Id = new Guid("569EFA2E-C315-426F-BC02-F76AB4E0EEDA"),
            //            Name = "this-sub-name-in-json",
            //            Elapsed = new TimeSpan(1, 10, 4, 19),
            //        }
            //    },
            //    IntegerValues = new List<int> { 190, 267, 33, -1 },
            //    PreprocessedSetting = "some-setting-which-will-be-wrapped",
            //};

            //string json = JsonConvert.SerializeObject(configuration);

            RunForAppConfig();
            RunForJsonConfig();

            Console.ReadKey();
        }

        private static void RunForAppConfig()
        {
            // Manual configuration creation sample
            Console.WriteLine(Environment.NewLine + "**************** Manual sample");
            MainAppConfiguration configuration = new MainAppConfiguration();
            DumpConfiguration(configuration);

            // Using Autofac sample
            Console.WriteLine("**************** Autofac sample");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<ConsoleAppAutofacModule>();
            IContainer container = builder.Build();
            configuration = container.Resolve<MainAppConfiguration>();
            DumpConfiguration(configuration);

            // Manual configuration creation sample with short properties mapping
            Console.WriteLine(Environment.NewLine + "**************** Manual sample (long name mapping)");
            MainAppConfigurationLongPropertyName configurationLongPropertyName = new MainAppConfigurationLongPropertyName();
            DumpConfiguration(configurationLongPropertyName);

            // Using Autofac sample with short properties mapping
            Console.WriteLine(Environment.NewLine + "**************** Autofac sample (long name mapping)");
            configurationLongPropertyName = container.Resolve<MainAppConfigurationLongPropertyName>();
            DumpConfiguration(configurationLongPropertyName);

            // Manual configuration creation sample with custom properties mapping
            Console.WriteLine(Environment.NewLine + "**************** Manual sample (custom mapping)");
            CustomSettingNameConfiguration customSettingNameConfiguration = new CustomSettingNameConfiguration();
            DumpConfiguration(customSettingNameConfiguration);

            // Using Autofac sample with custom properties mapping
            Console.WriteLine(Environment.NewLine + "**************** Autofac sample (custom mapping)");
            customSettingNameConfiguration = container.Resolve<CustomSettingNameConfiguration>();
            DumpConfiguration(customSettingNameConfiguration);

            // Lazy initialization to prefent loading the settings in the constructor
            Console.WriteLine(Environment.NewLine + "**************** Lazy initialization");
            Lazy<MainAppConfiguration> lazyConfiguration = new Lazy<MainAppConfiguration>();
            DumpConfiguration(lazyConfiguration.Value);
        }

        private static void RunForJsonConfig()
        {
            Console.WriteLine(Environment.NewLine + "**************** Json configuration");
            MainJsonConfiguration jsonConfiguration = new MainJsonConfiguration();
            DumpConfiguration(jsonConfiguration);
        }

        private static void DumpConfiguration(MainJsonConfiguration configuration)
        {
            Console.WriteLine("AppVersion: " + configuration.AppVersion);
            Console.WriteLine("ComplexDetails.DisplayName: " + configuration.ComplexDetails.DisplayName);
            Console.WriteLine("ComplexDetails.Expiration: " + configuration.ComplexDetails.Expiration);
            Console.WriteLine("ComplexDetails.SomeDetails.Id: " + configuration.ComplexDetails.SomeDetails.Id);
            Console.WriteLine("ComplexDetails.SomeDetails.Name: " + configuration.ComplexDetails.SomeDetails.Name);
            Console.WriteLine("ComplexDetails.SomeDetails.Elapsed: " + configuration.ComplexDetails.SomeDetails.Elapsed);

            string json = JsonConvert.SerializeObject(configuration.ComplexDetails);
            Console.WriteLine("ComplexDetails JSON: " + json);

            Console.WriteLine("Enumerable STRING setting: " + string.Join(";", configuration.IntegerValues));
        }

        private static void DumpConfiguration(MainAppConfigurationLongPropertyName configuration)
        {
            Console.WriteLine("AppVersion: " + configuration.AppVersion);
            Console.WriteLine("ComplexDetails.DisplayName: " + configuration.ComplexDetails.DisplayName);
            Console.WriteLine("ComplexDetails.Expiration: " + configuration.ComplexDetails.Expiration);
            Console.WriteLine("ComplexDetails.SomeDetails.Id: " + configuration.ComplexDetails.SomeDetails.Id);
            Console.WriteLine("ComplexDetails.SomeDetails.Name: " + configuration.ComplexDetails.SomeDetails.Name);
            Console.WriteLine("ComplexDetails.SomeDetails.Elapsed: " + configuration.ComplexDetails.SomeDetails.Elapsed);

            string json = JsonConvert.SerializeObject(configuration.ComplexDetails);
            Console.WriteLine("ComplexDetails JSON: " + json);

            Console.WriteLine("Enumerable STRING setting: " + string.Join(";", configuration.StringValues));
        }

        private static void DumpConfiguration(MainAppConfiguration configuration)
        {
            Console.WriteLine("AppVersion: " + configuration.AppVersion);
            Console.WriteLine("ComplexDetails.DisplayName: " + configuration.ComplexDetails.DisplayName);
            Console.WriteLine("ComplexDetails.Expiration: " + configuration.ComplexDetails.Expiration);
            Console.WriteLine("ComplexDetails.SomeDetails.Id: " + configuration.ComplexDetails.SomeDetails.Id);
            Console.WriteLine("ComplexDetails.SomeDetails.Name: " + configuration.ComplexDetails.SomeDetails.Name);
            Console.WriteLine("ComplexDetails.SomeDetails.Elapsed: " + configuration.ComplexDetails.SomeDetails.Elapsed);

            string json = JsonConvert.SerializeObject(configuration.ComplexDetails);
            Console.WriteLine("ComplexDetails JSON: " + json);

            Console.WriteLine("Enumerable INT setting: " + string.Join("#", configuration.IntegerValues));
            
            Console.WriteLine("Preprocessed: " + configuration.PreprocessedSetting);
        }

        private static void DumpConfiguration(CustomSettingNameConfiguration configuration)
        {
            Console.WriteLine("AppVersion: " + configuration.AppVersion);
            Console.WriteLine("ComplexDetails.DisplayName: " + configuration.ComplexDetails.DisplayName);
            Console.WriteLine("ComplexDetails.Expiration: " + configuration.ComplexDetails.Expiration);
            Console.WriteLine("ComplexDetails.SomeDetails.Id: " + configuration.ComplexDetails.SomeDetails.Id);
            Console.WriteLine("ComplexDetails.SomeDetails.Name: " + configuration.ComplexDetails.SomeDetails.Name);
            Console.WriteLine("ComplexDetails.SomeDetails.Elapsed: " + configuration.ComplexDetails.SomeDetails.Elapsed);

            string json = JsonConvert.SerializeObject(configuration.ComplexDetails);
            Console.WriteLine("ComplexDetails JSON: " + json);
        }
    }
}