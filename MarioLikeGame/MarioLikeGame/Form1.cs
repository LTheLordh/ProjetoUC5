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
    public partial class frmTelaJogo : Form
    {
        //Declarando a DAL
        GamerDAL gamerDAL;

        //Criar um atributo para pegar o nome do jogador
        public string nomeGamer { get; set; }

        
        //Atributos para controle da movimentação do personagem
        private bool paraCima;
        private bool paraBaixo;
        private bool paraDireita;
        private bool paraEsquerda;

        //Variavel para condições de vitoria/derrota
        private bool victory = false;

        //Variavel para contar os pontos (coletaveis coletados)
        private int pontos = 0;

        //Variavel para contar os segundos
        private int segundos = 0;

        //Variavel para contar os minutos    
        private int minutos = 0;

        //Biblioteca do Windows Media Player
        //WMPLib.WindowsMediaPlayer Tocador = new WMPLib.WindowsMediaPlayer();

        //Criar uma lista de midias
        List<System.Windows.Media.MediaPlayer> sounds = new List<System.Windows.Media.MediaPlayer>();

        //Atributo responsável pela velocidade de locomoção do personagem
        private int velocidade = 10;

        public frmTelaJogo()
        {
            InitializeComponent();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void frmTelaJogo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmTelaJogo_Load(object sender, EventArgs e)
        {
            playSound("MarioTheme.mp3");
            //Audio("MarioTheme.mp3", "Play");
            
        }

        //Movimentar o personagem quando pressiono a tecla
        private void KeyisDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                paraEsquerda = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                paraDireita = true;
            }

            if (e.KeyCode == Keys.Up)
            {
                paraCima = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                paraBaixo = true;
            }
        }

        //Parar o movimento do personagem quando soltar a tecla
        private void KeyisUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    paraEsquerda = false;
                    break;
                case Keys.Right:
                    paraDireita = false;
                    break;
                case Keys.Up:
                    paraCima = false;
                    break;
                case Keys.Down:
                    paraBaixo = false;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblPontos.Text = "Pontos: " + pontos;

            if (paraEsquerda)
            {
                //Movimenta o personagem para esquerda
                personagem.Left -= velocidade;
            }

            if (paraDireita)
            {
                //Movimenta o personagem para Direita
                personagem.Left += velocidade;
            }

            //Movimenta o personagem para cinma
            if (paraCima)
            {
                personagem.Top -= velocidade;
            }

            //Movimenta o personagem para baixo
            if (paraBaixo)
            {
                personagem.Top += velocidade;
            }

            //Posicionamento do personagem dentro da área do formulário (tela)
            if (personagem.Left < 0)
            {
                personagem.Left = 0;
            }

            if (personagem.Left > 1090)
            {
                personagem.Left = 1090;
            }

            if (personagem.Top < 70)
            {
                personagem.Top = 70;
            }

            if (personagem.Top > 680)
            {
                personagem.Top = 680;
            }


            //Loop para checar todos os controles inseridos no form

            foreach (Control item in this.Controls)
            {
                //Verifica se o jogador colidiu com o inimigo, caso positivo game over.
                if (item is PictureBox && (string)item.Tag == "inimigo")
                {
                    //Checa colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        victory = false;
                        pictureBoxDerrota.Visible = true;
                        GameOver(victory);
                        RemovePictureBox();
                        GravaHiScore();

                    }
                }
                if (item is PictureBox && (string)item.Tag == "coletaveis" || (string)item.Tag == "coletaveis2")
                {
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        if ((string)item.Tag == "coletaveis")
                        {
                            playSound("coin.wav");
                        }
                        else
                        {
                            playSound("cogumelo.wav");
                            personagem.Width += 16;
                            personagem.Height += 16;
                        }
                        

                        this.Controls.Remove(item);

                        pontos++;

                        if (pontos == 15)
                        {
                            
                            victory = true;
                            pictureBoxVitoria.Visible = true;
                            GameOver(victory);
                            RemovePictureBox();
                            GravaHiScore();
                            
                        }
                    }
                }


            }
        }

        private void RemovePictureBox()
        {
            foreach (Control item in this.Controls)
            {
                if (item is PictureBox && (string)item.Tag != "gameOver")
                {
                    ((PictureBox)item).Image = null;
                }
            }
        }

        private void GameOver(bool ganhou)
        {
            lblPontos.Text = "Pontos: " + pontos;
            personagem.Visible = false;
            btnRestart.Visible = true;
            btnRestart.Focus();
            
            if (ganhou)
            {
                stopSound();
                lblGameOver.Text = "       VICTORY";
                pictureBoxVitoria.Visible = true;
                playSound("stageclear.wav");
            }
            else
            {
                stopSound();
                lblGameOver.Text = "G A M E  O V E R";
                pictureBoxDerrota.Visible = true;
                playSound("mariodie.wav");
            }
            timer1.Stop();
            timer2.Stop();

        }

        private void GravaHiScore()
        {
            gamerDAL = new GamerDAL();

            Placar placar = new Placar();

            var frm = new frmTelaInicial();

            placar.NomeJogador = this.nomeGamer;

            if (!this.nomeGamer.Equals(""))
            {
                placar.NomeJogador = this.nomeGamer;
            }
            else
            {
                placar.NomeJogador = "Noob";
            }

            placar.TempoJogador = minutos.ToString("00") + ":" + segundos.ToString("00");
            placar.ScoreJogador = pontos;
            placar.DataScoreJogador = DateTime.Now;

            //Chama a função inserir da DAL passando o objeto populado como parâmetro
            if (!gamerDAL.Inserir(placar))
            {
                //Deu pau! Exibe mensagem de erro para o usuário... :(
                MessageBox.Show("Erro ao inserir os dados: \r\n\r\n" +
                   gamerDAL.MensagemErro, "Mario Like Game");
            }
            
        }

        

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            segundos++;
            if (segundos == 60)
            {
                minutos++;
                segundos = 0;
            }

            lblTempo.Text = "Tempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");


            
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //private void Audio(string caminho, string estadoMP)
        //{
        //    //Verifica se ocorreu erro ao instanciar o Windows Media Player
        //    Tocador.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Tocador_MediaError);

        //    Tocador.URL = caminho;
        //    if (estadoMP.Equals("Play"))
        //    {
        //        Tocador.controls.play();
        //    }
        //    else if (Tocador.Equals("Stop"))
        //    {
        //        Tocador.controls.stop();    
        //    }

        //}

        //private void Tocador_MediaError(object pMediaObject)
        //{
        //    MessageBox.Show("Não foi possível executar o arquivo de mídia");
        //    this.Close();
        //}

        private void playSound(string nome)
        {
            string url = Application.StartupPath + @"\" + nome;
            var sound = new System.Windows.Media.MediaPlayer();
            sound.Open(new Uri(url));
            sound.Play();
            sounds.Add(sound);


        }

        private void stopSound()
        {
            for (int i = sounds.Count -1; i >= 0; i--)
            {
                sounds[i].Stop();
                sounds.RemoveAt(i);

            }
        }

       
    }
}
