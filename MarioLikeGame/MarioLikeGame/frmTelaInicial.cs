using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarioLikeGame.DAL;
using MarioLikeGame.model;

namespace MarioLikeGame
{
    public partial class frmTelaInicial : Form
    {
        public frmTelaInicial()
        {
            InitializeComponent();
            txtNome.MaxLength = 10;
        }



        private void PreencherGrid()
        {
            //Declarando a DAL
            GamerDAL gamerDAL;

            //Instanciando a DAL na construção do formulário
            gamerDAL = new GamerDAL();

            //Limpando o DataSource
            dgvListaRecorde.DataSource = null;

            //Listando a DAL
            dgvListaRecorde.DataSource = gamerDAL.Listar();

            //Removendo a coluna Id_Jogador
            dgvListaRecorde.Columns.Remove("IdJogador");

            Mudafonte();


        }

        private void lblJogador_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var frm = new frmTelaJogo();
            frm.nomeGamer = txtNome.Text;
            frm.ShowDialog();
            this.Visible = true;
            PreencherGrid();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void frmTelaInicial_Load(object sender, EventArgs e)
        {
            //Preencher o grid
            PreencherGrid();

            //Setar o foco para o TextBox
            txtNome.Focus();
            txtNome.Select();
        }

        private void Mudafonte()
        {
            dgvListaRecorde.Columns[0].HeaderText = "Jogador";
            dgvListaRecorde.Columns[1].HeaderText = "Pontos";
            dgvListaRecorde.Columns[2].HeaderText = "Data/Hora";
            dgvListaRecorde.Columns[3].HeaderText = "Tempo";

            dgvListaRecorde.ColumnHeadersDefaultCellStyle.Font = new Font("Tekton Pro", 30, FontStyle.Regular);
            dgvListaRecorde.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            dgvListaRecorde.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaRecorde.EnableHeadersVisualStyles = false;
            if (dgvListaRecorde.RowCount > 0)
            {
                dgvListaRecorde.CurrentRow.DefaultCellStyle.BackColor = Color.OrangeRed;
            }
            dgvListaRecorde.DefaultCellStyle.Font = new Font("Tekton Pro", 30, FontStyle.Regular);
            dgvListaRecorde.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
            dgvListaRecorde.DefaultCellStyle.BackColor = Color.Blue;
            dgvListaRecorde.DefaultCellStyle.ForeColor = Color.White;
            dgvListaRecorde.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvListaRecorde.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

    }    

}
