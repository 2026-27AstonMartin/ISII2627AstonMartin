namespace AppForSEII.API.Data {
    public class SeedData {
        public static void Initialize(ApplicationDbContext dbContext, IServiceProvider serviceProvider, ILogger logger) {
            List<string> rolesNames = new List<string> { "Administrator", "Employee", "Customer" };

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            try {
                SeedRoles(roleManager, rolesNames);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the roles in the Database.");
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            try {
                SeedUsers(userManager, rolesNames);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the Users in the Database.");
            }

            try {
                SeedCompeticiones(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the Competiciones in the Database.");
            }

            try {
                SeedPistas(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the Pistas in the Database.");
            }

            //las reservas van despues de las pistas, y las pistas reservadas despues de ambas
            try {
                SeedReservas(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the Reservas in the Database.");
            }

            try {
                SeedPistasReservadas(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the PistasReservadas in the Database.");
            }

            try {
                SeedMateriales(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the Materiales in the Database.");
            }

            try {
                SeedAlquileres(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the Alquileres in the Database.");
            }

            try {
                SeedInscripcionesCompeticion(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the InscripcionesCompeticion in the Database.");
            }

            //el orden importa: cada tabla necesita que esten sembradas aquellas a las que apunta
            try {
                SeedTiposDeporte(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the TiposDeporte in the Database.");
            }

            try {
                SeedClasesDeportivas(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the ClasesDeportivas in the Database.");
            }

            try {
                SeedInscripciones(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the Inscripciones in the Database.");
            }

            try {
                SeedClasesInscritas(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the ClasesInscritas in the Database.");
            }

        }

        public static void SeedCompeticiones(ApplicationDbContext dbContext) {
            //it checks the table is empty before seeding it
            if (dbContext.Competiciones.Any())
                return;

            dbContext.Competiciones.AddRange(
                new Competicion {
                    Nombre = "Torneo de Padel de Primavera",
                    Fecha = new DateTime(2027, 3, 14),
                    Plazas = 32,
                    Lugar = "Polideportivo Municipal",
                    Precio = 25.00m
                },
                new Competicion {
                    Nombre = "Liga de Baloncesto 3x3",
                    Fecha = new DateTime(2027, 4, 22),
                    Plazas = 24,
                    Lugar = "Pabellon Central",
                    Precio = 18.50m
                },
                new Competicion {
                    Nombre = "Campeonato de Tenis Open",
                    Fecha = new DateTime(2027, 5, 9),
                    Plazas = 16,
                    Lugar = "Club de Tenis Aston",
                    Precio = 40.00m
                });

            dbContext.SaveChanges();
        }

        public static void SeedPistas(ApplicationDbContext dbContext) {
            if (dbContext.Pistas.Any())
                return;

            dbContext.Pistas.AddRange(
                new Pista {
                    NombrePista = "Pista de Padel 1",
                    NPersonas = 4,
                    TipoDeporte = new TipoDeporte { Nombre = "Padel" },
                    Precio = 20.00m,
                    Stock = 1
                },
                new Pista {
                    NombrePista = "Pista de Tenis 1",
                    NPersonas = 4,
                    TipoDeporte = new TipoDeporte { Nombre = "Tenis" },
                    Precio = 24.00m,
                    Stock = 1
                },
                new Pista {
                    NombrePista = "Pista Central de Baloncesto",
                    NPersonas = 20,
                    TipoDeporte = new TipoDeporte { Nombre = "Baloncesto" },
                    Precio = 55.00m,
                    Stock = 1
                });

            dbContext.SaveChanges();
        }

        public static void SeedReservas(ApplicationDbContext dbContext) {
            if (dbContext.Reservas.Any())
                return;

            dbContext.Reservas.AddRange(
                new Reserva {
                    NombreCliente = "Elena",
                    Apellidos = "Navarro Martinez",
                    Dni = "87654321B",
                    FechaReserva = new DateTime(2026, 12, 18),
                    MetodoPago = MetodoPago.Tarjeta,
                    PrecioTotal = 44.00m
                },
                new Reserva {
                    NombreCliente = "Peter",
                    Apellidos = "Jackson",
                    Dni = "12345678Z",
                    FechaReserva = new DateTime(2026, 12, 19),
                    MetodoPago = MetodoPago.Bizum,
                    PrecioTotal = 55.00m
                });

            dbContext.SaveChanges();
        }

        public static void SeedPistasReservadas(ApplicationDbContext dbContext) {
            if (dbContext.PistasReservadas.Any())
                return;

            //both the courts and the bookings must already exist
            var padel = dbContext.Pistas.FirstOrDefault(p => p.NombrePista == "Pista de Padel 1");
            var tenis = dbContext.Pistas.FirstOrDefault(p => p.NombrePista == "Pista de Tenis 1");
            var baloncesto = dbContext.Pistas.FirstOrDefault(p => p.NombrePista == "Pista Central de Baloncesto");
            var reservas = dbContext.Reservas.OrderBy(r => r.Id).ToList();
            if (padel == null || tenis == null || baloncesto == null || reservas.Count < 2)
                return;

            dbContext.PistasReservadas.AddRange(
                new PistaReservada {
                    IdPista = padel.IdPista,
                    IdReserva = reservas[0].Id,
                    Cantidad = 1,
                    Precio = 20.00m,
                    Observaciones = "Traeremos nuestras propias palas"
                },
                new PistaReservada {
                    IdPista = tenis.IdPista,
                    IdReserva = reservas[0].Id,
                    Cantidad = 1,
                    Precio = 24.00m
                },
                new PistaReservada {
                    IdPista = baloncesto.IdPista,
                    IdReserva = reservas[1].Id,
                    Cantidad = 1,
                    Precio = 55.00m,
                    Observaciones = "Partido entre amigos"
                });

            dbContext.SaveChanges();
        }

        public static void SeedMateriales(ApplicationDbContext dbContext) {
            if (dbContext.Materiales.Any())
                return;

            //the sport types must already exist (SeedPistas creates them) to be able to relate the materials to them
            var padel = dbContext.TiposDeporte.FirstOrDefault(t => t.Nombre == "Padel");
            var tenis = dbContext.TiposDeporte.FirstOrDefault(t => t.Nombre == "Tenis");
            var baloncesto = dbContext.TiposDeporte.FirstOrDefault(t => t.Nombre == "Baloncesto");
            if (padel == null || tenis == null || baloncesto == null)
                return;

            var raqueta = new TipoMaterial { NombreTipoMaterial = "Raqueta" };
            var balon = new TipoMaterial { NombreTipoMaterial = "Balon" };
            var equipacion = new TipoMaterial { NombreTipoMaterial = "Equipacion" };

            dbContext.Materiales.AddRange(
                new Material { Nombre = "Pala de Padel", Precio = 5.00m, Cantidad = 15, TipoMaterial = raqueta, TipoDeporte = padel },
                new Material { Nombre = "Raqueta de Tenis", Precio = 6.50m, Cantidad = 12, TipoMaterial = raqueta, TipoDeporte = tenis },
                new Material { Nombre = "Balon de Baloncesto", Precio = 3.00m, Cantidad = 20, TipoMaterial = balon, TipoDeporte = baloncesto },
                new Material { Nombre = "Juego de Petos", Precio = 4.25m, Cantidad = 8, TipoMaterial = equipacion, TipoDeporte = baloncesto });

            dbContext.SaveChanges();
        }

        public static void SeedAlquileres(ApplicationDbContext dbContext) {
            if (dbContext.Alquileres.Any())
                return;

            dbContext.Alquileres.AddRange(
                new Alquiler {
                    NombreUsuario = "Lucia",
                    ApellidosUsuario = "Martinez Gomez",
                    DNI = "12345678Z",
                    NumeroTelefono = "611223344",
                    FechaAlquiler = new DateTime(2026, 10, 5),
                    MetodoPago = MetodoPago.Bizum,
                    PrecioTotal = 13.00m
                },
                new Alquiler {
                    NombreUsuario = "Carlos",
                    ApellidosUsuario = "Ruiz Fernandez",
                    DNI = "87654321X",
                    NumeroTelefono = "622334455",
                    FechaAlquiler = new DateTime(2026, 10, 7),
                    MetodoPago = MetodoPago.Efectivo,
                    PrecioTotal = 10.75m
                });

            dbContext.SaveChanges();
        }

        public static void SeedInscripcionesCompeticion(ApplicationDbContext dbContext) {
            if (dbContext.InscripcionesCompeticion.Any())
                return;

            dbContext.InscripcionesCompeticion.AddRange(
                new InscripcionCompeticion {
                    NombreUsuario = "Peter",
                    ApellidosUsuario = "Jackson",
                    DNI = "12345678Z",
                    Telefono = "600123456",
                    FechaInscripcion = new DateTime(2026, 9, 28),
                    MetodoPago = MetodoPago.Tarjeta,
                    PrecioTotal = 25.00m
                },
                new InscripcionCompeticion {
                    NombreUsuario = "Elena",
                    ApellidosUsuario = "Navarro Martinez",
                    DNI = "87654321B",
                    Telefono = "600654321",
                    FechaInscripcion = new DateTime(2026, 9, 28),
                    MetodoPago = MetodoPago.Bizum,
                    PrecioTotal = 18.50m
                });

            dbContext.SaveChanges();
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager, List<string> roles) {

            foreach (string roleName in roles) {
                //it checks such role does not exist in the database 
                if (!roleManager.RoleExistsAsync(roleName).Result) {
                    IdentityRole role = new IdentityRole();
                    role.Name = roleName;
                    role.NormalizedName = roleName;
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }

        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager, List<string> roles) {
            //first, it checks the user does not already exist in the DB
            if (userManager.FindByNameAsync("elena@uclm.es").Result == null) {
                ApplicationUser user = new ApplicationUser("1", "Elena", "Navarro Martínez", "elena@uclm.es");
                user.EmailConfirmed = true;

                var result = userManager.CreateAsync(user, "Password1234%");
                result.Wait();

                if (result.IsCompletedSuccessfully) {
                    //administrator role
                    userManager.AddToRoleAsync(user, roles[0]).Wait();
                }
            }


            if (userManager.FindByNameAsync("peter@uclm.es").Result == null) {
                //A customer class has been defined because it has different attributes (purchase, rental, etc.)
                ApplicationUser user = new ApplicationUser("3", "Peter", "Jackson", "peter@uclm.es");
                user.EmailConfirmed = true;

                var result = userManager.CreateAsync(user, "OtherPass12$");

                result.Wait();

                if (result.IsCompletedSuccessfully) {
                    //customer role
                    userManager.AddToRoleAsync(user, roles[2]).Wait();

                }
            }

        }





    
        public static void SeedTiposDeporte(ApplicationDbContext dbContext) {
            //SeedPistas ya crea algunos tipos de deporte, asi que solo se anaden los que falten
            List<TipoDeporte> tipos = new List<TipoDeporte> {
                new TipoDeporte {
                    Nombre = "Padel",
                    Descripcion = "Deporte de raqueta por parejas en pista cerrada"
                },
                new TipoDeporte {
                    Nombre = "Yoga",
                    Descripcion = "Disciplina de estiramientos, equilibrio y respiracion"
                },
                new TipoDeporte {
                    Nombre = "Spinning",
                    Descripcion = "Ciclismo indoor en grupo con musica"
                }
            };

            foreach (TipoDeporte tipo in tipos) {
                if (!dbContext.TiposDeporte.Any(t => t.Nombre == tipo.Nombre))
                    dbContext.TiposDeporte.Add(tipo);
            }

            dbContext.SaveChanges();
        }

        public static void SeedClasesDeportivas(ApplicationDbContext dbContext) {
            if (dbContext.ClasesDeportivas.Any())
                return;

            //the sport types must already exist to be able to relate the classes to them
            var yoga = dbContext.TiposDeporte.FirstOrDefault(t => t.Nombre == "Yoga");
            var spinning = dbContext.TiposDeporte.FirstOrDefault(t => t.Nombre == "Spinning");
            var padel = dbContext.TiposDeporte.FirstOrDefault(t => t.Nombre == "Padel");
            if (yoga == null || spinning == null || padel == null)
                return;

            dbContext.ClasesDeportivas.AddRange(
                new ClaseDeportiva {
                    Descripcion = "Yoga suave para principiantes, centrado en la respiracion",
                    FechaHora = new DateTime(2027, 1, 12, 10, 0, 0),
                    Monitor = "Lucia Ramirez",
                    Nivel = "Principiante",
                    Lugar = "Sala polivalente 1",
                    PlazasDisponibles = 20,
                    PrecioUnitario = 8.50m,
                    TipoDeporteId = yoga.Id
                },
                new ClaseDeportiva {
                    Descripcion = "Spinning de alta intensidad con series por intervalos",
                    FechaHora = new DateTime(2027, 1, 13, 19, 30, 0),
                    Monitor = "Javier Soler",
                    Nivel = "Avanzado",
                    Lugar = "Sala de ciclo indoor",
                    PlazasDisponibles = 25,
                    PrecioUnitario = 10.00m,
                    TipoDeporteId = spinning.Id
                },
                new ClaseDeportiva {
                    Descripcion = "Iniciacion al padel: golpeo basico y posicion en pista",
                    FechaHora = new DateTime(2027, 1, 14, 17, 0, 0),
                    Monitor = "Marta Peña",
                    Nivel = "Intermedio",
                    PlazasDisponibles = 12,
                    PrecioUnitario = 12.75m,
                    TipoDeporteId = padel.Id
                });

            dbContext.SaveChanges();
        }

        public static void SeedInscripciones(ApplicationDbContext dbContext) {
            if (dbContext.Inscripciones.Any())
                return;

            //the users are seeded by SeedUsers with the identifiers "1" and "3"
            var elena = dbContext.ApplicationUsers.FirstOrDefault(u => u.Id == "1");
            var peter = dbContext.ApplicationUsers.FirstOrDefault(u => u.Id == "3");
            if (elena == null || peter == null)
                return;

            dbContext.Inscripciones.AddRange(
                new Inscripcion {
                    FechaInscripcion = new DateTime(2026, 12, 20),
                    MetodoPago = MetodoPago.Tarjeta,
                    DatosPago = "4539 1488 0343 6467",
                    PrecioTotal = 17.00m,
                    ClienteId = elena.Id
                },
                new Inscripcion {
                    FechaInscripcion = new DateTime(2026, 12, 22),
                    MetodoPago = MetodoPago.Bizum,
                    DatosPago = "600654321",
                    PrecioTotal = 22.75m,
                    ClienteId = peter.Id
                });

            dbContext.SaveChanges();
        }

        public static void SeedClasesInscritas(ApplicationDbContext dbContext) {
            if (dbContext.ClasesInscritas.Any())
                return;

            //both the classes and the inscriptions must already exist
            var clases = dbContext.ClasesDeportivas.OrderBy(c => c.Id).ToList();
            var inscripciones = dbContext.Inscripciones.OrderBy(i => i.Id).ToList();
            if (clases.Count < 3 || inscripciones.Count < 2)
                return;

            dbContext.ClasesInscritas.AddRange(
                new ClaseInscrita {
                    ClaseDeportivaId = clases[0].Id,
                    InscripcionId = inscripciones[0].Id,
                    PlazasReservadas = 2,
                    Precio = 17.00m,
                    Observaciones = "Asiste con una acompañante"
                },
                new ClaseInscrita {
                    ClaseDeportivaId = clases[1].Id,
                    InscripcionId = inscripciones[1].Id,
                    PlazasReservadas = 1,
                    Precio = 10.00m
                },
                new ClaseInscrita {
                    ClaseDeportivaId = clases[2].Id,
                    InscripcionId = inscripciones[1].Id,
                    PlazasReservadas = 1,
                    Precio = 12.75m,
                    Observaciones = "Primera vez que juega al padel"
                });

            dbContext.SaveChanges();
        }
}
}