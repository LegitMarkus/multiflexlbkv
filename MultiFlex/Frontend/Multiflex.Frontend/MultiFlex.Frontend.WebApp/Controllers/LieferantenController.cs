﻿using ElectronNET.API;
using Microsoft.AspNetCore.Mvc;
using Multiflex.Frontend.WebApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Multiflex.Frontend.WebApp.Controllers
{
    public class LieferantenController : Controller
    {
        public IActionResult Index()
        {
            Electron.IpcMain.On("loadLieferanten", async (arg) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                using var httpCliet = new HttpClient();
                //Console.WriteLine("geht");
                var requestlieferant = Task.Run(() =>
                {
                    return httpCliet.GetStringAsync("http://localhost:8080/lieferant");
                });
                //Console.WriteLine(requestlieferant);
                var json = JArray.Parse(await requestlieferant);
                Console.WriteLine("Json: Lieferant");
                Console.WriteLine(json);

                Electron.IpcMain.Send(mainWindow, "getLieferant-reply", json.ToString());
            });

            return View();
        }
        public IActionResult Material()
        {
            Electron.IpcMain.On("loadMaterialAndLieferanten", async (arg) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                using var httpCliet = new HttpClient();
                //Console.WriteLine("geht");
                var requestlieferant = Task.Run(() =>
                {
                    return httpCliet.GetStringAsync("http://localhost:8080/lieferant");
                });
                var requestMaterial = Task.Run(() =>
                {
                    return httpCliet.GetStringAsync("http://localhost:8080/material");
                });
                //Console.WriteLine(requestlieferant);
                var json = JArray.Parse(await requestlieferant);
                var json2 = JArray.Parse(await requestMaterial);
                Console.WriteLine("Json: Lieferant");
                Console.WriteLine(json);
                Console.WriteLine("Json: Material");
                Console.WriteLine(json2);

                Electron.IpcMain.Send(mainWindow, "getloadMaterialAndLieferanten-reply", json.ToString(), json2.ToString());
            });
            return View();
        }
    }
}
