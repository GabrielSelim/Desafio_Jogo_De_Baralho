﻿namespace Desafio_Jogo_De_Baralho.Models
{
    public class Jogador
    {
        public required string Nome { get; set; }
        public List<Carta> Cartas { get; set; } = new List<Carta>();
    }
}
