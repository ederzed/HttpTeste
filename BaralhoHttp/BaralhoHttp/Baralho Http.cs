﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace BaralhoHttp
{
   
    public partial class BaralhoHttp : Form
    {
        public BaralhoHttp()
        {
            InitializeComponent();
            pbBaralho.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta2.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta3.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta4.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta5.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta6.SizeMode = PictureBoxSizeMode.StretchImage;
            pbJogada1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbJogada2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // VARIAVEIS




        Jogador jooj = new Jogador();
        public Deck deck = new Deck();
        public Dados dados = new Dados();
        public Image j1 = null;
        public Image j2 = null;
        public Carta jog1;
        public Carta jog2;
        public Carta defineManilia;
        public List<Carta> maos;
        public String linkBaralho = "https://api.myjson.com/bins/1412o2";
        public String linkDados = "https://api.myjson.com/bins/macua";






        //SÓ MÉTODOS






        
        public void putBaralhoHttp(Deck d)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(linkBaralho);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                
                string json = JsonConvert.SerializeObject(d);

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();

            }
        }

        public Deck getBaralhoHttp()
        {
            var requisicaoWeb = WebRequest.CreateHttp(linkBaralho);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                Deck d = JsonConvert.DeserializeObject<Deck>(objResponse.ToString());



                streamDados.Close();
                resposta.Close();

                return d;
            }

        }

        public void putDadosHttp(string jN, string jS, string jL, string jO, string mani, int ponH, int ponV, int valor)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(linkDados);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Dados d = new Dados()
                {
                    jogadorN = jN,
                    jogadorS = jS,
                    jogadorL = jL,
                    jogadorO = jO,
                    manilha = mani,
                    pontosH = ponH,
                    pontosV = ponV,
                    valorRodada = valor
                };

                string json = JsonConvert.SerializeObject(d);

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();

            }
        }

        public Dados getDadosHttp()
        {
            var requisicaoWeb = WebRequest.CreateHttp(linkDados);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                Dados d = JsonConvert.DeserializeObject<Dados>(objResponse.ToString());



                streamDados.Close();
                resposta.Close();

                return d;
            }

        }
        public void conectar()
        {
            Dados data = getDadosHttp();
            if(data.jogadorS == "-")
            {
                jooj.setJogador("S-Vira");
                putDadosHttp(data.jogadorN, "Conectado", data.jogadorL, data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada);
                lblResultado.Text = "Conectado como jogador Sul";
            }
            else if(data.jogadorN == "-")
            {
                jooj.setJogador("N");
                putDadosHttp("Conectado", data.jogadorS, data.jogadorL, data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada);
                lblResultado.Text = "Conectado como jogador Norte";
            }
            else if (data.jogadorL == "-")
            {
                jooj.setJogador("L");
                putDadosHttp(data.jogadorN, data.jogadorS, "Conectado", data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada);
                lblResultado.Text = "Conectado como jogador Leste";
            }
            else if (data.jogadorO == "-")
            {
                jooj.setJogador("O");
                putDadosHttp(data.jogadorN, data.jogadorS, data.jogadorL, "Conectado", data.manilha, data.pontosH, data.pontosV, data.valorRodada);
                lblResultado.Text = "Conectado como jogador Oeste";
            }
        }

        public string cartaToString(Carta c)
        {
            string val = c.numero + "/" + c.naipe;

            return val;
        }
        public Carta stringToCarta(string c)
        {
            string[] icon = c.Split('/');
            Carta val = new Carta()
            {
                numero = icon[0],
                naipe = icon[1]
            };
            return val;
        }
        public byte[] imgToByteArray(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                return mStream.ToArray();
            }
        }

        public void reporCarta1()
        {
            if (imgToByteArray(pbCarta1.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta1.Image = j1;
            if (imgToByteArray(pbCarta2.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta2.Image = j1;
            if (imgToByteArray(pbCarta3.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta3.Image = j1;
            

        }
        public void reporCarta2()
        {
            if (imgToByteArray(pbCarta4.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta4.Image = j2;
            if (imgToByteArray(pbCarta5.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta5.Image = j2;
            if (imgToByteArray(pbCarta6.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta6.Image = j2;
        }
        public Image escolheCarta(Carta c)
        {
            Image[] imagens = { Properties.Resources.As_de_ouro, Properties.Resources._2_de_ouro, Properties.Resources._3_de_ouro,
            Properties.Resources._4_de_ouro,Properties.Resources._5_de_ouro,Properties.Resources._6_de_ouro,Properties.Resources._7_de_ouro,
            Properties.Resources.dama_de_ouro,
            Properties.Resources.valete_de_ouros,Properties.Resources.rei_de_ouro,Properties.Resources.As_de_espada, Properties.Resources._2_de_espada, Properties.Resources._3_de_espada,
            Properties.Resources._4_de_espada,Properties.Resources._5_de_espada,Properties.Resources._6_de_espada,Properties.Resources._7_de_espada,
            Properties.Resources.dama_de_espada,
            Properties.Resources.valete_de_espada,Properties.Resources.rei_de_espada,Properties.Resources.As_de_copas, Properties.Resources._2_de_copas, Properties.Resources._3_de_copas,
            Properties.Resources._4_de_copas,Properties.Resources._5_de_copas,Properties.Resources._6_de_copas,Properties.Resources._7_de_copas,
            Properties.Resources.dama_de_copas,
            Properties.Resources.valete_de_copas,Properties.Resources.rei_de_copas,Properties.Resources.As_de_paus, Properties.Resources._2_de_paus, Properties.Resources._3_de_paus,
            Properties.Resources._4_de_paus,Properties.Resources._5_de_paus,Properties.Resources._6_de_paus,Properties.Resources._7_de_paus,
            Properties.Resources.dama_de_paus,
            Properties.Resources.valete_de_paus,Properties.Resources.rei_de_paus};
            int nai = 0;
            int num = 0;

            try
            {
                num = Convert.ToInt32(c.numero);
            }
            catch
            {
                if (c.numero == "Ás")
                    num = 1;
                if (c.numero == "Rei")
                    num = 10;
                if (c.numero == "Dama")
                    num = 8;
                if (c.numero == "Valete")
                    num = 9;

            }

            if (c.naipe == "Ouro")
                nai = 0;
            if (c.naipe == "Espada")
                nai = 1;
            if (c.naipe == "Copas")
                nai = 2;
            if (c.naipe == "Paus")
                nai = 3;



            return imagens[(num + (nai * 10)) - 1];
        }

        public string verVencedor(Carta j1, Carta j2, Carta manilha)
        {
            // 4 - 7 ,Q ,J ,K ,A ,2 ,3
            int c1 = valorReal(j1, manilha);
            int c2 = valorReal(j2, manilha);
            int[] cartasEmJogo = { c1, c2 };
            Array.Sort(cartasEmJogo);
            if (cartasEmJogo[cartasEmJogo.Length - 1] == cartasEmJogo[cartasEmJogo.Length - 2])
                return "Amarrou";

            if (cartasEmJogo[cartasEmJogo.Length - 1] == c1)
                return "Jogador Sul Venceu";
            else if (cartasEmJogo[cartasEmJogo.Length - 1] == c2)
                return "Jogador Norte Venceu";
            else
                return "Erro!";

        }
        public int valorReal(Carta carta, Carta mani)
        {
            int valor = 0;
            int manilhona = 0;
            try
            {
                if (carta.numero == "2")
                    valor = 12;
                else if (carta.numero == "3")
                    valor = 13;
                else
                    valor = Convert.ToInt32(carta.numero);
            }
            catch
            {
                if (carta.numero == "Ás")
                    valor = 11;
                if (carta.numero == "Rei")
                    valor = 10;
                if (carta.numero == "Dama")
                    valor = 8;
                if (carta.numero == "Valete")
                    valor = 9;

            }
            try
            {
                if (mani.numero == "2")
                    manilhona = 13;
                else if (mani.numero == "3")
                    manilhona = 4;
                else
                    manilhona = Convert.ToInt32(mani.numero) + 1;
            }
            catch
            {
                if (mani.numero == "Ás")
                    manilhona = 12;
                if (mani.numero == "Rei")
                    manilhona = 11;
                if (mani.numero == "Dama")
                    manilhona = 9;
                if (mani.numero == "Valete")
                    manilhona = 10;

            }
            if (valor == manilhona)
            {
                valor += 100;

                if (carta.naipe == "Ouro")
                    valor++;
                else if (carta.naipe == "Espada")
                    valor += 2;
                else if (carta.naipe == "Copas")
                    valor += 3;
                else if (carta.naipe == "Paus")
                    valor = 9999;
            }

            return valor;
        }







        // EVENTOS DO FORM







        private void Form1_Load(object sender, EventArgs e)
        {
           // putDadosHttp("-", "-", "-", "-", "-", 0, 0, 1);
            //putBaralhoHttp(deck);
            conectar();
            dados = getDadosHttp();
            if (jooj.getJogador().EndsWith("Vira"))
            {
                deck = new Deck();
                deck.Shuffle();
                defineManilia = deck.GoFish();
                putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, cartaToString(defineManilia), dados.pontosH, dados.pontosV, dados.valorRodada);
            }  
            else
                deck = getBaralhoHttp();

            deck.Shuffle();
            List<Carta> mao = deck.GoFish(3);
            maos = mao;

            // CHECAR SE AS CARTAS E AS IMGS BATEM
            //List<string> teste = new List<string>();
            //for(int i = 0; i< mao.Count; i++)
            //{
            //    teste.Add(mao[i].numero + " de " + mao[i].naipe);
            //}
            //String m = String.Join(";", teste);
            //MessageBox.Show(m);
            
            putBaralhoHttp(deck);
            
            pbBaralho.Image = escolheCarta(defineManilia);
            if(jooj.getJogador().StartsWith("S"))
            {
                pbCarta1.Image = escolheCarta(mao[0]);
                pbCarta2.Image = escolheCarta(mao[1]);
                pbCarta3.Image = escolheCarta(mao[2]);
                pbCarta1.Enabled = true;
                pbCarta2.Enabled = true;
                pbCarta3.Enabled = true;
            }
            if (jooj.getJogador().StartsWith("N"))
            {
                pbCarta4.Image = escolheCarta(mao[0]);
                pbCarta5.Image = escolheCarta(mao[1]);
                pbCarta6.Image = escolheCarta(mao[2]);
                pbCarta4.Enabled = true;
                pbCarta5.Enabled = true;
                pbCarta6.Enabled = true;
            }
            
        }

       
        private void pbCarta1_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada1.Image = pbCarta1.Image;
            reporCarta1();
            pbCarta1.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[0];
            putDadosHttp(dados.jogadorN, cartaToString(jog1), dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada);
            j1 = pbJogada1.Image;
        }

        private void pbCarta2_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada1.Image = pbCarta2.Image;
            reporCarta1();
            pbCarta2.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[1];
            putDadosHttp(dados.jogadorN, cartaToString(jog1), dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada);
            j1 = pbJogada1.Image;
        }

        private void pbCarta3_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada1.Image = pbCarta3.Image;
            reporCarta1();
            pbCarta3.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[2];
            putDadosHttp(dados.jogadorN, cartaToString(jog1), dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada);
            j1 = pbJogada1.Image;
        }

       

        private void pbCarta4_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada2.Image = pbCarta4.Image;
            reporCarta2();
            pbCarta4.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[0];
            putDadosHttp(cartaToString(jog2), dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada);
            j2 = pbJogada2.Image;
        }

        private void pbCarta5_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada2.Image = pbCarta5.Image;
            reporCarta2();
            pbCarta5.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[1];
            putDadosHttp(cartaToString(jog2), dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada);
            j2 = pbJogada2.Image;
        }

        private void pbCarta6_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada2.Image = pbCarta6.Image;
            reporCarta2();
            pbCarta6.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[2];
            putDadosHttp(cartaToString(jog2), dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada);
            j2 = pbJogada2.Image;
        }

        private void tmrResultado_Tick(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            jooj = new Jogador();
            if (jooj.getJogador().StartsWith("N") && dados.jogadorS.Contains('/'))
                pbJogada1.Image = escolheCarta(stringToCarta(dados.jogadorS));
            if (jooj.getJogador().StartsWith("S") && dados.jogadorN.Contains('/'))
                pbJogada2.Image = escolheCarta(stringToCarta(dados.jogadorN));
            if (dados.jogadorN.Contains('/') && dados.jogadorS.Contains('/'))
            {
                lblResultado.Text = verVencedor(stringToCarta(dados.jogadorS), stringToCarta(dados.jogadorN), stringToCarta(dados.manilha));
            } 
        }

        private void BaralhoHttp_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.R)
            {
                putDadosHttp("-", "-", "-", "-", "-", 0, 0, 1);
            }
        }

        private void BaralhoHttp_Deactivate(object sender, EventArgs e)
        {
            Dados data = getDadosHttp();
            Jogador j = new Jogador();
            if (j.getJogador().StartsWith("N"))
            {
                
                putDadosHttp("-", data.jogadorS, data.jogadorL, data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada);
                
            }
            else if (j.getJogador().StartsWith("S"))
            {

                putDadosHttp(data.jogadorN, "-", data.jogadorL, data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada);

            }
            else if (j.getJogador().StartsWith("L"))
            {

                putDadosHttp(data.jogadorN, data.jogadorS, "-", data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada);

            }
            else if (j.getJogador().StartsWith("O"))
            {

                putDadosHttp(data.jogadorN, data.jogadorS, data.jogadorL, "-", data.manilha, data.pontosH, data.pontosV, data.valorRodada);

            }
        }
    }













    // OUTRAS CLASSES













    public class Jogador
    {
        public static string jogador = "";
        public string getJogador()
        {
            return jogador;
        }
        public void setJogador(string val)
        {
            jogador = val;
        }

        
    }
    public class Carta
    {
       
        public string naipe { get; set; }
        public string numero { get; set; }
    }
    public class Dados
    {
        public string jogadorN { get; set; }
        public string jogadorS { get; set; }
        public string jogadorL { get; set; }
        public string jogadorO { get; set; }
        public string manilha { get; set; }
        public int pontosH { get; set; }
        public int pontosV { get; set; }
        public int valorRodada { get; set; }

    }
    public class Deck
    {
        public List<Carta> DeckOfCartas { get; set; }
        
       
        public Deck()
        {
            List<Carta> deckOfCartas_ = new List<Carta>();
          
                string[] numbers = new string[] { "2", "3", "4", "5", "6", "7", "Valete", "Dama", "Rei", "Ás" };
                string[] suits = new string[] { "Espada", "Ouro", "Copas", "Paus" };
                foreach (string suit in suits)
                {
                    foreach (string number in numbers)
                    {
                        Carta c = new Carta() { naipe = suit, numero = number };
                        deckOfCartas_.Add(c);
                    }
                }
             
            DeckOfCartas = deckOfCartas_;
        }

        public void Shuffle()
        {
            List<Carta> deckOfCartas_ = DeckOfCartas;
            var randon = new Random();
            DeckOfCartas = deckOfCartas_.OrderBy(item => randon.Next()).ToList();
        }

        public Carta GoFish()
        {
            List<Carta> deckOfCartas_ = DeckOfCartas;
            var topCarta = deckOfCartas_.First();
            deckOfCartas_.Remove(topCarta);
            return topCarta;
        }

        public List<Carta> GoFish(int numberOfCartas)
        {
            List<Carta> backCartas = new List<Carta>();
            for (int i = 0; i < numberOfCartas; i++)
            {
                backCartas.Add(GoFish());
            }
            return backCartas;
        }

        public void RemountDeck()
        {
            List<Carta> deckOfCartas_ = new List<Carta>();
            deckOfCartas_ = new Deck().DeckOfCartas;
            DeckOfCartas = deckOfCartas_;
        }

    }
}
