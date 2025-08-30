namespace SistemPenitentiary.DTOs
{
    public class EnvironmentCreateUpdateDto
    {
        // No se incluye el ID porque lo genera la base de datos o el controlador.
        public string? EnvironmentName { get; set; }
        public string? UnrealLevelPath { get; set; }
        public string? ConfigurationSettings { get; set; }
        public Guid? ResponsibleUserId { get; set; }
    }
}