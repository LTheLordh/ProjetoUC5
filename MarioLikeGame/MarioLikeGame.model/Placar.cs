using System;
using System.Collections.Generic;
using System.Text;

namespace MarioLikeGame.model
{
    public class Placar
    {
        private int idJogador;
        public string nomeJogador;
        public int scoreJogador;
        public DateTime dataScoreJogador;
        public string tempoJogador;


        public Placar()
        {

        }

        public Placar(int idJogador, string nomeJogador, int scoreJogador, DateTime dataScoreJogador, string tempoJogador)
        {
            IdJogador = idJogador;
            NomeJogador = nomeJogador;
            ScoreJogador = scoreJogador;
            DataScoreJogador = dataScoreJogador;
            TempoJogador = tempoJogador;
        }

        public int IdJogador { get => idJogador; set => idJogador = value; }
        public string NomeJogador { get => nomeJogador; set => nomeJogador = value; }
        public int ScoreJogador { get => scoreJogador; set => scoreJogador = value; }
        public DateTime DataScoreJogador { get => dataScoreJogador; set => dataScoreJogador = value; }
        public string TempoJogador { get => tempoJogador; set => tempoJogador = value; }

        //public Placar(int id_Jogador, char nome_Jogador, int score_Jogador, DateTime datascore_Jogador)
        //{
        //    this.IdJogador = id_Jogador;
        //    this.NomeJogador = nome_Jogador;
        //    this.ScoreJogador = score_Jogador;
        //    this.DataScoreJogador = datascore_Jogador;
        //}

        //public int IdJogador { get => IdJogador; set => IdJogador = value; }

        //public char NomeJogador { get => NomeJogador; set => NomeJogador = value; }

        //public int ScoreJogador { get => ScoreJogador; set => ScoreJogador = value; }

        //public DateTime DataScoreJogador { get => DataScoreJogador; set => DataScoreJogador = value; }

}   }   
