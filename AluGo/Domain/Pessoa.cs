namespace AluGo.Domain
{
    public class Pessoa
    {
        public Guid Id { get; set; } = Guid.NewGuid();        
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;

        public DateTime? UltimaEdicao { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
    }
}
