namespace Tarefas.API.DTO
{
	public class UsuarioToken
	{
		public bool Autheticated { get; set; }
		public DateTime Expiration { get; set; }
		public string Token { get; set; }
		public string Message { get; set; }
	}
}
