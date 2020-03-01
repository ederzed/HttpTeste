using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace BaralhoHttp
{
    // CHECAR SE AS CARTAS E AS IMGS BATEM
    //List<string> teste = new List<string>();
    //for(int i = 0; i< mao.Count; i++)
    //{
    //    teste.Add(mao[i].numero + " de " + mao[i].naipe);
    //}
    //String m = String.Join(";", teste);
    //MessageBox.Show(m);
    //BLZ E AGR PRA DEIXAR ESSA MERDA POR TURNO?
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
            pbCarta7.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta8.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta9.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta10.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta11.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCarta12.SizeMode = PictureBoxSizeMode.StretchImage;
            pbJogada1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbJogada2.SizeMode = PictureBoxSizeMode.StretchImage;
            pbJogada3.SizeMode = PictureBoxSizeMode.StretchImage;
            pbJogada4.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVitoria1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVitoria2.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVitoria3.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVitoria4.SizeMode = PictureBoxSizeMode.StretchImage;
            btnTruco.Enabled = false;
        }

        // VARIAVEIS
        // VARIAVEIS
        // VARIAVEIS
        // VARIAVEIS
        // VARIAVEIS

        //reset do server Truco

        private Truco trReset = new Truco
        {
            pediu = "-",
            aceitou = "-",
            frase = "-",
            valor = 0,
            contador = 0,
            exibir = false
        };

        //inicializadores das classes especificas

        private Jogador jooj = new Jogador();
        private Confirma confirm = new Confirma();
        private Truco truco = new Truco();
        public Chat chat = new Chat();
        public Deck deck = new Deck();
        public Dados dados = new Dados();

        // definidores de certos eventos
        public int contMelhorDe3 = 0;
        public int contConfirma = 0;
        public int contMensagem = 0;
        public string atualizaHistorico = "-";
        public bool escondida = false;
        public bool maode11 = false;

        //guarda as cartas jogadas (talvez n precise de 1 e 2)
        public Image j1 = null;

        public Image j2 = null;
        public Image j3 = null;
        public Image j4 = null;
        public Carta jog1;
        public Carta jog2;
        public Carta jog3;
        public Carta jog4;

        //Carta sem valor
        public Carta esconde = new Carta { naipe = "esc", numero = "esc" };

        //manilha do turno
        public Carta defineManilia;

        // mão do jogador
        public List<Carta> maos;

        //SERVERS
        public String linkBaralho = "https://api.myjson.com/bins/1412o2";
        public String linkDados = "https://api.myjson.com/bins/macua";
        public String linkConfirmacao = "http://api.myjson.com/bins/1e9ep6";
        public String linkTruco = "http://api.myjson.com/bins/zvvlq";
        public String linkChat = "https://api.myjson.com/bins/1h14ga";

        //SÓ MÉTODOS
        //SÓ MÉTODOS
        //SÓ MÉTODOS
        //SÓ MÉTODOS
        //SÓ MÉTODOS

        //PUT E GET DO SERVER DO BARALHO PARA A CLASSE DECK

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

        //PUT  E GET DO SERVER DADOS PARA A CLASSE DADOS

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

        //PUT  E GET DO SERVER DADOS PARA A CLASSE CHAT

        public void putChatHttp(string tex, int onze)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(linkChat);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Chat c = new Chat()
                {
                   
                    texto = tex,
                    cont11 = onze,
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

        public Chat getChatHttp()
        {
            var requisicaoWeb = WebRequest.CreateHttp(linkChat);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                Chat c = JsonConvert.DeserializeObject<Chat>(objResponse.ToString());

                streamDados.Close();
                resposta.Close();

                return c;
            }
        }

        //PUT E GET DO SERVER CONFIRMA PARA A CLASSE CONFIRMA

        public void putConfirmasHttp(int co, string t, string pri)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(linkConfirmacao);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Confirma c = new Confirma()
                {
                    confirma = co,
                    turno = t,
                    primeiroPlayer = pri
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

        //GET E PUT DO SERVER TRUCO PARA A CLASSE TRUCO

        public void putTrucoHttp(Truco t)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(linkTruco);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(t);

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
            }
        }

        public Truco getTrucoHttp()
        {
            var requisicaoWeb = WebRequest.CreateHttp(linkTruco);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                Truco t = JsonConvert.DeserializeObject<Truco>(objResponse.ToString());

                streamDados.Close();
                resposta.Close();

                return t;
            }
        }

        // MÉTODOS ALÉM DE PUT E GET
        // MÉTODOS ALÉM DE PUT E GET
        // MÉTODOS ALÉM DE PUT E GET
        // MÉTODOS ALÉM DE PUT E GET
        // MÉTODOS ALÉM DE PUT E GET

        public string VencedorDaPrimeira()
        {
            string[] vencedores = lblMelhorDeTres.Text.Split(',');
            return vencedores[0];
        }
        //FUNCIONANDO

        public void conectar()
        {
            Dados data = getDadosHttp();
            if (data.jogadorS == "-")
            {
                jooj.setJogador("S-Vira");
                putDadosHttp(data.jogadorN, "Conectado", data.jogadorL, data.jogadorO, data.manilha, data.pontosH, data.pontosV, data.valorRodada, dados.numeroCartasJogadas);
                lblResultado.Text = "Conectado como jogador Sul";
            }
            else if (data.jogadorN == "-")
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

        //INTUITIVO

        public string cartaToString(Carta c)
        {
            string val = c.numero + "/" + c.naipe;

            return val;
        }

        public Carta stringToCarta(string c)
        {
            if (c.Contains("/"))
            {
                string[] icon = c.Split('/');
                Carta val = new Carta()
                {
                    numero = icon[0],
                    naipe = icon[1]
                };
                return val;
            }
            else
                return esconde;
        }

        //SÓ USAVA ISSO NO REPOR CARTA MAIS EH O "UNICO" JEITO DE COMPARAR IMGS NOS IF'S

        public byte[] imgToByteArray(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                return mStream.ToArray();
            }
        }

        //N TENHO CERTEZA MAIS ACHO Q OS reporCarta() FICARAM OBSOLETOS

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

        public void reporCarta3()
        {
            if (imgToByteArray(pbCarta7.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta7.Image = j3;
            if (imgToByteArray(pbCarta8.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta8.Image = j3;
            if (imgToByteArray(pbCarta9.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta9.Image = j3;
        }

        public void reporCarta4()
        {
            if (imgToByteArray(pbCarta10.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta10.Image = j4;
            if (imgToByteArray(pbCarta11.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta11.Image = j4;
            if (imgToByteArray(pbCarta12.Image).SequenceEqual(imgToByteArray(Properties.Resources.card_game_48983_960_720)))
                pbCarta12.Image = j4;
        }

        //TRAZ A IMG NOS RESORCES DO PRJ DE ACORDO COM O CARTA

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
            if (!(c.numero == "esc"))
                return imagens[(num + (nai * 10)) - 1];
            else
                return Properties.Resources.card_game_48983_960_720;
        }

        //(MUITO PROVAVELMENTE N FUNC) DEFINE QUEM SERA O PRIMEIRO A JOGAR NO TURNO SEGUINTE, OBS: AINDA N CONSIGUI TERMINAR UM TURNO

        public string novoPrimeiroPlayer()
        {
            confirm = getConfirmaHttp();
            if (confirm.primeiroPlayer == "S")
                return "L";
            else if (confirm.primeiroPlayer == "N")
                return "O";
            else if (confirm.primeiroPlayer == "O")
                return "S";
            else if (confirm.primeiroPlayer == "L")
                return "N";
            else
                return "Error";
        }

        //(FUNCIONANDO?) CHECA O VENCEDOR ENTRE DUAS CARTAS, POREM PODE FACILMENTE SER ALTERADO PARA MAIS, ALTERA VISUALMENTE PARA DIZER QUEM VENCEU

        public string verVencedor(Carta j1, Carta j2, Carta j3, Carta j4, Carta manilha)
        {
            // 4 - 7 ,Q ,J ,K ,A ,2 ,3
            int c1 = valorReal(j1, manilha);
            int c2 = valorReal(j2, manilha);
            int c3 = valorReal(j3, manilha);
            int c4 = valorReal(j4, manilha);
            int[] cartasEmJogo = { c1, c2, c3, c4 };
            pbVitoria1.Image = null;
            pbVitoria2.Image = null;
            pbVitoria3.Image = null;
            pbVitoria4.Image = null;
            Array.Sort(cartasEmJogo);
            if ((cartasEmJogo[cartasEmJogo.Length - 1] == cartasEmJogo[cartasEmJogo.Length - 2]))
            {
                if (cartasEmJogo[cartasEmJogo.Length - 1] == c1 && cartasEmJogo[cartasEmJogo.Length - 2] != c2)
                    return "Amarrou";
                if (cartasEmJogo[cartasEmJogo.Length - 1] == c2 && cartasEmJogo[cartasEmJogo.Length - 2] != c1)
                    return "Amarrou";
                if (cartasEmJogo[cartasEmJogo.Length - 1] == c3 && cartasEmJogo[cartasEmJogo.Length - 2] != c4)
                    return "Amarrou";
                if (cartasEmJogo[cartasEmJogo.Length - 1] == c4 && cartasEmJogo[cartasEmJogo.Length - 2] != c3)
                    return "Amarrou";
                if (cartasEmJogo[cartasEmJogo.Length - 1] == cartasEmJogo[cartasEmJogo.Length - 2]
                    && cartasEmJogo[cartasEmJogo.Length - 2] == cartasEmJogo[cartasEmJogo.Length - 3])
                    return "Amarrou";
                if (cartasEmJogo[cartasEmJogo.Length - 1] == cartasEmJogo[cartasEmJogo.Length - 2]
                    && cartasEmJogo[cartasEmJogo.Length - 2] == cartasEmJogo[cartasEmJogo.Length - 4])
                    return "Amarrou";
                if (cartasEmJogo[cartasEmJogo.Length - 1] == cartasEmJogo[cartasEmJogo.Length - 2]
                    && cartasEmJogo[cartasEmJogo.Length - 2] == cartasEmJogo[cartasEmJogo.Length - 3]
                    && cartasEmJogo[cartasEmJogo.Length - 3] == cartasEmJogo[cartasEmJogo.Length - 4])
                    return "Amarrou";
            }

            if (cartasEmJogo[cartasEmJogo.Length - 1] == c1)
            {
                pbVitoria1.Image = Properties.Resources.star;

                return "Jogador Sul Venceu";
            }
            else if (cartasEmJogo[cartasEmJogo.Length - 1] == c2)
            {
                pbVitoria2.Image = Properties.Resources.star;

                return "Jogador Norte Venceu";
            }
            else if (cartasEmJogo[cartasEmJogo.Length - 1] == c3)
            {
                pbVitoria3.Image = Properties.Resources.star;

                return "Jogador Leste Venceu";
            }
            else if (cartasEmJogo[cartasEmJogo.Length - 1] == c4)
            {
                pbVitoria4.Image = Properties.Resources.star;

                return "Jogador Oeste Venceu";
            }
            else
            {
                return "Erro!";
            }
        }

        //FUNCIONANDO PERFEITAMENTE, DA O VALOR REAL DE UMA CARTA NA SEQUENCIA DE FORÇA DO TRUCO

        public int valorReal(Carta carta, Carta mani)
        {
            int valor = 0;
            int manilhona = 0;
            if (carta.numero == "esc")
            {
                valor = 0;
            }
            else
            {
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
            }
            return valor;
        }

        //(DEVE TER ERRO AQUI) FAZ AS ALTERAÇÕES VISUAIS DE ACORDO COM O VENCEDOR DA MELHOR DE 3

        public void melhorDe3(string vencedor)
        {
            jooj = new Jogador();

            if (vencedor == "H")
            {
                jooj.setMelhorH(jooj.getMelhorH() + 1);
                lblMelhorDeTres.Text = lblMelhorDeTres.Text + " L/O,";
            }
            if (vencedor == "V")
            {
                jooj.setMelhorV(jooj.getMelhorV() + 1);
                lblMelhorDeTres.Text = lblMelhorDeTres.Text + " N/S,";
            }
            if (vencedor == "A")
            {
                jooj.setMelhorV(jooj.getMelhorV() + 1);
                jooj.setMelhorH(jooj.getMelhorH() + 1);
                lblMelhorDeTres.Text = lblMelhorDeTres.Text + " Amarrou,";
            }

            if ((jooj.getMelhorH() >= 2 || jooj.getMelhorV() >= 2) && jooj.getMelhorV() != jooj.getMelhorH())
            {

                putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, 99);
            }
            else if ((jooj.getMelhorH() >= 2 || jooj.getMelhorV() >= 2) && jooj.getMelhorV() == jooj.getMelhorH() && VencedorDaPrimeira() != "- Amarrou")
            {
                putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, 123);
            }
            else if ((jooj.getMelhorH() >= 3 || jooj.getMelhorV() >= 3) && jooj.getMelhorV() == jooj.getMelhorH())
            {
                putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, 99);
            }
        }

        //Define qual o próximo turno (Em fase de teste)

        public void proxTurno(string turnoAtual)
        {
            confirm = getConfirmaHttp();
            dados = getDadosHttp();
            if (!(dados.jogadorN.Contains('/') && dados.jogadorS.Contains('/') && dados.jogadorL.Contains('/') && dados.jogadorO.Contains('/')))
            {
                if (turnoAtual == "N")
                    putConfirmasHttp(confirm.confirma, "O", confirm.primeiroPlayer);
                if (turnoAtual == "S")
                    putConfirmasHttp(confirm.confirma, "L", confirm.primeiroPlayer);
                if (turnoAtual == "O")
                    putConfirmasHttp(confirm.confirma, "S", confirm.primeiroPlayer);
                if (turnoAtual == "L")
                    putConfirmasHttp(confirm.confirma, "N", confirm.primeiroPlayer);
            }
            else
            {
                if (verVencedor(stringToCarta(dados.jogadorS), stringToCarta(dados.jogadorN),
                    stringToCarta(dados.jogadorL), stringToCarta(dados.jogadorO), stringToCarta(dados.manilha)).Contains("Sul"))
                    putConfirmasHttp(confirm.confirma, "S", confirm.primeiroPlayer);
                if (verVencedor(stringToCarta(dados.jogadorS), stringToCarta(dados.jogadorN),
                    stringToCarta(dados.jogadorL), stringToCarta(dados.jogadorO), stringToCarta(dados.manilha)).Contains("Norte"))
                    putConfirmasHttp(confirm.confirma, "N", confirm.primeiroPlayer);
                if (verVencedor(stringToCarta(dados.jogadorS), stringToCarta(dados.jogadorN),
                    stringToCarta(dados.jogadorL), stringToCarta(dados.jogadorO), stringToCarta(dados.manilha)).Contains("Leste"))
                    putConfirmasHttp(confirm.confirma, "L", confirm.primeiroPlayer);
                if (verVencedor(stringToCarta(dados.jogadorS), stringToCarta(dados.jogadorN),
                    stringToCarta(dados.jogadorL), stringToCarta(dados.jogadorO), stringToCarta(dados.manilha)).Contains("Oeste"))
                    putConfirmasHttp(confirm.confirma, "O", confirm.primeiroPlayer);
                if (verVencedor(stringToCarta(dados.jogadorS), stringToCarta(dados.jogadorN),
                    stringToCarta(dados.jogadorL), stringToCarta(dados.jogadorO), stringToCarta(dados.manilha)).Contains("Amarrou"))
                    putConfirmasHttp(confirm.confirma, confirm.primeiroPlayer, confirm.primeiroPlayer);
            }


            if (!lblResultado.Text.Contains("VEZ") && (dados.numeroCartasJogadas % 4) == 0)
            {
                if (lblResultado.Text.Contains("Norte"))
                    putConfirmasHttp(confirm.confirma, "N", confirm.primeiroPlayer);
                if (lblResultado.Text.Contains("Sul"))
                    putConfirmasHttp(confirm.confirma, "S", confirm.primeiroPlayer);
                if (lblResultado.Text.Contains("Leste"))
                    putConfirmasHttp(confirm.confirma, "L", confirm.primeiroPlayer);
                if (lblResultado.Text.Contains("Oeste"))
                    putConfirmasHttp(confirm.confirma, "O", confirm.primeiroPlayer);
                if (lblResultado.Text.Contains("Amarrou"))
                    putConfirmasHttp(confirm.confirma, confirm.primeiroPlayer, confirm.primeiroPlayer);


            }

        }

        //RESETA AS MÃOS E OS SERVERS QND ALGUEM PONTUA (N TESTEI NA NOVA DINÂMICA AINDA, PQ O RESTO AINDA N FUNCIONOU)

        public void novaRodada()
        {
            btnEscondidaAberta.Enabled = false;
            btnEscondidaAberta.Text = "escondida";
            escondida = false;
            btnTruco.Enabled = true;
            btnTruco.Text = "TRUCO!";
            putTrucoHttp(trReset);
            dados = getDadosHttp();
            jooj = new Jogador();
            lblHitorico.Text = "    Histórico:";
            if (jooj.getJogador().EndsWith("Vira"))
            {
                deck = new Deck();
                deck.RemountDeck();
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
                }
            }
            else
            {
                do
                {
                    dados = getDadosHttp();
                    if (jooj.getJogador().StartsWith("N"))
                    {
                        if (getBaralhoHttp().DeckOfCartas.Count == 36)
                            deck = getBaralhoHttp();
                        else
                            deck = null;
                    }
                    if (jooj.getJogador().StartsWith("L"))
                    {
                        if (getBaralhoHttp().DeckOfCartas.Count == 33)
                            deck = getBaralhoHttp();
                        else
                            deck = null;
                    }
                    if (jooj.getJogador().StartsWith("O"))
                    {
                        if (getBaralhoHttp().DeckOfCartas.Count == 30)
                            deck = getBaralhoHttp();
                        else
                            deck = null;
                    }
                    defineManilia = stringToCarta(dados.manilha);
                } while (deck == null);

                deck.Shuffle();
                List<Carta> mao = deck.GoFish(3);
                maos = mao;

                if (jooj.getJogador().StartsWith("S"))
                {
                    deck.maoSul = maos;
                }
                if (jooj.getJogador().StartsWith("N"))
                {
                    deck.maoNorte = maos;
                }
                if (jooj.getJogador().StartsWith("L"))
                {
                    deck.maoLeste = maos;
                }
                if (jooj.getJogador().StartsWith("O"))
                {
                    deck.maoOeste = maos;
                }

                putBaralhoHttp(deck);

                pbBaralho.Image = escolheCarta(defineManilia);
                if (jooj.getJogador().StartsWith("N"))
                {
                    pbCarta4.Image = escolheCarta(mao[0]);
                    pbCarta5.Image = escolheCarta(mao[1]);
                    pbCarta6.Image = escolheCarta(mao[2]);
                }
                if (jooj.getJogador().StartsWith("L"))
                {
                    pbCarta7.Image = escolheCarta(mao[0]);
                    pbCarta8.Image = escolheCarta(mao[1]);
                    pbCarta9.Image = escolheCarta(mao[2]);
                }
                if (jooj.getJogador().StartsWith("O"))
                {
                    pbCarta10.Image = escolheCarta(mao[0]);
                    pbCarta11.Image = escolheCarta(mao[1]);
                    pbCarta12.Image = escolheCarta(mao[2]);
                }
            }
        }

        // DEFINE O CONTEUDO DA JANELA DO TRUCO, BOTA MAIS FRASE AI

        public string fraseDoTruco()
        {
            truco = getTrucoHttp();
            Random r = new Random();
            string[] frases = { "XABLAU"," CHEGA E CHORA", "VEM PRA CÁ HIHI", "CHORA AGORA, CHORA", "COMIGO EH NO GROSSO MESMO"
                    , "DROPEI", "*FORTE SOM DE MESA DE BAR BATENDO* "+ nomeJogada(truco.valor),"VÊM SE VC AGUENTA", "OBLITERAR!", "PODE CORRER JÁ", "*PLÁF* *PLÁF* SE FUDEU", "EU ACREDITO NO CORAÇÃO DAS CARTAS",
                    "EXCUZE-MOI","TA MEC, TAMO MEC", "VEM QUE AQUI TA SAFE", "OBJECTION!", "SEGURA QUE VEM PESADO", "LADRÃOZINHO", "TRY ME BICTH",
                "VOCÊ ESTÁ 10 ANOS ATRÁS DE MIM EM HABILIDADE","MUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDA MUUUDAA", "DESCE!","AQUI TEM CORAGEM",
                 ""};
            return frases[r.Next(0, frases.Length - 1)];
        }

        //GAME OVER

        public string gameOver()
        {
            dados = getDadosHttp();

            if (dados.pontosH >= 12)
            {
                return "L/O VENCEU";
            }
            else if (dados.pontosV >= 12)
            {
                return "N/S VENCEU";
            }
            else
            {
                return "";
            }
        }

        //ESSES DOIS ABAIXO DEFINEM A LEGENDA DA JANELA DE TRUCO

        public string nomeJogador(string jog)
        {
            if (jog == "S")
                return "SUL";
            else if (jog == "N")
                return "NORTE";
            else if (jog == "L")
                return "LESTE";
            else if (jog == "O")
                return "OESTE";
            else
                return "";
        }

        public string nomeJogada(int jog)
        {
            if (jog == 3)
                return "TRUCO";
            else if (jog == 6)
                return "SEIS";
            else if (jog == 9)
                return "NOVE";
            else if (jog == 12)
                return "DOZE";
            else
                return "";
        }

        //ÚNICO MÉTODO QUE MESMO DEPOIS DE MUDAR PRA DINAMICA DE TURNOS AINDA FUNCIONA

        public void atualizarTela()
        {
            dados = getDadosHttp();
            confirm = getConfirmaHttp();
            truco = getTrucoHttp();
            chat = getChatHttp();
            

            if (atualizaHistorico != chat.texto)
            {
                atualizaHistorico = chat.texto;
                lblHitorico.Text += "\r\n" + chat.texto;
            }


            if (jooj.getJogador().StartsWith("N") && (dados.jogadorS.Contains('/') || dados.jogadorL.Contains('/') || dados.jogadorO.Contains('/')))
            {
                if (dados.jogadorS.Contains('/'))
                    pbJogada1.Image = escolheCarta(stringToCarta(dados.jogadorS));
                if (dados.jogadorL.Contains('/'))
                    pbJogada3.Image = escolheCarta(stringToCarta(dados.jogadorL));
                if (dados.jogadorO.Contains('/'))
                    pbJogada4.Image = escolheCarta(stringToCarta(dados.jogadorO));


            }

            if (jooj.getJogador().StartsWith("S") && (dados.jogadorN.Contains('/') || dados.jogadorL.Contains('/') || dados.jogadorO.Contains('/')))
            {
                if (dados.jogadorN.Contains('/'))
                    pbJogada2.Image = escolheCarta(stringToCarta(dados.jogadorN));
                if (dados.jogadorL.Contains('/'))
                    pbJogada3.Image = escolheCarta(stringToCarta(dados.jogadorL));
                if (dados.jogadorO.Contains('/'))
                    pbJogada4.Image = escolheCarta(stringToCarta(dados.jogadorO));


            }

            if (jooj.getJogador().StartsWith("L") && (dados.jogadorS.Contains('/') || dados.jogadorN.Contains('/') || dados.jogadorO.Contains('/')))
            {
                if (dados.jogadorS.Contains('/'))
                    pbJogada1.Image = escolheCarta(stringToCarta(dados.jogadorS));
                if (dados.jogadorN.Contains('/'))
                    pbJogada2.Image = escolheCarta(stringToCarta(dados.jogadorN));
                if (dados.jogadorO.Contains('/'))
                    pbJogada4.Image = escolheCarta(stringToCarta(dados.jogadorO));


            }

            if (jooj.getJogador().StartsWith("O") && (dados.jogadorS.Contains('/') || dados.jogadorL.Contains('/') || dados.jogadorN.Contains('/')))
            {
                if (dados.jogadorS.Contains('/'))
                    pbJogada1.Image = escolheCarta(stringToCarta(dados.jogadorS));
                if (dados.jogadorL.Contains('/'))
                    pbJogada3.Image = escolheCarta(stringToCarta(dados.jogadorL));
                if (dados.jogadorN.Contains('/'))
                    pbJogada2.Image = escolheCarta(stringToCarta(dados.jogadorN));


            }
        }

        public void rodada1()
        {
            dados = getDadosHttp();
            jooj = new Jogador();
            confirm = getConfirmaHttp();

            lblResultado.Text = "SUA VEZ!";
            if (confirm.confirma == 0 && dados.numeroCartasJogadas >= 99)
                rearranjar();
            if (confirm.turno == "S")
            {
                pbCarta1.Enabled = true;
                pbCarta2.Enabled = true;
                pbCarta3.Enabled = true;
                btnTruco.Enabled = true;
                contMelhorDe3 = 0;
            }
            if (confirm.turno == "N")
            {
                pbCarta4.Enabled = true;
                pbCarta5.Enabled = true;
                pbCarta6.Enabled = true;
                btnTruco.Enabled = true;
                contMelhorDe3 = 0;
            }
            if (confirm.turno == "L")
            {
                pbCarta7.Enabled = true;
                pbCarta8.Enabled = true;
                pbCarta9.Enabled = true;
                btnTruco.Enabled = true;
                contMelhorDe3 = 0;
            }
            if (confirm.turno == "O")
            {
                pbCarta10.Enabled = true;
                pbCarta11.Enabled = true;
                pbCarta12.Enabled = true;
                btnTruco.Enabled = true;
                contMelhorDe3 = 0;
            }

            if (jooj.getJogador().StartsWith("N") && (dados.jogadorS.Contains('/') || dados.jogadorL.Contains('/') || dados.jogadorO.Contains('/')))
            {
                if (dados.jogadorS.Contains('/'))
                    pbJogada1.Image = escolheCarta(stringToCarta(dados.jogadorS));
                if (dados.jogadorL.Contains('/'))
                    pbJogada3.Image = escolheCarta(stringToCarta(dados.jogadorL));
                if (dados.jogadorO.Contains('/'))
                    pbJogada4.Image = escolheCarta(stringToCarta(dados.jogadorO));


            }

            if (jooj.getJogador().StartsWith("S") && (dados.jogadorN.Contains('/') || dados.jogadorL.Contains('/') || dados.jogadorO.Contains('/')))
            {
                if (dados.jogadorN.Contains('/'))
                    pbJogada2.Image = escolheCarta(stringToCarta(dados.jogadorN));
                if (dados.jogadorL.Contains('/'))
                    pbJogada3.Image = escolheCarta(stringToCarta(dados.jogadorL));
                if (dados.jogadorO.Contains('/'))
                    pbJogada4.Image = escolheCarta(stringToCarta(dados.jogadorO));


            }

            if (jooj.getJogador().StartsWith("L") && (dados.jogadorS.Contains('/') || dados.jogadorN.Contains('/') || dados.jogadorO.Contains('/')))
            {
                if (dados.jogadorS.Contains('/'))
                    pbJogada1.Image = escolheCarta(stringToCarta(dados.jogadorS));
                if (dados.jogadorN.Contains('/'))
                    pbJogada2.Image = escolheCarta(stringToCarta(dados.jogadorN));
                if (dados.jogadorO.Contains('/'))
                    pbJogada4.Image = escolheCarta(stringToCarta(dados.jogadorO));


            }

            if (jooj.getJogador().StartsWith("O") && (dados.jogadorS.Contains('/') || dados.jogadorL.Contains('/') || dados.jogadorN.Contains('/')))
            {
                if (dados.jogadorS.Contains('/'))
                    pbJogada1.Image = escolheCarta(stringToCarta(dados.jogadorS));
                if (dados.jogadorL.Contains('/'))
                    pbJogada3.Image = escolheCarta(stringToCarta(dados.jogadorL));
                if (dados.jogadorN.Contains('/'))
                    pbJogada2.Image = escolheCarta(stringToCarta(dados.jogadorN));


            }
            confirm = getConfirmaHttp();
            dados = getDadosHttp();
            if (dados.jogadorN.Contains('/') && dados.jogadorS.Contains('/') &&
                dados.jogadorL.Contains('/') && dados.jogadorO.Contains('/') &&
                !confirm.turno.Contains("Rodada"))
            {
                putConfirmasHttp(0, "RodadaS", confirm.primeiroPlayer);

            }
        }

        //OS PRÓXIMOS MÉTODOS VOID FUNCIONAM MAIS

        public void rodada2()
        {
            dados = getDadosHttp();
            confirm = getConfirmaHttp();
            contConfirma = 0;


            lblResultado.Text = verVencedor(stringToCarta(dados.jogadorS), stringToCarta(dados.jogadorN), stringToCarta(dados.jogadorL), stringToCarta(dados.jogadorO), stringToCarta(dados.manilha));


            if (contMelhorDe3 == 0)
            {
                if (lblResultado.Text.Contains("Leste") || lblResultado.Text.Contains("Oeste"))
                    melhorDe3("H");

                if (lblResultado.Text.Contains("Norte") || lblResultado.Text.Contains("Sul"))
                    melhorDe3("V");
                if (lblResultado.Text.Contains("Amarrou"))
                    melhorDe3("A");
            }
            contMelhorDe3++;
            j1 = Properties.Resources.card_game_48983_960_720;
            j2 = Properties.Resources.card_game_48983_960_720;
            j3 = Properties.Resources.card_game_48983_960_720;
            j4 = Properties.Resources.card_game_48983_960_720;


            if (jooj.getJogador().StartsWith("S"))
            {

                if (dados.numeroCartasJogadas >= 99)
                {
                    putConfirmasHttp(0, "RodadaL", confirm.primeiroPlayer);
                    rearranjar();
                }
                else
                {
                    putConfirmasHttp(confirm.confirma, "RodadaL", confirm.primeiroPlayer);
                }

            }
            if (jooj.getJogador().StartsWith("L"))
            {

                if (dados.numeroCartasJogadas >= 99)
                {
                    putConfirmasHttp(0, "RodadaN", confirm.primeiroPlayer);
                    rearranjar();
                }
                else
                {
                    putConfirmasHttp(confirm.confirma, "RodadaN", confirm.primeiroPlayer);
                }

            }
            if (jooj.getJogador().StartsWith("N"))
            {

                if (dados.numeroCartasJogadas >= 99)
                {
                    putConfirmasHttp(0, "RodadaO", confirm.primeiroPlayer);
                    rearranjar();
                }
                else
                {
                    putConfirmasHttp(confirm.confirma, "RodadaO", confirm.primeiroPlayer);
                }

            }
            if (jooj.getJogador().StartsWith("O"))
            {
                putDadosHttp("Conectado", "Conectado", "Conectado", "Conectado", dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas);

                if (dados.numeroCartasJogadas >= 99)
                {
                    putConfirmasHttp(0, "S", confirm.primeiroPlayer);
                    rearranjar();
                }
                else
                {
                    proxTurno("");
                }

            }


        }

        public void rearranjar()
        {
            dados = getDadosHttp();

            jooj = new Jogador();
            confirm = getConfirmaHttp();

            if ((dados.numeroCartasJogadas >= 99 && confirm.confirma <= 1) || confirm.turno.Contains("nova"))
            {
                confirm = getConfirmaHttp();
                if (dados.numeroCartasJogadas == 147)
                {
                    truco = getTrucoHttp();
                    if (truco.pediu == "L" || truco.pediu == "O")
                        jooj.setMelhorH(7);
                    if (truco.pediu == "N" || truco.pediu == "S")
                        jooj.setMelhorV(7);
                }
                if (jooj.getJogador().EndsWith("Vira") && getConfirmaHttp().turno == "S")
                {
                    putConfirmasHttp(0, novoPrimeiroPlayer(), novoPrimeiroPlayer());
                    if (jooj.getMelhorH() > jooj.getMelhorV())
                        putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH + dados.valorRodada, dados.pontosV, 1, 0);

                    if (jooj.getMelhorV() > jooj.getMelhorH())
                        putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV + dados.valorRodada, 1, 0);

                    if (jooj.getMelhorH() == jooj.getMelhorV() && dados.numeroCartasJogadas != 123)
                        putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, 1, 0);
                    else if (dados.numeroCartasJogadas == 123)
                    {
                        if (VencedorDaPrimeira() == "- Norte" || VencedorDaPrimeira() == "- Sul")
                            putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV + dados.valorRodada, 1, 0);
                        else
                            putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH + dados.valorRodada, dados.pontosV, 1, 0);
                    }
                    if ((pbJogada1.Image == null || pbJogada2.Image == null || pbJogada3.Image == null || pbJogada4.Image == null) && !maode11 && dados.numeroCartasJogadas != 147)
                    {
                    }
                    else
                    {
                        novaRodada();
                        jooj.setMelhorH(0);
                        jooj.setMelhorV(0);
                        pbVitoria1.Image = null;
                        pbVitoria2.Image = null;
                        pbVitoria3.Image = null;
                        pbVitoria4.Image = null;
                        pbJogada1.Image = null;
                        pbJogada2.Image = null;
                        pbJogada3.Image = null;
                        pbJogada4.Image = null;
                        lblResultado.Text = "Nova Rodada";
                        dados = getDadosHttp();
                        lblPontosLO.Text = "Pontos L/O: " + dados.pontosH;
                        lblPontosNS.Text = "Pontos N/S: " + dados.pontosV;
                        lblMelhorDeTres.Text = "-";
                        putConfirmasHttp(0, "novaN", confirm.primeiroPlayer);
                        if (dados.pontosV == 11)
                            maode11 = true;
                        if (maode11)
                        {
                            do
                            {
                                deck = getBaralhoHttp();
                            } while (deck.maoNorte == null);
                            deck = getBaralhoHttp();
                            DialogResult dialogResult = MessageBox.Show("Aceitar?\r\n" + string.Join(Environment.NewLine, deck.maoNorte) + "\r\n (Mão do parceiro)", "mão de 11", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                maode11 = false;
                                putChatHttp("Sul aceitou a mão de 11", 0);
                                putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, 3, dados.numeroCartasJogadas);
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                chat = getChatHttp();
                                if(chat.cont11 >= 2)
                                {
                                    dados = getDadosHttp();
                                    confirm = getConfirmaHttp();
                                    putConfirmasHttp(0, "RodadaS", confirm.primeiroPlayer);
                                    if (jooj.getJogador().Replace("-Vira", "") == "N" || jooj.getJogador().Replace("-Vira", "") == "S")
                                    {
                                        putDadosHttp("esc/esc", "esc/esc", "3/Paus", "3/Paus", dados.manilha, dados.pontosH, dados.pontosV, 1, 147);
                                        trReset.pediu = "L";
                                    }
                                    if (jooj.getJogador().Replace("-Vira", "") == "L" || jooj.getJogador().Replace("-Vira", "") == "O")
                                    {
                                        putDadosHttp("3/Paus", "3/Paus", "esc/esc", "esc/esc", dados.manilha, dados.pontosH, dados.pontosV, 1, 147);
                                        trReset.pediu = "S";
                                    }
                                    putTrucoHttp(trReset);
                                    trReset.pediu = "-";
                                }
                                else
                                {
                                    putChatHttp("Sul não aceitou a mão de 11", chat.cont11 + 1);
                                }
                                

                            }
                        }
                    }
                }
                dados = getDadosHttp();
                confirm = getConfirmaHttp();
                if (!jooj.getJogador().EndsWith("Vira") && confirm.turno.Contains("novaN"))
                {
                    novaRodada();
                    jooj.setMelhorH(0);
                    jooj.setMelhorV(0);
                    pbVitoria1.Image = null;
                    pbVitoria2.Image = null;
                    pbVitoria3.Image = null;
                    pbVitoria4.Image = null;
                    pbJogada1.Image = null;
                    pbJogada2.Image = null;
                    pbJogada3.Image = null;
                    pbJogada4.Image = null;
                    lblResultado.Text = "Nova Rodada";
                    putConfirmasHttp(0, "novaL", confirm.primeiroPlayer);
                    dados = getDadosHttp();
                    lblPontosLO.Text = "Pontos L/O: " + dados.pontosH;
                    lblPontosNS.Text = "Pontos N/S: " + dados.pontosV;
                    lblMelhorDeTres.Text = "-";
                    if (dados.pontosV == 11)
                        maode11 = true;
                    if (maode11)
                    {
                        do
                        {
                            deck = getBaralhoHttp();
                        } while (deck.maoSul == null);
                        deck = getBaralhoHttp();
                        DialogResult dialogResult = MessageBox.Show("Aceitar?\r\n" + string.Join(Environment.NewLine, deck.maoSul) + "\r\n (Mão do parceiro)", "mão de 11", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            maode11 = false;
                            putChatHttp("Norte aceitou a mão de 11", 0);
                            putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, 3, dados.numeroCartasJogadas);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            chat = getChatHttp();
                            if (chat.cont11 >= 2)
                            {
                                dados = getDadosHttp();
                                confirm = getConfirmaHttp();
                                putConfirmasHttp(0, "RodadaS", confirm.primeiroPlayer);
                                if (jooj.getJogador().Replace("-Vira", "") == "N" || jooj.getJogador().Replace("-Vira", "") == "S")
                                {
                                    putDadosHttp("esc/esc", "esc/esc", "3/Paus", "3/Paus", dados.manilha, dados.pontosH, dados.pontosV, 1, 147);
                                    trReset.pediu = "L";
                                }
                                if (jooj.getJogador().Replace("-Vira", "") == "L" || jooj.getJogador().Replace("-Vira", "") == "O")
                                {
                                    putDadosHttp("3/Paus", "3/Paus", "esc/esc", "esc/esc", dados.manilha, dados.pontosH, dados.pontosV, 1, 147);
                                    trReset.pediu = "S";
                                }
                                putTrucoHttp(trReset);
                                trReset.pediu = "-";
                            }
                            else
                            {
                                putChatHttp("Norte não aceitou a mão de 11", chat.cont11 + 1);
                            }

                        }
                    }
                }
                if (!jooj.getJogador().EndsWith("Vira") && confirm.turno.Contains("novaL"))
                {
                    novaRodada();
                    jooj.setMelhorH(0);
                    jooj.setMelhorV(0);
                    pbVitoria1.Image = null;
                    pbVitoria2.Image = null;
                    pbVitoria3.Image = null;
                    pbVitoria4.Image = null;
                    pbJogada1.Image = null;
                    pbJogada2.Image = null;
                    pbJogada3.Image = null;
                    pbJogada4.Image = null;
                    lblResultado.Text = "Nova Rodada";
                    putConfirmasHttp(0, "novaO", confirm.primeiroPlayer);
                    dados = getDadosHttp();
                    lblPontosLO.Text = "Pontos L/O: " + dados.pontosH;
                    lblPontosNS.Text = "Pontos N/S: " + dados.pontosV;
                    lblMelhorDeTres.Text = "-";
                    if (dados.pontosH == 11)
                        maode11 = true;
                    if (maode11)
                    {
                        do
                        {
                            deck = getBaralhoHttp();
                        } while (deck.maoOeste == null);
                        deck = getBaralhoHttp();
                        DialogResult dialogResult = MessageBox.Show("Aceitar?\r\n" + string.Join(Environment.NewLine, deck.maoOeste) + "\r\n (Mão do parceiro)", "mão de 11", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            maode11 = false;
                            putChatHttp("Leste aceitou a mão de 11", 0);
                            putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, 3, dados.numeroCartasJogadas);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            chat = getChatHttp();
                            if (chat.cont11 >= 2)
                            {
                                dados = getDadosHttp();
                                confirm = getConfirmaHttp();
                                putConfirmasHttp(0, "RodadaS", confirm.primeiroPlayer);
                                if (jooj.getJogador().Replace("-Vira", "") == "N" || jooj.getJogador().Replace("-Vira", "") == "S")
                                {
                                    putDadosHttp("esc/esc", "esc/esc", "3/Paus", "3/Paus", dados.manilha, dados.pontosH, dados.pontosV, 1, 147);
                                    trReset.pediu = "L";
                                }
                                if (jooj.getJogador().Replace("-Vira", "") == "L" || jooj.getJogador().Replace("-Vira", "") == "O")
                                {
                                    putDadosHttp("3/Paus", "3/Paus", "esc/esc", "esc/esc", dados.manilha, dados.pontosH, dados.pontosV, 1, 147);
                                    trReset.pediu = "S";
                                }
                                putTrucoHttp(trReset);
                                trReset.pediu = "-";
                            }
                            else
                            {
                                putChatHttp("Leste não aceitou a mão de 11", chat.cont11 + 1);
                            }

                        }
                    }
                }
                if (!jooj.getJogador().EndsWith("Vira") && confirm.turno.Contains("novaO"))
                {
                    novaRodada();
                    jooj.setMelhorH(0);
                    jooj.setMelhorV(0);
                    pbVitoria1.Image = null;
                    pbVitoria2.Image = null;
                    pbVitoria3.Image = null;
                    pbVitoria4.Image = null;
                    pbJogada1.Image = null;
                    pbJogada2.Image = null;
                    pbJogada3.Image = null;
                    pbJogada4.Image = null;
                    lblResultado.Text = "Nova Rodada";
                    putConfirmasHttp(0, novoPrimeiroPlayer(), novoPrimeiroPlayer());
                    putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, 1, 0);
                    dados = getDadosHttp();
                    lblPontosLO.Text = "Pontos L/O: " + dados.pontosH;
                    lblPontosNS.Text = "Pontos N/S: " + dados.pontosV;
                    lblMelhorDeTres.Text = "-";
                    if (dados.pontosH == 11)
                        maode11 = true;
                    if (maode11)
                    {
                        do
                        {
                            deck = getBaralhoHttp();
                        } while (deck.maoLeste == null);
                        deck = getBaralhoHttp();
                        DialogResult dialogResult = MessageBox.Show("Aceitar?\r\n" + string.Join(Environment.NewLine, deck.maoLeste) + "\r\n (Mão do parceiro)", "mão de 11", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            maode11 = false;
                            putChatHttp("Oeste aceitou a mão de 11", chat.cont11 + 1);
                            putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, 3, dados.numeroCartasJogadas);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            chat = getChatHttp();
                            if (chat.cont11 >= 2)
                            {
                                dados = getDadosHttp();
                                confirm = getConfirmaHttp();
                                putConfirmasHttp(0, "RodadaS", confirm.primeiroPlayer);
                                if (jooj.getJogador().Replace("-Vira", "") == "N" || jooj.getJogador().Replace("-Vira", "") == "S")
                                {
                                    putDadosHttp("esc/esc", "esc/esc", "3/Paus", "3/Paus", dados.manilha, dados.pontosH, dados.pontosV, 1, 147);
                                    trReset.pediu = "L";
                                }
                                if (jooj.getJogador().Replace("-Vira", "") == "L" || jooj.getJogador().Replace("-Vira", "") == "O")
                                {
                                    putDadosHttp("3/Paus", "3/Paus", "esc/esc", "esc/esc", dados.manilha, dados.pontosH, dados.pontosV, 1, 147);
                                    trReset.pediu = "S";
                                }
                                putTrucoHttp(trReset);
                                trReset.pediu = "-";
                            }
                            else
                            {
                                putChatHttp("Oeste não aceitou a mão de 11", chat.cont11 + 1);
                            }

                        }
                    }
                }
                if (gameOver().Contains("VENCEU"))
                {
                    MessageBox.Show(gameOver());
                    putDadosHttp("-", "-", "-", "-", "-", 0, 0, 1, 0);
                    putBaralhoHttp(deck);
                    putConfirmasHttp(0, "S", "S");
                    putTrucoHttp(trReset);
                    putChatHttp("-", 0);
                    this.Close();
                }
            }
        }

        //ESSE AQUI EU NEM TESTEI AINDA

        public void MetodoTrucoAceita()
        {
            truco = getTrucoHttp();
            confirm = getConfirmaHttp();
            Truco tr = new Truco();
            dados = getDadosHttp();
            jooj = new Jogador();

            tr.pediu = truco.pediu;
            tr.aceitou = truco.aceitou;
            tr.frase = truco.frase;
            tr.valor = truco.valor;
            tr.contador = truco.contador;
            tr.exibir = false;
            if (truco.pediu != jooj.getJogador().Replace("-Vira", ""))
                putTrucoHttp(tr);
            if (truco.pediu != "-" && truco.pediu != jooj.getJogador().Replace("-Vira", "") &&
                (("N" == truco.pediu && jooj.getJogador().Replace("-Vira", "") != "S") ||
                ("S" == truco.pediu && jooj.getJogador().Replace("-Vira", "") != "N") ||
                ("L" == truco.pediu && jooj.getJogador().Replace("-Vira", "") != "O") ||
                ("O" == truco.pediu && jooj.getJogador().Replace("-Vira", "") != "L"))
                && truco.exibir == true)
            {
                truco = getTrucoHttp();
                DialogResult dialogResult = MessageBox.Show(truco.frase + "    (Aceitar?)", nomeJogada(truco.valor) + " DE " + nomeJogador(truco.pediu), MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    truco = getTrucoHttp();
                    if(truco.aceitou == "-")
                    {
                        putConfirmasHttp(confirm.confirma, truco.pediu, confirm.primeiroPlayer);
                        tr.pediu = "-";
                        tr.aceitou = jooj.getJogador().Replace("-Vira", "");
                        tr.frase = "-";
                        tr.valor = truco.valor;
                        tr.contador = 0;
                        tr.exibir = false;
                        putTrucoHttp(tr);
                        putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, tr.valor, dados.numeroCartasJogadas);
                        btnTruco.Enabled = true;
                        btnTruco.Text = nomeJogada(tr.valor + 3);
                        putChatHttp(nomeJogador(jooj.getJogador().Replace("-Vira", "")) + " aceitou o " + nomeJogada(tr.valor), 0);
                    }
                   
                }
                else if (dialogResult == DialogResult.No)
                {
                    truco = getTrucoHttp();
                    if (truco.aceitou == "-")
                    {
                        
                        tr.pediu = truco.pediu;
                        tr.aceitou = truco.aceitou;
                        tr.frase = truco.frase;
                        tr.valor = truco.valor;
                        tr.contador = truco.contador + 1;
                        tr.exibir = false;
                        putChatHttp(nomeJogador(jooj.getJogador().Replace("-Vira", "")) + " correu do " + nomeJogada(tr.valor), 0);
                        putTrucoHttp(tr);
                    }
                }
            }

        }
        public void MetodoTrucoCorreram()
        {
            int val = 1;
            dados = getDadosHttp();
            truco = getTrucoHttp();
            if (truco.valor > 3)
                val = truco.valor - 3;
            confirm = getConfirmaHttp();
            putConfirmasHttp(0, "RodadaS", confirm.primeiroPlayer);
            if (truco.pediu == "S" || truco.pediu == "N")
            {
                putDadosHttp("esc/esc", "esc/esc", "3/Paus", "3/Paus", dados.manilha, dados.pontosH, dados.pontosV, val, 147);
            }

            if (truco.pediu == "L" || truco.pediu == "O")
            {
                putDadosHttp("3/Paus", "3/Paus", "esc/esc", "esc/esc", dados.manilha, dados.pontosH, dados.pontosV, val, 147);
            }
            MessageBox.Show("Não Aceitaram, a rodada é sua.");
            putChatHttp("Correram do truco",0);

        }
        // EVENTOS DO FORM
        // EVENTOS DO FORM
        // EVENTOS DO FORM
        // EVENTOS DO FORM
        // EVENTOS DO FORM
        // EVENTOS DO FORM
        private void Form1_Load(object sender, EventArgs e)
        {
            //ATENÇÃO: ESSAS LINHAS ABAIXO RESETAM OS SERVERS, BASTA COLOCAR UM BREAK POINT EM CONECTAR() E DAR JUNINHO
            //putDadosHttp("-", "-", "-", "-", "-", 0, 0, 1, 0);
            //putBaralhoHttp(deck);
            //putConfirmasHttp(0, "S", "S");
            //putTrucoHttp(trReset);
            //putChatHttp("-", 0);
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


            if (jooj.getJogador().StartsWith("S"))
            {
                deck.maoSul = maos;
            }
            if (jooj.getJogador().StartsWith("N"))
            {
                deck.maoNorte = maos;
            }
            if (jooj.getJogador().StartsWith("L"))
            {
                deck.maoLeste = maos;
            }
            if (jooj.getJogador().StartsWith("O"))
            {
                deck.maoOeste = maos;
            }

            putBaralhoHttp(deck);

            pbBaralho.Image = escolheCarta(defineManilia);
            if (jooj.getJogador().StartsWith("S"))
            {
                pbCarta1.Image = escolheCarta(mao[0]);
                pbCarta2.Image = escolheCarta(mao[1]);
                pbCarta3.Image = escolheCarta(mao[2]);
            }
            if (jooj.getJogador().StartsWith("N"))
            {
                pbCarta4.Image = escolheCarta(mao[0]);
                pbCarta5.Image = escolheCarta(mao[1]);
                pbCarta6.Image = escolheCarta(mao[2]);
            }
            if (jooj.getJogador().StartsWith("L"))
            {
                pbCarta7.Image = escolheCarta(mao[0]);
                pbCarta8.Image = escolheCarta(mao[1]);
                pbCarta9.Image = escolheCarta(mao[2]);
            }
            if (jooj.getJogador().StartsWith("O"))
            {
                pbCarta10.Image = escolheCarta(mao[0]);
                pbCarta11.Image = escolheCarta(mao[1]);
                pbCarta12.Image = escolheCarta(mao[2]);
            }
        }

        //BOTÕES: MUDAM AS IMAGENS DAS CARTAS E BLOQUEIAM OS CLIQUES EM SEGUINDA, TB MANDAM O PRÓXIMO TURNO PRO SERVER

        private void pbCarta1_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada1.Image = pbCarta1.Image;
            reporCarta1();
            pbCarta1.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[0];
            j1 = pbJogada1.Image;
            if (escondida)
            {
                jog1 = esconde;
                pbJogada1.Image = Properties.Resources.card_game_48983_960_720;
            }
            if (confirm.turno.Length <= 1)
                proxTurno("S");
            putDadosHttp(dados.jogadorN, cartaToString(jog1), dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            putChatHttp("Sul jogou: " + cartaToString(jog1).Replace("/"," de "), chat.cont11);
            confirm = getConfirmaHttp();
            pbCarta1.Enabled = false;
            pbCarta2.Enabled = false;
            pbCarta3.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";

        }
        private void pbCarta2_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada1.Image = pbCarta2.Image;
            reporCarta1();
            pbCarta2.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[1];
            j1 = pbJogada1.Image;
            if (escondida)
            {
                jog1 = esconde;
                pbJogada1.Image = Properties.Resources.card_game_48983_960_720;
            }
            if (confirm.turno.Length <= 1)
                proxTurno("S");
            putDadosHttp(dados.jogadorN, cartaToString(jog1), dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            confirm = getConfirmaHttp();
            putChatHttp("Sul jogou: " + cartaToString(jog1).Replace("/", " de "), chat.cont11);
            pbCarta1.Enabled = false;
            pbCarta2.Enabled = false;
            pbCarta3.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }
        private void pbCarta3_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada1.Image = pbCarta3.Image;
            reporCarta1();
            pbCarta3.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[2];
            j1 = pbJogada1.Image;
            if (escondida)
            {
                jog1 = esconde;
                pbJogada1.Image = Properties.Resources.card_game_48983_960_720;
            }
            if (confirm.turno.Length <= 1)
                proxTurno("S");
            putDadosHttp(dados.jogadorN, cartaToString(jog1), dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            confirm = getConfirmaHttp();
            putChatHttp("Sul jogou: " + cartaToString(jog1).Replace("/", " de "), chat.cont11);
            pbCarta1.Enabled = false;
            pbCarta2.Enabled = false;
            pbCarta3.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }

        private void pbCarta4_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada2.Image = pbCarta4.Image;
            reporCarta2();
            pbCarta4.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[0];
            j2 = pbJogada2.Image;
            if (escondida)
            {
                jog2 = esconde;
                pbJogada2.Image = Properties.Resources.card_game_48983_960_720;
            }
            if (confirm.turno.Length <= 1)
                proxTurno("N");
            putDadosHttp(cartaToString(jog2), dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            confirm = getConfirmaHttp();
            putChatHttp("Norte jogou: " + cartaToString(jog2).Replace("/", " de "), chat.cont11);
            pbCarta4.Enabled = false;
            pbCarta5.Enabled = false;
            pbCarta6.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }
        private void pbCarta5_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada2.Image = pbCarta5.Image;
            reporCarta2();
            pbCarta5.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[1];
            j2 = pbJogada2.Image;
            if (escondida)
            {
                jog2 = esconde;
                pbJogada2.Image = Properties.Resources.card_game_48983_960_720;
            }
            if (confirm.turno.Length <= 1)
                proxTurno("N");
            putDadosHttp(cartaToString(jog2), dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            confirm = getConfirmaHttp();
            putChatHttp("Norte jogou: " + cartaToString(jog2).Replace("/", " de "), chat.cont11);
            pbCarta4.Enabled = false;
            pbCarta5.Enabled = false;
            pbCarta6.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }
        private void pbCarta6_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada2.Image = pbCarta6.Image;
            reporCarta2();
            pbCarta6.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[2];
            if (confirm.turno.Length <= 1)
                proxTurno("N");

            if (escondida)
            {
                jog2 = esconde;
                pbJogada2.Image = Properties.Resources.card_game_48983_960_720;
            }
            putDadosHttp(cartaToString(jog2), dados.jogadorS, dados.jogadorL, dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j2 = pbJogada2.Image;
            confirm = getConfirmaHttp();
            putChatHttp("Norte jogou: " + cartaToString(jog2).Replace("/", " de "), chat.cont11);
            pbCarta4.Enabled = false;
            pbCarta5.Enabled = false;
            pbCarta6.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }

        private void pbCarta7_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada3.Image = pbCarta7.Image;
            reporCarta3();
            pbCarta7.Image = Properties.Resources.card_game_48983_960_720;
            jog3 = maos[0];
            if (confirm.turno.Length <= 1)
                proxTurno("L");

            if (escondida)
            {
                jog3 = esconde;
                pbJogada3.Image = Properties.Resources.card_game_48983_960_720;
            }
            putDadosHttp(dados.jogadorN, dados.jogadorS, cartaToString(jog3), dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j3 = pbJogada3.Image;
            confirm = getConfirmaHttp();
            putChatHttp("Leste jogou: " + cartaToString(jog3).Replace("/", " de "), chat.cont11);
            pbCarta7.Enabled = false;
            pbCarta8.Enabled = false;
            pbCarta9.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }
        private void pbCarta8_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada3.Image = pbCarta8.Image;
            reporCarta3();
            pbCarta8.Image = Properties.Resources.card_game_48983_960_720;
            jog3 = maos[1];
            if (confirm.turno.Length <= 1)
                proxTurno("L");

            if (escondida)
            {
                jog3 = esconde;
                pbJogada3.Image = Properties.Resources.card_game_48983_960_720;
            }
            putDadosHttp(dados.jogadorN, dados.jogadorS, cartaToString(jog3), dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j3 = pbJogada3.Image;
            confirm = getConfirmaHttp();
            putChatHttp("Leste jogou: " + cartaToString(jog3).Replace("/", " de "), chat.cont11);
            pbCarta7.Enabled = false;
            pbCarta8.Enabled = false;
            pbCarta9.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }
        private void pbCarta9_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada3.Image = pbCarta9.Image;
            reporCarta3();
            pbCarta9.Image = Properties.Resources.card_game_48983_960_720;
            jog3 = maos[2];
            if (confirm.turno.Length <= 1)
                proxTurno("L");

            if (escondida)
            {
                jog3 = esconde;
                pbJogada3.Image = Properties.Resources.card_game_48983_960_720;
            }
            putDadosHttp(dados.jogadorN, dados.jogadorS, cartaToString(jog3), dados.jogadorO, dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j3 = pbJogada3.Image;
            confirm = getConfirmaHttp();
            putChatHttp("Leste jogou: " + cartaToString(jog3).Replace("/", " de "), chat.cont11);
            pbCarta7.Enabled = false;
            pbCarta8.Enabled = false;
            pbCarta9.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }

        private void pbCarta10_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada4.Image = pbCarta10.Image;
            reporCarta4();
            pbCarta10.Image = Properties.Resources.card_game_48983_960_720;
            jog4 = maos[0];
            if (confirm.turno.Length <= 1)
                proxTurno("O");

            if (escondida)
            {
                jog4 = esconde;
                pbJogada4.Image = Properties.Resources.card_game_48983_960_720;
            }
            putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, cartaToString(jog4), dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j4 = pbJogada4.Image;
            confirm = getConfirmaHttp();
            putChatHttp("Oeste jogou: " + cartaToString(jog4).Replace("/", " de "), chat.cont11);
            pbCarta10.Enabled = false;
            pbCarta11.Enabled = false;
            pbCarta12.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }
        private void pbCarta11_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada4.Image = pbCarta11.Image;
            reporCarta4();
            pbCarta11.Image = Properties.Resources.card_game_48983_960_720;
            jog4 = maos[1];
            if (confirm.turno.Length <= 1)
                proxTurno("O");

            if (escondida)
            {
                jog4 = esconde;
                pbJogada4.Image = Properties.Resources.card_game_48983_960_720;
            }
            putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, cartaToString(jog4), dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j4 = pbJogada4.Image;
            confirm = getConfirmaHttp();
            putChatHttp("Oeste jogou: " + cartaToString(jog4).Replace("/", " de "), chat.cont11);
            pbCarta10.Enabled = false;
            pbCarta11.Enabled = false;
            pbCarta12.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }
        private void pbCarta12_Click(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            pbJogada4.Image = pbCarta12.Image;
            reporCarta4();
            pbCarta12.Image = Properties.Resources.card_game_48983_960_720;
            jog4 = maos[2];
            if (confirm.turno.Length <= 1)
                proxTurno("O");

            if (escondida)
            {
                jog4 = esconde;
                pbJogada4.Image = Properties.Resources.card_game_48983_960_720;
            }
            putDadosHttp(dados.jogadorN, dados.jogadorS, dados.jogadorL, cartaToString(jog4), dados.manilha, dados.pontosH, dados.pontosV, dados.valorRodada, dados.numeroCartasJogadas + 1);
            j4 = pbJogada4.Image;
            confirm = getConfirmaHttp();
            putChatHttp("Oeste jogou: " + cartaToString(jog4).Replace("/", " de "), chat.cont11);
            pbCarta10.Enabled = false;
            pbCarta11.Enabled = false;
            pbCarta12.Enabled = false;
            lblResultado.Text = "Aguarde as jogadas";
        }
        //R DE RESET

        private void BaralhoHttp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                putDadosHttp("-", "-", "-", "-", "-", 0, 0, 1, 0);
                putBaralhoHttp(deck);
                putConfirmasHttp(0, "S", "S");
                Truco tr = new Truco();
                tr.pediu = "-";
                tr.aceitou = "-";
                tr.frase = "-";
                tr.valor = 0;
                tr.contador = 0;
                tr.exibir = false;
                putTrucoHttp(tr);
                putChatHttp("-", 0);
            }
        }

        //Limpa o campo do jogador no server

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

        //FUNCIONA

        private void btnTruco_Click(object sender, EventArgs e)
        {
            truco = getTrucoHttp();
            chat = getChatHttp();
            jooj = new Jogador();
            Truco tr = new Truco();
            if (truco.valor == 0)
                tr.valor = 3;
            if (truco.valor == 3)
                tr.valor = 6;
            if (truco.valor == 6)
                tr.valor = 9;
            if (truco.valor == 9)
                tr.valor = 12;

            tr.pediu = jooj.getJogador().Replace("-Vira", "");
            tr.aceitou = "-";
            tr.frase = fraseDoTruco();
            tr.contador = 0;
            tr.exibir = true;
            putTrucoHttp(tr);
            confirm = getConfirmaHttp();
            putConfirmasHttp(confirm.confirma, "Truco", confirm.primeiroPlayer);
            putChatHttp(nomeJogador(jooj.getJogador().Replace("-Vira","")) + " pediu " + nomeJogada(tr.valor), 0);
            btnTruco.Enabled = false;
        }

        //FUNCIONA

        private void btnEscondidaAberta_Click(object sender, EventArgs e)
        {
            if (btnEscondidaAberta.Text == "escondida")
            {
                btnEscondidaAberta.Text = "aberta";
                escondida = true;
            }
            else if (btnEscondidaAberta.Text == "aberta")
            {
                btnEscondidaAberta.Text = "escondida";
                escondida = false;
            }
        }

        //Tentativa de timer global (N FUNCIONA TD)

        private void tmrAltera_Tick(object sender, EventArgs e)
        {
            dados = getDadosHttp();
            lblPontosLO.Text = "Pontos L/O: " + dados.pontosH;
            lblPontosNS.Text = "Pontos N/S: " + dados.pontosV;
            atualizarTela();
            if (lblMelhorDeTres.Text.Contains(","))
                btnEscondidaAberta.Enabled = true;
            confirm = getConfirmaHttp();
            if (jooj.getJogador().Replace("-Vira", "") == confirm.turno)
            {
                rodada1();
            }

            if (confirm.turno.Contains("Rodada") && confirm.turno.Contains(jooj.getJogador().Replace("-Vira", "")))
            {
                rodada2();
            }
            if (confirm.turno.Contains("nova") && confirm.turno.Contains(jooj.getJogador().Replace("-Vira", "")))
            {
                rearranjar();
            }
            truco = getTrucoHttp();
            if ("Truco" == confirm.turno)
            {
                if (truco.pediu != jooj.getJogador().Replace("-Vira", "") &&
                (("N" == truco.pediu && jooj.getJogador().Replace("-Vira", "") != "S") ||
                ("S" == truco.pediu && jooj.getJogador().Replace("-Vira", "") != "N") ||
                ("L" == truco.pediu && jooj.getJogador().Replace("-Vira", "") != "O") ||
                ("O" == truco.pediu && jooj.getJogador().Replace("-Vira", "") != "L")))
                    MetodoTrucoAceita();
                if (truco.pediu == jooj.getJogador().Replace("-Vira", "") && truco.contador >= 2)
                    MetodoTrucoCorreram();

            }
        }

    }

    // OUTRAS CLASSES
    // OUTRAS CLASSES
    // OUTRAS CLASSES
    // OUTRAS CLASSES
    // OUTRAS CLASSES
    // OUTRAS CLASSES

    // CLASSE JOGADOR, GUARDA 3 VARIAVEIS ENCAPSULADAS (Nº DE CARTAS JOGADAS, PONTOS DA MELHOR DE TRES DA DUPLA HORIZONTAL
    //, PONTOS DA MELHOR DE TRES DA DUPLA VERTICAL

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

    //Cartas, pra deixar intuitivo

    public class Carta
    {
        public string naipe { get; set; }
        public string numero { get; set; }
    }

    //Confirma pra se cominucar com o server q controla os turnos (N TA FUNCIONANDO DIREITO PELO VISTO)

    public class Confirma
    {
        public int confirma { get; set; }
        public string turno { get; set; }
        public string primeiroPlayer { get; set; }
    }

    //DADOS, guarda as informações da messa e os pontos das duplas

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

    // Truco pra controlar o pedido de truco, se comunica com um server separado

    public class Truco
    {
        public string pediu { get; set; }
        public string aceitou { get; set; }
        public string frase { get; set; }
        public bool exibir { get; set; }
        public int valor { get; set; }
        public int contador { get; set; }
    }
    //  Chat e contador para melhor de 11
    public class Chat
    {
        
        public string texto { get; set; }
        public int cont11 { get; set; }
    }
    // Classe  de baralho q aparece na primeira opção de procura do google, no momento funciona perfeitamente

    public class Deck
    {
        public List<Carta> maoSul { get; set; }
        public List<Carta> maoNorte { get; set; }
        public List<Carta> maoLeste { get; set; }
        public List<Carta> maoOeste { get; set; }
        public List<Carta> DeckOfCartas { get; set; }
        private Jogador j = new Jogador();

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