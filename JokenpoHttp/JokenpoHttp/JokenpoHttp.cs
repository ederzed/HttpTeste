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
        public int player = 0;
        public string jogada = "";
        public Bitmap jp1;
        public Bitmap jp2;
        private void btnConectar_Click(object sender, EventArgs e)
        {
            Jogo jogo = getHttp();
            if(jogo.player1 == "-")
            {
                putHttp("conectado", jogo.player2, jogo.confirmado);
                lblResultado.Text = "Conectado como player 1 (esquerda), aguardando oponente";
                player = 1;
            }
            else if(jogo.player2 == "-")
            {
                putHttp(jogo.player1, "conectado", jogo.confirmado);
                lblResultado.Text = "Conectado como player 2 (direita)";
                player = 2;
            }
            else
            {
                lblResultado.Text = "Sala cheia, por favor tente mais tarde";
            }
            btnConectar.Enabled = false;
            btnPapel.Enabled = true;
            btnPedra.Enabled = true;
            btnTesoura.Enabled = true;

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
            player = 0;
            jogada = "";
            lblResultado.Text = "Aguardando conexão";
            jp1 = null;
            jp2 = null;
        }

        private void btnPedra_Click(object sender, EventArgs e)
        {
            jogada = "pedra";
            if (player == 1)
            {
                jp1 = Properties.Resources.pedra;
                pbPlayer1.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPlayer1.Image = jp1;
            }
            else
            {
                jp2 = Properties.Resources.pedraop;
                pbPlayer2.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPlayer2.Image = jp2;
            }
            lblResultado.Text = "Pedra. Clique em confirmar para enviar";
            btnConfirma.Enabled = true;
        }

        private void btnPapel_Click(object sender, EventArgs e)
        {
            jogada = "papel";
            if (player == 1)
            {
                jp1 = Properties.Resources.papel;
                pbPlayer1.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPlayer1.Image = jp1;
            }
            else
            {
                jp2 = Properties.Resources.papelop;
                pbPlayer2.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPlayer2.Image = jp2;
            }
            lblResultado.Text = "Papel. Clique em confirmar para enviar";
            btnConfirma.Enabled = true;
        }

        private void btnTesoura_Click(object sender, EventArgs e)
        {
            jogada = "tesoura";
            if (player == 1)
            {
                jp1 = Properties.Resources.tesoura;
                pbPlayer1.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPlayer1.Image = jp1;
            }
            else
            {
                jp2 = Properties.Resources.tesouraop;
                pbPlayer2.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPlayer2.Image = jp2;
            }
            lblResultado.Text = "Tesoura. Clique em confirmar para enviar";
            btnConfirma.Enabled = true;
        }

        private void btnConfirma_Click(object sender, EventArgs e)
        {
            Jogo jogo = getHttp();
            if(player == 1)
            {
                putHttp(jogada, jogo.player2, jogo.confirmado + 1);
            }
            else
            {
                putHttp(jogo.player1, jogada, jogo.confirmado + 1);
            }
            lblResultado.Text = "Jogada confirmada. Aguardando Oponente";
            btnPapel.Enabled = false;
            btnPedra.Enabled = false;
            btnTesoura.Enabled = false;
            btnConfirma.Enabled = false;
            tmResultado.Enabled = true;
        }

        private void tmResultado_Tick(object sender, EventArgs e)
        {
            Jogo result = getHttp();
            if(result.confirmado == 3)
            {
                putHttp("-", "-", 0);
                player = 0;
                jogada = "";
                lblResultado.Text = "Aguardando conexão";
                jp1 = null;
                jp2 = null;
                btnConectar.Enabled = true;
                btnPapel.Enabled = false;
                btnPedra.Enabled = false;
                btnTesoura.Enabled = false;
                btnConfirma.Enabled = false;
                tmResultado.Enabled = false;
            }
            if(result.confirmado == 2)
            {
                if(player == 1)
                {
                    if(result.player2 == "tesoura")
                    {
                        jp2 = Properties.Resources.tesouraop;
                        pbPlayer2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbPlayer2.Image = jp2;
                    }
                    if (result.player2 == "pedra")
                    {
                        jp2 = Properties.Resources.pedraop;
                        pbPlayer2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbPlayer2.Image = jp2;
                    }
                    if (result.player2 == "papel")
                    {
                        jp2 = Properties.Resources.papelop;
                        pbPlayer2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbPlayer2.Image = jp2;
                    }

                }
                else
                {
                    if (result.player1 == "tesoura")
                    {
                        jp1 = Properties.Resources.tesoura;
                        pbPlayer1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbPlayer1.Image = jp1;
                    }
                    if (result.player1 == "pedra")
                    {
                        jp1 = Properties.Resources.pedra;
                        pbPlayer1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbPlayer1.Image = jp1;
                    }
                    if (result.player1 == "papel")
                    {
                        jp1 = Properties.Resources.papel;
                        pbPlayer1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbPlayer1.Image = jp1;
                    }

                }
                if(checarResultado(result) == 0)
                {
                    lblResultado.Text = "Você perdeu. Aguarde para jogar novamente";
                }
                if (checarResultado(result) == 1)
                {
                    lblResultado.Text = "Você venceu. Aguarde para jogar novamente";
                }
                if (checarResultado(result) == 2)
                {
                    lblResultado.Text = "Empate. Aguarde para jogar novamente";
                }
                putHttp("-","-",3);
            }
        }
        public int checarResultado(Jogo resul)
        {
            if(player == 1)
            {
                if (jogada == resul.player2)
                    return 2;
                else if (jogada == "pedra" && resul.player2 == "papel")
                    return 0;
                else if (jogada == "papel" && resul.player2 == "tesoura")
                    return 0;
                else if (jogada == "tesoura" && resul.player2 == "pedra")
                    return 0;
                else
                    return 1;
            }
            else
            {
                if (jogada == resul.player1)
                    return 2;
                else if (jogada == "pedra" && resul.player1 == "papel")
                    return 0;
                else if (jogada == "papel" && resul.player1 == "tesoura")
                    return 0;
                else if (jogada == "tesoura" && resul.player1 == "pedra")
                    return 0;
                else
                    return 1;
            }
        }
    }

    public class Jogo
    {

        public string player1 { get; set; }
        public string player2 { get; set; }
        public int confirmado { get; set; }
    }
}
