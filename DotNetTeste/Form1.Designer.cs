
namespace DotNetTeste
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
            this.GrbDados = new System.Windows.Forms.GroupBox();
            this.DgvDados = new System.Windows.Forms.DataGridView();
            this.grbAgrupamento = new System.Windows.Forms.GroupBox();
            this.cboAgrupamento = new System.Windows.Forms.ComboBox();
            this.grbExportacao = new System.Windows.Forms.GroupBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.cboExtensao = new System.Windows.Forms.ComboBox();
            this.LblStatus = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.BtnLog = new System.Windows.Forms.Button();
            this.grdLog = new System.Windows.Forms.GroupBox();
            this.GrbDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDados)).BeginInit();
            this.grbAgrupamento.SuspendLayout();
            this.grbExportacao.SuspendLayout();
            this.grdLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrbDados
            // 
            this.GrbDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrbDados.Controls.Add(this.DgvDados);
            this.GrbDados.Location = new System.Drawing.Point(12, 72);
            this.GrbDados.Name = "GrbDados";
            this.GrbDados.Size = new System.Drawing.Size(1209, 620);
            this.GrbDados.TabIndex = 0;
            this.GrbDados.TabStop = false;
            this.GrbDados.Text = "Dados";
            // 
            // DgvDados
            // 
            this.DgvDados.AllowUserToAddRows = false;
            this.DgvDados.AllowUserToDeleteRows = false;
            this.DgvDados.AllowUserToOrderColumns = true;
            this.DgvDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDados.Location = new System.Drawing.Point(6, 19);
            this.DgvDados.MultiSelect = false;
            this.DgvDados.Name = "DgvDados";
            this.DgvDados.ReadOnly = true;
            this.DgvDados.Size = new System.Drawing.Size(1197, 595);
            this.DgvDados.TabIndex = 0;
            // 
            // grbAgrupamento
            // 
            this.grbAgrupamento.Controls.Add(this.cboAgrupamento);
            this.grbAgrupamento.Location = new System.Drawing.Point(12, 17);
            this.grbAgrupamento.Name = "grbAgrupamento";
            this.grbAgrupamento.Size = new System.Drawing.Size(282, 49);
            this.grbAgrupamento.TabIndex = 1;
            this.grbAgrupamento.TabStop = false;
            this.grbAgrupamento.Text = "Agrupar Por";
            // 
            // cboAgrupamento
            // 
            this.cboAgrupamento.FormattingEnabled = true;
            this.cboAgrupamento.Location = new System.Drawing.Point(6, 19);
            this.cboAgrupamento.Name = "cboAgrupamento";
            this.cboAgrupamento.Size = new System.Drawing.Size(269, 21);
            this.cboAgrupamento.TabIndex = 1;
            this.cboAgrupamento.SelectedIndexChanged += new System.EventHandler(this.cboAgrupamento_SelectedIndexChanged);
            // 
            // grbExportacao
            // 
            this.grbExportacao.Controls.Add(this.btnExportar);
            this.grbExportacao.Controls.Add(this.cboExtensao);
            this.grbExportacao.Location = new System.Drawing.Point(300, 17);
            this.grbExportacao.Name = "grbExportacao";
            this.grbExportacao.Size = new System.Drawing.Size(221, 49);
            this.grbExportacao.TabIndex = 2;
            this.grbExportacao.TabStop = false;
            this.grbExportacao.Text = "Exportar dados para";
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(129, 18);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(75, 23);
            this.btnExportar.TabIndex = 1;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // cboExtensao
            // 
            this.cboExtensao.FormattingEnabled = true;
            this.cboExtensao.Location = new System.Drawing.Point(6, 19);
            this.cboExtensao.Name = "cboExtensao";
            this.cboExtensao.Size = new System.Drawing.Size(117, 21);
            this.cboExtensao.TabIndex = 0;
            // 
            // LblStatus
            // 
            this.LblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblStatus.Location = new System.Drawing.Point(663, 56);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(558, 13);
            this.LblStatus.TabIndex = 3;
            this.LblStatus.Text = "Status";
            this.LblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // BtnLog
            // 
            this.BtnLog.Location = new System.Drawing.Point(6, 19);
            this.BtnLog.Name = "BtnLog";
            this.BtnLog.Size = new System.Drawing.Size(75, 23);
            this.BtnLog.TabIndex = 4;
            this.BtnLog.Text = "Abrir";
            this.BtnLog.UseVisualStyleBackColor = true;
            this.BtnLog.Click += new System.EventHandler(this.BtnLog_Click);
            // 
            // grdLog
            // 
            this.grdLog.Controls.Add(this.BtnLog);
            this.grdLog.Location = new System.Drawing.Point(527, 17);
            this.grdLog.Name = "grdLog";
            this.grdLog.Size = new System.Drawing.Size(88, 49);
            this.grdLog.TabIndex = 5;
            this.grdLog.TabStop = false;
            this.grdLog.Text = "Arquivo Log";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 704);
            this.Controls.Add(this.grdLog);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.grbExportacao);
            this.Controls.Add(this.grbAgrupamento);
            this.Controls.Add(this.GrbDados);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizador";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.Shown += new System.EventHandler(this.FrmPrincipal_Shown);
            this.GrbDados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvDados)).EndInit();
            this.grbAgrupamento.ResumeLayout(false);
            this.grbExportacao.ResumeLayout(false);
            this.grdLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrbDados;
        private System.Windows.Forms.DataGridView DgvDados;
        private System.Windows.Forms.GroupBox grbAgrupamento;
        private System.Windows.Forms.ComboBox cboAgrupamento;
        private System.Windows.Forms.GroupBox grbExportacao;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.ComboBox cboExtensao;
        private System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button BtnLog;
        private System.Windows.Forms.GroupBox grdLog;
    }
}

