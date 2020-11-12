

using EdmiTest.Data.Contexts;
using EdmiTest.Data.Enums;
using EdmiTest.Data.Models;
using EdmiTest.Data.Models.Responses;
using EdmiTest.Services;
using EdmiTest.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EdmiTest.ConsoleApp
{
    class Program
    {
        private static IServiceCollection servicesCollection;
        public static IConfiguration configuration { get; set; }
        public static ServiceProvider serviceProvider { get; set; }

        public static string ActualEnvironment { get { return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"; } }

        static void Main(string[] args)
        {
            configuration = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{ActualEnvironment}.json", false, true)
                    .Build();

            servicesCollection = new ServiceCollection();
            servicesCollection.AddDbContext<EdmiDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EdmiDb")));

            servicesCollection.AddCustomServices().AddCustomDataServices();

            serviceProvider = servicesCollection.BuildServiceProvider();

            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Welcome to EdmiTest 1.0\n");

            string input = "";

            while (input != "exit")
            {
                switch (input)
                {
                    case "list": await ListDevices(); break;
                    case "add": await AddNewDevice(); break;
                    default: break;
                }
                ShowInstructions();
                input = Console.ReadLine();
            }

        }

        private static void ShowInstructions()
        {
            Console.WriteLine("Enter one of the following commands:");
            Console.WriteLine("list -> Show saved devices");
            Console.WriteLine("add -> Add a new device");
            Console.WriteLine("exit -> Exit program\n");
        }

        private async static Task<bool> ListDevices()
        {
            IElectricMeterService electricMeterService = serviceProvider.GetService<IElectricMeterService>();
            IWaterMeterService waterMeterService = serviceProvider.GetService<IWaterMeterService>();
            IGatewayService gatewayService = serviceProvider.GetService<IGatewayService>();

            List<ElectricMeter> electricMeters = await electricMeterService.GetAll();
            if (electricMeters.Count > 0)
            {
                Console.WriteLine("***************");
                Console.WriteLine("ELECTRIC METERS");
                Console.WriteLine("***************");
                foreach (var item in electricMeters)
                {
                    Console.WriteLine($"Serial Number: {item.SerialNumber}");
                    Console.WriteLine($"Firmware Version: {item.FirmwareVersion}");
                    Console.WriteLine($"State: {MapStateValue((int)item.State)}");
                    Console.WriteLine("---------------------------------------");
                }
            }

            List<WaterMeter> waterMeters = await waterMeterService.GetAll();
            if (waterMeters.Count > 0)
            {
                Console.WriteLine("************");
                Console.WriteLine("WATER METERS");
                Console.WriteLine("************");
                foreach (var item in waterMeters)
                {
                    Console.WriteLine($"Serial Number: {item.SerialNumber}");
                    Console.WriteLine($"Firmware Version: {item.FirmwareVersion}");
                    Console.WriteLine($"State: {MapStateValue((int)item.State)}");
                    Console.WriteLine("---------------------------------------");
                }
            }

            List<Gateway> gateways = await gatewayService.GetAll();
            if (gateways.Count > 0)
            {
                Console.WriteLine("********");
                Console.WriteLine("GATEWAYS");
                Console.WriteLine("********");
                foreach (var item in gateways)
                {
                    Console.WriteLine($"Serial Number: {item.SerialNumber}");
                    Console.WriteLine($"Firmware Version: {item.FirmwareVersion}");
                    Console.WriteLine($"State: {MapStateValue((int)item.State)}");
                    Console.WriteLine($"IP: {item.IP}");
                    Console.WriteLine($"Port: {item.Port}");
                    Console.WriteLine("---------------------------------------");
                }
                Console.WriteLine();
            }

            return true;
        }

        private async static Task<bool> AddNewDevice()
        {
            IWaterMeterService waterMeterService = serviceProvider.GetService<IWaterMeterService>();
            IGatewayService gatewayService = serviceProvider.GetService<IGatewayService>();

            string type = "";
            string serialNumber;
            string firmwareVersion;
            string state = "";

            while (type != "E" && type != "W" && type != "G")
            {
                Console.Write("Device type (E)lectricMeter, (W)aterMeter, (G)ateway: ");
                type = Console.ReadLine().ToUpper();
            }

            Console.Write("Serial Number: ");
            serialNumber = Console.ReadLine();

            Console.Write("Firmware Version: ");
            firmwareVersion = Console.ReadLine();

            while (state != "0" && state != "1" && state != "2")
            {
                Console.Write("State (0)-Inactive, (1)-Active, (2)-Repairing: ");
                state = Console.ReadLine();
            }

            BaseResponse response = new BaseResponse();
            switch (type)
            {
                case "E":
                    response = await AddNewElectricMeter(serialNumber, firmwareVersion, state);
                    break;
                case "W":
                    response = await AddNewWaterMeter(serialNumber, firmwareVersion, state);
                    break;
                case "G":
                    string ip;
                    string port;

                    Console.Write("IP: ");
                    ip = Console.ReadLine();

                    Console.Write("Port: ");
                    port = Console.ReadLine();
                    response = await AddNewGateway(serialNumber, firmwareVersion, state, ip, port);
                    break;
            }

            if (response.Valid)
            {
                Console.WriteLine("Device added!");
            }
            else
            {
                Console.WriteLine("Something went wrong!");
                foreach (string msg in response.ErrorMessages)
                {
                    Console.WriteLine(msg);
                }
            }
            Console.WriteLine("");

            return true;
        }

        private async static Task<BaseResponse> AddNewElectricMeter(string serialNumber, string firmwareVersion, string state)
        {
            IDeviceService<Device> deviceService = serviceProvider.GetService<IDeviceService<Device>>();
            IElectricMeterService electricMeterService = serviceProvider.GetService<IElectricMeterService>();
            ElectricMeter electricMeter = new ElectricMeter
            {
                SerialNumber = serialNumber,
                FirmwareVersion = firmwareVersion,
                State = (EState)Enum.Parse(typeof(EState), state)
            };

            BaseResponse result = await deviceService.Exists(electricMeter.SerialNumber);
            if (!result.Valid)
            {
                return result;
            }

            result = electricMeterService.Validate(electricMeter);
            if (!result.Valid)
            {
                return result;
            }

            return await electricMeterService.Add(electricMeter);
        }

        private async static Task<BaseResponse> AddNewWaterMeter(string serialNumber, string firmwareVersion, string state)
        {
            IDeviceService<Device> deviceService = serviceProvider.GetService<IDeviceService<Device>>();
            IWaterMeterService waterMeterService = serviceProvider.GetService<IWaterMeterService>();
            WaterMeter waterMeter = new WaterMeter
            {
                SerialNumber = serialNumber,
                FirmwareVersion = firmwareVersion,
                State = (EState)Enum.Parse(typeof(EState), state)
            };

            BaseResponse result = await deviceService.Exists(waterMeter.SerialNumber);
            if (!result.Valid)
            {
                return result;
            }

            result = waterMeterService.Validate(waterMeter);
            if (!result.Valid)
            {
                return result;
            }

            return await waterMeterService.Add(waterMeter);
        }

        private async static Task<BaseResponse> AddNewGateway(string serialNumber, string firmwareVersion, string state, string ip, string port)
        {
            IDeviceService<Device> deviceService = serviceProvider.GetService<IDeviceService<Device>>();
            IGatewayService gatewayService = serviceProvider.GetService<IGatewayService>();
            int portNum;

            Gateway gateway = new Gateway
            {
                SerialNumber = serialNumber,
                FirmwareVersion = firmwareVersion,
                State = (EState)Enum.Parse(typeof(EState), state),
                IP = ip,
                Port = Int32.TryParse(port, out portNum) ? portNum : 0
            };

            BaseResponse result = await deviceService.Exists(gateway.SerialNumber);
            if (!result.Valid)
            {
                return result;
            }

            result = gatewayService.Validate(gateway);
            if (!result.Valid)
            {
                return result;
            }

            return await gatewayService.Add(gateway);
        }

        private static string MapStateValue(int state)
        {
            return state == 0 ? "Inactive" : state == 1 ? "Active" : state == 2 ? "Repairing" : "";
        }

    }
}
