using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CarDealershipCapstonePartII.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipCapstonePartII.Controllers
{
    public class CarController : Controller
    {
        private readonly HttpClient _client;

        public CarController(IHttpClientFactory client)
        {
            _client = client.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44318/");
        }
        [HttpGet]
        public IActionResult SearchCars()
        {
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchCars(Car car)
        {
            string carCar = $"api/Car/search?make={car.make}&model={car.model}&color={car.color}&year={car.year}";

            var response = await _client.GetAsync(carCar);
            var results = await response.Content.ReadAsAsync<List<Car>>();

            return View(results);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(Car car)
        {
            var something = await _client.PostAsJsonAsync("api/Car", car);
      
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditCar(int id)
        {
            var response = await _client.GetAsync($"api/car/{id}");
            Car car = await response.Content.ReadAsAsync<Car>();

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> EditCar(int id, Car car)
        {
            var something = await _client.PutAsJsonAsync($"api/Car/{id}", car);
            
            return RedirectToAction("Index", "Home");
        }
    }
}