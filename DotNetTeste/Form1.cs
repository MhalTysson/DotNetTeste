using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetTeste
{
    public partial class Form1 : Form
    {
        Stopwatch stopWatch = new Stopwatch();        

        public Form1()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {            
            CarregaCombos();
        }

        private void CarregaCombos()
        {
            string[] Agrupamentos = new string[] { "sem agrupamento", "padrao", "ativo", "tipoOperacao", "conta" };
            string[] Extensoes = new string[] { "csv", "xlsx" };

            cboAgrupamento.Items.AddRange(Agrupamentos);
            cboAgrupamento.SelectedIndex = 0;

            cboExtensao.Items.AddRange(Extensoes);
            cboExtensao.SelectedIndex = 0;
        }

        private void FrmPrincipal_Shown(object sender, EventArgs e)
        {
            //CarregaDadosAsync();
            cboAgrupamento.SelectedIndex = 0;
            cboExtensao.SelectedIndex = 0;
        }

        private async Task CarregaDadosAsync()
        {
            try
            {
                string Mensagem = "Solicitação em andamento, buscando dados " + cboAgrupamento.Text + ", por favor aguarde...";
                Log.GravaLog(Mensagem);

                AtivaFuncionalidades(false);

                LblStatus.Text = Mensagem;
                LblStatus.Refresh();

                stopWatch.Start();

                DgvDados.DataSource = null;
                DgvDados.DataSource = await AcessoAPI.GetDadosApiAsync(cboAgrupamento.Text);

                stopWatch.Stop();

                int TempoDecorrido = (int)stopWatch.ElapsedMilliseconds;
                stopWatch.Reset();

                Mensagem = "Dados prontos para exportação. Tempo da solicitação " + TempoDecorrido + " ms.";
                LblStatus.Text = Mensagem;
                LblStatus.Refresh();

                AtivaFuncionalidades(true);
                
                Log.GravaLog(Mensagem);
            }
            catch (Exception ex)
            {
                Log.GravaLog(ex.Message.Replace("\n\r"," "));
                MessageBox.Show("Houve um erro na aplicação, tente novamente. Caso o erro persista entre em contato com o desenvolvedor do sistema.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtivaFuncionalidades(bool Status)
        {
            grbAgrupamento.Enabled = Status;
            grbExportacao.Enabled = Status;
            grdLog.Enabled = Status;
        }

        private void BtnAgrupar_Click(object sender, EventArgs e)
        {
            CarregaDadosAsync();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportaDados();
        }

        private async Task ExportaDados()
        {
            try
            {
                AtivaFuncionalidades(false);

                saveFileDialog.FileName = "Relatorio_" + cboAgrupamento.Text + "_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + "." + cboExtensao.Text;
                saveFileDialog.Title = "Exportação dos Dados";
                saveFileDialog.Filter = cboExtensao.Text + " files (*." + cboExtensao.Text + ")|*." + cboExtensao.Text +"|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "." + cboExtensao.Text ;
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                DialogResult resposta = saveFileDialog.ShowDialog();

                if(resposta == DialogResult.Cancel)
                {
                    AtivaFuncionalidades(true);
                    return;
                }

                LblStatus.Text = "Solicitação em andamento, gerando o arquivo " + cboExtensao.Text  + ", por favor aguarde...";
                LblStatus.Refresh();

                stopWatch.Start();

                byte[] Retorno = await AcessoAPI.GetArquivoAsync(cboAgrupamento.Text,cboExtensao.Text);

                File.WriteAllBytes(saveFileDialog.FileName, Retorno);

                stopWatch.Stop();

                int TempoDecorrido = (int)stopWatch.ElapsedMilliseconds;
                stopWatch.Reset();

                LblStatus.Text = "Dados exportados com sucesso. Tempo da solicitação " + TempoDecorrido + " ms.";
                LblStatus.Refresh();

                MessageBox.Show("Arquivo Exportado com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                AtivaFuncionalidades(true);
            }
            catch (Exception ex)
            {
                Log.GravaLog(ex.Message.Replace("\n\r", " "));
                MessageBox.Show("Houve um erro na aplicação, tente novamente. Caso o erro persista entre em contato com o desenvolvedor do sistema.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboAgrupamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDadosAsync();
        }

        private void BtnLog_Click(object sender, EventArgs e)
        {
            Process arquivo = new Process();
            arquivo.StartInfo.FileName = Log.GetArquivoLog();
            arquivo.Start();
        }
    }
}
