using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace JokenpoHttp
{
    public partial class JokenpoHttp : Form
    {
        public JokenpoHttp()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Jogo jogo = getHttp();
            if(jogo.player1 == "-")
            {
                putHttp("conectado", jogo.player2, 0);
                lblResultado.Text = "Conectado como player 1 (esquerda), aguardando oponente";
            }
            else if(jogo.player2 == "-")
            {
                putHttp(jogo.player1, "conectado", 0);
                lblResultado.Text = "Conectado como player 2 (direita)";
            }
            else
            {
                
            }
            
            
        }

        public void putHttp(string p1, string p2, int c)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.myjson.com/bins/v5ck6");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Jogo jogo = new Jogo()
                {
                    player1 = p1,
                    player2 = p2,
                    confirmado = c
                };
                string json = JsonConvert.SerializeObject(jogo);

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();

            }
        }

        public Jogo getHttp()
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://api.myjson.com/bins/v5ck6");
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                Jogo j  = JsonConvert.DeserializeObject<Jogo>(objResponse.ToString());

                

                streamDados.Close();
                resposta.Close();

                return j;
            }
   
        }

        private void lblResultado_Click(object sender, EventArgs e)
        {
            putHttp("-", "-", 0);
        }
    }

    public class Jogo
    {

        public string player1 { get; set; }
        public string player2 { get; set; }
        public int confirmado { get; set; }
    }
}
