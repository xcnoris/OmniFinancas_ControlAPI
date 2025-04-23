using Azure.Core;
using DataBase.Data;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Modelos.DTOs.PlanoLicenca;
using Modelos.EF.Lincenca;
using Modelos.Enuns;

namespace API_Central.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoletoPDFController : ControllerBase
    {
        private readonly string _diretorioBoletos;
        private readonly DAL<LicencaModel> _dalLicenca;

        public BoletoPDFController(
            DAL<LicencaModel> dalLicenca)
        {
            _dalLicenca = dalLicenca;
            _diretorioBoletos = Environment.GetEnvironmentVariable("DIRETORIO_BOLETOS") ?? "Boletos";

            if (!Directory.Exists(_diretorioBoletos))
                Directory.CreateDirectory(_diretorioBoletos);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody] UploadPDFRequest request)
        {
            var licencaExistente = await _dalLicenca.BuscarPorAsync(l => l.ChaveAtivacao == request.Licenca);

            if (licencaExistente is null) return NotFound("Licença não encontrada.");
            if (licencaExistente.Situacao != SituacaoLicenca.Ativa) return BadRequest("Licença não está ativa.");
            if (licencaExistente.EnderecoMac != request.EnderecoMAC) return BadRequest("Endereço Mac Errado!");


            try
            {
                // Verifica se a string Base64 é válida
                if (string.IsNullOrWhiteSpace(request.Base64))
                    return BadRequest("A string Base64 não pode estar vazia.");

                byte[] pdfBytes;
                try
                {
                    pdfBytes = Convert.FromBase64String(request.Base64);
                }
                catch (FormatException)
                {
                    return BadRequest("Formato Base64 inválido.");
                }

                // Formatar a data corretamente (yyyyMMddHHmmss)
                string formattedDate = DateTime.Now.ToString("dd-MM-yyyyHHmmss");

                Random random = new Random();
                // Criar o nome do arquivo (Removendo espaços desnecessários)
                string safeFileName = $"{request.CNPJ}_{formattedDate}_{request.FileName}_{random.Next(0,999)}".Replace(" ", "_");

                // Caminho completo do arquivo
                string filePath = Path.Combine(_diretorioBoletos, safeFileName);

                // Salvar o arquivo
                await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);

                // Gerar a URL correta para acesso
                string fileUrl = $"{Request.Scheme}://{Request.Host}/files/{safeFileName}";

                return Ok(new { message = "Upload realizado com sucesso!", url = fileUrl });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao salvar arquivo: {ex.Message}");
            }
        }
    }
}
