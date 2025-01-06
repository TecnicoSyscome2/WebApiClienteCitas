namespace WebApi.Citas.ClientesApp.Modelos
{
    public interface Login
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
