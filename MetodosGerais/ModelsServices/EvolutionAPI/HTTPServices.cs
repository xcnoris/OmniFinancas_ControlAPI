using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Modelos.DTOs.EvolutionAPI;
using Newtonsoft.Json;

namespace MetodosGerais.ModelsServices.EvolutionAPI
{
    public class HTTPServices
    {
    
        // Metodo responsavel apenas para enviar Mensagem para o cliente
        public static async Task<bool> EnviarMensagemViaAPI(DTOEnviarMensagem Request)
        {
            using (HttpClient client = ConfigurarHttpClient(Request.Token))
            {
                // Definir URL do endpoint da Evolution API
                string url = $"https://n8n-evolution-api.usbaxy.easypanel.host/message/sendText/{Request.Instancia}";

                try
                {
                    // Executando metodo Http Post
                    HttpContent content = CriarConteudoJson(new DTORequestMensagem() { number= Request.Numero, text = Request.Mensagem});
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private static HttpClient ConfigurarHttpClient(string token)
        {
            var client = new HttpClient();
            // Correto: adiciona a chave apikey no header
            client.DefaultRequestHeaders.Add("apikey", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private static HttpContent CriarConteudoJson(object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
