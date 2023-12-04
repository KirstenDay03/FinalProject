using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using FinalProject.Data;
using System.Xml.Linq;

namespace FinalProject.Services
{
    public class APIService
    {

        private HttpClient httpClient;
        protected virtual string BASE_ADDR => "http://127.0.0.1:8000";


        public APIService()
        {
            httpClient = new HttpClient();
        }

        public async Task<int> GetRecipeCount()
        {
            var apiResponse = await httpClient.GetFromJsonAsync<JsonElement>($"{BASE_ADDR}/count");
            return apiResponse.GetInt32();
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            var apiResponse = await httpClient.GetFromJsonAsync<JsonElement>($"{BASE_ADDR}/recipe/{id}");

            return new Recipe
            {
                Id = id,
                Name = apiResponse.GetProperty("name").GetString(),
                Cuisine = apiResponse.GetProperty("cuisine").GetString(),
                prepTime = apiResponse.GetProperty("prepTime").GetString(),
                cookTime = apiResponse.GetProperty("cookTime").GetString(),
                Ingredient1 = apiResponse.GetProperty("ingredient1").GetString(),
                Ingredient2 = apiResponse.GetProperty("ingredient2").GetString(),
                Ingredient3 = apiResponse.GetProperty("ingredient3").GetString(),
                Ingredient4 = apiResponse.GetProperty("ingredient4").GetString(),
                Ingredient5 = apiResponse.GetProperty("ingredient5").GetString(),
                Instruction1 = apiResponse.GetProperty("instruction1").GetString(),
                Instruction2 = apiResponse.GetProperty("instruction2").GetString(),
                Instruction3 = apiResponse.GetProperty("instruction3").GetString(),
                Instruction4 = apiResponse.GetProperty("instruction4").GetString(),
                Instruction5 = apiResponse.GetProperty("instruction5").GetString()
            };
        }

        public async Task PostRecipe(Recipe recipe)
        {
            try
            {
                string json = '{' + $"\"id\":\"{recipe.Id}\",\n" +
                                    $"\"name\":\"{recipe.Name}\",\n" +
                                    $"\"cuisine\":\"{recipe.Cuisine}\",\n" +
                                    $"\"prepTime\":\"{recipe.prepTime}\",\n" +
                                    $"\"cookTime\":\"{recipe.cookTime}\",\n" +
                                    $"\"ingredient1\":\"{recipe.Ingredient1}\",\n" +
                                    $"\"ingredient2\":\"{recipe.Ingredient2}\",\n" +
                                    $"\"ingredient3\":\"{recipe.Ingredient3}\",\n" +
                                    $"\"ingredient4\":\"{recipe.Ingredient4}\",\n" +
                                    $"\"ingredient5\":\"{recipe.Ingredient5}\",\n" +
                                    $"\"instruction1\":\"{recipe.Instruction1}\",\n" +
                                    $"\"instruction2\":\"{recipe.Instruction2}\",\n" +
                                    $"\"instruction3\":\"{recipe.Instruction3}\",\n" +
                                    $"\"instruction4\":\"{recipe.Instruction4}\",\n" +
                                    $"\"instruction5\":\"{recipe.Instruction5}\"" + '}';

                StringContent JsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                await httpClient.PostAsync($"{BASE_ADDR}/recipe", JsonContent);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Failed to post the Recipe. The Recipe may already exist. \n" + ex.Message);
            }
        }

        public async Task PutRecipe(int id, Recipe recipe)
        {
            try
            {
                string json = '{' + $"\"id\":\"{recipe.Id}\",\n" +
                                    $"\"name\":\"{recipe.Name}\",\n" +
                                    $"\"cuisine\":\"{recipe.Cuisine}\",\n" +
                                    $"\"prepTime\":\"{recipe.prepTime}\",\n" +
                                    $"\"cookTime\":\"{recipe.cookTime}\",\n" +
                                    $"\"ingredient1\":\"{recipe.Ingredient1}\",\n" +
                                    $"\"ingredient2\":\"{recipe.Ingredient2}\",\n" +
                                    $"\"ingredient3\":\"{recipe.Ingredient3}\",\n" +
                                    $"\"ingredient4\":\"{recipe.Ingredient4}\",\n" +
                                    $"\"ingredient5\":\"{recipe.Ingredient5}\",\n" +
                                    $"\"instruction1\":\"{recipe.Instruction1}\",\n" +
                                    $"\"instruction2\":\"{recipe.Instruction2}\",\n" +
                                    $"\"instruction3\":\"{recipe.Instruction3}\",\n" +
                                    $"\"instruction4\":\"{recipe.Instruction4}\",\n" +
                                    $"\"instruction5\":\"{recipe.Instruction5}\"" + '}';

                StringContent JsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                await httpClient.PutAsync($"{BASE_ADDR}/recipe/{id}", JsonContent);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Failed to update recipe. \n" + ex.Message);
            }
        }

        public async Task UpdateName(int id, string name)
        {
            string jsonName = $"{{\"name\": \"{name}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/name?name={name}", JsonContentName);

        }

        public async Task UpdateCuisine(int id, string cuisine)
        {
            string jsonName = $"{{\"cuisine\": \"{cuisine}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/cuisine?cuisine={cuisine}", JsonContentName);
        }

        public async Task UpdatePrepTime(int id, string prep)
        {
            string jsonName = $"{{\"prepTime\": \"{prep}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/prepTime?prepTime={prep}", JsonContentName);
        }

        public async Task UpdateCookTime(int id, string cook)
        {
            string jsonName = $"{{\"cookTime\": \"{cook}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/cookTime?cookTime={cook}", JsonContentName);
        }

        public async Task UpdateIngredient1(int id, string i)
        {
            string jsonName = $"{{\"ingredient1\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/ingredient1?ingredient1={i}", JsonContentName);
        }

        public async Task UpdateIngredient2(int id, string i)
        {
            string jsonName = $"{{\"ingredient2\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/ingredient2?ingredient2={i}", JsonContentName);
        }

        public async Task UpdateIngredient3(int id, string i)
        {
            string jsonName = $"{{\"ingredient3\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/ingredient3?ingredient3={i}", JsonContentName);
        }

        public async Task UpdateIngredient4(int id, string i)
        {
            string jsonName = $"{{\"ingredient4\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/ingredient4?ingredient4={i}", JsonContentName);
        }

        public async Task UpdateIngredient5(int id, string i)
        {
            string jsonName = $"{{\"ingredient5\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/ingredient5?ingredient5={i}", JsonContentName);
        }

        public async Task UpdateInstruction1(int id, string i)
        {
            string jsonName = $"{{\"instruction1\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/instruction1?instruction1={i}", JsonContentName);
        }

        public async Task UpdateInstruction2(int id, string i)
        {
            string jsonName = $"{{\"instruction2\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/instruction2?instruction2={i}", JsonContentName);
        }

        public async Task UpdateInstruction3(int id, string i)
        {
            string jsonName = $"{{\"instruction3\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/instruction3?instruction3={i}", JsonContentName);
        }

        public async Task UpdateInstruction4(int id, string i)
        {
            string jsonName = $"{{\"instruction4\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/instruction4?instruction4={i}", JsonContentName);
        }

        public async Task UpdateInstruction5(int id, string i)
        {
            string jsonName = $"{{\"instruction5\": \"{i}\" }}";

            StringContent JsonContentName = new StringContent(jsonName, Encoding.UTF8, "application/json");

            var results = await httpClient.PatchAsync($"{BASE_ADDR}/recipe/{id}/instruction5?instruction5={i}", JsonContentName);
        }

        public async Task DeleteRecipe(int id)
        {
            await httpClient.DeleteAsync($"{BASE_ADDR}/recipe/{id}");
        }
    }
}
