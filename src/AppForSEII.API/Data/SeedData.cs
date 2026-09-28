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

            try {
                SeedMateriales(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the Materiales in the Database.");
            }

            try {
                SeedInscripcionesCompeticion(dbContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the InscripcionesCompeticion in the Database.");
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
                    Nombre = "Pista de Padel 1",
                    Aforo = 4,
                    TipoDeporte = "Padel",
                    PrecioPorDia = 20.00m
                },
                new Pista {
                    Nombre = "Pista de Tenis 1",
                    Aforo = 4,
                    TipoDeporte = "Tenis",
                    PrecioPorDia = 24.00m
                },
                new Pista {
                    Nombre = "Pista Central de Baloncesto",
                    Aforo = 20,
                    TipoDeporte = "Baloncesto",
                    PrecioPorDia = 55.00m
                });

            dbContext.SaveChanges();
        }

        public static void SeedMateriales(ApplicationDbContext dbContext) {
            if (dbContext.Materiales.Any())
                return;

            dbContext.Materiales.AddRange(
                new Material { Nombre = "Pala de Padel", Precio = 5.00m, Cantidad = 15 },
                new Material { Nombre = "Raqueta de Tenis", Precio = 6.50m, Cantidad = 12 },
                new Material { Nombre = "Balon de Baloncesto", Precio = 3.00m, Cantidad = 20 },
                new Material { Nombre = "Juego de Petos", Precio = 4.25m, Cantidad = 8 });

            dbContext.SaveChanges();
        }

        public static void SeedInscripcionesCompeticion(ApplicationDbContext dbContext) {
            if (dbContext.InscripcionesCompeticion.Any())
                return;

            //the competitions must already exist to be able to relate the inscriptions to them
            var competicion = dbContext.Competiciones.FirstOrDefault();
            if (competicion == null)
                return;

            dbContext.InscripcionesCompeticion.AddRange(
                new InscripcionCompeticion {
                    NombreUsuario = "Peter",
                    ApellidosUsuario = "Jackson",
                    DNI = "12345678Z",
                    Telefono = "600123456",
                    FechaInscripcion = new DateTime(2026, 9, 28),
                    MetodoPago = MetodoPago.Tarjeta,
                    PrecioTotal = competicion.Precio,
                    CompeticionId = competicion.Id,
                    UsuarioId = "3"
                },
                new InscripcionCompeticion {
                    NombreUsuario = "Elena",
                    ApellidosUsuario = "Navarro Martinez",
                    DNI = "87654321B",
                    Telefono = "600654321",
                    FechaInscripcion = new DateTime(2026, 9, 28),
                    MetodoPago = MetodoPago.Bizum,
                    PrecioTotal = competicion.Precio,
                    CompeticionId = competicion.Id,
                    UsuarioId = "1"
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





    }
}