namespace BaralhoHttp
{
    partial class Form1
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
            this.pbBaralho = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBaralho)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBaralho
            // 
            this.pbBaralho.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbBaralho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBaralho.Image = global::BaralhoHttp.Properties.Resources.card_game_48983_960_720;
            this.pbBaralho.ImageLocation = "";
            this.pbBaralho.Location = new System.Drawing.Point(360, 173);
            this.pbBaralho.Name = "pbBaralho";
            this.pbBaralho.Size = new System.Drawing.Size(53, 71);
            this.pbBaralho.TabIndex = 0;
            this.pbBaralho.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbBaralho);
            this.Name = "Form1";
            this.Text = "Baralho Http";
            ((System.ComponentModel.ISupportInitialize)(this.pbBaralho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBaralho;
    }
}

