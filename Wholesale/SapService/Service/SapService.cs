﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wholesale.Server.Service
{
    public class SapService
    {
        private readonly HttpClient _client;
        private const string Username = "manager";
        private const string Password = "2023";
        private const string Company = "SBO_GT_FFACSA";

        public SapService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> LoginAsync()
        {
            var loginData = new
            {
                CompanyDB = Company,
                UserName = Username,
                Password = Password
            };

            var response = await _client.PostAsync("Login", new StringContent(
                JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(content);
                return result.SessionId;
            }

            throw new Exception("Error al iniciar sesión en SAP.");
        }

        public async Task LogoutAsync(string sessionId)
        {
            _client.DefaultRequestHeaders.Remove("Cookie");
            _client.DefaultRequestHeaders.Add("Cookie", $"B1SESSION={sessionId}");
            await _client.PostAsync("Logout", null);
        }

        public async Task<string> GetClientAsync(string filter, string sessionId)
        {
            _client.DefaultRequestHeaders.Remove("Cookie");
            _client.DefaultRequestHeaders.Add("Cookie", $"B1SESSION={sessionId}");

            var response = await _client.GetAsync($"BusinessPartners?$filter={filter}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
