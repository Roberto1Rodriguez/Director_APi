﻿using System;
using System.Collections.Generic;

namespace DirectorAPI.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Matricula { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int Edad { get; set; }

    public string Curp { get; set; } = null!;

    public double Peso { get; set; }

    public double Estatura { get; set; }

    public string? Alergico { get; set; }

    public int IdGrupo { get; set; }

    public virtual ICollection<AlumnoTutor> AlumnoTutor { get; } = new List<AlumnoTutor>();

    public virtual ICollection<Calificacion> Calificacion { get; } = new List<Calificacion>();

    public virtual ICollection<DocenteAlumno> DocenteAlumno { get; } = new List<DocenteAlumno>();

    public virtual Grupo IdGrupoNavigation { get; set; } = null!;
}
