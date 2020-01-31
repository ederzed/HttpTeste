using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public Deck deck = new Deck();
        Image j1 = null;
        Image j2 = null;
        Carta jog1;
        Carta jog2;
        Carta defineManilia;
        List<Carta> maos;
        int manilha = 0;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            deck.Shuffle();
            List<Carta> mao = deck.GoFish(6);
            maos = mao;
            List<string> teste = new List<string>();
            for(int i = 0; i< mao.Count; i++)
            {
                teste.Add(mao[i].numero + " de " + mao[i].naipe);
            }
            String m = String.Join(";", teste);
            MessageBox.Show(m);
            defineManilia = deck.GoFish();
            pbBaralho.Image = escolheCarta(defineManilia);
            pbCarta1.Image = escolheCarta(mao[0]);
            pbCarta2.Image = escolheCarta(mao[1]);
            pbCarta3.Image = escolheCarta(mao[2]);
            pbCarta4.Image = escolheCarta(mao[3]);
            pbCarta5.Image = escolheCarta(mao[4]);
            pbCarta6.Image = escolheCarta(mao[5]);
        }

        public Image escolheCarta(Carta c)
        {
            Image[] imagens = { Properties.Resources.As_de_ouro, Properties.Resources._2_de_ouro, Properties.Resources._3_de_ouro,
            Properties.Resources._4_de_ouro,Properties.Resources._5_de_ouro,Properties.Resources._6_de_ouro,Properties.Resources._7_de_ouro,
            Properties.Resources._8_de_ouro,Properties.Resources._9_de_ouro,Properties.Resources._10_de_ouro,Properties.Resources.dama_de_ouro,
            Properties.Resources.valete_de_ouros,Properties.Resources.rei_de_ouro,Properties.Resources.As_de_espada, Properties.Resources._2_de_espada, Properties.Resources._3_de_espada,
            Properties.Resources._4_de_espada,Properties.Resources._5_de_espada,Properties.Resources._6_de_espada,Properties.Resources._7_de_espada,
            Properties.Resources._8_de_espada,Properties.Resources._9_de_espadas,Properties.Resources._10_de_espada,Properties.Resources.dama_de_espada,
            Properties.Resources.valete_de_espada,Properties.Resources.rei_de_espada,Properties.Resources.As_de_copas, Properties.Resources._2_de_copas, Properties.Resources._3_de_copas,
            Properties.Resources._4_de_copas,Properties.Resources._5_de_copas,Properties.Resources._6_de_copas,Properties.Resources._7_de_copas,
            Properties.Resources._8_de_copas,Properties.Resources._9_de_copas,Properties.Resources._10_de_copas,Properties.Resources.dama_de_copas,
            Properties.Resources.valete_de_copas,Properties.Resources.rei_de_copas,Properties.Resources.As_de_paus, Properties.Resources._2_de_paus, Properties.Resources._3_de_paus,
            Properties.Resources._4_de_paus,Properties.Resources._5_de_paus,Properties.Resources._6_de_paus,Properties.Resources._7_de_paus,
            Properties.Resources._8_de_paus,Properties.Resources._9_de_paus,Properties.Resources._10_de_paus,Properties.Resources.dama_de_paus,
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
                    num = 13;
                if (c.numero == "Dama")
                    num = 11;
                if (c.numero == "Valete")
                    num = 12;
                    
            }

            if (c.naipe == "Ouro")
                nai = 0;
            if (c.naipe == "Espada")
                nai = 1;
            if (c.naipe == "Copas")
                nai = 2;
            if (c.naipe == "Paus")
                nai = 3;
       


            return imagens[(num + (nai*13)) - 1];
        }

        public string verVencedor(Carta j1,Carta j2, Carta manilha)
        {
            // 4 - 7 ,Q ,J ,K ,A ,2 ,3
            int c1 = valorReal(j1, manilha);
            int c2 = valorReal(j2, manilha);
            int[] cartasEmJogo = { c1, c2 };
            Array.Sort(cartasEmJogo);
            if (cartasEmJogo[cartasEmJogo.Length -1] == c1)
                return "Jogador Sul Venceu";
            else if (cartasEmJogo[cartasEmJogo.Length - 1] == c2)
                return "Jogador Norte Venceu";
            else
                return "Amarrou";
        }
        public int valorReal(Carta carta, Carta mani)
        {
            int valor = 0;
            int manilhona = 0;
            try
            {
                if (carta.numero == "2")
                    valor = 15;
                else if (carta.numero == "3")
                    valor = 16;
                else
                    valor = Convert.ToInt32(carta.numero);
            }
            catch
            {
                if (carta.numero == "Ás")
                    valor = 14;
                if (carta.numero == "Rei")
                    valor = 13;
                if (carta.numero == "Dama")
                    valor = 11;
                if (carta.numero == "Valete")
                    valor = 12;

            }
            try
            {
                if (mani.numero == "2")
                    manilhona = 16;
                else if (mani.numero == "3")
                    manilhona = 4;
                else
                    manilhona = Convert.ToInt32(mani.numero) + 1;
            }
            catch
            {
                if (mani.numero == "Ás")
                    manilhona = 15;
                if (mani.numero == "Rei")
                    manilhona = 14;
                if (mani.numero == "Dama")
                    manilhona = 12;
                if (mani.numero == "Valete")
                    manilhona = 13;

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
        private void pbCarta1_Click(object sender, EventArgs e)
        {
            pbJogada1.Image = pbCarta1.Image;
            reporCarta1();
            pbCarta1.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[0];
            j1 = pbJogada1.Image;
        }

        private void pbCarta2_Click(object sender, EventArgs e)
        {
            pbJogada1.Image = pbCarta2.Image;
            reporCarta1();
            pbCarta2.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[1];
            j1 = pbJogada1.Image;
        }

        private void pbCarta3_Click(object sender, EventArgs e)
        {
            pbJogada1.Image = pbCarta3.Image;
            reporCarta1();
            pbCarta3.Image = Properties.Resources.card_game_48983_960_720;
            jog1 = maos[2];
            j1 = pbJogada1.Image;
        }

       

        private void pbCarta4_Click(object sender, EventArgs e)
        {
            pbJogada2.Image = pbCarta4.Image;
            reporCarta2();
            pbCarta4.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[3];
            j2 = pbJogada2.Image;
        }

        private void pbCarta5_Click(object sender, EventArgs e)
        {
            pbJogada2.Image = pbCarta5.Image;
            reporCarta2();
            pbCarta5.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[4];
            j2 = pbJogada2.Image;
        }

        private void pbCarta6_Click(object sender, EventArgs e)
        {
            pbJogada2.Image = pbCarta6.Image;
            reporCarta2();
            pbCarta6.Image = Properties.Resources.card_game_48983_960_720;
            jog2 = maos[5];
            j2 = pbJogada2.Image;
        }

        private void tmrResultado_Tick(object sender, EventArgs e)
        {
            if(j2 != null && j1 != null)
            {
                lblResultado.Text = verVencedor(jog1,jog2,defineManilia);
            } 
        }
    }

    public class Carta
    {
       
        public string naipe { get; set; }
        public string numero { get; set; }
    }
    public class Deck
    {
        public List<Carta> DeckOfCartas { get; set; }

        public Deck()
        {
            List<Carta> deckOfCartas_ = new List<Carta>();
            string[] numbers = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Valete", "Dama", "Rei", "Ás" };
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
