using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoboCep
{
    static class Cep
    {

        public static async void ProcessarDadosCep(string numeroCep, string arquivo)
        {
            var client =
                new RestClient("http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaCepEndereco.cfm");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "CFID=Z16c97xp1nfat85vel35thx6qc0x7tyryg54wwfrh1nlek21mq-204782035; CFTOKEN=Z16c97xp1nfat85vel35thx6qc0x7tyryg54wwfrh1nlek21mq-f7699528b528b41d-456C7A5F-ACE8-5F29-FDA78901C66FD614; JSESSIONID=D9112780B0C10A8B8B68CB3F90FC7C39.cfusion01; ssvbr0327_buscacep=sac2095_cep; _op_aixPageId=a2_872857bd-55ba-4ae7-896c-d253ec607c0b; CFGLOBALS=urltoken%3DCFID%23%3D204782035%26CFTOKEN%23%3Df7699528b528b41d%2D456C7A5F%2DACE8%2D5F29%2DFDA78901C66FD614%26jsessionid%23%3DD9112780B0C10A8B8B68CB3F90FC7C39%2Ecfusion01%23lastvisit%3D%7Bts%20%272020%2D07%2D15%2015%3A32%3A35%27%7D%23hitcount%3D6%23timecreated%3D%7Bts%20%272020%2D07%2D15%2015%3A30%3A21%27%7D%23cftoken%3Df7699528b528b41d%2D456C7A5F%2DACE8%2D5F29%2DFDA78901C66FD614%23cfid%3D204782035%23; CFGLOBALS=urltoken%3DCFID%23%3D204782035%26CFTOKEN%23%3Df7699528b528b41d%2D456C7A5F%2DACE8%2D5F29%2DFDA78901C66FD614%26jsessionid%23%3DD9112780B0C10A8B8B68CB3F90FC7C39%2Ecfusion01%23lastvisit%3D%7Bts%20%272020%2D07%2D15%2015%3A32%3A35%27%7D%23hitcount%3D6%23timecreated%3D%7Bts%20%272020%2D07%2D15%2015%3A30%3A21%27%7D%23cftoken%3Df7699528b528b41d%2D456C7A5F%2DACE8%2D5F29%2DFDA78901C66FD614%23cfid%3D204782035%23");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("relaxation", numeroCep);
            request.AddParameter("tipoCEP", "LOG");
            request.AddParameter("semelhante", "N");
            IRestResponse response = client.Execute(request);
            var conteudo = response.Content;

            var htmldocument = new HtmlDocument();

            htmldocument.LoadHtml(conteudo);

            if (conteudo.Contains("http://www.buscacep.correios.com.br/robo.html"))
            {
                Thread.Sleep(600000) ;
                return;
            }


            var divs = htmldocument.DocumentNode.Descendants("div")
                .Where(n => n.GetAttributeValue("Class", "").Equals("ctrlcontent")).ToList();

            string[] dados = new string[4];


            var CepNaoEncontrado = htmldocument.DocumentNode.SelectNodes("/html/body/div[1]/div[3]/div[2]/div/div/div[2]/div[2]/div[2]/p").First().InnerText.Equals("DADOS NAO ENCONTRADOS");

            if (CepNaoEncontrado)
                return;

            foreach (HtmlNode row in htmldocument.DocumentNode.SelectNodes("//table[@class='tmptabela']//tr"))
            {
                int i = 0;

                foreach (HtmlNode col in row.SelectNodes("//td"))
                {
                    if (i > dados.Length - 1)
                        continue;

                    WebUtility.HtmlDecode(dados[i] = col.InnerText.Replace("&nbsp;", ""));
                    i++;
                }

            }

            dados[0] = dados[0] == "" ? "Sem registro" : dados[0];
            dados[1] = dados[1] == "" ? "Sem registro" : dados[1];
            dados[2] = dados[2] == "" ? "Sem registro" : dados[2];
            dados[3] = dados[3] == "" ? "Sem registro" : dados[3];

            EscreverResultadoCep(arquivo, dados);

        }

        static async void EscreverResultadoCep(string arquivo, string[] dados)
        {
            using (StreamWriter stream = File.AppendText(arquivo))
            {
                stream.WriteLine($"{dados[0]};{dados[1]};{dados[2]};{dados[3]};{DateTime.Now}");
            }
        }

    }
}
