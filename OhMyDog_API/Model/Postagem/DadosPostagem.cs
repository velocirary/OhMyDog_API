﻿namespace OhMyDog_API.Model.Postagem
{
    public class DadosPostagem
    {
        public string IdPostagem { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string DataPublicacao { get; set; }

        public string idUsuario { get; set; }

        public string Foto { get; set; }

        public string Doacao { get; set; }

        public string ChavePix { get; set; }

        public string Status { get; set; }
    }

    public class AtualizaPostagem
    {
        public string IdPostagem { get; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string Foto { get; set; }

        public int Doacao { get; set; }

        public string ChavePix { get; set; }

        public string Status { get; set; }
    }

    public class AprovaPostagem
    {
        public string IdPostagem { get; }

        public string Status { get; set; }
    }
}
