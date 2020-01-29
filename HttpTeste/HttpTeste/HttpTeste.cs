using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace HttpTeste
{
    
    public partial class HttpTeste : Form
    {
        public HttpTeste()
        {
            InitializeComponent();
        }
        public static string resp = "";
        private void btnEnviar_Click(object sender, EventArgs e)
        {

            //string dadosPOST = "id=2";
            //dadosPOST = dadosPOST + "&valor=" + txtRequest.Text;


            //var dados = Encoding.UTF8.GetBytes(dadosPOST);

            //var requisicaoWeb = WebRequest.CreateHttp("https://api.myjson.com/bins/1fkh2e");

            //requisicaoWeb.Method = "PUT";
            //requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
            //requisicaoWeb.ContentLength = dados.Length;
            //requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            ////precisamos escrever os dados post para o stream
            //using (var stream = requisicaoWeb.GetRequestStream())
            //{
            //    stream.Write(dados, 0, dados.Length);
            //    stream.Close();
            //}
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.myjson.com/bins/1fkh2e");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Mensagem mensagem = new Mensagem()
                {
                    id = 2,
                    valor = txtRequest.Text
                };
                string json = JsonConvert.SerializeObject(mensagem);

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();

            }


            txtRequest.Text = "Mensagem arquivada";
        }
    
        private void btnReceber_Click(object sender, EventArgs e)
        {
            //EnviaRequisicaoPOST();
            //txtRequest.Text = resp;
            var requisicaoWeb = WebRequest.CreateHttp("https://api.myjson.com/bins/1fkh2e");
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                Mensagem mensagenArquivada = JsonConvert.DeserializeObject<Mensagem>(objResponse.ToString());

                txtRequest.Text = mensagenArquivada.valor;

                streamDados.Close();
                resposta.Close();
            }
            
            
        }
    }
    public class Post
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public class Mensagem
    {
       
        public int id { get; set; }
        public string valor { get; set; }
    }
}
