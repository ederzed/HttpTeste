using System;
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
   //LEMBRETE: TENTAR COLOCAR O Nº DE CARTAS JOGADAS NO SERVER AO INVES DE POR VARIAVEL LOCAL 
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
            pbVitoria1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVitoria2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // VARIAVEIS




        Jogador jooj = new Jogador();
        Confirma confirm = new Confirma();
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
        public String linkConfirmacao = "http://api.myjson.com/bins/1e9ep6";
        




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

        public void putDadosHttp(string jN, string jS, string jL, string jO, string mani, int ponH, int ponV, int valor, int nC)
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
                    valorRodada = valor,
                    numeroCartasJogadas = nC
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

        public void putConfirmasHttp(int co)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(linkConfirmacao);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Confirma c = new Confirma()
                {
                    confirma = co,
                };

                string json = JsonConvert.SerializeObject(c);

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();

            }
        }

        public Confirma getConfirmaHttp()
        {
            var requisicaoWeb = WebRequest.CreateHttp(linkConfirmacao);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                Confirma c = JsonConvert.DeserializeObject<Confirma>(objResponse.ToString());



                streamDados.Close();
                resposta.Close();

                return c;
            }

        }
        public void conectar()
        {
            Dados data = getDadosHttp();
            if(data.jogadorS == "-")
            {
                jooj.setJogador("S-Vira");
                putDadosHttp(data.jogadorN, "Conectado", data.jogadorL, data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada, dados.numeroCartasJogadas);
                lblResultado.Text = "Conectado como jogador Sul";
            }
            else if(data.jogadorN == "-")
            {
                jooj.setJogador("N");
                putDadosHttp("Conectado", data.jogadorS, data.jogadorL, data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada, dados.numeroCartasJogadas);
                lblResultado.Text = "Conectado como jogador Norte";
            }
            else if (data.jogadorL == "-")
            {
                jooj.setJogador("L");
                putDadosHttp(data.jogadorN, data.jogadorS, "Conectado", data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada, dados.numeroCartasJogadas);
                lblResultado.Text = "Conectado como jogador Leste";
            }
            else if (data.jogadorO == "-")
            {
                jooj.setJogador("O");
                putDadosHttp(data.jogadorN, data.jogadorS, data.jogadorL, "Conectado", data.manilha, data.pontosH, data.pontosV, data.valorRodada, dados.numeroCartasJogadas);
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
            pbVitoria1.Image = null;
            pbVitoria2.Image = null;
            Array.Sort(cartasEmJogo);
            if (cartasEmJogo[cartasEmJogo.Length - 1] == cartasEmJogo[cartasEmJogo.Length - 2])
            {
                //melhorDe3("A");
                return "Amarrou";
            }
                

            if (cartasEmJogo[cartasEmJogo.Length - 1] == c1)
            {
                pbVitoria1.Image = Properties.Resources.star;
               // melhorDe3("H");
                return "Jogador Sul Venceu";

            }
               
            else if (cartasEmJogo[cartasEmJogo.Length - 1] == c2)
            {
                pbVitoria2.Image = Properties.Resources.star;
               // melhorDe3("V");
                return "Jogador Norte Venceu";
            }

            else
            {
                return "Erro!";
            }
                

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
        public void melhorDe3(string vencedor)
        {
            jooj = new Jogador();
            if(vencedor == "H")
            {
                jooj.setMelhorH(jooj.getMelhorH() + 1);
                lblMelhorDeTres.Text = lblMelhorDeTres.Text + " Sul,";
            }if (vencedor == "V")
            {
                jooj.setMelhorV(jooj.getMelhorV() + 1);
                lblMelhorDeTres.Text = lblMelhorDeTres.Text + " Norte,";
            }
            if (vencedor == "A")
            {
                jooj.setMelhorV(jooj.getMelhorV() + 1);
                jooj.setMelhorH(jooj.getMelhorH() + 1);
                lblMelhorDeTres.Text = lblMelhorDeTres.Text + " Amarrou,";
            }
            if ((jooj.getMelhorH() >= 2 || jooj.getMelhorV() >= 2) && jooj.getMelhorV() != jooj.getMelhorH())
            {
                jooj.setCartasJogadas(99);
                putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, jooj.getCartasJogadas());
            }else if ((jooj.getMelhorH() >= 3 || jooj.getMelhorV() >= 3) && jooj.getMelhorV() == jooj.getMelhorH())
            {
                jooj.setCartasJogadas(99);
                putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, jooj.getCartasJogadas());

            }

        }
        public void novaRodada()
        {
            dados = getDadosHttp();
            jooj = new Jogador();
            
            if (jooj.getJogador().EndsWith("Vira"))
            {
                deck = new Deck();
                deck.Shuffle();
                defineManilia = deck.GoFish();
                putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, cartaToString(defineManilia), dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas);
                deck.Shuffle();
                List<Carta> mao = deck.GoFish(3);
                maos = mao;
                

                putBaralhoHttp(deck);

                pbBaralho.Image = escolheCarta(defineManilia);
                if (jooj.getJogador().StartsWith("S"))
                {
                    pbCarta1.Image = escolheCarta(mao[0]);
                    pbCarta2.Image = escolheCarta(mao[1]);
                    pbCarta3.Image = escolheCarta(mao[2]);
                    pbCarta1.Enabled = true;
                    pbCarta2.Enabled = true;
                    pbCarta3.Enabled = true;
                }

            }
            else
            {
                do
                {
                    dados = getDadosHttp();
                    if (getBaralhoHttp().DeckOfCartas.Count == 36)
                        deck = getBaralhoHttp();
                    else
                        deck = null;
                    defineManilia = stringToCarta(dados.manilha);

                } while (deck == null);

                deck.Shuffle();
                List<Carta> mao = deck.GoFish(3);
                maos = mao;

               
                putBaralhoHttp(deck);

                pbBaralho.Image = escolheCarta(defineManilia);
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

            
            


        }

        public string fraseDoTruco(string tipo)
        {
            dados = getDadosHttp();
            Random r = new Random();
            string[] frases = { "XABLAU"," CHEGA E CHORA", "VEM PRA CÁ HIHI", "CHORA AGORA, CHORA", "COMIGO EH NO GROSSO MESMO" 
                    , "DROPEI", "VÊM SE VC AGUENTA", "PODE CORRER JÁ", "*PLÁF* *PLÁF* SE FUDEU", "EU ACREDITO NO CORAÇÃO DAS CARTAS",
                    "EXCUZE-MOI","TA MEC, TAM MEC", "VEM QUE AQUI TA SAFE", "OBJECTION", "SEGURA QUE VEM PESADO", "LADRÃOZINHO", "TRY ME BICTH","LADRÃO"};
            return tipo + frases[r.Next(0, frases.Length - 1)];

        }
        public string gameOver()
        {
            dados = getDadosHttp();
            if(dados.pontosH >= 12)
            {
                return "SUL VENCEU";
            }
            else if(dados.pontosV >= 12)
            {
                return "NORTE VENCEU";
            }
            else
            {
                return "";
            }
        }




        // EVENTOS DO FORM







        private void Form1_Load(object sender, EventArgs e)
        {
            //putDadosHttp("-", "-", "-", "-", "-", 0, 0, 1, 0);
            //putBaralhoHttp(deck);
            //putConfirmasHttp(0);
            conectar();
            dados = getDadosHttp();
            if (jooj.getJogador().EndsWith("Vira"))
            {
                deck = new Deck();
                deck.Shuffle();
                defineManilia = deck.GoFish();
                putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, cartaToString(defineManilia), dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas);
            }
            else
            {
                deck = getBaralhoHttp();

                defineManilia = stringToCarta(dados.manilha);
            }




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
            putDadosHttp(dados.jogadorN, cartaToString(jog1), dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j1 = pbJogada1.Image;
            pbCarta1.Enabled = false;
            pbCarta2.Enabled = false;
            pbCarta3.Enabled = false;
        }

        private void pbCarta2_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada1.Image = pbCarta2.Image;
            reporCarta1();
            pbCarta2.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[1];
            putDadosHttp(dados.jogadorN, cartaToString(jog1), dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j1 = pbJogada1.Image;
            pbCarta1.Enabled = false;
            pbCarta2.Enabled = false;
            pbCarta3.Enabled = false;
        }

        private void pbCarta3_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada1.Image = pbCarta3.Image;
            reporCarta1();
            pbCarta3.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[2];
            putDadosHttp(dados.jogadorN, cartaToString(jog1), dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j1 = pbJogada1.Image;
            pbCarta1.Enabled = false;
            pbCarta2.Enabled = false;
            pbCarta3.Enabled = false;
        }

       

        private void pbCarta4_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada2.Image = pbCarta4.Image;
            reporCarta2();
            pbCarta4.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[0];
            putDadosHttp(cartaToString(jog2), dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j2 = pbJogada2.Image;
            pbCarta4.Enabled = false;
            pbCarta5.Enabled = false;
            pbCarta6.Enabled = false;
        }

        private void pbCarta5_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada2.Image = pbCarta5.Image;
            reporCarta2();
            pbCarta5.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[1];
            putDadosHttp(cartaToString(jog2), dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j2 = pbJogada2.Image;
            pbCarta4.Enabled = false;
            pbCarta5.Enabled = false;
            pbCarta6.Enabled = false;
        }

        private void pbCarta6_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada2.Image = pbCarta6.Image;
            reporCarta2();
            pbCarta6.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[2];
            putDadosHttp(cartaToString(jog2), dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j2 = pbJogada2.Image;
            pbCarta4.Enabled = false;
            pbCarta5.Enabled = false;
            pbCarta6.Enabled = false;
        }

        private void tmrResultado_Tick(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            jooj = new Jogador();
            confirm = getConfirmaHttp();
            tmrRearranjar.Enabled = true;
            if (jooj.getJogador().StartsWith("N") && dados.jogadorS.Contains('/'))
            {
                pbJogada1.Image = escolheCarta(stringToCarta(dados.jogadorS));
                putConfirmasHttp(confirm.confirma + 1);
            }
                
            if (jooj.getJogador().StartsWith("S") && dados.jogadorN.Contains('/'))
            {
                pbJogada2.Image = escolheCarta(stringToCarta(dados.jogadorN));
                putConfirmasHttp(confirm.confirma + 1);
            }
                
            if (dados.jogadorN.Contains('/') && dados.jogadorS.Contains('/') && confirm.confirma >= 2)
            {
                
                lblResultado.Text = verVencedor(stringToCarta(dados.jogadorS), stringToCarta(dados.jogadorN), stringToCarta(dados.manilha));
                putConfirmasHttp(0);
                // jooj.setCartasJogadas(jooj.getCartasJogadas() + 1);
                if (lblResultado.Text.Contains("Sul"))
                    melhorDe3("H");
                
                if (lblResultado.Text.Contains("Norte"))
                    melhorDe3("V");
                if (lblResultado.Text.Contains("Amarrou"))
                    melhorDe3("A");
                j1 = Properties.Resources.card_game_48983_960_720;
                j2 = Properties.Resources.card_game_48983_960_720;
                dados = getDadosHttp();
                putDadosHttp("Conectado", "Conectado", dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas);

                if (jooj.getJogador().StartsWith("S"))
                {
                    pbCarta1.Enabled = true;
                    pbCarta2.Enabled = true;
                    pbCarta3.Enabled = true;
                }
                if (jooj.getJogador().StartsWith("N"))
                {
                    pbCarta4.Enabled = true;
                    pbCarta5.Enabled = true;
                    pbCarta6.Enabled = true;
                }
                    
                
                    
            } 
        }
        private void tmrRearranjar_Tick(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            jooj = new Jogador();
            confirm = getConfirmaHttp();
            if (dados.numeroCartasJogadas >= 99 && confirm.confirma == 0)
            {
                if (jooj.getMelhorH() > jooj.getMelhorV())
                    putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH + dados.valorRodada, dados.pontosV, 1, 0);
               
                
            
                if (jooj.getMelhorV() > jooj.getMelhorH())
                    putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV + dados.valorRodada, 1, 0);

                if (jooj.getMelhorH() == jooj.getMelhorV())
                    putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, 1, 0);
                dados = getDadosHttp();
                if (gameOver().Contains("VENCEU"))
                {
                    MessageBox.Show(gameOver());
                    this.Close();

                }
                else
                {
                    novaRodada();
                    jooj.setMelhorH(0);
                    jooj.setMelhorV(0);
                    pbVitoria1.Image = null;
                    pbVitoria2.Image = null;
                    lblResultado.Text = "Nova Rodada";
                    lblPontosSul.Text = "Pontos Sul: " + dados.pontosH;
                    lblPontosNorte.Text = "Pontos Norte: " + dados.pontosV;
                    lblMelhorDeTres.Text = "-";
                }
                
            }
            tmrRearranjar.Enabled = false;
        }

        private void BaralhoHttp_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.R)
            {
                putDadosHttp("-", "-", "-", "-", "-", 0, 0, 1, 0);
                putConfirmasHttp(0);
                deck = new Deck();
                putBaralhoHttp(deck);
            }

        }

        private void BaralhoHttp_Deactivate(object sender, EventArgs e)
        {
            
        }

        private void BaralhoHttp_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dados data = getDadosHttp();
            Jogador j = new Jogador();
            if (j.getJogador().StartsWith("N"))
            {

                putDadosHttp("-", data.jogadorS, data.jogadorL, data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada, dados.numeroCartasJogadas);

            }
            else if (j.getJogador().StartsWith("S"))
            {

                putDadosHttp(data.jogadorN, "-", data.jogadorL, data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada, dados.numeroCartasJogadas);

            }
            else if (j.getJogador().StartsWith("L"))
            {

                putDadosHttp(data.jogadorN, data.jogadorS, "-", data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada, dados.numeroCartasJogadas);

            }
            else if (j.getJogador().StartsWith("O"))
            {

                putDadosHttp(data.jogadorN, data.jogadorS, data.jogadorL, "-", data.manilha, data.pontosH, data.pontosV, data.valorRodada, dados.numeroCartasJogadas);

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
        public static int cartasJogadas = 0;
        public int getCartasJogadas()
        {
            return cartasJogadas;
        }
        public void setCartasJogadas(int val)
        {
            cartasJogadas = val;
        }
        public static int melhorH = 0;
        public int getMelhorH()
        {
            return melhorH;
        }
        public void setMelhorH(int val)
        {
            melhorH = val;
        }

        public static int melhorV = 0;
        public int getMelhorV()
        {
            return melhorV;
        }
        public void setMelhorV(int val)
        {
            melhorV = val;
        }
    }
    public class Carta
    {
       
        public string naipe { get; set; }
        public string numero { get; set; }
    }
    public class Confirma
    {

        public int confirma { get; set; }
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
        public int numeroCartasJogadas { get; set; }

    }
    public class Deck
    {
        public List<Carta> DeckOfCartas { get; set; }
        Jogador j = new Jogador();
       
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
            if (j.getJogador().EndsWith("Vira"))
            {
                DeckOfCartas = deckOfCartas_;
            }
            else { DeckOfCartas = null; }
            
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
