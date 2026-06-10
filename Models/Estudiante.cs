using System;

namespace AcademiaMusicaAPI.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Instrumento { get; set; } = null!;
        
        // Al ponerle el signo ?, le decimos a .NET que puede ir nulo y que SQL maneje su Default
        public DateTime? FechaInscripcion { get; set; }
        public bool Activo { get; set; }
    }
}
