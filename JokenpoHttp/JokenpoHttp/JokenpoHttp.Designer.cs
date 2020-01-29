namespace JokenpoHttp
{
    partial class JokenpoHttp
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
            this.pbPlayer1 = new System.Windows.Forms.PictureBox();
            this.pbPlayer2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnPedra = new System.Windows.Forms.Button();
            this.btnPapel = new System.Windows.Forms.Button();
            this.btnTesoura = new System.Windows.Forms.Button();
            this.btnConfirma = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer2)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPlayer1
            // 
            this.pbPlayer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbPlayer1.Location = new System.Drawing.Point(12, 63);
            this.pbPlayer1.Name = "pbPlayer1";
            this.pbPlayer1.Size = new System.Drawing.Size(253, 241);
            this.pbPlayer1.TabIndex = 0;
            this.pbPlayer1.TabStop = false;
            // 
            // pbPlayer2
            // 
            this.pbPlayer2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbPlayer2.Location = new System.Drawing.Point(355, 63);
            this.pbPlayer2.Name = "pbPlayer2";
            this.pbPlayer2.Size = new System.Drawing.Size(253, 241);
            this.pbPlayer2.TabIndex = 1;
            this.pbPlayer2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(265, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Jôkenpo";
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(192, 327);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(235, 23);
            this.btnConectar.TabIndex = 3;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            // 
            // btnPedra
            // 
            this.btnPedra.Location = new System.Drawing.Point(192, 356);
            this.btnPedra.Name = "btnPedra";
            this.btnPedra.Size = new System.Drawing.Size(75, 23);
            this.btnPedra.TabIndex = 4;
            this.btnPedra.Text = "Pedra";
            this.btnPedra.UseVisualStyleBackColor = true;
            // 
            // btnPapel
            // 
            this.btnPapel.Location = new System.Drawing.Point(271, 356);
            this.btnPapel.Name = "btnPapel";
            this.btnPapel.Size = new System.Drawing.Size(75, 23);
            this.btnPapel.TabIndex = 5;
            this.btnPapel.Text = "Papel";
            this.btnPapel.UseVisualStyleBackColor = true;
            // 
            // btnTesoura
            // 
            this.btnTesoura.Location = new System.Drawing.Point(352, 356);
            this.btnTesoura.Name = "btnTesoura";
            this.btnTesoura.Size = new System.Drawing.Size(75, 23);
            this.btnTesoura.TabIndex = 6;
            this.btnTesoura.Text = "Tesoura";
            this.btnTesoura.UseVisualStyleBackColor = true;
            // 
            // btnConfirma
            // 
            this.btnConfirma.Location = new System.Drawing.Point(192, 385);
            this.btnConfirma.Name = "btnConfirma";
            this.btnConfirma.Size = new System.Drawing.Size(235, 23);
            this.btnConfirma.TabIndex = 7;
            this.btnConfirma.Text = "Confirmar";
            this.btnConfirma.UseVisualStyleBackColor = true;
            // 
            // JokenpoHttp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 450);
            this.Controls.Add(this.btnConfirma);
            this.Controls.Add(this.btnTesoura);
            this.Controls.Add(this.btnPapel);
            this.Controls.Add(this.btnPedra);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbPlayer2);
            this.Controls.Add(this.pbPlayer1);
            this.Name = "JokenpoHttp";
            this.Text = "Jokenpo Http";
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPlayer1;
        private System.Windows.Forms.PictureBox pbPlayer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnPedra;
        private System.Windows.Forms.Button btnPapel;
        private System.Windows.Forms.Button btnTesoura;
        private System.Windows.Forms.Button btnConfirma;
    }
}

