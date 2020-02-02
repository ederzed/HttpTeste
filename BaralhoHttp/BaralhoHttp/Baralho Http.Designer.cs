namespace BaralhoHttp
{
    partial class BaralhoHttp
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblResultado = new System.Windows.Forms.Label();
            this.tmrResultado = new System.Windows.Forms.Timer(this.components);
            this.pbVitoria2 = new System.Windows.Forms.PictureBox();
            this.pbVitoria1 = new System.Windows.Forms.PictureBox();
            this.pbJogada2 = new System.Windows.Forms.PictureBox();
            this.pbJogada1 = new System.Windows.Forms.PictureBox();
            this.pbCarta6 = new System.Windows.Forms.PictureBox();
            this.pbCarta5 = new System.Windows.Forms.PictureBox();
            this.pbCarta4 = new System.Windows.Forms.PictureBox();
            this.pbCarta3 = new System.Windows.Forms.PictureBox();
            this.pbCarta2 = new System.Windows.Forms.PictureBox();
            this.pbCarta1 = new System.Windows.Forms.PictureBox();
            this.pbBaralho = new System.Windows.Forms.PictureBox();
            this.tmrRearranjar = new System.Windows.Forms.Timer(this.components);
            this.lblPontosSul = new System.Windows.Forms.Label();
            this.lblPontosNorte = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMelhorDeTres = new System.Windows.Forms.Label();
            this.btnTruco = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbVitoria2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVitoria1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbJogada2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbJogada1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBaralho)).BeginInit();
            this.SuspendLayout();
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(12, 425);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(120, 13);
            this.lblResultado.TabIndex = 9;
            this.lblResultado.Text = "Aguardando Oponentes";
            // 
            // tmrResultado
            // 
            this.tmrResultado.Enabled = true;
            this.tmrResultado.Interval = 1000;
            this.tmrResultado.Tick += new System.EventHandler(this.tmrResultado_Tick);
            // 
            // pbVitoria2
            // 
            this.pbVitoria2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbVitoria2.ImageLocation = "";
            this.pbVitoria2.Location = new System.Drawing.Point(288, 64);
            this.pbVitoria2.Name = "pbVitoria2";
            this.pbVitoria2.Size = new System.Drawing.Size(17, 19);
            this.pbVitoria2.TabIndex = 11;
            this.pbVitoria2.TabStop = false;
            // 
            // pbVitoria1
            // 
            this.pbVitoria1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbVitoria1.ImageLocation = "";
            this.pbVitoria1.Location = new System.Drawing.Point(488, 367);
            this.pbVitoria1.Name = "pbVitoria1";
            this.pbVitoria1.Size = new System.Drawing.Size(17, 19);
            this.pbVitoria1.TabIndex = 10;
            this.pbVitoria1.TabStop = false;
            // 
            // pbJogada2
            // 
            this.pbJogada2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbJogada2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbJogada2.ImageLocation = "";
            this.pbJogada2.Location = new System.Drawing.Point(370, 89);
            this.pbJogada2.Name = "pbJogada2";
            this.pbJogada2.Size = new System.Drawing.Size(53, 71);
            this.pbJogada2.TabIndex = 8;
            this.pbJogada2.TabStop = false;
            // 
            // pbJogada1
            // 
            this.pbJogada1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbJogada1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbJogada1.ImageLocation = "";
            this.pbJogada1.Location = new System.Drawing.Point(370, 290);
            this.pbJogada1.Name = "pbJogada1";
            this.pbJogada1.Size = new System.Drawing.Size(53, 71);
            this.pbJogada1.TabIndex = 7;
            this.pbJogada1.TabStop = false;
            // 
            // pbCarta6
            // 
            this.pbCarta6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCarta6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCarta6.Enabled = false;
            this.pbCarta6.Image = global::BaralhoHttp.Properties.Resources.card_game_48983_960_720;
            this.pbCarta6.ImageLocation = "";
            this.pbCarta6.Location = new System.Drawing.Point(429, 12);
            this.pbCarta6.Name = "pbCarta6";
            this.pbCarta6.Size = new System.Drawing.Size(53, 71);
            this.pbCarta6.TabIndex = 6;
            this.pbCarta6.TabStop = false;
            this.pbCarta6.Click += new System.EventHandler(this.pbCarta6_Click);
            // 
            // pbCarta5
            // 
            this.pbCarta5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCarta5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCarta5.Enabled = false;
            this.pbCarta5.Image = global::BaralhoHttp.Properties.Resources.card_game_48983_960_720;
            this.pbCarta5.ImageLocation = "";
            this.pbCarta5.Location = new System.Drawing.Point(370, 12);
            this.pbCarta5.Name = "pbCarta5";
            this.pbCarta5.Size = new System.Drawing.Size(53, 71);
            this.pbCarta5.TabIndex = 5;
            this.pbCarta5.TabStop = false;
            this.pbCarta5.Click += new System.EventHandler(this.pbCarta5_Click);
            // 
            // pbCarta4
            // 
            this.pbCarta4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCarta4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCarta4.Enabled = false;
            this.pbCarta4.Image = global::BaralhoHttp.Properties.Resources.card_game_48983_960_720;
            this.pbCarta4.ImageLocation = "";
            this.pbCarta4.Location = new System.Drawing.Point(311, 12);
            this.pbCarta4.Name = "pbCarta4";
            this.pbCarta4.Size = new System.Drawing.Size(53, 71);
            this.pbCarta4.TabIndex = 4;
            this.pbCarta4.TabStop = false;
            this.pbCarta4.Click += new System.EventHandler(this.pbCarta4_Click);
            // 
            // pbCarta3
            // 
            this.pbCarta3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCarta3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCarta3.Enabled = false;
            this.pbCarta3.Image = global::BaralhoHttp.Properties.Resources.card_game_48983_960_720;
            this.pbCarta3.ImageLocation = "";
            this.pbCarta3.Location = new System.Drawing.Point(429, 367);
            this.pbCarta3.Name = "pbCarta3";
            this.pbCarta3.Size = new System.Drawing.Size(53, 71);
            this.pbCarta3.TabIndex = 3;
            this.pbCarta3.TabStop = false;
            this.pbCarta3.Click += new System.EventHandler(this.pbCarta3_Click);
            // 
            // pbCarta2
            // 
            this.pbCarta2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCarta2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCarta2.Enabled = false;
            this.pbCarta2.Image = global::BaralhoHttp.Properties.Resources.card_game_48983_960_720;
            this.pbCarta2.ImageLocation = "";
            this.pbCarta2.Location = new System.Drawing.Point(370, 367);
            this.pbCarta2.Name = "pbCarta2";
            this.pbCarta2.Size = new System.Drawing.Size(53, 71);
            this.pbCarta2.TabIndex = 2;
            this.pbCarta2.TabStop = false;
            this.pbCarta2.Click += new System.EventHandler(this.pbCarta2_Click);
            // 
            // pbCarta1
            // 
            this.pbCarta1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCarta1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCarta1.Enabled = false;
            this.pbCarta1.Image = global::BaralhoHttp.Properties.Resources.card_game_48983_960_720;
            this.pbCarta1.ImageLocation = "";
            this.pbCarta1.Location = new System.Drawing.Point(311, 367);
            this.pbCarta1.Name = "pbCarta1";
            this.pbCarta1.Size = new System.Drawing.Size(53, 71);
            this.pbCarta1.TabIndex = 1;
            this.pbCarta1.TabStop = false;
            this.pbCarta1.Click += new System.EventHandler(this.pbCarta1_Click);
            // 
            // pbBaralho
            // 
            this.pbBaralho.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbBaralho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBaralho.Image = global::BaralhoHttp.Properties.Resources.card_game_48983_960_720;
            this.pbBaralho.ImageLocation = "";
            this.pbBaralho.Location = new System.Drawing.Point(370, 184);
            this.pbBaralho.Name = "pbBaralho";
            this.pbBaralho.Size = new System.Drawing.Size(53, 71);
            this.pbBaralho.TabIndex = 0;
            this.pbBaralho.TabStop = false;
            // 
            // tmrRearranjar
            // 
            this.tmrRearranjar.Interval = 3000;
            this.tmrRearranjar.Tick += new System.EventHandler(this.tmrRearranjar_Tick);
            // 
            // lblPontosSul
            // 
            this.lblPontosSul.AutoSize = true;
            this.lblPontosSul.Location = new System.Drawing.Point(517, 425);
            this.lblPontosSul.Name = "lblPontosSul";
            this.lblPontosSul.Size = new System.Drawing.Size(70, 13);
            this.lblPontosSul.TabIndex = 12;
            this.lblPontosSul.Text = "Pontos Sul: 0";
            // 
            // lblPontosNorte
            // 
            this.lblPontosNorte.AutoSize = true;
            this.lblPontosNorte.Location = new System.Drawing.Point(671, 425);
            this.lblPontosNorte.Name = "lblPontosNorte";
            this.lblPontosNorte.Size = new System.Drawing.Size(81, 13);
            this.lblPontosNorte.TabIndex = 13;
            this.lblPontosNorte.Text = "Pontos Norte: 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(517, 373);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Rodada:";
            // 
            // lblMelhorDeTres
            // 
            this.lblMelhorDeTres.AutoSize = true;
            this.lblMelhorDeTres.Location = new System.Drawing.Point(580, 373);
            this.lblMelhorDeTres.Name = "lblMelhorDeTres";
            this.lblMelhorDeTres.Size = new System.Drawing.Size(10, 13);
            this.lblMelhorDeTres.TabIndex = 15;
            this.lblMelhorDeTres.Text = "-";
            // 
            // btnTruco
            // 
            this.btnTruco.Location = new System.Drawing.Point(15, 373);
            this.btnTruco.Name = "btnTruco";
            this.btnTruco.Size = new System.Drawing.Size(75, 23);
            this.btnTruco.TabIndex = 16;
            this.btnTruco.Text = "TRUCO!";
            this.btnTruco.UseVisualStyleBackColor = true;
            // 
            // BaralhoHttp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTruco);
            this.Controls.Add(this.lblMelhorDeTres);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPontosNorte);
            this.Controls.Add(this.lblPontosSul);
            this.Controls.Add(this.pbVitoria2);
            this.Controls.Add(this.pbVitoria1);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.pbJogada2);
            this.Controls.Add(this.pbJogada1);
            this.Controls.Add(this.pbCarta6);
            this.Controls.Add(this.pbCarta5);
            this.Controls.Add(this.pbCarta4);
            this.Controls.Add(this.pbCarta3);
            this.Controls.Add(this.pbCarta2);
            this.Controls.Add(this.pbCarta1);
            this.Controls.Add(this.pbBaralho);
            this.Name = "BaralhoHttp";
            this.Text = "Baralho Http";
            this.Deactivate += new System.EventHandler(this.BaralhoHttp_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BaralhoHttp_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BaralhoHttp_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbVitoria2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVitoria1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbJogada2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbJogada1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCarta1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBaralho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBaralho;
        private System.Windows.Forms.PictureBox pbCarta1;
        private System.Windows.Forms.PictureBox pbCarta2;
        private System.Windows.Forms.PictureBox pbCarta3;
        private System.Windows.Forms.PictureBox pbCarta6;
        private System.Windows.Forms.PictureBox pbCarta5;
        private System.Windows.Forms.PictureBox pbCarta4;
        private System.Windows.Forms.PictureBox pbJogada1;
        private System.Windows.Forms.PictureBox pbJogada2;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Timer tmrResultado;
        private System.Windows.Forms.PictureBox pbVitoria1;
        private System.Windows.Forms.PictureBox pbVitoria2;
        private System.Windows.Forms.Timer tmrRearranjar;
        private System.Windows.Forms.Label lblPontosSul;
        private System.Windows.Forms.Label lblPontosNorte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMelhorDeTres;
        private System.Windows.Forms.Button btnTruco;
    }
}

