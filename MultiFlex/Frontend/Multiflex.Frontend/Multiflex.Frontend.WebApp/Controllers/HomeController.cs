﻿using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Multiflex.Frontend.WebApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Multiflex.Frontend.WebApp.Controllers
{
    //private static List<Models.WareDto> ware = new();
    //private static List<Models.ShelfDto> shelf = new();
    //private static List<Models.RegalDto> rsw = new();

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {

            //Stopwatch stopWatch1 = new Stopwatch();
            //stopWatch1.Start();
            ////lieferanten = new();
            //using var httpCliet = new HttpClient();

            //Stopwatch stopWatch = new Stopwatch();

            //stopWatch.Start();
            //var requestRegal = Task.Run(() =>
            //{
            //    return httpCliet.GetStringAsync("http://localhost:8080/regal");
            //});

            //var requestFach = Task.Run(() =>
            //{
            //    return httpCliet.GetStringAsync("http://localhost:8080/shelf");
            //});

            //var requestWare = Task.Run(() =>
            //{
            //    return httpCliet.GetStringAsync("http://localhost:8080/ware");
            //});

            //stopWatch.Stop();

            //System.Console.WriteLine("Databse: " + stopWatch.Elapsed);

            //stopWatch.Reset();
            //stopWatch.Start();
            //var itemsRegal = Task.Run(() =>
            //{
            //    return System.Text.Json.JsonSerializer.Deserialize<Models.SupplierDto[]>(requestRegal.Result.ToString());
            //});
            //var itemsShelf = Task.Run(() =>
            //{
            //    return System.Text.Json.JsonSerializer.Deserialize<Models.SupplierDto[]>(requestFach.Result.ToString());
            //});
            //var itemsWare = Task.Run(() =>
            //{
            //    return System.Text.Json.JsonSerializer.Deserialize<Models.SupplierDto[]>(requestWare.Result.ToString());
            //});
            //stopWatch.Stop();
            //System.Console.WriteLine("JSONparse" + stopWatch.Elapsed);

            //stopWatch1.Stop();
            //System.Console.WriteLine($"Gesammt: {stopWatch1.Elapsed}");
            //return View(itemsRegal.Result, itemsShelf.Result, itemsWare.Result);

    
            Electron.IpcMain.On("open-add-window", async (arg) =>
            {
                //var mainWindow = Electron.WindowManager.BrowserWindows.First();
                //var browserWindow = await Electron.WindowManager.CreateWindowAsync();
                var options = new BrowserWindowOptions
                {
                    Fullscreenable = false,
                    Minimizable = false,
                    SkipTaskbar = false,
                    EnableLargerThanScreen = false,
                    AutoHideMenuBar = true,
                    TitleBarStyle = TitleBarStyle.hiddenInset
                };
                BrowserWindow win = await AddWindow("/Home/Add", new BrowserWindowOptions());
                win.Show();
            });
            
            Electron.IpcMain.On("searchRegal", async (arg) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                using var httpCliet = new HttpClient();

                var requestRegal = await Task.Run(() =>
                {
                    return httpCliet.GetStringAsync($"http://localhost:8080/regal/{arg}");
                });
                Console.WriteLine(requestRegal);

                var requestWare = await Task.Run(() =>
                {
                    return httpCliet.GetStringAsync("http://localhost:8080/ware/product");
                });

                Console.WriteLine(requestWare);
                var requestFach = await Task.Run(() =>
                {
                    return httpCliet.GetStringAsync("http://localhost:8080/fach");
                });

                Console.WriteLine(requestWare);
                var json1 = JArray.Parse(requestRegal);
                var json2 = JArray.Parse(requestWare);
                var json3 = JArray.Parse(requestFach);
                Console.WriteLine("Json1");
                Console.WriteLine(json1);
                //Console.WriteLine("Json2");
                //Console.WriteLine(json2);
                //Console.WriteLine("Json3");
                //Console.WriteLine(json3);

                Electron.IpcMain.Send(mainWindow, "getSearchedRegal-reply", json1.ToString(), json2.ToString(), json3.ToString());
            });

            Electron.IpcMain.On("loadFinished", async (arg) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                using var httpCliet = new HttpClient();

                var requestRegal = await Task.Run(() =>
                {
                    return httpCliet.GetStringAsync("http://localhost:8080/regal");
                });
                Console.WriteLine(requestRegal);
                var requestWare = await Task.Run(() =>
                {
                    return httpCliet.GetStringAsync("http://localhost:8080/ware/product");
                });
                Console.WriteLine(requestWare);
                var requestFach = await Task.Run(() =>
                {
                    return httpCliet.GetStringAsync("http://localhost:8080/shelf");
                });
                Console.WriteLine(requestFach);
                var json1 = JArray.Parse(requestRegal);
                var json2 = JArray.Parse(requestWare);
                var json3 = JArray.Parse(requestFach);
                //Console.WriteLine("Json1: Regal");
                //Console.WriteLine(json1);
                //Console.WriteLine("Json2: Ware");
                //Console.WriteLine(json2);
                //Console.WriteLine("Json3: Fach");
                //Console.WriteLine(json3);
                //var requestRegalFachWare = await Task.Run(() =>
                //{
                //    return httpCliet.GetStringAsync("http://localhost:8080/regal/queries/regal-fach");
                //});
                //var json1 = JArray.Parse(requestRegalFachWare);
                //Electron.IpcMain.Send(mainWindow, "getRegal-reply", json1.ToString());
                Electron.IpcMain.Send(mainWindow, "getRegal-reply", json1.ToString(), json2.ToString(), json3.ToString());
            });
            return View();
        }
        public IActionResult Lieferanten()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        //[HttpPost()]
        public IActionResult Add()
        {
            RegalDto regal = new RegalDto();
            Electron.IpcMain.On("add-regal-input-name", (arg) =>
            {
                regal.name = arg.ToString();
                //Console.WriteLine(arg.ToString());
            });
            Electron.IpcMain.On("add-regal-input-max-value", (arg) =>
            {
                regal.maxAmountShelfs = Convert.ToInt32(arg.ToString());
                //Console.WriteLine(arg.ToString());
                //if (regal.max_anzahl_faecher < 0 || regal.max_anzahl_faecher > 100)
                //{
                //    Electron.IpcMain.Send(mainWindow, "get-regal-input-max-value-reply");
                    
                //}
            });
            Electron.IpcMain.On("add-regal", async (arg) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                var json = JsonConvert.SerializeObject(regal);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using var httpCliet = new HttpClient();
                //Console.WriteLine("DATA");
                //Console.WriteLine(regal.name);
                if (regal.maxAmountShelfs > 0 && regal.maxAmountShelfs < 100)
                    {
                    //    Electron.IpcMain.Send(mainWindow, "get-regal-input-max-value-reply");
                    await httpCliet.PostAsync("http://localhost:8080/regal/add", data);

                }
                else
                {
                    Electron.IpcMain.Send(mainWindow, "add-regal-reply");
                }
                    //await httpCliet.PostAsync("http://localhost:8080/regal/add", data);
                
                //Console.WriteLine("test2");
            });
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task<BrowserWindow> AddWindow(string path, BrowserWindowOptions options)
        {
            options.Show = false;
            //path = Path.GetFullPath(@"../../../Views/Home/Add.cshtml");
            path = $"http://localhost:{BridgeSettings.WebPort}{path}";
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(options, path);
            //path = Path.GetFullPath(@"../../../Views/Home/Add.cshtml");
            //browserWindow.LoadURL(path);
            //Console.WriteLine(path);
            //browserWindow.OnReadyToShow += () => browserWindow.Show();
            //browserWindow.Show();

            return browserWindow;
        }
    }
}
