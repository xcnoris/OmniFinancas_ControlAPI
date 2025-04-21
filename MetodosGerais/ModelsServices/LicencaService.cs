using System.Security.Cryptography;
using System.Text;
using Modelos.DTOs.NumeroContrato.InPut;
using Modelos.DTOs.PlanoLicenca;
using Modelos.EF.Contrato;
using Modelos.EF.Lincenca;

namespace MetodosGerais.ModelsServices
{
    public class LicencaService
    {
        private const string _chavePrivada = "Minhachaveultrasecret1434tjn~];.4O5a";

        public static string GerarChaveAtivacao(int contratoId)
        {
            var parteAleatoria = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();

            // Opcional: usar contratoId e chave privada para criar um hash
            string dados = $"{contratoId}-{parteAleatoria}-{_chavePrivada}";
            string hash = GerarHashSHA256(dados).Substring(0, 10).ToUpper();

            return $"LIC-{parteAleatoria}-{hash}";
        }

        private static string GerarHashSHA256(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public static IEnumerable<DTOLicenca> InstanciarListaLicenca(IEnumerable<LicencaModel> ListLicenca)
        {
            return ListLicenca.Select(DTO_Licenca => new DTOLicenca
            {
                Id = DTO_Licenca.Id,
                ChaveAtivacao = DTO_Licenca.ChaveAtivacao,
                ContratoId = DTO_Licenca.ContratoId,
                Situacao = DTO_Licenca.Situacao,
            }).ToList();
        }
        public static DTOLicenca InstanciarDTOLicenca(LicencaModel Licenca)
        {
            return new DTOLicenca()
            {
                Id = Licenca.Id,
                ChaveAtivacao = Licenca.ChaveAtivacao,
                ContratoId = Licenca.ContratoId,
                Situacao = Licenca.Situacao,
            };
        }

        public static LicencaModel InstanciarNumeroContrato(LicencaModel existente, DTOLicenca DTOLicenca)
        {
            existente.ChaveAtivacao = DTOLicenca.ChaveAtivacao;
            existente.ContratoId = DTOLicenca.ContratoId;
            existente.Situacao = DTOLicenca.Situacao;
            existente.DataAtualizacao = DateTime.Now;

            return existente;
        }
    }
}
