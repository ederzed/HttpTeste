using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        Image j1;
        Image j2;
        private void Form1_Load(object sender, EventArgs e)
        {
            deck.Shuffle();
            List<Carta> mao = deck.GoFish(6);
            List<string> teste = new List<string>();
            for(int i = 0; i< mao.Count; i++)
            {
                teste.Add(mao[i].numero + " de " + mao[i].naipe);
            }
            String m = String.Join(";", teste);
            MessageBox.Show(m);
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
            if (c.naipe == "Espadas")
                nai = 1;
            if (c.naipe == "Copas")
                nai = 2;
            if (c.naipe == "Paus")
                nai = 3;
       


            return imagens[(num + (nai*13)) - 1];
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
